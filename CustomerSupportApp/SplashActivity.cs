using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;

namespace CustomerSupportApp
{
    [Activity(Label = "CustomerSupportApp", MainLauncher = true, Theme = "@style/Theme.Splash")]
    public class SplashActivity : Activity
    {  
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Thread.Sleep(1000);
            StartActivity(typeof(MainActivity));
        }

        public override void OnBackPressed() { }

        
    }
}