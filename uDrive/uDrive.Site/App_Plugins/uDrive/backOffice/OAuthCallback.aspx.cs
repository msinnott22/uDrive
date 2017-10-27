using System;
using System.Linq;
using Skybrud.Social.Google.Common;
using Skybrud.Social.Google.Common.Models.Authentication;
using Skybrud.Social.Google.Common.OAuth;
using Skybrud.Social.Google.Common.Responses;
using Skybrud.Social.Google.Common.Responses.Authentication;
using uDrive.Core.Helpers;
using umbraco;
using umbraco.BusinessLogic;
using Umbraco.Web;
using Umbraco.Web.UI.Pages;

namespace uDrive.Core.App_Plugins.uDrive.backOffice
{

    public partial class OAuthCallback : UmbracoEnsuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get current user
            var currentUser = UmbracoContext.Current.Security.CurrentUser;

            //Check a user is logged into backoffice
            if (currentUser == null)
            {
                //Ouput an error message
                Content.Text += ui.Text("udrive", "noAccess");
                return;
            }

            //Check the user has access to the analytics section
            //Prevents anyone with this URL to page from just hitting it
            if (!currentUser.AllowedSections.Contains("udrive"))
            {
                //Ouput an error message
                Content.Text += ui.Text("udrive", "noAccess");
                return;
            }

            // Get the state from the query string
            string state = Request.QueryString["state"];

            if (state == null)
            {
                Content.Text += "<div class=\"error\">No <strong>state</strong> specified in the query string.</div>";
                //return;
            }

            // Get the session value
            string session = Session["uDrive_" + state] as string;

            // If the session value is null, the session has most likely expired
            if (session == null)
            {
                Content.Text += "<div class=\"error\">Session expired?</div>";
                //return;
            }

            // Get the refresh token from the query string (kinda bad practice though)
            string refreshToken = Request.QueryString["token"];

            // Do we have a refresh token?
            if (String.IsNullOrWhiteSpace(refreshToken))
            {
                //Ouput an error message
                Content.Text += ui.Text("udrive", "somethingWentWrong");
                return;
            }

            GoogleOAuthClient client = new GoogleOAuthClient
            {
                ClientId = UDriveConfig.ClientId,
                ClientSecret = UDriveConfig.ClientSecret
            };

            GoogleTokenResponse response = client.GetAccessTokenFromRefreshToken(refreshToken);
            GoogleService service = GoogleService.CreateFromAccessToken(response.Body.AccessToken);

            try
            {
                //Get the authenticated user
                GoogleGetUserInfoResponse user = service.GetUserInfo();

                //Set the refresh token in our config
                UDriveConfig.RefreshToken = refreshToken;

                //Ouput some info about the user
                //Using UmbracoUser (obsolete) - somehow it fails to compile when using Security.CurrentUser
                //ui.text requires OLD BusinessLogic User object type not shiny new one
                //Can we use another helper/library to get the translation text?
                var umbUser = new User(currentUser.Id);
                Content.Text = ui.Text("udrive", "informationSavedMessage", user.Body.Name, umbUser);
            }
            catch
            {
                //Ouput an error message
                Content.Text += ui.Text("udrive", "somethingWentWrong");
            }

            // Clear the session state
            Session.Remove("uDrive_" + state);
        }
    }

}