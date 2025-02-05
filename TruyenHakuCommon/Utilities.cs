using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace TruyenHakuCommon
{
    public static class Utilities
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task SendExceptionToWebhookAsync(
            Exception? ex,
            string? data = "",
            DateTime from = default,
            DateTime? to = default,
            string webhookUrl = "https://discord.com/api/webhooks/1328564383156146276/Vv3lpYhmGg-nvTkqvDtmip5NzQFGl_4_c7ffCVfUvu3tKBXlakZjxDATHVtMGTMZzwkb",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                var contentMessage = $"{DateTime.Now}\n" +
                                     $"**Exception occurred:**\n" +
                                     $"```{ex.Message ?? "No exception message"}```\n" +
                                     $"**Stack Trace:**\n" +
                                     $"```{ex.StackTrace ?? "No stack trace available"}```\n" +
                                     $"**Path:**\n" +
                                     $"```Function: {memberName} at {sourceFilePath}:{sourceLineNumber}```";
                if (!string.IsNullOrEmpty(data))
                {
                    contentMessage += $"\n**Data:**\n" +
                                      $"```{data}```";
                }

                var jsonPayload = new
                {
                    content = contentMessage
                };

                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonPayload);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(webhookUrl, content);
                response.EnsureSuccessStatusCode(); // Throw if the response indicates an error
            }
            catch (Exception sendEx)
            {
                // Log the exception that occurred while trying to send the webhook
                Console.WriteLine("Failed to send exception to Discord webhook: " + sendEx.Message);
            }
        }

        public static async Task SendMessageToWebhookAsync(
            string message,
            string? data = "",
            DateTime from = default,
            DateTime? to = default,
            string webhookUrl = "https://discord.com/api/webhooks/1328564383156146276/Vv3lpYhmGg-nvTkqvDtmip5NzQFGl_4_c7ffCVfUvu3tKBXlakZjxDATHVtMGTMZzwkb",
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
            {
                try
                {
                    var contentMessage = $"{DateTime.Now}\n" +
                                         $"**Message:**\n" +
                                         $"```{message}```\n" +
                                         $"**Path:**\n" +
                                         $"```Function: {memberName} at {sourceFilePath}:{sourceLineNumber}```";

                    if (!string.IsNullOrEmpty(data))
                    {
                        contentMessage += $"\n**Data:**\n" +
                                          $"```{data}```";
                    }

                    var jsonPayload = new
                    {
                        content = contentMessage
                    };

                    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonPayload);
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(webhookUrl, content);
                    response.EnsureSuccessStatusCode(); // Throw if the response indicates an error
                }
                catch (Exception sendEx)
                {
                    // Log the exception that occurred while trying to send the webhook
                    Console.WriteLine("Failed to send message to Discord webhook: " + sendEx.Message);
                }
        }


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
            return string.Concat( Constants.Constants.PathFile.DEFAULT_ROOT_DIRECTORY ,@"\", mangaDir, @"\", chapterDir);
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
