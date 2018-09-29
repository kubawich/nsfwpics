using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using NSFWpics.Pages;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Renci.SshNet.Sftp;

namespace NSFWpics.DBEntities
{
	/// <summary>
	/// This class is complete framework of tools used in NSFWpics project for managing content as Images, Movies, Users etc. Best use case is invoking it's methods via API endpoint calls
	/// </summary>
    public class DBEntity
    {
		/// <summary>
		/// Static instance of Db entites class, use it wherever you want
		/// </summary>
        public static DBEntity Instance = new DBEntity();

		/// <summary>
		/// DB connection string
		/// </summary>
        public MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder()
        {
            Server = "185.28.102.194",
            UserID = "root",
            Password = "Kubawich1",
            Database = "content",
            SslMode = MySqlSslMode.None,
            AllowUserVariables = true			
        };

		//Content based tools

        /// <summary>
        /// Increment image's point rating by one
        /// </summary>
        /// <param name="ID">Number for identifying image in DB</param>
        public void Plus(int ID)
        {
            ID = Convert.ToUInt16(ID);            
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            cmd = new MySqlCommand($"UPDATE imgs " +
                $"SET points = points + 1 " +
                $"WHERE id = {ID};", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Decrement image's point rating by one
        /// </summary>
        /// <param name="ID">Number for identifying image in DB</param>
        public void Minus(int ID)
        {
            ID = Convert.ToUInt16(ID);
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            cmd = new MySqlCommand($"UPDATE imgs " +
                $"SET points = points - 1 " +
                $"WHERE id = {ID};", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Gets last ID entrance from DB
        /// </summary>
        /// <returns>
        /// Returns highest ID from DB
        /// </returns>
        public int MaxId()
        {
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            var Id = 1;
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

        /// <summary>
        /// Adds cdn's uploaded photo to DB
        /// </summary>
        /// <param name="MaxIdPlusOne">Required to increment photo's id in DB</param>l        
        /// /// <param name="file">File to upload</param>
        public void UploadImgToDb(int MaxIdPlusOne, IFormFile file)
        {
            using (SftpClient client = new SftpClient("185.28.102.194", 22, "root", "Kubawich1"))
            {
                if (file != null)
                {
                    client.Connect();
                    client.ChangeDirectory("/var/www/html/img");
                    using (FileStream fs = new FileStream(Path.GetFileName(file.FileName), FileMode.Create))
                    {
                        client.UploadFile(file.OpenReadStream(),
                            $"{MaxIdPlusOne}{Path.GetExtension(file.FileName)}");
                    }
                }
                client.Disconnect();
                client.Dispose();
            }

            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            cmd = new MySqlCommand($"INSERT INTO imgs(uri,author,points) " +
                $"VALUES('https://cdn.nsfwpics.pw/img/{MaxIdPlusOne}{Path.GetExtension(file.FileName)}','Anonymous',0)", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Returns image with given id
        /// </summary>
        /// <param name="ID">Number for identifying image in DB</param>
        /// <param name="image">Returns class based on DB table architecture</param>
        /// <returns>
        /// Returns Image module from DB with given Image's id
        /// </returns>
        public Image View(int ID, Image image)
        {
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;

            cmd = new MySqlCommand($"SELECT uri, author, date, id, points FROM imgs WHERE id={ID}", conn);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                return image = new Image
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Uri = reader["uri"].ToString(),
                    Author = reader["author"].ToString(),
                    Points = int.Parse(reader["points"].ToString()),
                    Date = reader["date"].ToString()
                };
            }
            conn.Close();
            return image;
        }

        /// <summary>
        /// Picks image with highest point rating
        /// </summary>
        /// <param name="image">Returns class based on DB table architecture</param>
        /// <returns>
        /// Returns Image module from DB where id's highest
        /// </returns>
        public Image Best(Image image)
        {
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;

            cmd = new MySqlCommand($"SELECT id,uri,author,date,points " +
                $"FROM imgs " +
                $"WHERE points=(SELECT MAX(points) FROM imgs);", conn);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                return image = new Image
                {
                    Id = int.Parse(reader["id"].ToString()),
                    Uri = reader["uri"].ToString(),
                    Author = reader["author"].ToString(),
                    Points = int.Parse(reader["points"].ToString()),
                    Date = reader["date"].ToString()
                };
            }
            conn.Close();
            return image;
        }

        /// <summary>
        /// Picks number of indicies in database, and next pick random photo from first to maximum.
        /// </summary>
        /// <param name="image">Returns class based on DB table architecture</param>
        /// <returns>
        /// Image class represtantion filled with random DB entry
        /// </returns>
        public Image Random(Image image)
        {
            Random rand = new Random();
			int MaxId = this.MaxId();
            int i = rand.Next(9, MaxId+1);

            MySqlConnection conn2 = new MySqlConnection(connection.ToString());
            MySqlCommand cmd2 = new MySqlCommand($"SELECT uri, author, date, id, points FROM imgs WHERE id={i}", conn2);
            conn2.Open();

            MySqlDataReader reader2 = cmd2.ExecuteReader();

            while (reader2.Read())
            {
                return image = new Image
                {
                    Id = int.Parse(reader2["id"].ToString()),
                    Uri = reader2["uri"].ToString(),
                    Author = reader2["author"].ToString(),
                    Points = int.Parse(reader2["points"].ToString()),
                    Date = reader2["date"].ToString()
                };
            }
            conn2.Close();
            return image;
        }

        /// <summary>
        /// Returns whole 'site' module. Each site has 10 view entries
        /// </summary>
        /// <param name="id">Which site's entries to get</param>
        /// <param name="image">Returns class based on DB table architecture</param>
        /// <returns>
        /// List of 10 Image entities representing given id's site module
        /// </returns>
        public List<Image> Site(int id, List<Image> image)
        {
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;

            if (id == 0 || id == 1)
            {
                cmd = new MySqlCommand($"SELECT * " +
                    $"FROM imgs " +
                    $"WHERE id " +
                    $"ORDER BY id DESC " +
                    $"LIMIT 10;", conn);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    image.Add(new Image
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Uri = reader["uri"].ToString(),
                        Author = reader["author"].ToString(),
                        Points = int.Parse(reader["points"].ToString()),
                        Date = reader["date"].ToString()
                    });
                }

                conn.Close();
                return image;
            }
            else
            {
                cmd = new MySqlCommand($"SELECT * " +
                    $"FROM imgs " +
                    $"WHERE id " +
                    $"ORDER BY id DESC " +
                    $"LIMIT 10 OFFSET {id * 10 - 10};", conn);

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    image.Add(new Image
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Uri = reader["uri"].ToString(),
                        Author = reader["author"].ToString(),
                        Points = int.Parse(reader["points"].ToString()),
                        Date = reader["date"].ToString()
                    });
                }

                conn.Close();
                return image;
            }           
        }

