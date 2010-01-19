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
    using System.Web.Routing;
    using Ext.Direct.Mvc.Configuration;

    public class DirectHttpModule : IHttpModule {
        private static bool _initialized = false;
        private static readonly object _syncLock = new object();

        public void Init(HttpApplication context) {
            if (!_initialized) {
                lock (_syncLock) {
                    if (!_initialized) {
                        _initialized = true;
                        // Mapping Direct API configurator url
                        RouteTable.Routes.Insert(0, new Route(
                            DirectConfig.ApiUrl,
                            new RouteValueDictionary(new { controller = "", action = "" }),
                            new DirectMvcRouteHandler()
                        ));

                        // Mapping Direct router url
                        RouteTable.Routes.Insert(1, new Route(
                            DirectConfig.RouterUrl,
                            new RouteValueDictionary(new { controller = "", action = "" }),
                            new DirectMvcRouteHandler()
                        ));
                    }
                }
            }
        }

        public void Dispose() { }
    }
}