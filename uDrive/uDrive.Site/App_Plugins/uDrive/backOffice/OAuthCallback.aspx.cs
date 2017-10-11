using System;
using System.Linq;
using uDrive.Core.Helpers;
using umbraco;
using umbraco.BusinessLogic;
using Umbraco.Web;
using Umbraco.Web.UI.Pages;

namespace uDrive.Core.App_Plugins.uDrive.backOffice {

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

            // Check whether the state is present
            if (String.IsNullOrWhiteSpace(state))
            {
                //Ouput an error message
                Content.Text += ui.Text("udrive", "noStateSpecified");
                return;
            }

            // Get the session value
            string session = Session["uDrive_" + state] as string;

            // Has the session expire?
            if (session == null)
            {
                //Ouput an error message
                Content.Text += ui.Text("udrive", "sorrySessionExpired");
                return;
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

            // Initalize a new instance of the GoogleService class
            //GoogleService service = GoogleService.CreateFromRefreshToken(UDriveConfig.ClientId, UDriveConfig.ClientSecret, refreshToken);

            try {

                //Get the authenticated user
                //GoogleGetUserInfoResponse user = service.GetUserInfo();

                //Set the refresh token in our config
                UDriveConfig.RefreshToken = refreshToken;
                

                //Ouput some info about the user
                //Using UmbracoUser (obsolete) - somehow it fails to compile when using Security.CurrentUser
                //ui.text requires OLD BusinessLogic User object type not shiny new one
                //Can we use another helper/library to get the translation text?
                var umbUser = new User(currentUser.Id);
                Content.Text = ui.Text("udrive", "informationSavedMessage", "Test User", umbUser);
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