using Google.Apis.Auth.OAuth2.Mvc;

namespace uDrive.Core.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
        protected override FlowMetadata FlowData
        {
            get { return null; }
        }
    }
}
