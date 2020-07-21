using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using AppTesteBinding.Utils;
using AppTesteBinding.View;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Login), typeof(AppTesteBinding.Droid.FBLoginPageRenderer))]
namespace AppTesteBinding.Droid
{
    class FBLoginPageRenderer : PageRenderer
    {
        public FBLoginPageRenderer(Context context) : base(context)
        {
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "663340980776115", // your OAuth2 client id
                scope: "", // the scopes for the particular API you're accessing, delimited by "+" symbols
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());

                    Settings.Senha = obj["id"].ToString().Replace("\"", "");
                    Settings.Usuario = obj["name"].ToString().Replace("\"", "");
                    Settings.Logado = true;
                    Settings.Facebook = true;

                    Analytics.TrackEvent("Registro", new Dictionary<string, string>
                    {
                        {"Logado", "Facebook"}
                    });

                    await App.NavigateToProfile();
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}