		/// <summary>
		/// Removes content with given ID and extension
		/// </summary>
		/// <param name="id">ID of content entry to remove</param>
		/// <param name="extension">Content's file extension in ?extension=png/jpeg/webm... format </param>
		public void RemoveImg(int id, string extension)
        {
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            
            using (SftpClient client = new SftpClient("185.28.102.194", 22, "root", "Kubawich1"))
            {
                client.Connect();
				client.ChangeDirectory($"/var/www/html/img");
                client.DeleteFile($"{id}.{extension}");
                client.Disconnect();
                client.Dispose();
            }

            cmd = new MySqlCommand($"DELETE FROM imgs " +
                $"WHERE id = {id};", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

			int MaxId = this.MaxId();
			cmd = new MySqlCommand($"ALTER TABLE imgs " +
				$"AUTO_INCREMENT = {MaxId}", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
		}
		
		//User based tools

		public void Login(string login, string password)
		{

		}

		/// <summary>
		/// Registers new user in system
		/// </summary>
		/// <param name="login">Login to identify user in system. There can be only one user in system with given Login</param>
		/// <param name="password">SHA1 hash which reperesents password to identify user why loging to system</param>
		/// <param name="mail">User's email address</param>
		/// <returns>Registration proccess status success/failure</returns>
		public string Register(string login, string password, string mail)
		{
			MySqlConnection conn = new MySqlConnection(connection.ToString());
			MySqlCommand cmd;

			cmd = new MySqlCommand($"SELECT EXISTS(" +
				$"SELECT 1 " +
				$"FROM users " +
				$"WHERE login = '{login}' " +
				$"LIMIT 1)", conn);
			conn.Open();
			var i = cmd.ExecuteScalar();
			conn.Close();
			//var DoesUserAlreadyExist = (int.Parse(i.ToString()) == 1) ? "User already exists" : "Successfully Registered user";

			if (int.Parse(i.ToString()) == 1)
			{
				return "User already exists";
			}
			else
			{
				cmd = new MySqlCommand($"INSERT INTO users (guid, login, password, mail)" +
					$"VALUES (1, '{login}', SHA1('{password}'), '{mail}')", conn);
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();
				return $"Successfully Registered user {login}";
			}
			return "Internal Error";
		}

		/// <summary>
		/// Returns user data with given id
		/// </summary>
		/// <param name="uid"></param>
		/// <returns>User from DB with given id</returns>
		public User GetUser(int? uid)
		{
			MySqlConnection conn = new MySqlConnection(connection.ToString());
			MySqlCommand cmd;
			MySqlDataReader reader = null;

			if (uid == null)
			{
				cmd = new MySqlCommand($"SELECT * FROM users", conn);
				conn.Open();
				reader = cmd.ExecuteReader();				

				while (reader.Read())
				{
					return new User
					{
						Guid = reader["guid"].ToString(),
						Uid = reader["uid"].ToString(),
						Login = reader["login"].ToString(),
						Password = reader["password"].ToString(),
						Mail = reader["mail"].ToString(),
						Uploads = int.Parse(reader["uploads"].ToString())
					};
				}
				conn.Close();
			}
			else if(uid != null)
			{
				cmd = new MySqlCommand($"SELECT * FROM users WHERE uid = {uid}", conn);
				conn.Open();
				reader = cmd.ExecuteReader();				

				while (reader.Read())
				{
					return new User
					{
						Guid = reader["guid"].ToString(),
						Uid = reader["uid"].ToString(),
						Login = reader["login"].ToString(),
						Password = reader["password"].ToString(),
						Mail = reader["mail"].ToString(),
						Uploads = int.Parse(reader["uploads"].ToString())
					};
				}
				conn.Close();
			}
			else
			{
				return new User();
			}
			return new User();
		}

		/// <summary>
		/// Deletes user account with given id
		/// </summary>
		/// <param name="uid">User id to delete</param>
		/// <returns>String status of operation Sucess/Failure</returns>
		public string DeleteUser(int uid)
		{
			MySqlConnection conn = new MySqlConnection(connection.ToString());
			MySqlCommand cmd;

			return $"User {uid} deleted";
		}

		/// <summary>
		/// Updates/changes user parameter with given id
		/// </summary>
		/// <param name="uid">User id to make update on</param>
		/// <param name="changeType">Column name to update</param>
		/// <param name="changeValue">New value for old resource at given column</param>
		/// <returns>String status of operation Updated/Failed</returns>
		public string UpdateUser(int uid, string changeType, string changeValue)
		{
			MySqlConnection conn = new MySqlConnection(connection.ToString());
			MySqlCommand cmd;

			return $"Updated user {uid}";
		}		
	}
}
