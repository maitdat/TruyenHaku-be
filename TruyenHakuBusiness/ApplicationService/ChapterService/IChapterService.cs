using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.Entities;
using TruyenHakuModels.RequestModels;
using TruyenHakuModels.ResponseModels;
using TruyenHakuModels.ResponseModels.Application.Chapter;

namespace TruyenHakuBusiness.ApplicationService.ChapterService
{
    public interface IChapterService
    {
        public Task AddChapterByUploadFolder();
        Task AddChapterAsync(Chapter chapter);
        Task UpdateChapterAsync(Chapter chapter);
        Task DeleteChapterAsync(long chapterId);
        Task<ChapterResponse> GetChapterByIdAsync(long chapterId);
        Task<BasePaginationResponse<Chapter>> GetChaptersWithPaginationAsync(BasePaginationRequest request);
    }
}
