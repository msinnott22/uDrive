using System;
using System.Configuration;
using System.Web.UI;
using Skybrud.Social.Google.Common;
using Skybrud.Social.Google.Common.OAuth;
using Skybrud.Social.Google.Common.Scopes;
using Skybrud.Social.Google.Drive.Scopes;
using uDrive.Core.Helpers;

namespace uDrive.Auth
{
    public partial class OAuth : Page
    {
        /// <summary>
        /// Gets the current action from the query string.
        /// </summary>
        public string Action
        {
            get { return Request.QueryString["do"]; }
        }

        /// <summary>
        /// Gets the OAuth authorization code from the query string.
        /// </summary>
        public string Code
        {
            get { return Request.QueryString["code"]; }
        }

        /// <summary>
        /// Gets the OAuth error message from the query string.
        /// </summary>
        public new string Error
        {
            get { return Request.QueryString["error"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GoogleOAuthClient client = new GoogleOAuthClient()
            {
                ClientId = ConfigurationManager.AppSettings["ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["ClientSecret"],
                RedirectUri = "http://udriveauth.azurewebsites.net"
            };

            var scope = new[]
            {
                GoogleScopes.OpenId,
                GoogleScopes.Email,
                GoogleScopes.Profile,
                DriveScopes.Default
            };

            if (Action == "login")
            {
                // Where should the user be redirected to after a successful login?
                string redirect = (Request.QueryString["clientcallback"] ?? "default");

                // Get the state from the query string
                string state = Request.QueryString["clientstate"];

                // Get the session value
                string session = Session["uDrive_" + state] as string;

                // If the session value is null, the session has most likely expired
                if (session == null)
                {
                    Session.Add("uDrive_" + state, "");
                    //Content.Text += "<div class=\"error\">Session expired?</div>";
                    //return;
                }

                Session["uDrive_" + state] = redirect;

                // Generate the authorization URL (and make sure to request offline access)
                string url = client.GetAuthorizationUrl(state, scope, true);

                // Redirect the user
                Response.Redirect(url);
            }
            else if (Error != null)
            {

                // Get the state from the query string
                string state = Request.QueryString["state"];

                // Remove the session
                if (state != null) Session.Remove("uDrive_" + state);

                // Print out the error
                Content.Text += "<div class=\"error\">" + Request.QueryString["error"] + "</div>";

            }
            else if (Code != null)
            {

                // Get the state from the query string
                string state = Request.QueryString["session_state"];

                if (state == null)
                {
                    Content.Text += "<div class=\"error\">No <strong>state</strong> specified in the query string.</div>";
                    return;
                }

                // Get the session value
                string session = Session["uDrive_" + state] as string;

                // If the session value is null, the session has most likely expired
                if (session == null)
                {
                    Content.Text += "<div class=\"error\">Session expired?</div>";
                    //return;
                }

                // After a successful login, the user is redirected back to this page, and an
                // authorization code is specified as part of the query string. This authorization
                // code can be used to acquire an access token (which has a lifetime of an hour),
                // and since we requsted offline access, we also get a refresh token that can be
                // used to acquire new access tokens.
                var info = client.GetAccessTokenFromAuthorizationCode(Code);

                // If we previously have received a refresh token, and then try to autenticate the
                // user again, the refresh token in the new response will be empty. Therefore the
                // user must deauthenticate our application before continueing.
                if (String.IsNullOrWhiteSpace(info.Body.RefreshToken))
                {
                    Content.Text += (
                        "<div class=\"error\">\n" +
                            "No refresh token specified in response from the Google API. If you\n" +
                            "have authenticated with this app before, try deauthorizing it HERE,\n" +
                            "and then try again\n" +
                        "</div>"
                    );
                    return;
                }

                // We're now ready to initialize an instance of the GoogleService class
                GoogleService service = GoogleService.CreateFromRefreshToken(client.ClientId, client.ClientSecret, info.Body.RefreshToken);

                // Get all accounts we have access to
                var userInfo = service.Client.GetUserInfo();
                var drive = service.Drive.Files.GetFiles();
                

                // Write information to the user
                Content.Text += "<p><b>User Info</b>" + userInfo + "</p>\n";
                Content.Text = "<div class=\"error\">" + (drive.Body.Files.Length == 0 ? "Noes! Seems you don't have access to any accounts." : "Yay! You have access to <b>" + drive.Body.Files.Length + "</b> accounts.") + "</div>";
                Content.Text += "<p><b>Access Token</b> " + info.Body.AccessToken + "</p>\n";
                Content.Text += "<p><b>Refresh Token</b> " + (String.IsNullOrWhiteSpace(info.Body.RefreshToken) ? "<em>N/A</em>" : info.Body.RefreshToken) + "</p>\n";

                //UDriveConfig.RefreshToken = info.Body.RefreshToken;
            }
            else
            {
                Content.Text += "<a href=\"?do=login\" class=\"button\">Login with <b>Google Accounts</b> using <b>OAuth 2</b></a>";
            }
        }
    }
}