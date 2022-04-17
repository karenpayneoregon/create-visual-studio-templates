using System;

namespace EntityFrameworkCoreHelpers.LanguageExtensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Determine if any token exists in a string case insensitive
        /// </summary>
        /// <param name="sender">string to check</param>
        /// <param name="items">tokens to check if in sender</param>
        /// <returns>true/false</returns>
        public static bool Has(this string sender, string[] items)
        {

            foreach (var item in items)
            {
                if (sender.Contains(item, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;

        }
    }
}
