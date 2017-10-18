using Skybrud.Social.Google.Common;
using Skybrud.Social.Google.Common.Responses;
using Skybrud.Social.Google.Drive.Options.Files;
using Skybrud.Social.Http;
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
        /// Get Drive User Info Response
        /// </summary>
        /// <returns></returns>
        public GoogleGetUserInfoResponse GetDetails()
        {
            return GetGoogleService().GetUserInfo();
        }

        public SocialHttpResponse GetFiles()
        {
            return GetGoogleService().Client.DoHttpGetRequest("https://www.googleapis.com/drive/v3/files", new DriveGetFilesOptions());
        }
    }
}
