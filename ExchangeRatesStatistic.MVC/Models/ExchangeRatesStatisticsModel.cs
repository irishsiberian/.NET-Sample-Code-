using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;

namespace ExchangeRatesStatistic.MVC.Models
{
    public class ExchangeRatesStatisticsModel
    {
        public ExchangeRatesStatisticsModel()
        {
            StartDate = DateTime.Today.AddDays(-7);
            EndDate = DateTime.Today;
            BaseCurrencyCode = "USD";
            RelationalCurrencyCode = "RUB";
        }

        /// <summary>
        /// Start date of statistics period
        /// </summary>
        [Display(Name = "Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be empty")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of statistics period
        /// </summary>
        [Display(Name = "End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be empty")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Base currency code
        /// </summary>
        [Display(Name = "Base currency")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be empty")]
        public string BaseCurrencyCode { get; set; }

        /// <summary>
        /// Relational currency code
        /// </summary>
        [Display(Name = "Relational currency")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This field cannot be empty")]
        public string RelationalCurrencyCode { get; set; }

        /// <summary>
        /// Chart data
        /// </summary>
        public List<CurrencyChartDataPoint> ExchangeRatesChartData { get; set; }

        /// <summary>
        /// Serialized chart data
        /// </summary>
        public string ExchangeRatesJsonChartData
        {
            get
            {
                return new JavaScriptSerializer().Serialize(ExchangeRatesChartData);
            }
        }
    }
}