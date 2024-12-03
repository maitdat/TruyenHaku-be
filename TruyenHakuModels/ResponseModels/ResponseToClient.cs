namespace TruyenHakuModels.ResponseModels
{
    public class ResponseToClient
    {
        public bool Succeed { get; set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
    }
}
