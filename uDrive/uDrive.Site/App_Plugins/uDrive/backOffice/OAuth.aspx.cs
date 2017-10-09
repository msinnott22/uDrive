using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using uDrive.Core.Constants;
using uDrive.Core.Helpers;
using Umbraco.Core.Security;
using uDrive.Core.Models.Google;

namespace uDrive.Core.App_Plugins.uDrive.backOffice
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
        /// Gets the OAuth error message from the query string.
        /// </summary>
        public new string Error
        {
            get { return Request.QueryString["error"]; }
        }

        /// <summary>
        /// Gets the OAuth authorization code from the query string.
        /// </summary>
        public string Code
        {
            get { return Request.QueryString["code"]; }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if (PackageConstants.UmbracoVersion != "7.2.2") return;

            // Handle authentication stuff to counteract bug in Umbraco 7.2.2 (see U4-6342)
            HttpContextWrapper http = new HttpContextWrapper(Context);
            FormsAuthenticationTicket ticket = http.GetUmbracoAuthTicket();
            http.AuthenticateCurrentRequest(ticket, true);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Client client = new Client()
            {
                ClientId = UDriveConfig.ClientId,
                ClientSecret = UDriveConfig.ClientSecret,
                RedirectUri = UDriveConfig.RedirectUri
            };

            // When requesting access to the Google API on behalf of a user, we must specify one or
            // more scopes. Most importantly we must request read access to the user's Analytics
            // information.
            string[] scope = new[] {
                "openid",
                "email",
                "profile"
            };

            if (Action == "login")
            {
                string redirect = Request.QueryString["redirect"] ?? "default";

                string state = Guid.NewGuid().ToString();
                Session["uDrive_" + state] = redirect;

                string url = client.GetAuthorisationUrl(state, string.Join(" ", scope), true);

                Response.Redirect("");
            }
            else if (Error != null)
            {
                string state = Request.QueryString["state"];

                if (state != null)
                {
                    Session.Remove("uDrive_" + state);
                }

                Content.Text += "<div class=\"error\">" + Request.QueryString["error"] + "</div>";
            }
            else if (Code != null)
            {
                string state = Request.QueryString["state"];

                if (state == null)
                {
                    Content.Text +=
                        "<div class=\"error\">No <strong>state</strong> specificed in the query string.</div>";
                    return;
                }

                string session = Session["uDrive_" + state] as string;

                if (session == null)
                {
                    Content.Text += "<div class=\"error\">Session Expired?</div>";
                }

                // After a successful login, the user is redirected back to this page, and an
                // authorization code is specified as part of the query string. This authorization
                // code can be used to acquire an access token (which has a lifetime of an hour),
                // and since we requsted offline access, we also get a refresh token that can be
                // used to acquire new access tokens.
                //GoogleAccessTokenResponse info = client.GetAccessTokenFromAuthorizationCode(Code);

                // If we previously have received a refresh token, and then try to autenticate the
                // user again, the refresh token in the new response will be empty. Therefore the
                // user must deauthenticate our application before continueing.
                //if (String.IsNullOrWhiteSpace(info.RefreshToken))
                //{
                //    Content.Text += (
                //        "<div class=\"error\">\n" +
                //        "No refresh token specified in response from the Google API. If you\n" +
                //        "have authenticated with this app before, try deauthorizing it HERE,\n" +
                //        "and then try again\n" +
                //        "</div>"
                //    );
                //    return;
                //}

                // We're now ready to initialize an instance of the GoogleService class
                //GoogleService service = GoogleService.CreateFromRefreshToken(client.ClientId, client.ClientSecret, info.RefreshToken);

                //// Get all accounts we have access to
                //AnalyticsAccountsResponse accounts = service.Analytics.GetAccounts();

                // Write information to the user
                //Content.Text = "<div class=\"error\">" + (accounts.Items.Length == 0 ? "Noes! Seems you don't have access to any accounts." : "Yay! You have access to <b>" + accounts.Items.Length + "</b> accounts.") + "</div>";

                //Content.Text += "<p><b>Access Token</b> " + info.AccessToken + "</p>\n";
                //Content.Text += "<p><b>Refresh Token</b> " +
                //               (String.IsNullOrWhiteSpace(info.RefreshToken) ? "<em>N/A</em>" : info.RefreshToken) +
                //              "</p>\n";

                //UDriveConfig.RefreshToken = info.RefreshToken;
            }
            else
            {
                var nvc = new NameValueCollection
                {
                    {"clientcallback", Request.Url.AbsoluteUri.Replace("/OAuth.aspx", "/OAuthCallback.aspx")},
                    {"clientstate", Guid.NewGuid().ToString()}
                };

                Content.Text += "<a href=\"?do=login&" + Client.NameValueCollectionToQueryString(nvc) + "\" class=\"button\">Login with <b>Google Account</b></a>";
            }
        }
    }
}