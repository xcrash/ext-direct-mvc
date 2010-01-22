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
    using System.Web.Mvc;

    public class DirectController : Controller {

        readonly DirectProvider _provider = DirectProvider.GetCurrentProvider();

        [AcceptVerbs("GET")]
        public ActionResult Api() {
            // Write Ext.Direct API

            string format = this.HttpContext.Request.QueryString["format"];
            bool toJson = (format != null && format == "json"); // for integration with the Ext Designer

            return JavaScript(_provider.ToJavaScript(toJson));
        }

        [AcceptVerbs("POST")]
        public ActionResult Route() {
            // Process Ext.Direct requests

            _provider.Execute(this.ControllerContext.RequestContext);
            return new EmptyResult();
        }
    }
}
