using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NSFWpics.Interfaces;
using Renci.SshNet;

namespace NSFWpics.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Main'
	public class Main : Credentials, IMain
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Main'
	{
		/// <summary>
		/// Returns whole 'site' module. Each site has 10 view entries
		/// </summary>
		/// <param name="_id">Which site's entries to get</param>
		/// <param name="_images">Returns class based on DB table architecture</param>
		/// <returns>
		/// List of 10 Image entities representing given id's site module
		/// </returns>
		public List<Image> GetAll(int _id, List<Image> _images)
		{
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;

			if (_id == 0 || _id == 1)
			{
				cmd = new MySqlCommand($"SELECT * " +
					$"FROM imgs " +
					$"WHERE id " +
					$"ORDER BY id DESC " +
					$"LIMIT 10;", conn);
			}
			else
			{
				cmd = new MySqlCommand($"SELECT * " +
					$"FROM imgs " +
					$"WHERE id " +
					$"ORDER BY id DESC " +
					$"LIMIT 10 OFFSET {_id * 10 - 10};", conn);
			}
			conn.Open();
			var reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				_images.Add(new Image
				{
					Id = int.Parse(reader["id"].ToString()),
					Uri = reader["uri"].ToString(),
					Author = reader["author"].ToString(),
					Points = int.Parse(reader["points"].ToString()),
					Date = reader["date"].ToString()
				});
			}
			conn.Close();
			return _images;
		}

		/// <summary>
		/// Returns whole 'site' module, but only image formats. Each site has 10 view entries
		/// </summary>
		/// <param name="_id">Which site's entries to get</param>
		/// <param name="_images">Returns class based on DB table architecture</param>
		/// <returns>
		/// List of 10 Image entities representing given id's site module
		/// </returns>
		public List<Image> GetImages(int _id, List<Image> _images)
		{
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;

			if (_id == 0 || _id == 1)
			{
				cmd = new MySqlCommand($"SELECT * " +
					$"FROM imgs " +
					$"WHERE uri " +
					$"LIKE '%.png' " +
					$"OR uri LIKE '%.jpg' " +
					$"OR uri LIKE '%.jpeg' " +
					$"AND id " +
					$"ORDER BY id DESC " +
					$"LIMIT 10;", conn);
			}
			else
			{
				cmd = new MySqlCommand($"SELECT * " +
					$"FROM imgs " +
					$"WHERE uri " +
					$"LIKE '%.png' " +
					$"OR uri LIKE '%.jpg' " +
					$"OR uri LIKE '%.jpeg' " +
					$"AND id " +
					$"ORDER BY id DESC " +
					$"LIMIT 10 OFFSET {_id * 10 - 10};", conn);
			}
			conn.Open();
			var reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				_images.Add(new Image
				{
					Id = int.Parse(reader["id"].ToString()),
					Uri = reader["uri"].ToString(),
					Author = reader["author"].ToString(),
					Points = int.Parse(reader["points"].ToString()),
					Date = reader["date"].ToString()
				});
			}

			conn.Close();
			return _images;
		}

		/// <summary>
		/// Returns whole 'site' module but animated formats only. Each site has 10 view entries
		/// </summary>
		/// <param name="_id">Which site's entries to get</param>
		/// <param name="_images">Returns class based on DB table architecture</param>
		/// <returns>
		/// List of 10 videos entities representing given id's site module
		/// </returns>
		public List<Image> GetVideos(int _id, List<Image> _images)
		{
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;

			if (_id == 0 || _id == 1)
			{
				cmd = new MySqlCommand($"SELECT * " +
					$"FROM imgs " +
					$"WHERE uri " +
					$"LIKE '%.webm' " +
					$"OR uri LIKE '%.gif' " +
					$"OR uri LIKE '%.mp4' " +
					$"AND id " +
					$"ORDER BY id DESC " +
					$"LIMIT 10;", conn);
			}
			else
			{
				cmd = new MySqlCommand($"SELECT * " +
					$"FROM imgs " +
					$"WHERE uri " +
					$"LIKE '%.webm' " +
					$"OR uri LIKE '%.gif' " +
					$"OR uri LIKE '%.mp4' " +
					$"AND id " +
					$"ORDER BY id DESC " +
					$"LIMIT 10 OFFSET {_id * 10 - 10};", conn);
			}
			conn.Open();
			var reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				_images.Add(new Image
				{
					Id = int.Parse(reader["id"].ToString()),
					Uri = reader["uri"].ToString(),
					Author = reader["author"].ToString(),
					Points = int.Parse(reader["points"].ToString()),
					Date = reader["date"].ToString()
				});
			}
			conn.Close();
			return _images;
		}

		/// <summary>
		/// Removes content with given ID and extension
		/// </summary>
		/// <param name="_id">ID of content entry to remove</param>
		/// <param name="_extension">Content's file extension in ?extension=png/jpeg/webm... format </param>
		public void Remove(int _id, string _extension)
		{
			var conn = new MySqlConnection(Connection.ToString());
			MySqlCommand cmd;

			using (var client = SftpConnection)
			{
				client.Connect();
				client.ChangeDirectory($"/var/www/html/img");
				client.DeleteFile($"{_id}.{_extension}");
				client.Disconnect();
				client.Dispose();
			}

			cmd = new MySqlCommand($"DELETE FROM imgs " +
				$"WHERE id = {_id};", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();

			int MaxId = new Tools().MaxId(0);
			cmd = new MySqlCommand($"ALTER TABLE imgs " +
				$"AUTO_INCREMENT = {MaxId}", conn);
			conn.Open();
			cmd.ExecuteNonQuery();
			conn.Close();
		}
	}
}
