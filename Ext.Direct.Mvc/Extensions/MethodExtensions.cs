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
    using System.Reflection;

    internal static class MethodExtensions {

        internal static string GetName(this MethodBase method) {
            var attr = method.GetAttribute<ActionNameAttribute>();
            return attr != null ? attr.Name : method.Name;
        }

        internal static bool IsDirectMethod(this MethodInfo method) {
            // Any controller action with return type DirectResult is a Direct method unless marked with DirectIgnoreAttribute
			bool returnsActionResult = (method.ReturnType == typeof(DirectResult) || method.ReturnType.IsSubclassOf(typeof(ActionResult)));
            bool ignore = method.HasAttribute<DirectIgnoreAttribute>();
            return (returnsActionResult && !ignore);
        }

        internal static bool IsFormHandler(this MethodBase method) {
            return method.HasAttribute<FormHandlerAttribute>();
        }

        internal static bool HasAttribute<T>(this MethodBase method) where T : Attribute {
            T attribute = method.GetAttribute<T>();
            return attribute != null;
        }

        internal static T GetAttribute<T>(this MethodBase method) where T : Attribute {
            T attribute = null;
            var attributes = (T[])method.GetCustomAttributes(typeof(T), true);
            if (attributes != null && attributes.Length > 0) {
                attribute = attributes[0];
            }
            return attribute;
        }
    }
}
