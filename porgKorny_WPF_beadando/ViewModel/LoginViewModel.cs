﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace porgKorny_WPF_beadando.ViewModel
{
    class LoginViewModel
    {

		private string password;

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		private string username;

		public string Username
		{
			get { return username; }
			set { username = value; }
		}


		public LoginViewModel()
        {
            
        }

    }
}
