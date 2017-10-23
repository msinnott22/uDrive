using Newtonsoft.Json;
using Skybrud.Social.Google.Common;
using Skybrud.Social.Google.Drive.Options.Files;
using Skybrud.Social.Http;
using uDrive.Core.Helpers;
using uDrive.Core.Models.Google.User;
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

        public User GetUserInfo()
        {
            var response = GetGoogleService().Client.DoHttpGetRequest("https://www.googleapis.com/drive/v3/about?fields=kind%2Cuser");
            var settings = new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.All};
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(response.Body, settings);
            return userInfo.User;
        }

        public SocialHttpResponse GetFiles()
        {
            return GetGoogleService().Client.DoHttpGetRequest("https://www.googleapis.com/drive/v3/files", new DriveGetFilesOptions());
        }
    }
}
