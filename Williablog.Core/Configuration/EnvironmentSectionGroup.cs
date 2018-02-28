﻿namespace Williablog.Core.Configuration
{
    using System.Configuration;

    public class EnvironmentSectionGroup : ConfigurationSectionGroup
    {

        #region Properties

        [ConfigurationProperty("appSettings")]
        public AppSettingsSection AppSettings
        {
            get
            {
                return (AppSettingsSection)Sections["appSettings"];
            }
        }

        [ConfigurationProperty("connectionStrings")]
        public ConnectionStringsSection ConnectionStrings
        {
            get
            {
                return (ConnectionStringsSection)Sections["connectionStrings"];
            }
        }

        #endregion

    }
}
