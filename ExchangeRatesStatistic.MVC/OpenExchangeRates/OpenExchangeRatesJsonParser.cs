using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using ExchangeRatesStatistic.MVC.Models;
using ExchangeRatesStatistic.MVC.Configuration;
using ExchangeRatesStatistic.MVC.DAL;

namespace ExchangeRatesStatistic.MVC.OpenExchangeRatesEntities
{
    public class OpenExchangeRatesJsonParser
    {
        /// <summary>
        /// Parse Json data from Open Exchange Rates web service
        /// </summary>
        /// <param name="jsonString">received json string</param>
        /// <returns>list of parsed exchange rates history object</returns>
        public static List<ExchangeRatesHistory> ParseHistoricalJsonString(string jsonString)
        {
            List<ExchangeRatesHistory> parsedObjects = new List<ExchangeRatesHistory>();
            JObject parsedJObject = JObject.Parse(jsonString);

            if (parsedJObject.HasValues && parsedJObject.SelectToken("rates") != null && parsedJObject.SelectToken("timestamp") != null)
            {
                double timestamp = parsedJObject.SelectToken("timestamp").Value<double>();
                //convert from javascript timestamp to DateTime
                DateTime parsedDate = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timestamp).Date;
                foreach (var allowedCurrency in Parameters.Instance.Config.AllowedCurrencyCodes)
                {
                    AllowedCurrencyConfigElement configElement = (AllowedCurrencyConfigElement)allowedCurrency;

                    if (parsedJObject.SelectToken("rates").SelectToken(configElement.Code) != null)
                    {
                        double parsedRate = parsedJObject.SelectToken("rates").SelectToken(configElement.Code).Value<double>();
                        ExchangeRatesHistory parsedRateHistoryItem = new ExchangeRatesHistory 
                        { 
                            CurrencyRate = parsedRate, 
                            Date = parsedDate, 
                            Currency = new Currency { ServiceCode = configElement.Code } 
                        };
                        parsedObjects.Add(parsedRateHistoryItem);
                    }
                }
            }

            return parsedObjects;
        }
    }
}