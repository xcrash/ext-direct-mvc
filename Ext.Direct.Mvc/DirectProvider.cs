/* ****************************************************************************
 * 
 * Copyright (c) 2010 Eugene Lishnevsky. All rights reserved.
 * 
 * This file is part of Ext.Direct.Mvc.
 *
 * Ext.Direct.Mvc is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Ext.Direct.Mvc is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Ext.Direct.Mvc.  If not, see <http://www.gnu.org/licenses/>.
 *
 * ***************************************************************************/

namespace Ext.Direct.Mvc {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Ext.Direct.Mvc.Configuration;
    using Ext.Direct.Mvc.Resources;
    using Newtonsoft.Json;

    public class DirectProvider {

        private static DirectProvider _currentProvider;
        private static readonly object _syncLock = new object();

        public const string RemotingProvider = "remoting";
        private IControllerFactory _factory;

        private readonly IDictionary<string, DirectAction> _actions = new Dictionary<string, DirectAction>();

        public DirectProvider() { }

        public DirectProvider(Assembly assembly) {
            if (assembly != null) {
                Configure(assembly);
            }
        }

        public string Name {
            get;
            set;
        }

        public string Url {
            get {
                string appPath = HttpContext.Current.Request.ApplicationPath;
                if (!appPath.EndsWith("/")) {
                    appPath += "/";
                }
                return appPath + "Direct/Route";
            }
        }

        public string Namespace {
            get;
            set;
        }

        public int? Buffer {
            get;
            set;
        }

        public bool Configured {
            get;
            private set;
        }

        public void Configure(Assembly assembly) {
            if (!this.Configured) {
                var types = assembly.GetTypes();
                foreach (var type in types) {
                    if (type.IsDirectAction()) {
                        var action = new DirectAction(type);
                        _actions.Add(action.Name, action);
                    }
                }
                this.Configured = true;
            }
        }

        public string ToJavaScript() {
            return this.ToJavaScript(false);
        }

        // Returns JavaScript representation of Ext.Direct API
        public string ToJavaScript(bool toJson) {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            using (JsonWriter jsonWriter = new JsonTextWriter(sw)) {
#if DEBUG
                jsonWriter.Formatting = Formatting.Indented;
#endif
                jsonWriter.WriteStartObject();
                jsonWriter.WriteProperty("url", this.Url);
                jsonWriter.WriteProperty("type", RemotingProvider);
                if (!String.IsNullOrEmpty(this.Namespace)) {
                    jsonWriter.WriteProperty("namespace", this.Namespace);
                }
                // for integration with the Ext Designer
                if (toJson) {
                    jsonWriter.WriteProperty("descriptor", this.Name);
                }
                if (this.Buffer.HasValue) {
                    jsonWriter.WriteProperty("enableBuffer", this.Buffer);
                }
                jsonWriter.WritePropertyName("actions");
                jsonWriter.WriteStartObject();
                foreach (var action in _actions.Values) {
                    action.WriteJson(jsonWriter);
                }
                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            if (toJson) return sb.ToString();

            return String.Format("{0}={1};", this.Name, sb);
        }

        internal DirectAction GetAction(string name) {
            if (!_actions.ContainsKey(name)) {
                return null;
            }
            return _actions[name];
        }

        internal DirectMethod GetActionMethod(string actionName, string methodName) {
            DirectMethod method = null;
            DirectAction action = this.GetAction(actionName);
            if (action != null) {
                method = action.GetMethod(methodName);
            }
            return method;
        }

        public void Execute(RequestContext requestContext) {
            HttpContextBase httpContext = requestContext.HttpContext;
            _factory = ControllerBuilder.Current.GetControllerFactory();

            if (httpContext.Request.Form.Count == 0) {
                // Raw HTTP Post request(s)

                var reader = new StreamReader(httpContext.Request.InputStream, new UTF8Encoding());
                var json = reader.ReadToEnd();

                if (json.StartsWith("[") && json.EndsWith("]")) {
                    // Batch of requests
                    var requests = JsonConvert.DeserializeObject<List<DirectRequest>>(json);
                    httpContext.Response.Write("[");
                    foreach (var request in requests) {
                        ExecuteRequest(requestContext, request);
                        if (request != requests[requests.Count - 1]) {
                            httpContext.Response.Write(",");
                        }
                    }
                    httpContext.Response.Write("]");
                } else {
                    // Single request
                    var request = JsonConvert.DeserializeObject<DirectRequest>(json);
                    ExecuteRequest(requestContext, request);
                }
            } else {
                // Form Post request

                var request = new DirectRequest(httpContext.Request);
                ExecuteRequest(requestContext, request);
            }
        }

        private void ExecuteRequest(RequestContext requestContext, DirectRequest request) {
            HttpContextBase httpContext = requestContext.HttpContext;
            RouteData routeData = requestContext.RouteData;

            routeData.Values["controller"] = request.Action;
            routeData.Values["action"] = request.Method;
            httpContext.Items[DirectRequest.DirectRequestKey] = request;
            var controller = (Controller)_factory.CreateController(requestContext, request.Action);

            DirectAction action = this.GetAction(request.Action);
            if (action == null) {
                throw new NullReferenceException(String.Format(DirectResources.DirectProvider_ActionNotFound, request.Action));
            }

            DirectMethod method = action.GetMethod(request.Method);
            if (method == null) {
                throw new NullReferenceException(String.Format(DirectResources.DirectProvider_MethodNotFound, request.Method, action.Name));
            }

            if (!method.IsFormHandler && request.Data != null && request.Data.Length != method.Len) {
                throw new ArgumentException(DirectResources.DirectProvider_WrongNumberOfArguments);
            }

            try {
                controller.ActionInvoker = new DirectMethodInvoker();
                (controller as IController).Execute(requestContext);
            } catch (DirectException exception) {
                var errorResponse = new DirectErrorResponse(request, exception);
                errorResponse.Write(httpContext.Response);
            } finally {
                _factory.ReleaseController(controller);
            }

            httpContext.Items.Remove(DirectRequest.DirectRequestKey);
        }

        internal static DirectProvider GetCurrentProvider() {
            if (_currentProvider == null) {
                lock (_syncLock) {
                    if (_currentProvider == null) {
                        Assembly assembly = Assembly.Load(DirectConfig.Assembly);
                        _currentProvider = new DirectProvider(assembly) {
                            Name = DirectConfig.ProviderName,
                            Namespace = DirectConfig.Namespace,
                            Buffer = DirectConfig.Buffer
                        };
                    }
                }
            }
            return _currentProvider;
        }
    }
}
