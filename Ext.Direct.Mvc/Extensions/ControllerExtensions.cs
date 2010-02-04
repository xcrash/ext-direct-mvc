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
    using System.Text;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using Ext.Direct.Mvc.Resources;

    public static class ControllerExtensions {

        public static DirectResult Direct(this IController controller, object data) {
            return controller.Direct(data, (string)null);
        }

        public static DirectResult Direct(this IController controller, object data, string contentType) {
            return controller.Direct(data, contentType, (Encoding)null);
        }

        public static DirectResult Direct(this IController controller, object data, string contentType, Encoding contentEncoding) {
            return controller.Direct(data, contentType, contentEncoding, (JsonSerializerSettings)null);
        }

        public static DirectResult Direct(this IController controller, object data, params JsonConverter[] converters) {
            return controller.Direct(data, null, converters);
        }

        public static DirectResult Direct(this IController controller, object data, string contentType, params JsonConverter[] converters) {
            return controller.Direct(data, contentType, null, converters);
        }

        public static DirectResult Direct(this IController controller, object data, string contentType, Encoding contentEncoding, params JsonConverter[] converters) {
            JsonSerializerSettings settings = (converters != null && converters.Length > 0)
                ? new JsonSerializerSettings { Converters = converters }
                : null;

            return controller.Direct(data, contentType, contentEncoding, settings);
        }

        public static DirectResult Direct(this IController controller, object data, string contentType, Encoding contentEncoding, JsonSerializerSettings settings) {
            return new DirectResult {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Settings = settings
            };
        }

        public static DirectEventResult DirectEvent(this IController controller, string name) {
            return controller.DirectEvent(name, null);
        }

        public static DirectEventResult DirectEvent(this IController controller, string name, object data) {
            return controller.DirectEvent(name, data, (string)null);
        }

        public static DirectEventResult DirectEvent(this IController controller, string name, object data, string contentType) {
            return controller.DirectEvent(name, data, contentType, (Encoding)null);
        }

        public static DirectEventResult DirectEvent(this IController controller, string name, object data, string contentType, Encoding contentEncoding) {
            return controller.DirectEvent(name, data, contentType, contentEncoding, (JsonSerializerSettings)null);
        }

        public static DirectEventResult DirectEvent(this IController controller, string name, object data, params JsonConverter[] converters) {
            return controller.DirectEvent(name, data, null, converters);
        }

        public static DirectEventResult DirectEvent(this IController controller, string name, object data, string contentType, params JsonConverter[] converters) {
            return controller.DirectEvent(name, data, contentType, null, converters);
        }

        public static DirectEventResult DirectEvent(this IController controller, string name, object data, string contentType, Encoding contentEncoding, params JsonConverter[] converters) {
            JsonSerializerSettings settings = (converters != null && converters.Length > 0)
                ? new JsonSerializerSettings { Converters = converters }
                : null;

            return controller.DirectEvent(name, data, contentType, contentEncoding, settings);
        }

        public static DirectEventResult DirectEvent(this IController controller, string name, object data, string contentType, Encoding contentEncoding, JsonSerializerSettings settings) {
            if (String.IsNullOrEmpty(name)) {
                throw new ArgumentException(DirectResources.Common_NullOrEmpty, "name");
            }

            return new DirectEventResult {
                Name = name,
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                Settings = settings
            };
        }
    }
}
