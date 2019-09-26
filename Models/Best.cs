using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NSFWpics.Interfaces;

namespace NSFWpics.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Best'
	public class Best :  Credentials, ISingleImage
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Best'
	{
#pragma warning disable CS1572 // XML comment has a param tag for 'image', but there is no parameter by that name
#pragma warning disable CS1573 // Parameter '_id' has no matching param tag in the XML comment for 'Best.GetEntry(int?, Image)' (but other parameters do)
#pragma warning disable CS1573 // Parameter '_image' has no matching param tag in the XML comment for 'Best.GetEntry(int?, Image)' (but other parameters do)
		/// <summary>
		/// Picks image with highest point rating
		/// </summary>
		/// <param name="image">Returns class based on DB table architecture</param>
		/// <returns>
		/// Returns Image module from DB where id's highest
		/// </returns>
		public Image GetEntry(int? _id, Image _image)
#pragma warning restore CS1573 // Parameter '_image' has no matching param tag in the XML comment for 'Best.GetEntry(int?, Image)' (but other parameters do)
#pragma warning restore CS1573 // Parameter '_id' has no matching param tag in the XML comment for 'Best.GetEntry(int?, Image)' (but other parameters do)
#pragma warning restore CS1572 // XML comment has a param tag for 'image', but there is no parameter by that name
		{
			var conn = new MySqlConnection(base.Connection.ToString());
			MySqlCommand cmd;

			cmd = new MySqlCommand($"SELECT id,uri,author,date,points " +
				$"FROM imgs " +
				$"WHERE points=(SELECT MAX(points) FROM imgs);", conn);
			conn.Open();

			MySqlDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				return _image = new Image
				{
					Id = int.Parse(reader["id"].ToString()),
					Uri = reader["uri"].ToString(),
					Author = reader["author"].ToString(),
					Points = int.Parse(reader["points"].ToString()),
					Date = reader["date"].ToString()
				};
			}
			conn.Close();
			return _image;
		}
	}
}
