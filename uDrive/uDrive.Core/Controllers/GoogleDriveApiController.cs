using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using uDrive.Core.Constants;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace uDrive.Core.Controllers
{
    [PluginController(PackageConstants.SectionAlias)]
    public class GoogleDriveApiController : UmbracoAuthorizedApiController
    {
        public async Task<string> TestApiCall(CancellationToken cancellationToken)
        {
            var baseClient = new BaseClientService.Initializer()
            {
                ApiKey = "",
                ApplicationName = "",
            };

            var driveApi = new DriveService(baseClient);

            var getSomething = await driveApi.Files.List().ExecuteAsync();

            if (getSomething != null)
            {
                return string.Concat(getSomething.Files.Select(f => f.Name));
            }

            return null;
        }
    }
}
