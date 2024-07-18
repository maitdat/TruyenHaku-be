using System.ComponentModel.DataAnnotations;

namespace TruyenHakuModels.Entities.BaseEnitities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
