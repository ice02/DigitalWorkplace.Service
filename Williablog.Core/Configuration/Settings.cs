namespace Williablog.Core.Configuration
{
    using System.Configuration;

    public static class Settings
    {
        public static ConnectionStringSettings Default
        {
            get
            {
                return AdvancedSettingsManager.SettingsFactory().ConnectionStrings["AppData"];
            }
        }

        public static ConnectionStringSettings Elmah
        {
            get
            {
                return AdvancedSettingsManager.SettingsFactory().ConnectionStrings["ErrorDB"];
            }
        }

        public static string SmtpServer
        {
            get
            {
                return AdvancedSettingsManager.SettingsFactory().AppSettings["SmtpServer"];
            }
        }

        public static string ECommerceStoreUrl
        {
            get
            {
                return AdvancedSettingsManager.SettingsFactory().AppSettings["WebServiceUrl"];
            }
        }
    }
}
