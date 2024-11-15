namespace TruyenHakuBusiness.CommonService
{
    public interface ICommonService
    {
        Task DownloadImgFromURLAsync(HttpClient client, string imgUrl, string filePath, string fileName);
        Task DownloadListImgFromURLAsync(List<string> imgUrls, string filePath);
    }
}
