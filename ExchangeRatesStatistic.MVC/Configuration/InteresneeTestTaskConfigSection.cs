using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ExchangeRatesStatistic.MVC.Configuration
{
    /// <summary>
    /// Main configuration section of the application
    /// </summary>
    public class InteresneeTestTaskConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("AllowedCurrencyCodes")]
        public AllowedCurrencyCodesCollection AllowedCurrencyCodes
        {
            get { return ((AllowedCurrencyCodesCollection)(base["AllowedCurrencyCodes"])); }
        }

        [ConfigurationProperty("OpenExchangeRates")]
        public OpenExchangeRatesConfigElement OpenExchangeRates
        {
            get { return ((OpenExchangeRatesConfigElement)base["OpenExchangeRates"]); }
        }
    }
}