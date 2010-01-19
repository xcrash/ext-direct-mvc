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

namespace Ext.Direct.Mvc.Configuration {
    using System;
    using System.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Ext.Direct.Mvc.Resources;

    public class DirectConfig {

        private static readonly DirectSection _directSection;
        private static JsonConverter _defaultConverter;

        static DirectConfig() {
            _directSection = ConfigurationManager.GetSection("ext.direct") as DirectSection;
            if (_directSection == null || _directSection.Equals(new DirectSection())) {
                throw new ConfigurationErrorsException(DirectResources.DirectConfig_NoExtDirectSectionFound);
            }
        }

        public static string ProviderName {
            get { return _directSection.ProviderName; }
        }

        public static string ApiUrl {
            get { return _directSection.ApiUrl; }
        }

        public static string RouterUrl {
            get { return _directSection.RouterUrl; }
        }

        public static string Namespace {
            get { return _directSection.Namespace; }
        }

        public static string Assembly {
            get { return _directSection.Assembly; }
        }

        public static int? Buffer {
            get { return _directSection.Buffer; }
        }

        public static string DateFormat {
            get { return _directSection.DateFormat; }
        }

        public static bool Debug {
            get { return _directSection.Debug; }
        }

        internal static JsonConverter DefaultDateTimeConverter {
            get {
                if (_defaultConverter == null && !String.IsNullOrEmpty(DirectConfig.DateFormat)) {
                    switch (DirectConfig.DateFormat.ToLower()) {
                        case "javascript":
                            _defaultConverter = new JavaScriptDateTimeConverter();
                            break;
                        case "iso":
                            _defaultConverter = new IsoDateTimeConverter();
                            break;
                    }
                }

                return _defaultConverter;
            }
        }
    }
}
