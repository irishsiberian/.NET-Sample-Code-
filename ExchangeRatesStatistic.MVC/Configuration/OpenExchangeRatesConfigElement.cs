using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ExchangeRatesStatistic.MVC.Configuration
{
    /// <summary>
    /// Configuration element containing OpenExchangeRates configuration
    /// </summary>
    public class OpenExchangeRatesConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("link", IsKey = true, IsRequired = true)]
        public string Link
        {
            get { return ((string)(base["link"])); }
            set { base["link"] = value; }
        }
    }
}