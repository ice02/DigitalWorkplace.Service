namespace Williablog.Core.Configuration
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.IO;

    public class BasicSettingsManager
    {
        #region fields

        private const string ConfigurationFileName = "Williablog.Core.config";

        /// <summary>
        /// default path to the config file that contains the settings we are using
        /// </summary>
        private static string configurationFile;

        /// <summary>
        /// Stores an instance of this class, to cut down on I/O: No need to keep re-loading that config file
        /// </summary>
        /// <remarks>Cannot use system.web.caching since agents will not have access to this by default, so use static member instead.</remarks>
        private static BasicSettingsManager instance;

        private static Configuration config;

        #endregion

        #region Constructors

        private BasicSettingsManager()
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configurationFile
            };
            config = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the ConnectionStrings section
        /// </summary>
        public ConnectionStringSettingsCollection ConnectionStrings
        {
            get => config.ConnectionStrings.ConnectionStrings;
        }

        /// <summary>
        /// Returns the AppSettings Section
        /// </summary>
        public NameValueCollection AppSettings
        {
            get
            {
                NameValueCollection settings = new NameValueCollection();
                foreach (KeyValueConfigurationElement element in config.AppSettings.Settings)
                {
                    settings.Add(element.Key, element.Value);
                }

                return settings;
            }
        }

        #endregion

        #region static factory methods

        /// <summary>
        /// Public factory method
        /// </summary>
        /// <returns></returns>
        public static BasicSettingsManager SettingsFactory()
        {
            // If there is a bin folder, such as in web projects look for the config file there first
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\bin"))
            {
                configurationFile = string.Format(@"{0}\bin\{1}", AppDomain.CurrentDomain.BaseDirectory, ConfigurationFileName);
            }
            else
            {
                // agents, for example, won't have a bin folder in production
                configurationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationFileName);
            }

            // If we still cannot find it, quit now!
            if (!File.Exists(configurationFile))
            {
                throw new FileNotFoundException(configurationFile);
            }

            return CreateSettingsFactoryInternal();
        }

        /// <summary>
        /// Overload that allows you to pass in the full path and filename of the config file you want to use.
        /// </summary>
        /// <param name="fullPathToConfigFile"></param>
        /// <returns></returns>
        public static BasicSettingsManager SettingsFactory(string fullPathToConfigFile)
        {
            configurationFile = fullPathToConfigFile;
            return CreateSettingsFactoryInternal();
        }

        /// <summary>internal Factory Method
        /// </summary>
        /// <returns>ConfigurationSettings object
        /// </returns>
        internal static BasicSettingsManager CreateSettingsFactoryInternal()
        {
            // If we havent created an instance yet, do so now
            if (instance == null)
            {
                instance = new BasicSettingsManager();
            }

            return instance;
        }

        #endregion
    }
}
