﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppRun.ViewModel
{
    public class GoogleUser
    {
        public string id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
    public interface IGoogleManager
    {
        void Login(Action<GoogleUser, string> OnLoginComplete);

        void Logout();

       

    }
}
