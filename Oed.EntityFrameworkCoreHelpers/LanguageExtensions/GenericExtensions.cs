using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCoreHelpers.LanguageExtensions
{
    public static class GenericExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
            => source
                .Select((value, index) => new { Index = index, Value = value })
                .GroupBy(item => item.Index / chunkSize)
                .Select(grp => grp.Select(item => item.Value).ToList())
                .ToList();


        /// <summary>
        /// Convert T[] to object[]
        /// </summary>
        /// <typeparam name="T">type to convert</typeparam>
        /// <param name="sender">array</param>
        /// <returns>object array</returns>
        /// <remarks>
        /// Although this looks great there can be side effects, better to change from T to int as
        /// the purpose for this is <see cref="Classes.DbContexts.FindAllAsync"/>
        /// </remarks>
        public static object[] ToObjectArray<T>(this T[] sender) => sender.OfType<object>().ToArray();
    }
}
