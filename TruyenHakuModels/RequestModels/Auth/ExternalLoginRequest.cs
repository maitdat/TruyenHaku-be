public class ExternalLoginRequest
{
    public string Provider { get; set; } // Google, Facebook, etc.
    public string ReturnUrl { get; set; } // URL quay về sau khi đăng nhập
}
