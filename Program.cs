using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NSFWpics
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Program'
    public class Program
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Program'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Program.Main(string[])'
        public static void Main(string[] args)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Program.Main(string[])'
        {
            BuildWebHost(args).Run();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Program.BuildWebHost(string[])'
        public static IWebHost BuildWebHost(string[] args) =>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Program.BuildWebHost(string[])'
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
