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
    using Newtonsoft.Json;
    using Ext.Direct.Mvc.Resources;

    internal class DirectAction {

        private readonly IDictionary<string, DirectMethod> _methods;

        internal DirectAction(Type type) {
            this.Name = type.Name;
            if (this.Name.EndsWith("Controller")) {
                this.Name = this.Name.Substring(0, this.Name.IndexOf("Controller"));
            }
            _methods = new Dictionary<string, DirectMethod>();
            ConfigureMethods(type);
        }

        internal string Name {
            get;
            private set;
        }

        // Adds class methods (controller actions) to the internal collection
        private void ConfigureMethods(Type type) {
            var methods = type.GetMethods();
            foreach (var method in methods) {
                if (method.IsDirectMethod()) {
                    string name = method.GetName();
                    if (_methods.ContainsKey(name)) {
                        throw new Exception(String.Format(DirectResources.DirectAction_MethodExists, name, this.Name));
                    }
                    _methods.Add(name, new DirectMethod(method));
                }
            }
        }

        internal DirectMethod GetMethod(string name) {
            return _methods.ContainsKey(name) ? _methods[name] : null;
        }

        // Writes JSON representaion of the action to the specified JsonWriter.
        // Used to generate Ext.Direct API
        internal void WriteJson(JsonWriter jsonWriter) {
            jsonWriter.WritePropertyName(Name);
            jsonWriter.WriteStartArray();
            foreach (var method in _methods.Values) {
                method.WriteJson(jsonWriter);
            }
            jsonWriter.WriteEndArray();
        }
    }
}
