using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ExchangeRatesStatistic.MVC.Configuration
{
    /// <summary>
    /// Collection of configuration properties representing allowed in this application currencies codes
    /// </summary>
    [ConfigurationCollection(typeof(AllowedCurrencyConfigElement))]
    public class AllowedCurrencyCodesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AllowedCurrencyConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AllowedCurrencyConfigElement)(element)).Code;
        }

        public AllowedCurrencyConfigElement this[int idx]
        {
            get { return (AllowedCurrencyConfigElement)BaseGet(idx); }
        }
    }

}