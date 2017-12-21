using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Views;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using CustomerSupportApp.Models;

namespace CustomerSupportApp
{
    [Activity(Label = "CustomerSupportApp", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var result = String.Empty;
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);
            var loginButton = FindViewById<Button>(Resource.Id.loginbtn);
            loginButton.Click += (object sender, EventArgs e) =>
            {
                redirectToNotifications();
                //Validations();
            };
        }

        public void Validations()
        {
            string email = FindViewById<EditText>(Resource.Id.email).Text;
            string password = FindViewById<EditText>(Resource.Id.password).Text;
            bool processForm = true;
            if (string.IsNullOrEmpty(email))
            {
                this.ShowMessage(Resource.Id.errorEmailLogin, "Please enter the email ID.");
                processForm = false;
            }
            else
            {
                this.ShowMessage(Resource.Id.errorEmailLogin, string.Empty);
            }
            if (string.IsNullOrEmpty(password))
            {
                this.ShowMessage(Resource.Id.errorPasswordLogin, "Please enter the password.");
                processForm = false;
            }
            else
            {
                this.ShowMessage(Resource.Id.errorPasswordLogin, string.Empty);
            }
            if(processForm)
            {
                GetUserDetails();
            }
            
        }

        private void ShowMessage(int controlId, string message)
        {
            FindViewById<TextView>(controlId).Text = message;
            FindViewById<TextView>(controlId).Visibility = string.IsNullOrEmpty(message) ? ViewStates.Invisible : ViewStates.Visible;
        }

        private void redirectToNotifications()
        {  
                Intent intent = new Intent(this, typeof(NotificationActivity));
                this.StartActivity(intent);
                this.OverridePendingTransition(Android.Resource.Animation.SlideInLeft, Android.Resource.Animation.SlideOutRight);
                this.Finish();
        }

        public void GetUserDetails()
        {
            try
            {
                string password = FindViewById<EditText>(Resource.Id.password).Text;
                string email = FindViewById<EditText>(Resource.Id.email).Text;            
                var client = new HttpClient();
                var parameters = new Dictionary<string, string> { { "email", email }, { "password", password } };
                var dataSent = new FormUrlEncodedContent(parameters);
                var data = client.PostAsync(Resources.GetString(Resource.String.loginURL), dataSent).Result;
                var result = string.Empty;
                UserViewModel user = new UserViewModel();
                if (data.IsSuccessStatusCode)
                {
                    result = data.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<UserViewModel>(result);
                    if (user != null && user.success)
                    {
                        redirectToNotifications();
                    }
                }
                this.ShowMessage(Resource.Id.loginErrorMsg, user.message);
            }
            catch (Exception ex)
            {
                this.ShowMessage(Resource.Id.loginErrorMsg, "Unknown Error, Please Try Again.");
            }
        }
    }
}

