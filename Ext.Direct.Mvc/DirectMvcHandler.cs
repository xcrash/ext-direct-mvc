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
    using System.Web.Mvc;
    using System.Web.Routing;

    public class DirectMvcHandler : MvcHandler {

        public DirectMvcHandler(RequestContext requestContext) : base(requestContext) { }

        protected override void ProcessRequest(HttpContextBase httpContext) {
            DirectProvider provider = DirectProvider.GetCurrentProvider();

            if (httpContext.Request.RequestType == "GET") {

                // Write Ext.Direct API

                string format = httpContext.Request.QueryString["format"];
                bool toJson = (format != null && format == "json"); // for integration with the Ext Designer

                httpContext.Response.ContentType = toJson ? "application/json" : "text/javascript";
                httpContext.Response.Write(provider.ToJavaScript(toJson));

            } else {

                // Process Ext.Direct requests

                provider.Execute(this.RequestContext);
            }
        }
    }
}
