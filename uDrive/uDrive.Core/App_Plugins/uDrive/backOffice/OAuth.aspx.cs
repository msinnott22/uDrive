using System;
using System.Collections.Specialized;
using System.Linq;
using uDrive.Core.Models.Google;
using umbraco;
using Umbraco.Web.UI.Pages;

namespace uDrive.Core.App_Plugins.uDrive.backOffice
{
    public partial class OAuth : UmbracoEnsuredPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get current user
            var currentUser = Security.CurrentUser;

            if (currentUser == null)
            {
                Content.Text += ui.Text("udrive", "noAccess");
                return;
            }

            if (!currentUser.AllowedSections.Contains("udrive"))
            {
                Content.Text += ui.Text("udrive", "noAccess");
                return;
            }

            var callback = "http://udriveauth.azurewebsites.net/OAuth.aspx";
            var state = Guid.NewGuid().ToString();

            Session.Add("uDrive_" + state, "#h5yr");

            var query = new NameValueCollection
            {
                {"do", "login"},
                {"clientcallback", callback},
                {"clientstate", state},
                {"lang", currentUser.Language}
            };

            var oAuthUrl = "http://udriveauth.azurewebsites.net/OAuth.aspx?" + Client.NameValueCollectionToQueryString(query);

            Response.Redirect(oAuthUrl);
        }
    }
}