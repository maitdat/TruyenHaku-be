namespace TruyenHakuModels.ResponseModels
{
    public class BaseResponse
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
    }
}
