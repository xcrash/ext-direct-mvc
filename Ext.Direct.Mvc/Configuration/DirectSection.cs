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

    public class DirectSection : ConfigurationSection {

        [ConfigurationProperty("providerName", IsRequired = true)]
        public string ProviderName {
            get { return (string)this["providerName"]; }
        }

        [ConfigurationProperty("apiUrl", IsRequired = true)]
        public string ApiUrl {
            get { return (string)this["apiUrl"]; }
        }

        [ConfigurationProperty("routerUrl", IsRequired = true)]
        public string RouterUrl {
            get { return (string)this["routerUrl"]; }
        }

        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly {
            get { return (string)this["assembly"]; }
        }

        [ConfigurationProperty("namespace", IsRequired = false)]
        public string Namespace {
            get { return (string)this["namespace"]; }
        }

        [ConfigurationProperty("buffer", IsRequired = false, DefaultValue = null)]
        public int? Buffer {
            get { return (int?)this["buffer"]; }
        }

        [ConfigurationProperty("dateFormat", IsRequired = false, DefaultValue = null)]
        public string DateFormat {
            get { return (string)this["dateFormat"]; }
        }

        [ConfigurationProperty("debug", IsRequired = false, DefaultValue = false)]
        public bool Debug {
            get { return (bool)this["debug"]; }
        }
    }
}
