using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using NSFWpics.Interfaces;

namespace NSFWpics.Models
{
	public class User : Credentials , IUser
	{
		public string Uid		{ get; internal set; }
		public string Guid		{ get; internal set; }
		public string Login		{ get; internal set; }
		public string Password	{ get; internal set; }
		public string Mail		{ get; internal set; }
		public string Uploads	{ get; internal set; }

		/// <summary>
		/// Ctor with default null values
		/// </summary>
		public User()
		{
			Uid = null;
			Guid = "1";
			Login = null;
			Password = null;
			Mail = null;
			Uploads = 0.ToString();
		}

		/// <summary>
		/// ctor with already defined minimal values
		/// </summary>
		/// <param name="uid">User id</param>
		/// <param name="login">Login</param>
		/// <param name="password">Password</param>
		/// <param name="mail">E-Mail</param>
		public User(string uid, string login, string password, string mail)
		{
			Uid = uid;
			Guid = "1";
			Login = login;
			Password = password;
			Mail = mail;
			Uploads = 0.ToString();
		}

		/// <summary>
		/// Retrives individual user from database
		/// </summary>
		/// <param name="_uid">User id to pick from DB</param>
		/// <returns>Dictionary of fields and it's values</returns>
		public Dictionary<string, string> GetUser(int _uid)
		{
			User user = null;
			var conn = new MySqlConnection(Connection.ToString());
			var cmd = new MySqlCommand($"SELECT * FROM users WHERE uid = {_uid}", conn);
			conn.Open();
			MySqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				user = new User()
				{
					Guid = reader["guid"].ToString(),
					Uid = reader["uid"].ToString(),
					Login = reader["login"].ToString(),
					Password = reader["password"].ToString(),
					Mail = reader["mail"].ToString(),
					Uploads = reader["uploads"].ToString()
				};
			}
			conn.Close();
			return new Dictionary<string, string> {
				{"GUID", user.Guid},
				{"UID", user.Uid},
				{"Login", user.Login},
				{"Pass", user.Password},
				{"Mail", user.Mail},
				{"Uploads", user.Uploads}
			};
		}

		/// <summary>
		/// Retrieves all users from database
		/// </summary>
		/// <returns>List of dictionaries of individual users, cuz it looks good as json</returns>
		public List<Dictionary<string, string>> GetUsers()
		{
			var conn = new MySqlConnection(Connection.ToString());
			MySqlDataReader reader = null;

			var cmd = new MySqlCommand($"SELECT * FROM users", conn);
			conn.Open();
			reader = cmd.ExecuteReader();
			var users = new List<Dictionary<string, string>>();

			while (reader.Read())
			{
				users.Add(new Dictionary<string, string>()
				{				
					{ "GUID", reader["guid"].ToString()},
					{ "UID", reader["uid"].ToString() },
					{ "Login", reader["login"].ToString()},
					{ "Pass", reader["password"].ToString()},
					{ "Mail", reader["mail"].ToString()},
					{ "Uploads",  reader["uploads"].ToString()}
				});
			}
			conn.Close();			
			return users;
		}

		/// <summary>
		/// Registers new user to app
		/// </summary>
		/// <param name="_login">Login</param>
		/// <param name="_password">Password</param>
		/// <param name="_mail">E-Mail</param>
		/// <param name="_ip">user ip address</param>
		/// <returns>String status with action completion</returns>
		public string RegisterUser(string _login, string _password, string _mail, string _ip)
		{
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;

			cmd = new MySqlCommand($"SELECT EXISTS(" +
				$"SELECT 1 " +
				$"FROM users " +
				$"WHERE login = '{_login}' " +
				$"LIMIT 1)", conn);
			conn.Open();
			var i = cmd.ExecuteScalar();
			conn.Close();

			if (int.Parse(i.ToString()) == 1)
			{
				return "User already exists";
			}
			else
			{
				cmd = new MySqlCommand($"INSERT INTO users (guid, login, password, mail, ip)" +
					$"VALUES (1, '{_login}', SHA1('{_password}'), '{_mail}', {_ip})", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();
				return $"Successfully Registered user {_login}";
			}
		}

		/// <summary>
		/// Logs in given user in app 
		/// </summary>
		/// <param name="_login">Identifer in DB</param>
		/// <param name="_password">User password</param>
		/// <returns>String with login status success/failure</returns>
		public string LoginUser(string _login, string _password)
		{
			var conn = new MySqlConnection(Connection.ToString());

			var cmd = new MySqlCommand($"SELECT EXISTS(" +
				$"SELECT 1 " +
				$"FROM users " +
				$"WHERE login = '{_login}' AND password = SHA1('{_password}')" +
				$"LIMIT 1)", conn);
			conn.Open();
			var i = cmd.ExecuteScalar();
			conn.Close();

			if (int.Parse(i.ToString()) == 1)
			{
				return "Login success";
			}
			else
			{
				return "Error, either login or password is incorrect";
			}
		}

		/// <summary>
		/// Updates/changes user parameter with given id
		/// </summary>
		/// <param name="_uid">User id to make update on</param>
		/// <param name="_changeParam">Column name to update</param>
		/// <param name="_changeValue">New value for old resource at given column</param>
		/// <returns>String status of operation Updated/Failed</returns>
		public string UpdateUser(int _uid, string _changeParam, string _changeValue)
		{
			var conn = new MySqlConnection(Connection.ToString());
			var cmd = new MySqlCommand($"UPDATE users " +
				$"SET {_changeParam} = '{_changeValue}' " +
				$"WHERE uid = {_uid};", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();

			return $"Updated user {_uid}";
		}

		/// <summary>
		/// Deletes individual user from database
		/// </summary>
		/// <param name="_uid">Id for user to delete account</param>
		/// <returns>Infromation about deletion</returns>
		public string DeleteUser(int _uid)
		{
			try
			{
				var conn = new MySqlConnection(Connection.ToString());
				var cmd = new MySqlCommand($"DELETE FROM users " +
					$"WHERE uid={_uid};", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();

				return $"User {_uid} deleted succesfuly";
			}
			catch (ArgumentNullException)
			{
				throw new Exception("Argument must be non null");
			}
		}
	}
}