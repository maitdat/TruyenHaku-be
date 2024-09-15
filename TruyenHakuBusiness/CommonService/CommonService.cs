using TruyenHakuModels;
using System.Drawing;

namespace TruyenHakuBusiness.CommonService
{
    public class CommonService : ICommonService
    {
        private readonly AppDbContext _appDbContext;
        public CommonService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task DownloadImgFromURLAsync(string imgUrl,string filePath,string fileName)
        {
            if (string.IsNullOrEmpty(imgUrl)) return;
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri(imgUrl);

                var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
                var fileExtension = Path.GetExtension(uriWithoutQuery);

                var path = Path.Combine(filePath, $"{fileName}{fileExtension}");

                byte[] imageBytes = await client.GetByteArrayAsync(imgUrl);
                // Save the byte array to a file
                await File.WriteAllBytesAsync(path, imageBytes);
            }
        }
    }
}
