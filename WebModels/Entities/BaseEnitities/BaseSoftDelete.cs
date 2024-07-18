namespace WebModels.Entities.BaseEnitities
{
    public class BaseSoftDelete : BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}
