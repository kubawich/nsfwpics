using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSFWpics.Models;

namespace NSFWpics.Interfaces
{
	interface IUser
	{
		string UpdateUser(int _uid, string _changeParam, string _changeValue);
		string DeleteUser(int _uid);
		List<Dictionary<string, string>> GetUsers();
		Dictionary<string,string> GetUser(int _uid);
		string RegisterUser(string _login, string _password, string _mail, string _ip);
		string LoginUser(string _login, string _password);
	}
}
