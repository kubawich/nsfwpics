﻿using System;
using MySql.Data.MySqlClient;
using Renci.SshNet;

namespace NSFWpics.Interfaces
{
	public abstract class Credentials
	{
		/// <summary>
		/// Used to connect to MySQL DB
		/// </summary>
		 public MySqlConnectionStringBuilder Connection { get; protected set; } =
			new MySqlConnectionStringBuilder()
			{
				Server = "185.28.102.194",
				UserID = "root",
				Password = "Kubawich1",
				Database = "content",
				SslMode = MySqlSslMode.None,
				AllowUserVariables = true
			};

		/// <summary>
		/// Used to connect to SFTP server
		/// </summary>
		public SftpClient SftpConnection { get; protected set; } =
			new SftpClient("185.28.102.194", 22, "root", "Kubawich1");
	}
}
