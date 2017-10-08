using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.Linq;
using uDrive.Core.Constants;

namespace uDrive.Core.Helpers
{
    public class UDriveConfig
    {
        public static string ConfigFilePath
        {
            get { return HostingEnvironment.MapPath(PackageConstants.SettingsConfigPath); }
        }

        private static string GetSetting(string key)
        {
            XElement xUDrive = XElement.Load(ConfigFilePath);
            XElement xSetting = xUDrive.Element(key);
            return xSetting == null ? null : xSetting.Value;
        }

        private static void SetSetting(string key, string value)
        {
            XElement xUDrive = XElement.Load(ConfigFilePath);
            XElement xSetting = xUDrive.Element(key);
            if (xSetting == null)
            {
                xUDrive.Add(new XElement(key, new XAttribute("label", key), new XAttribute("description", ""),
                    value ?? ""));
            }
            else
            {
                xSetting.Value = value;
            }

            xUDrive.Save(ConfigFilePath);
        }

        /// <summary>
        /// Gets the client ID from the config file.
        /// </summary>
        public static string ClientId
        {
            get { return GetSetting("ClientId"); }
        }

        /// <summary>
        /// Gets the client secret from the config file.
        /// </summary>
        public static string ClientSecret
        {
            get { return GetSetting("ClientSecret"); }
        }

        /// <summary>
        /// Get the redirect URI from the config file.
        /// </summary>
        public static string RedirectUri
        {
            get { return GetSetting("RedirectUri"); }
        }

        /// <summary>
        /// The refresh token used to acquire new access tokens. This is sensitive information.
        /// </summary>
        public static string RefreshToken
        {
            get { return GetSetting("RefreshToken"); }
            set { SetSetting("RefreshToken", value); }
        }
    }
}
