using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TruyenHakuCommon
{
    public class BaseEntityCommon
    {
        public bool IsDeleted { get; set; }
        public DateTime? DateCreated {  get; set; }
        public DateTime? DateModified { get; set;}
        public string? CreatorName { get; set; }
        public string? ModifierName { get; set; }
        public virtual void PrepareSave(IHttpContextAccessor httpContextAccessor, EntityState state)
        {
            var identityName = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            var now = DateTime.Now;
            string nguoiTao = string.IsNullOrEmpty(NguoiTao) ? "unknown" : NguoiTao;
            if (state == EntityState.Added)
            {
                NguoiTao = identityName ?? nguoiTao;
                NgayTao = now;
                NgayTaoAL = CalendarGetLunarDate.GetNgayAL(now);
            }
            string nguoiCapNhat = string.IsNullOrEmpty(NguoiCapNhat) ? "unknown" : NguoiCapNhat;
            NguoiCapNhat = identityName ?? nguoiCapNhat;
            NgayCapNhat = now;
            NgayCapNhatAL = CalendarGetLunarDate.GetNgayAL(now);
        }                                                                                                                                                                        
    }
}
