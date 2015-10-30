using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExchangeRatesStatistic.MVC.Models;
using System.Configuration;
using ExchangeRatesStatistic.MVC.Configuration;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using ExchangeRatesStatistic.MVC.OpenExchangeRatesEntities;
using ExchangeRatesStatistic.MVC.DAL;

namespace ExchangeRatesStatistic.MVC.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// maximum interval for statistics query in days
        /// </summary>
        private const int MaxStatisticsInterval = 62;

        /// <summary>
        /// Initial action.
        /// </summary>
        /// <param name="statisticsModel"></param>
        /// <returns></returns>
        public ActionResult Index(ExchangeRatesStatisticsModel statisticsModel)
        {
            ViewBag.AvailableCurrencies = GetAvailableCurrencies();

            return View(statisticsModel);
        }

        /// <summary>
        /// Show to user exchange rates statistics by his query
        /// </summary>
        /// <param name="statisticsModel"></param>
        /// <returns></returns>
        public ActionResult GetExchangeRatesForPeriod(ExchangeRatesStatisticsModel statisticsModel)
        {
            if (statisticsModel.EndDate > statisticsModel.StartDate &&
                (statisticsModel.EndDate - statisticsModel.StartDate).TotalDays < MaxStatisticsInterval)
            {
                statisticsModel.ExchangeRatesChartData = GetExchangeRates(statisticsModel);
            }
            else
            {
                ModelState.AddModelError("", string.Format(
                    "Wrong period. Start date must be earlier than end date. Period length must be less than {0} days", MaxStatisticsInterval));
            }

            ViewBag.AvailableCurrencies = GetAvailableCurrencies();

            //the same view as in Index action
            return View("Index", statisticsModel);
        }


        /// <summary>
        /// Get list of exchange rates chart datapoints by model's criteria. 
        /// </summary>
        /// <param name="statisticsModel"></param>
        /// <returns></returns>
        private List<CurrencyChartDataPoint> GetExchangeRates(ExchangeRatesStatisticsModel statisticsModel)
        {
            List<ExchangeRatesHistory> baseRatesHistory = GetExchangeRatesForCurrency(statisticsModel, statisticsModel.BaseCurrencyCode);
            List<ExchangeRatesHistory> relationalRatesHistory = GetExchangeRatesForCurrency(statisticsModel, statisticsModel.RelationalCurrencyCode);
            List<CurrencyChartDataPoint> resultingDataForChart = new List<CurrencyChartDataPoint>();

            foreach (ExchangeRatesHistory baseRateHistoryItem in baseRatesHistory)
            {
                //database keeps rates only for USD, so we have to calculate any other rate
                ExchangeRatesHistory relationalRateHistoryItem = relationalRatesHistory.Where(r => r.Date == baseRateHistoryItem.Date).First();
                double calculatedRate = calculatedRate = relationalRateHistoryItem.CurrencyRate / baseRateHistoryItem.CurrencyRate;
                //remember calculated rate in chart data
                resultingDataForChart.Add(new CurrencyChartDataPoint(baseRateHistoryItem.Date, calculatedRate));
            }

            return resultingDataForChart.OrderBy(er => er.Date).ToList();
        }

        /// <summary>
        /// Get list of exchange rates history entities for specific period (from model) for specific currency
        /// </summary>
        /// <param name="statisticsModel">Data Model</param>
        /// <param name="currencyCode">Currency Code</param>
        /// <returns></returns>
        private List<ExchangeRatesHistory> GetExchangeRatesForCurrency(ExchangeRatesStatisticsModel statisticsModel, string currencyCode)
        {
            List<ExchangeRatesHistory> ratesHistory = new List<ExchangeRatesHistory>();
            try
            {
                using (ExchangeRatesDataModelContainer dbModel = new ExchangeRatesDataModelContainer())
                {
                    //Extract existing data from database
                    ratesHistory = dbModel.ExchangeRatesHistories
                        .Where(r =>
                            r.Date >= statisticsModel.StartDate &&
                            r.Date <= statisticsModel.EndDate &&
                            (r.Currency.ServiceCode == currencyCode))
                        .ToList();
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to get rates history from Database", e);
            }

            for (DateTime currentDate = statisticsModel.StartDate; currentDate <= statisticsModel.EndDate; currentDate = currentDate.AddDays(1))
            {
                List<ExchangeRatesHistory> existingRate = ratesHistory
                    .Where(rh => rh.Date == currentDate).ToList();

                if (existingRate.Count == 0)
                {
                    List<ExchangeRatesHistory> ratesFromService = GetExchangeRatesFromService(currentDate)
                        .Where(r => r.Currency.ServiceCode == currencyCode).ToList();
                    AddRatesToDb(ratesFromService);
                    ratesHistory.AddRange(ratesFromService);
                }
            }
            return ratesHistory;
        }

        /// <summary>
        /// Get exchange rates from Open Exchange Rates service, write them into database and 
        /// return list of ExchangeRatesHistory for available currencies for specific date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private List<ExchangeRatesHistory> GetExchangeRatesFromService(DateTime date)
        {
            string requestLink = string.Format(Parameters.Instance.Config.OpenExchangeRates.Link, date);
            List<ExchangeRatesHistory> resultList = new List<ExchangeRatesHistory>();
            HttpWebRequest serviceRequest = (HttpWebRequest)HttpWebRequest.Create(requestLink);
            HttpWebResponse serviceResponse;
            try
            {
                serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();
            }
            catch (WebException)
            {
                return resultList;
            }
            Stream responseStream = serviceResponse.GetResponseStream();
            StreamReader responseStreamReader = new StreamReader(responseStream);
            string responseString = responseStreamReader.ReadToEnd();
            serviceResponse.Close();
            resultList = OpenExchangeRatesJsonParser.ParseHistoricalJsonString(responseString);
            return resultList;
        }

        /// <summary>
        /// Add new exchange rates history entries to database
        /// </summary>
        /// <param name="rates">list of entities to add</param>
        private void AddRatesToDb(List<ExchangeRatesHistory> rates)
        {
            try
            {
                using (ExchangeRatesDataModelContainer dbModel = new ExchangeRatesDataModelContainer())
                {
                    foreach (var rate in rates)
                    {
                        rate.Currency = dbModel.Currencies.Where(c => c.ServiceCode == rate.Currency.ServiceCode).First();
                        dbModel.ExchangeRatesHistories.AddObject(rate);
                    }
                    dbModel.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to add rates history to Database", e);
            }
        }

        /// <summary>
        /// Get list of available currencies for binding to currencies comboboxes
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetAvailableCurrencies()
        {
            List<SelectListItem> resultLIst = new List<SelectListItem>();

            foreach (var allowedCurrency in Parameters.Instance.Config.AllowedCurrencyCodes)
            {
                AllowedCurrencyConfigElement configElement = (AllowedCurrencyConfigElement)allowedCurrency;
                resultLIst.Add(new SelectListItem{ Value = configElement.Code, Text = configElement.Name });
            }

            return resultLIst;
        }

    }
}
