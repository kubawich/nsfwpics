using MySql.Data.MySqlClient;
using NSFWpics.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            SslMode = MySqlSslMode.None
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
            cmd = new MySqlCommand($"UPDATE imgs SET points = points + 1 WHERE id = {ID};", conn);
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
            cmd = new MySqlCommand($"UPDATE imgs SET points = points - 1 WHERE id = {ID};", conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Returns image with given id
        /// </summary>
        /// <param name="ID">Number for identifying image in DB</param>
        /// <param name="image">Returns class based on DB table architecture</param>
        /// <returns></returns>
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
        /// <returns></returns>
        public Image Best(Image image)
        {
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;

            cmd = new MySqlCommand($"SELECT id,uri,author,date,points FROM imgs WHERE points=(SELECT MAX(points) FROM imgs);", conn);
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
        /// <returns></returns>
        public Image Random(Image image)
        {
            Random rand = new Random();
            int MaxId = rand.Next(1, 10700); ;
            MySqlConnection conn = new MySqlConnection(connection.ToString());
            MySqlCommand cmd;
            cmd = new MySqlCommand($"SELECT COUNT(*) FROM imgs;", conn);

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
    }
}
