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

namespace CustomerSupportApp.Models
{
    class UserViewModel
    {
        public User user { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public class User
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string user_role_id { get; set; }
            public string id { get; set; }
        }
    }
}