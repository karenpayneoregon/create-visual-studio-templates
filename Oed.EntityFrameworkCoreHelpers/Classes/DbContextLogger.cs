using System;
using System.IO;

namespace Oed.EntityFrameworkCoreHelpers.Classes
{
    /// <summary>
    /// For logging messages from DbContext.
    /// </summary>
    public class DbContextLogger
    {
        /// <summary>
        /// append to the existing stream
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {

            StreamWriter streamWriter = new(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Log.txt"),
                true);

            streamWriter.WriteLine(message);

            streamWriter.WriteLine("-----------------------------------------------------------------");

            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}