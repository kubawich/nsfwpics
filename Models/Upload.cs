using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NSFWpics.Interfaces;
using Renci.SshNet;

namespace NSFWpics.Models
{
	 public  class Upload : Credentials, IUploads
	{
		public static Upload instance = new Upload();
		/// <summary>
		/// Adds cdn's uploaded photo to DB
		/// </summary>
		/// <param name="_maxIdPlusOne">Required to increment photo's id in DB</param>l        
		/// <param name="_file">File to upload</param>
		/// <param name="_login">File to upload</param>
		public int UploadToMain(int _maxIdPlusOne, IFormFile _file, string _login)
		{
			using (var client = SftpConnection)
			{
				if (_file != null)
				{
					client.Connect();
					client.ChangeDirectory("/var/www/html/img");
					using (var fs = new FileStream(Path.GetFileName(_file.FileName), FileMode.Create))
					{
						client.UploadFile(_file.OpenReadStream(),
							$"{_maxIdPlusOne}{Path.GetExtension(_file.FileName)}");
					}
				}
				client.Disconnect();
				client.Dispose();
			}
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;
			cmd = new MySqlCommand($"INSERT INTO imgs(uri,author,points) " +
				$"VALUES('https://cdn.nsfwpics.pw/img/{_maxIdPlusOne}{Path.GetExtension(_file.FileName)}','{_login}',0)", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			cmd = new MySqlCommand($"UPDATE users " +
				$"SET uploads = uploads + 1  " +
				$"WHERE login = '{_login}'", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			return 1;
		}

		/// <summary>
		/// Adds cdn's uploaded photo to page's queue
		/// </summary>
		/// <param name="_maxIdPlusOne">Required to increment photo's id in DB</param>       
		/// <param name="_file">File to upload</param>
		/// <param name="_login">User who becomes owner of this picture</param>
		public int UploadToQueue(int _maxIdPlusOne, IFormFile _file, string _login)
		{
			using (var client = SftpConnection)
			{
				if (_file != null)
				{
					client.Connect();
					client.ChangeDirectory("/var/www/html/img_queue");
					using (var fs = new FileStream(Path.GetFileName(_file.FileName), FileMode.Create))
					{
						client.UploadFile(_file.OpenReadStream(),
							$"{_maxIdPlusOne}{Path.GetExtension(_file.FileName)}");
					}
				}
				client.Disconnect();
				client.Dispose();
			}
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;
			cmd = new MySqlCommand($"INSERT INTO queue(uri,author,points) " +
				$"VALUES('https://cdn.nsfwpics.pw/img_queue/{_maxIdPlusOne}{Path.GetExtension(_file.FileName)}','{_login}',0)", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			cmd = new MySqlCommand($"UPDATE users " +
				$"SET uploads = uploads + 1  " +
				$"WHERE login = '{_login}'", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
			File.Delete(Directory.GetFiles(Directory.GetCurrentDirectory(), _file.FileName)[0]);
			_file = null;

			return 1;
		}
	}
}
