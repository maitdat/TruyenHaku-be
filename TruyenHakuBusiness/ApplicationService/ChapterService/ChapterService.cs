using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TruyenHakuBusiness.DesignPattern.Repository;
using TruyenHakuCommon;
using TruyenHakuCommon.Constants;
using TruyenHakuModels.Entities;
using TruyenHakuModels.RequestModels;
using TruyenHakuModels.ResponseModels;
using TruyenHakuModels.ResponseModels.Application.Chapter;

namespace TruyenHakuBusiness.ApplicationService.ChapterService
{
    public class ChapterService : IChapterService
    {
        private readonly IGenericRepository<Chapter> _chapterRepository;
        private readonly IGenericRepository<Manga> _mangaRepository;
        private const string CHAPTER = "Chapter";

        public ChapterService(IGenericRepository<Chapter> chapterRepository, IGenericRepository<Manga> mangaRepository)
        {
            _chapterRepository = chapterRepository;
            _mangaRepository = mangaRepository;
        }

        public async Task AddChapterAsync(Chapter chapter)
        {
            _chapterRepository.Add(chapter);
            await _chapterRepository.SaveChangesAsync();
        }

        public async Task UpdateChapterAsync(Chapter chapter)
        {
            _chapterRepository.Update(chapter);
            await _chapterRepository.SaveChangesAsync();
        }

        public async Task DeleteChapterAsync(long chapterId)
        {
            var chapter = await _chapterRepository.GetByIdAsync(chapterId);
            if (chapter != null)
            {
                _chapterRepository.Remove(chapter);
                await _chapterRepository.SaveChangesAsync();
            }
        }

        

        public async Task<BasePaginationResponse<Chapter>> GetChaptersWithPaginationAsync(BasePaginationRequest request)
        {
            var query = _chapterRepository.GetAll();

            // Lọc theo từ khóa
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword));
            }

            var totalItems = query.Count();

            var data = await query
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new BasePaginationResponse<Chapter>(
                pageNo: request.PageNo,
                pageSize: request.PageSize,
                data: data,
                totalItem: totalItems
            );
        }

        public async Task<ChapterResponse> GetChapterByIdAsync(long id)
        {
            var chapter =await _chapterRepository
                .GetByIdAsync(id, x => x.Manga);

            if (chapter == null)
                throw new Exception(string.Format(Constants.Commons.NOT_FOUND, CHAPTER));

            chapter.Views = chapter.Views++;
            _chapterRepository.Update(chapter);
            await _chapterRepository.SaveChangesAsync();

            var chapterResponse = new ChapterResponse()
            {
                Id = chapter.Id,
                Name = chapter.Name,
                ChapterDir = Utilities.ConcatChapterDir(chapter.Manga.NameFolder, chapter.NameFolder),
                DateCreated = chapter.DateCreated,
                DateModified = chapter.DateModified,
            };
            return chapterResponse;
        }
        public Task AddChapterByUploadFolder()
        {
            throw new NotImplementedException();
        }
    }
}
