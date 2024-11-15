using TruyenHakuModels;

namespace TruyenHakuBusiness.CommonService
{
    public class CommonService : ICommonService
    {
        private readonly AppDbContext _appDbContext;
        public CommonService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task DownloadImgFromURLAsync(HttpClient client, string imgUrl, string filePath, string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(imgUrl)) return;

                
                var path = Path.Combine(filePath, $"{fileName}.jpg");

                var response = await client.GetAsync(imgUrl);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task DownloadListImgFromURLAsync(List<string> imgUrls, string filePath)
        {
            if (!imgUrls.Any()) return;

            using (HttpClient client = new HttpClient())
            {
                var tasks = new List<Task>();

                for (int i = 0; i < imgUrls.Count; i++)
                {
                    // Đặt tên file theo chỉ số ảnh
                    string fileName = $"{i + 1}";

                    // Tải mỗi ảnh và thêm vào danh sách Task
                    tasks.Add(DownloadImgFromURLAsync(client, imgUrls[i], filePath, fileName));
                }

                // Chờ tất cả các ảnh được tải xong
                await Task.WhenAll(tasks);
            }
        }
    }

}

