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
    public class DBEntity
    {
        public static DBEntity Instance = new DBEntity();
        public MySqlConnectionStringBuilder connection = new MySqlConnectionStringBuilder()
        {
            Server = "185.28.102.194",
            UserID = "root",
            Password = "Kubawich1",
            Database = "content",
            SslMode = MySqlSslMode.None,
            AllowUserVariables = true
        };

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
            int MaxId = rand.Next(1, 10700); ;
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            cmd = new MySqlCommand($"SELECT COUNT(*) " +
                $"FROM imgs;", conn);

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                MaxId = int.Parse(reader["COUNT(*)"].ToString());
            }
            int i = rand.Next(1, MaxId);

            conn.Close();

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

        public void RemoveImg(int id)
        {
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            
            using (SftpClient client = new SftpClient("185.28.102.194", 22, "root", "Kubawich1"))
            {
                client.Connect();
                client.DeleteFile($"../var/www/html/img/{id}.PNG");
                client.Disconnect();
                client.Dispose();
            }

            cmd = new MySqlCommand($"DELETE FROM imgs " +
                $"WHERE id = {id};", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd.CommandText = $"CREATE PROCEDURE reset_imgs_autoincrement" +
                $"BEGIN" +
                $"SELECT @max := MAX(id) FROM imgs;" +
                $"set @alter_statement = concat('ALTER TABLE imgs AUTO_INCREMENT = ', @max);" +
                $"PREPARE stmt FROM @alter_statement;" +
                $"EXECUTE stmt;" +
                $"DEALLOCATE PREPARE stmt;" +
                $"END";
            cmd.ExecuteNonQuery();
            conn.Close();
    }
    }
}
