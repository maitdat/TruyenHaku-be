using TruyenHakuCommon;


namespace TruyenHakuModels.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public Enums.Category CategoryEnum { get; set; }
    }
}
