using Newtonsoft.Json;
using Skybrud.Social.Google.Common;
using Skybrud.Social.Google.Drive.Options.Files;
using Skybrud.Social.Http;
using uDrive.Core.Constants;
using uDrive.Core.Helpers;
using uDrive.Core.Models.Google.Files;
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

        public AllFiles GetFiles()
        {
            var fields = string.Join(",", new string[] { GoogleDriveConstants.Fields.Kind, GoogleDriveConstants.Fields.NextPageToken, GoogleDriveConstants.Fields.Files });
            var response = GetGoogleService().Client.DoHttpGetRequest("https://www.googleapis.com/drive/v3/files", new DriveGetFilesOptions() { Fields = fields, Query = "mimeType contains \"image\"" });
            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            var allFiles = JsonConvert.DeserializeObject<AllFiles>(response.Body, settings);
            return allFiles;
        }

        //https://www.googleapis.com/drive/v3/files/0B3d-ObcEOIBQT0hndFNaWTZfRzg?alt=media
    }
}
