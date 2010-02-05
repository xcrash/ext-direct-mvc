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

    public class DirectResponse : DirectResponseBase {

        public DirectResponse(DirectRequest request) : base(request) {
            this.TransactionId = request.TransactionId;
            this.Action = request.Action;
            this.Method = request.Method;
        }

        [JsonProperty("type")]
        public virtual string Type {
            get { return "rpc"; }
        }

        [JsonProperty("tid")]
        public int TransactionId {
            get;
            set;
        }

        [JsonProperty("action")]
        public string Action {
            get;
            set;
        }

        [JsonProperty("method")]
        public string Method {
            get;
            set;
        }

        [JsonProperty("result", NullValueHandling = NullValueHandling.Ignore)]
        public object Result {
            get;
            set;
        }
    }
}
