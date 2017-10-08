using System.Xml;
using uDrive.Core.Helpers;
using Umbraco.Web.WebApi;

namespace uDrive.Core.Controllers
{
    /// <summary>
    /// Web API Controller for Fetching & Saving values from settings.config file
    /// 
    /// Example route URL to API
    /// http://localhost:62315/umbraco/backoffice/uDrive/oAuthApi/PostSettingValue
    /// </summary>
    public class oAuthApiController : UmbracoApiController
    {
        public string PostSettingValue(string key, string value)
        {
            XmlDocument settingsXml = new XmlDocument();
            settingsXml.Load(UDriveConfig.ConfigFilePath);

            XmlNode settingNode = settingsXml.SelectSingleNode("//uDrive/" + key);

            if (settingNode != null)
            {
                settingNode.InnerText = value;
            }

            settingsXml.Save(UDriveConfig.ConfigFilePath);
            return value;
        }
    }
}
