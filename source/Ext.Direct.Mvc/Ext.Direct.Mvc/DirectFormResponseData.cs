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
    using Newtonsoft.Json;

    [JsonObject]
    public class DirectFormResponseData {

        public DirectFormResponseData() { }

        public DirectFormResponseData(bool success) : this(success, null) { }

        public DirectFormResponseData(bool success, object data) {
            this.Success = success;
            if (success) {
                this.Data = data;
            } else {
                this.Errors = data;
            }
        }

        [JsonProperty("success")]
        public bool Success {
            get;
            set;
        }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public object Data {
            get;
            set;
        }

        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public object Errors {
            get;
            set;
        }
    }
}
