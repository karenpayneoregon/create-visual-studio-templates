using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Oed.EntityFrameworkCoreHelpers.LanguageExtensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Generic reload for consistency 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="modelEntityEntry"></param>
        public static void Reload(this DbContext context, EntityEntry modelEntityEntry)
        {
            context.Entry(modelEntityEntry).Reload();
        }

        /// <summary>
        /// Determine if a connection can be made asynchronously
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns>if a connection can be made</returns>
        public static async Task<bool> TestConnectionTask(this DbContext context) =>
            await Task.Run(async () => await context.Database.CanConnectAsync());

        /// <summary>
        /// Determine if a connection can be made asynchronously
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <param name="token">token which can be used to set the timeout</param>
        /// <returns>if a connection can be made</returns>
        public static async Task<bool> TestConnectionTask(this DbContext context, CancellationToken token) =>
            await Task.Run(async () => await context.Database.CanConnectAsync(token), token);

        /// <summary>
        /// Test connection with exception handling
        /// </summary>
        /// <param name="context"></param>
        /// <returns>if a connection can be made</returns>
        public static (bool success, Exception exception) CanConnect(this DbContext context)
        {
            try
            {
                return (context.Database.CanConnect(), null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }

        /// <summary>
        /// Test connection with exception handling
        /// </summary>
        /// <param name="context"><see cref="DbContext"/></param>
        /// <returns></returns>
        public static async Task<(bool success, Exception exception)> CanConnectAsync(this DbContext context)
        {
            try
            {
                var result = await Task.Run(async () => await context.Database.CanConnectAsync());
                return (result, null);
            }
            catch (Exception e)
            {
                return (false, e);
            }
        }

        /// <summary>
        /// Kind of drastic and not recommended 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task Refresh(this DbContext context)
        {
            await context.Database.CloseConnectionAsync();
            await context.Database.OpenConnectionAsync();
        }
    }
}
