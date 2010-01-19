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
    using System.Reflection;
    using Newtonsoft.Json;

    internal class DirectMethod {

        internal DirectMethod(MethodBase method) {
            this.Name = method.GetName();
            this.Len = method.GetParameters().Length;
            this.IsFormHandler = method.IsFormHandler();
        }

        internal string Name {
            get;
            private set;
        }

        internal int Len {
            get;
            private set;
        }

        internal bool IsFormHandler {
            get;
            private set;
        }

        // Writes JSON representaion of the method to the specified JsonWriter.
        // Used to generate Ext.Direct API
        internal void WriteJson(JsonWriter jsonWriter) {
            jsonWriter.WriteStartObject();
            jsonWriter.WriteProperty("name", this.Name);
            jsonWriter.WriteProperty("len", this.Len);
            if (this.IsFormHandler) {
                jsonWriter.WriteProperty("formHandler", true);
            }
            jsonWriter.WriteEndObject();
        }
    }
}
