using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NSFWpics.Interfaces;
using Renci.SshNet;

namespace NSFWpics.Models
{
	/// <summary>
	/// Database tools class, constains: maxId, plus, minus and image promotion
	/// </summary>
	public class Tools : Credentials, ITools
	{
		/// <summary>
		/// Gets last ID entrance from DB (0 = mine, 1 = mine images, 2 = mine videos, 3 = queue)
		/// </summary>
		/// <returns>
		/// Returns highest ID from DB
		/// </returns>
		public int MaxId(int _siteType)
		{
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;
			var Id = 0;
			if (_siteType == 0)
			{
				cmd = new MySqlCommand($"SELECT MAX(id) " +
					$"FROM imgs;", conn);
				conn.Open();
				MySqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Id = int.Parse(reader["max(id)"].ToString());
				}
				int MaxId = Id;

				conn.Close();
				return MaxId;
			}
			else if (_siteType == 1)
			{
				cmd = new MySqlCommand($"SELECT MAX(id) " +
					$"FROM imgs WHERE uri LIKE '%.png' OR uri LIKE '%.jpg' OR uri LIKE '%.jpeg';", conn);
				conn.Open();
				MySqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Id = int.Parse(reader["max(id)"].ToString());
				}
				int MaxId = Id;

				conn.Close();
				return MaxId;
			}
			else if (_siteType == 2)
			{
				cmd = new MySqlCommand($"SELECT MAX(id) " +
					$"FROM imgs WHERE uri LIKE '%.webm' OR uri LIKE '%.gif' OR uri LIKE '%.mp4';", conn);
				conn.Open();
				MySqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Id = int.Parse(reader["max(id)"].ToString());
				}
				int MaxId = Id;

				conn.Close();
				return MaxId;
			}
			else if (_siteType == 3)
			{
				cmd = new MySqlCommand($"SELECT MAX(id)  " +
					$"FROM queue;", conn);
				conn.Open();
				MySqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					Id = int.Parse(reader["max(id)"].ToString());
				}
				int MaxId = Id;

				conn.Close();
				return MaxId;
			}
			return 0;
		}

		/// <summary>
		/// Decrement image's point rating by one
		/// </summary>
		/// <param name="_id">Number for identifying image in DB</param>
		public void Minus(int _id)
		{
			_id = Convert.ToUInt16(_id);
			var conn = new MySqlConnection(Connection.ToString());
			var cmd = new MySqlCommand($"UPDATE imgs " +
				$"SET points = points - 1 " +
				$"WHERE id = {_id};", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
		}

		/// <summary>
		/// Increment image's point rating by one
		/// </summary>
		/// <param name="_id">Number for identifying image in DB</param>
		public void Plus(int _id)
		{
			_id = Convert.ToUInt16(_id);
			var conn = new MySqlConnection(Connection.ToString());
			var cmd = new MySqlCommand($"UPDATE imgs " +
				$"SET points = points + 1 " +
				$"WHERE id = {_id};", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
		}

		/// <summary>
		/// Promotes image from queue to main page
		/// </summary>
		/// <param name="_id">Id of promoted image in queue, may be null for max value</param>
		public void PromoteImage(int? _id)
		{
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;

			if (_id == null)
			{
				cmd = new MySqlCommand($"SELECT * " +
					$"FROM queue " +
					$"WHERE id = {this.MaxId(3)}", conn);

				conn.Open();
				MySqlDataReader reader = cmd.ExecuteReader();
				Image promotedImg = null;

				while (reader.Read())
				{
					promotedImg = new Image
					{
						Id = int.Parse(reader["id"].ToString()),
						Uri = reader["uri"].ToString(),
						Author = reader["author"].ToString(),
						Points = int.Parse(reader["points"].ToString()),
						Date = reader["date"].ToString()
					};
				}
				conn.Close();

				try
				{
					using (var client = SftpConnection)
					{
						client.Connect();
						client.ChangeDirectory($"/var/www/html/img_queue");
						var f = client.Get($"{MaxId(3)}{Path.GetExtension(promotedImg.Uri)}");
						f.MoveTo($"/var/www/html/img/{MaxId(0) + 1}{Path.GetExtension(promotedImg.Uri)}");
						client.Disconnect();
						client.Dispose();
					}
				}
				catch (Exception ex)
				{
					throw new Exception($"Failed to move. File:  {promotedImg.Author} {promotedImg.Id} {promotedImg.Uri} MaxID {MaxId(3)} {ex}");
				}

				cmd = new MySqlCommand($"INSERT INTO imgs(uri,author,points) " +
					$"VALUES('https://cdn.nsfwpics.pw/img/{(MaxId(0) + 1)}{Path.GetExtension(promotedImg.Uri)}','{promotedImg.Author}',{promotedImg.Points})", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();

				cmd = new MySqlCommand($"DELETE FROM queue " +
					$"WHERE id = {promotedImg.Id};", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();

				cmd = new MySqlCommand($"ALTER TABLE queue " +
				$"AUTO_INCREMENT = {MaxId(3)}", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();
			}
			else
			{
				cmd = new MySqlCommand($"SELECT * " +
					$"FROM queue " +
					$"WHERE id = {_id}", conn);

				conn.Open();
				MySqlDataReader reader = cmd.ExecuteReader();
				Image promotedImg = null;

				while (reader.Read())
				{
					promotedImg = new Image
					{
						Id = int.Parse(reader["id"].ToString()),
						Uri = reader["uri"].ToString(),
						Author = reader["author"].ToString(),
						Points = int.Parse(reader["points"].ToString()),
						Date = reader["date"].ToString()
					};
				}
				conn.Close();

				using (var client = SftpConnection)
				{
					client.Connect();
					client.ChangeDirectory($"/var/www/html/img_queue");
					client.Get($"{promotedImg.Id}{Path.GetExtension(promotedImg.Uri)}").MoveTo($"/var/www/html/img/{MaxId(0) + 1}{Path.GetExtension(promotedImg.Uri)}");
					client.Disconnect();
					client.Dispose();
				}

				cmd = new MySqlCommand($"INSERT INTO imgs(uri,author,points) " +
					$"VALUES('https://cdn.nsfwpics.pw/img/{(MaxId(0) + 1)}{Path.GetExtension(promotedImg.Uri)}','{promotedImg.Author}',{promotedImg.Points})", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();

				cmd = new MySqlCommand($"DELETE FROM queue " +
					$"WHERE id = {promotedImg.Id};", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();

				cmd = new MySqlCommand($"ALTER TABLE queue " +
				$"AUTO_INCREMENT = {MaxId(3)}", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();
			}
		}
	}
}
