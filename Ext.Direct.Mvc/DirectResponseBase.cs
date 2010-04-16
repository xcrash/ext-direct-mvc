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
    using System.Web;
    using System.Text;
    using System.IO;
    using Newtonsoft.Json;
    using Ext.Direct.Mvc.Configuration;

    [JsonObject]
    public abstract class DirectResponseBase {

        protected DirectResponseBase(DirectRequest request) {
            this.IsFileUpload = request.IsFileUpload;
        }

        [JsonIgnore]
        public bool IsFileUpload {
            get;
            private set;
        }

        [JsonIgnore]
        public JsonSerializerSettings Settings {
            get;
            set;
        }

        public string ToJson() {
            string jsonResponse;

            JsonSerializer serializer = JsonSerializer.Create(this.Settings);
            // Default DateTime converter is added last therefore will not override the
            // one provided by the caller.
            if (DirectConfig.DefaultDateTimeConverter != null) {
                serializer.Converters.Add(DirectConfig.DefaultDateTimeConverter);
            }

            using (var stringWriter = new StringWriter()) {
                using (var jsonWriter = new JsonTextWriter(stringWriter)) {
                    jsonWriter.Formatting = DirectConfig.Debug ? Formatting.Indented : Formatting.None;
                    serializer.Serialize(jsonWriter, this);
                    jsonResponse = stringWriter.ToString();
                }
            }

            return jsonResponse;
        }

        public void Write(HttpResponseBase httpResponse) {
            this.Write(httpResponse, null, null);
        }

        public void Write(HttpResponseBase httpResponse, string contentType, Encoding contentEncoding) {
            string jsonResponse = this.ToJson();

            if (this.IsFileUpload) {
                const string s = "<html><body><textarea>{0}</textarea></body></html>";
                httpResponse.ContentType = "text/html";
                httpResponse.Write(String.Format(s, jsonResponse.Replace("&quot;", "\\&quot;")));
            } else {
                if (!String.IsNullOrEmpty(contentType)) {
                    httpResponse.ContentType = contentType;
                } else {
                    httpResponse.ContentType = "application/json";
                }

                if (contentEncoding != null) {
                    httpResponse.ContentEncoding = contentEncoding;
                }

                httpResponse.Write(jsonResponse);
            }
        }
    }
}
