using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ExchangeRatesStatistic.MVC.Configuration
{
    /// <summary>
    /// Configuration element containing info about one of allowed currencies
    /// </summary>
    public class AllowedCurrencyConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["name"])); }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("code", IsKey = false, IsRequired = true)]
        public string Code
        {
            get { return ((string)(base["code"])); }
            set { base["code"] = value; }
        }
    }
}