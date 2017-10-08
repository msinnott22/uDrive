namespace uDrive.Core.Constants
{
    public class PackageConstants
    {
        public const string SectionAlias = "udrive";
        public const string SectionName = "uDrive";
        public const string SectionIcon = "icon-cloud-drive";

        public const string SettingsConfigPath = "~/App_Plugins/uDrive/settings.config";

        public static string UmbracoVersion
        {
            get { return Umbraco.Core.Configuration.UmbracoVersion.Current.ToString(); }
        }
    }
}
