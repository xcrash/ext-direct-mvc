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

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class DirectHandleErrorAttribute : FilterAttribute, IExceptionFilter {

        public void OnException(ExceptionContext filterContext) {
            if (filterContext == null) {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.ExceptionHandled) {
                return;
            }

            Exception exception = filterContext.Exception;

            var directRequest = filterContext.HttpContext.Items[DirectRequest.DirectRequestKey] as DirectRequest;

            if (directRequest != null) {
                var errorResponse = new DirectErrorResponse(directRequest, exception);
                errorResponse.Write(filterContext.HttpContext.Response);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
