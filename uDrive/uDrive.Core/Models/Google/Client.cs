using System;
using System.Collections.Specialized;
using System.Web;

namespace uDrive.Core.Models.Google
{
    public class Client
    {
        public string ClientId { get; set; }
        public string ClientIdFull
        {
            get
            {
                if (String.IsNullOrWhiteSpace(ClientId)) return null;
                return ClientId.EndsWith(".apps.googleusercontent.com") ? ClientId : ClientId + ".apps.googleusercontent.com";
            }
        }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }

        public string GetAuthorisationUrl(string state, string scope, bool offline = false)
        {
            return GenerateUrl("https://accounts.google.com/o/oauth2/auth", new NameValueCollection
            {
                {"response_type", "code"},
                {"client_id", ClientIdFull},
                {"access_type", offline ? "offline" : "online"},
                {"scope", scope},
                {"redirect_uri", RedirectUri},
                {"state", state}
            });
        }

        public string GenerateUrl(string url, NameValueCollection query)
        {
            return url + (query.Count == 0 ? "" : "?" + NameValueCollectionToQueryString(query));
        }

        public static string NameValueCollectionToQueryString(NameValueCollection collection)
        {
            return String.Join("&", Array.ConvertAll(collection.AllKeys, key => HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(collection[key])));
        }
    }
}
