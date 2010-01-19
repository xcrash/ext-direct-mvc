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
    using Newtonsoft.Json.Linq;

    internal class RequestDataConverter : JsonConverter {

        public override object ReadJson(JsonReader reader, Type objectType, JsonSerializer serializer) {
            var data = new List<object>();
            var dataArray = JToken.ReadFrom(reader);

            if (!dataArray.HasValues) return null;

            foreach (JToken dataItem in dataArray) {
                if (dataItem is JValue) {
                    var value = (dataItem as JValue).Value;
                    data.Add(value == null ? value : value.ToString());
                } else {
                    data.Add(dataItem);
                }
            }

            return data.ToArray();
        }

        public override bool CanConvert(Type objectType) {
            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
