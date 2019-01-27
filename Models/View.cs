using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NSFWpics.Interfaces;

namespace NSFWpics.Models
{
	/// <summary>
	/// Returns image with given id
	/// </summary>
	public class View : Credentials, ISingleImage
	{
		/// <summary>
		/// Returns image with given id
		/// </summary>
		/// <param name="_id">Number for identifying image in DB</param>
		/// <param name="_image">Returns class based on DB table architecture</param>
		/// <returns>
		/// Returns Image module from DB with given Image's id
		/// </returns>
		public Image GetEntry(int? _id, Image _image)
		{
			var conn = new MySqlConnection(Connection.ToString());
			var cmd = new MySqlCommand($"SELECT * FROM imgs WHERE id={_id}", conn);
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
