namespace TruyenHakuBusiness.CommonService
{
    public interface ICommonService
    {
        public Task DownloadImgFromURLAsync(string imgUrl, string filePath, string fileName);
    }
}
