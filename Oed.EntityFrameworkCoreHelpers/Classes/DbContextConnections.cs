using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationLibrary.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Oed.EntityFrameworkCoreHelpers.Classes
{
    public class DbContextConnections
    {
        /// <summary>
        /// Simple configuration for setting the connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public static void NoLogging(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationHelper.ConnectionString());
        }

        /// <summary>
        /// Default logging to output window
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public static void StandardLogging(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationHelper.ConnectionString())
                .EnableSensitiveDataLogging()
                .LogTo(message => Debug.WriteLine(message));
        }
        /// <summary>
        /// Writes/appends to a file
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public static void CustomLogging(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(ConfigurationHelper.ConnectionString()).EnableSensitiveDataLogging()
                .LogTo(new DbContextLogger().Log)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

    }
}
