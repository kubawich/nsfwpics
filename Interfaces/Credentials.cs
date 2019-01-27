using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace NSFWpics.Interfaces
{
	public abstract class Credentials
	{
		protected MySqlConnectionStringBuilder Connection { get; } =
			new MySqlConnectionStringBuilder()
			{
				Server = "185.28.102.194",
				UserID = "root",
				Password = "Kubawich1",
				Database = "content",
				SslMode = MySqlSslMode.None,
				AllowUserVariables = true
			};
	}
}
