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
    using Newtonsoft.Json;

    [JsonObject]
    public class DirectRequest {

        public static string DirectRequestKey = "Ext.Direct.Mvc.DirectRequest";

        public const string RequestFormTransactionId = "extTID";
        public const string RequestFormType = "extType";
        public const string RequestFormAction = "extAction";
        public const string RequestFormMethod = "extMethod";
        public const string RequestFileUpload = "extUpload";

        public DirectRequest() { }

        public DirectRequest(HttpRequestBase httpRequest) {
            // This constructor is called in case of Form post only
            this.IsFormPost = true;
            this.Action = httpRequest[RequestFormAction] ?? String.Empty;
            this.Method = httpRequest[RequestFormMethod] ?? String.Empty;
            this.Type = httpRequest[RequestFormType] ?? String.Empty;
            this.IsFileUpload = Convert.ToBoolean(httpRequest[RequestFileUpload]);
            this.TransactionId = Convert.ToInt32(httpRequest[RequestFormTransactionId]);
        }

        public string Action {
            get;
            set;
        }

        public string Method {
            get;
            set;
        }

        [JsonConverter(typeof(RequestDataConverter))]
        public object[] Data {
            get;
            set;
        }

        public string Type {
            get;
            set;
        }

        public bool IsFormPost {
            get;
            set;
        }

        public bool IsFileUpload {
            get;
            set;
        }

        [JsonProperty("tid")]
        public int TransactionId {
            get;
            set;
        }
    }
}
