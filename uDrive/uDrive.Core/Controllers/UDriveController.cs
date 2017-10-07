using System.Net;
using System.Net.Http;
using System.Web.Http;
using uDrive.Core.Helpers;
using Umbraco.Web.WebApi;

namespace uDrive.Core.Controllers
{
    public class UDriveController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public HttpResponseMessage GetTest(int testModelId)
        {
            var model = TestData.Get(testModelId);

            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
