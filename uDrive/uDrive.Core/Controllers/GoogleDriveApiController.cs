using Skybrud.Social.Google.Analytics.Objects.Accounts;
using Skybrud.Social.Google.Common;
using uDrive.Core.Helpers;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace uDrive.Core.Controllers
{
    [PluginController("uDrive")]
    public class GoogleDriveApiController : UmbracoAuthorizedApiController
    {
        private GoogleService GetGoogleService()
        {
            return GoogleService.CreateFromRefreshToken(UDriveConfig.ClientId, UDriveConfig.ClientSecret,
                UDriveConfig.RefreshToken);
        }

        /// <summary>
        /// Get's Accounts on this authenticated user account
        /// </summary>
        /// <returns></returns>
        public AnalyticsAccount[] GetAccounts()
        {
            // Get the accounts from the Google Analytics API
            return new AnalyticsAccount[0];
        }
    }
}
