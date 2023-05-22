using System;

namespace PropterHoc
{
    internal static class FilenameExtensions
    {
        private static Random random = new Random();

        internal static string GetSortableFilename(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy_MM_dd-HH_mm_ss");
        }

        internal static string AppendRandom(this string filename, string separator = "_")
        {
            string randomString = random.Next().ToString("x");
            return filename + randomString;
        }

        internal static string GetSortableUniqueFilename(this DateTime dateTime)
        {
            return dateTime.GetSortableFilename().AppendRandom();
        }
    }
}
