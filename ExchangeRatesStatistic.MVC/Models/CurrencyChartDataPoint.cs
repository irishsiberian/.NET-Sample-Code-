using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeRatesStatistic.MVC.Models
{
    /// <summary>
    /// Simple serializable class to keep exchange rates chart data
    /// </summary>
    [Serializable]
    public class CurrencyChartDataPoint
    {
        public CurrencyChartDataPoint(DateTime date, double rate)
        {
            Date = date;
            Rate = rate;
        }

        public DateTime Date { get; set; }
        public double Rate { get; set; }
    }
}