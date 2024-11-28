using System.Text.RegularExpressions;

namespace TruyenHakuCommon
{
    public static class Utilities
    {
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

        public static string JoinEnDash(string input)
        {
            return string.Join("-",input.Split());
        }
    }
}
