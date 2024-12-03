using System.Text.RegularExpressions;
using TruyenHakuCommon.Constants;

namespace TruyenHakuCommon
{
    public static class Utilities
    {

        public static IEnumerable<TSource> ApplyPaging<TSource>(this IEnumerable<TSource> source, int pageNo, int pageSize)
        {
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize) : source;
        }

        public static IEnumerable<TSource> ApplyPaging<TSource>(this IEnumerable<TSource> source, int pageNo, int pageSize, out int totalItem)
        {
            totalItem = source.Count();
            return pageSize > 0 ? source.Skip((pageNo - 1) * pageSize).Take(pageSize) : source;
        }

        public static string RemoveVietNameseChars(string source)
        {
            string[] VietnameseSigns = new string[]
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    source = source.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return source;
        }

        public static string RemoveSpecialCharacters(string input)
        {
            // Chỉ giữ lại các chữ cái (a-z, A-Z) và chữ số (0-9)
            return Regex.Replace(input, "[^a-zA-Z0-9]", "");
        }

        public static string GetChapterNumber(string input)
        {
            var regex = @"\d+(.\d+)?";
            return Regex.Match(input, regex).Value;
        }

        public static string ConcatChapterDir(string mangaDir, string chapterDir)
        {
            return string.Concat( Constants.Constants.PathFile.DEFAULT_ROOT_DIRECTORY ,@"\\", mangaDir, @"\\", chapterDir);
        }

        public static string SanitizeFolderName(string folderName)
        {
            // Replace invalid characters with an empty string
            string sanitizedFolderName = Regex.Replace(folderName, @"[\\/:*?""<>|]", "");
            return sanitizedFolderName;
        }
        public static string JoinEnDash(string input)
        {
            string result = Regex.Replace(input, @"\s+", "-").ToLower();
            string normalized = Regex.Replace(result, "-{2,}", "-");
            return normalized;
        }
    }
}
