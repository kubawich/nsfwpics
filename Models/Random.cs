using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NSFWpics.Interfaces;

namespace NSFWpics.Models
{
	public class Random : Credentials, ISingleImage
	{
		/// <summary>
		/// Picks number of indicies in database, and next pick random photo from first to maximum.
		/// </summary>
		/// <param name="image">Returns class based on DB table architecture</param>
		/// <returns>
		/// Image class represtantion filled with random DB entry
		/// </returns>
		public Image GetEntry(int? _id, Image _image)
		{
			var rand = new System.Random();
			int MaxId = new Tools().MaxId(0);
			int i = rand.Next(9, MaxId + 1);

			var conn = new MySqlConnection(Connection.ToString());
			var cmd = new MySqlCommand($"SELECT uri, author, date, id, points FROM imgs WHERE id={i}", conn);
			conn.Open();

			MySqlDataReader reader2 = cmd.ExecuteReader();

			while (reader2.Read())
			{
				return _image = new Image
				{
					Id = int.Parse(reader2["id"].ToString()),
					Uri = reader2["uri"].ToString(),
					Author = reader2["author"].ToString(),
					Points = int.Parse(reader2["points"].ToString()),
					Date = reader2["date"].ToString()
				};
			}
			conn.Close();
			return _image;
		}
	}
}
