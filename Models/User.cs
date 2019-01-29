using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using NSFWpics.Interfaces;

namespace NSFWpics.Models
{
	public class User : Credentials , IUser
	{
		public User(string uid, string login, string password, string mail)
		{
			Uid = uid;
			Guid = "1";
			Login = login;
			Password = password;
			Mail = mail;
			Uploads = 0;
		}

		public User()
		{
			Uid = null;
			Guid = "1";
			Login = null;
			Password = null;
			Mail = null;
			Uploads = 0;
		}

		public string Uid { get; set; }
        public string Guid { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
		public int Uploads { get; set; }


		public User GetUser(int _uid)
		{
			throw new NotImplementedException();
		}

		public List<User> GetUsers()
		{
			throw new NotImplementedException();
		}

		public string RegisterUser(string _login, string _password, string _mail)
		{
			throw new NotImplementedException();
		}

		public string LoginUser(string _login, string _password)
		{
			throw new NotImplementedException();
		}

		public string UpdateUser(int _uid, string _changeParam, string _changeValue)
		{
			throw new NotImplementedException();
		}

		public string DeleteUser(int _uid)
		{
			throw new NotImplementedException();
		}
	}
}