using System.Collections.Generic;
using System.Linq;
using uDrive.Core.Constants;
using uDrive.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using System.Web.Hosting;
using System.Xml;

namespace uDrive.Core.Controllers
{
    /// <summary>
    /// Web API Controller for fetching & saving values from settings.config file
    /// 
    /// Example route URL
    /// [[HOST]]/umbraco/backoffice/uDrive/SettingsApi/GetAllSettingss
    /// </summary>
    [PluginController(PackageConstants.SectionName)]
    public class SettingsApiController : UmbracoAuthorizedApiController
    {
        private readonly string _configPath = HostingEnvironment.MapPath(PackageConstants.SettingsConfigPath);

        /// <summary>
        /// Gets all settings from settings.config
        /// </summary>
        /// <returns>Serializes settings from XML file to Setting objects</returns>
        public List<Setting> GetAllSettings()
        {
            var settings = new List<Setting>();

            var settingsXml = GetSettingsXmlDocument();

            XmlNodeList udriveNode = settingsXml.SelectNodes("//uDrive");

            if (udriveNode != null)
            {
                foreach (XmlNode settingNode in udriveNode)
                {
                    foreach (XmlNode setting in settingNode.ChildNodes)
                    {
                        var settingToAdd = new Setting()
                        {
                            Key = setting.Name,
                            Label = setting.Attributes["label"].Value,
                            Description = setting.Attributes["description"].Value,
                            Value = setting.InnerText
                        };

                        settings.Add(settingToAdd);
                    }
                }
            }

            return settings;
        }

        /// <summary>
        /// Saves all settings passed in to settings.config
        /// </summary>
        /// <param name="settings"></param>
        /// <returns>List of Setting Objects</returns>
        public List<Setting> SaveAllSettings(List<Setting> settings)
        {
            var settingsXml = GetSettingsXmlDocument();

            XmlNodeList uDriveNode = settingsXml.SelectNodes("//uDrive");

            foreach (XmlNode settingNode in uDriveNode)
            {
                foreach (XmlNode setting in settingNode.ChildNodes)
                {
                    setting.InnerText = settings.SingleOrDefault(x => x.Key == setting.Name).Value;
                }
            }

            settingsXml.Save(_configPath);

            return settings;
        }

        /// <summary>
        /// Check to see if RefreshToken has a value & we have auth from Google
        /// </summary>
        /// <returns></returns>
        public bool GetAuth()
        {
            var settingsXml = GetSettingsXmlDocument();

            XmlNode refreshNode = settingsXml.SelectSingleNode("//uDrive/RefreshToken");

            if (refreshNode != null)
            {
                return refreshNode.InnerText.Length > 0;
            }

            return false;
        }

        #region Private Methods

        private XmlDocument GetSettingsXmlDocument()
        {
            XmlDocument settingsXml = new XmlDocument();
            settingsXml.Load(_configPath);
            return settingsXml;
        }

        #endregion
    }
}
