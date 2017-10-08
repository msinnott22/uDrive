using Skybrud.Social.Google;
using uDrive.Core.Constants;
using uDrive.Core.Helpers;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace uDrive.Core.Controllers
{
    [PluginController(PackageConstants.SectionAlias)]
    public class GoogleDriveApiController : UmbracoAuthorizedApiController
    {
        private GoogleService GetGoogleService()
        {
            return GoogleService.CreateFromRefreshToken(UDriveConfig.ClientId, UDriveConfig.ClientSecret,
                UDriveConfig.RefreshToken);
        }

        /// <summary>
        /// Get Google User Info
        /// </summary>
        /// <returns></returns>
        //public GoogleGetUserInfoResponse GetUser()
        //{
        //    return GetGoogleService().GetUserInfo();
        //}
    }
}
