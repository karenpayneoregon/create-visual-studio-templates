using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCoreHelpers.LanguageExtensions
{
    /// <summary>
    /// Extension methods for assisting in both learning EF Core and debugging.
    /// </summary>
    /// <remarks>
    /// See also  https://docs.microsoft.com/en-us/ef/core/change-tracking/debug-views
    /// Note that future releases of EF Core may break these methods but should be easy to tweak.
    /// </remarks>
    public static class DebugViewExtensions
    {
        /// <summary>
        /// Filter LongView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tokens">Filter token array</param>
        /// <param name="chunkSize"></param>
        /// <returns>filtered view</returns>
        public static string CustomViewByChunks(this DebugView sender, string[] tokens, int chunkSize)
        {
            var longViewLinesList = sender
                .LongView.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .ToList();

            /*
             * Split LongView into chunks
             */
            var chunks = longViewLinesList.ChunkBy(chunkSize);

            StringBuilder builder = new();

            /*
             * Incrementally find items in tokens array
             */
            foreach (var chunk in chunks)
            {
                foreach (var item in chunk)
                {
                    if (item.Has(tokens))
                    {
                        builder.AppendLine(item);
                    }
                }
            }

            return builder.ToString();

        }

        /// <summary>
        /// Variation of the above
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tokens"></param>
        /// <param name="lineCount"></param>
        /// <returns></returns>
        public static string CustomView(this DebugView sender, string[] tokens, int? lineCount)
        {
            var longViewLines = sender
                .LongView.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            StringBuilder builder = new();
            if (lineCount.HasValue)
            {
                var result = longViewLines
                    .Where(item => item.Has(tokens))
                    .Take(lineCount.Value)
                    .ToArray();

                foreach (var line in result)
                {
                    builder.AppendLine(line.Contains("Unchanged", StringComparison.OrdinalIgnoreCase) ?
                        "" :
                        line.TrimStart());
                }
            }

            return builder.ToString();

        }
    }
}


