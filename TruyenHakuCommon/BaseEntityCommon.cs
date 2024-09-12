using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TruyenHakuCommon
{
    public class BaseEntityCommon : BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateCreated {  get; set; }
        public DateTime? DateModified { get; set;}
        public string? CreatorName { get; set; }
        public string? ModifierName { get; set; }
        public virtual void PrepareSave(IHttpContextAccessor httpContextAccessor, EntityState state)
        {
            var identityName = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value;
            var now = DateTime.Now;
            string creatorName = string.IsNullOrEmpty(CreatorName) ? "unknown" : CreatorName;
            if (state == EntityState.Added)
            {
                CreatorName = identityName ?? creatorName;
                DateCreated = now;
            }
            string modifierName = string.IsNullOrEmpty(ModifierName) ? "unknown" : ModifierName;
            ModifierName = identityName ?? modifierName;
            DateModified = now;
        }                                                                                                                                                                        
    }
}
