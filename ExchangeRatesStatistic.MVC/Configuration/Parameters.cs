using System.Configuration;

namespace ExchangeRatesStatistic.MVC.Configuration
{
    /// <summary>
    /// This singleton contains all application's parameters.
    /// </summary>
    public class Parameters
    {
        InteresneeTestTaskConfigSection config;
        public InteresneeTestTaskConfigSection Config
        {
            get
            {
                if (config == null)
                {
                    config = (InteresneeTestTaskConfigSection)ConfigurationManager.GetSection("InteresneeTestTask");
                }
                return config;
            }
        }

        private static Parameters _instance;
        public static Parameters Instance
        {
            get { return _instance ?? (_instance = new Parameters()); }
        }
    }
}
