namespace WebModels.RequestModels.RoleRequestModel
{
    public class RoleRequestModel
    {
        public string UserId { get; set; }
        public List<InputRoleName> InputRoleName { get; set; }
    }
}
