using System.ComponentModel.DataAnnotations;

namespace WebModels.Entities.BaseEnitities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
