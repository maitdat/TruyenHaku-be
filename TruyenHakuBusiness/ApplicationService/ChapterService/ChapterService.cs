using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TruyenHakuCommon;
using TruyenHakuCommon.Constants;
using TruyenHakuModels;
using TruyenHakuModels.Entities;
using TruyenHakuModels.ResponseModels.Application.Chapter;

namespace TruyenHakuBusiness.ApplicationService.ChapterService
{
    public class ChapterService : IChapterService
    {
        private readonly AppDbContext _appDbContext;
        private const string CHAPTER = "Chapter";

        public ChapterService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddChapterAsync(Chapter chapter)
        {
            _appDbContext.Chapter.Add(chapter);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateChapterAsync(Chapter chapter)
        {
            _appDbContext.Chapter.Update(chapter);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteChapterAsync(long chapterId)
        {
            var chapter = await _appDbContext.Chapter.Where(x=>x.Id == chapterId).FirstOrDefaultAsync();
            if (chapter != null)
            {
                _appDbContext.Chapter.Remove(chapter);
                await _appDbContext.SaveChangesAsync();
            }
        }

        

        public async Task<List<ChapterResponse>> GetChaptersAsync(long mangaId)
        {
            var res =await _appDbContext.Chapter.Where(x=>x.Manga.Id == mangaId)
                .Select(x=>new ChapterResponse
            {
                Id = x.Id,
                ChapterDir = x.NameFolder,
                Name = x.Name,
                DateCreated = x.DateCreated,
            }).ToListAsync();

            return res;
        }

        public async Task<ChapterResponse> GetChapterByIdAsync(long id)
        {
            
            var chapter = await _appDbContext.Chapter
                .Include(x=>x.Manga)
                .Where(x=>x.Id == id).FirstOrDefaultAsync();

            if (chapter == null)
                throw new Exception(string.Format(Constants.Commons.NOT_FOUND, CHAPTER));

            _appDbContext.Chapter.Update(chapter);
            await _appDbContext.SaveChangesAsync();

            var fullPathDir = Utilities.ConcatChapterDir(chapter.Manga.NameFolder, chapter.NameFolder);
            var pathForServerImg = string.Concat(chapter.Manga.NameFolder, @"/", chapter.NameFolder);

            var httpsNginx = Constants.SeverNginx.HTTPS;

            if (Directory.Exists(fullPathDir))
            {
                DirectoryInfo di = new DirectoryInfo(fullPathDir);
                var files = di.GetFiles();

                var listImgsName = files.Select(x => x.Name)
                    .OrderBy(x => int.Parse(Regex.Match(x, @"\d+").Value))
                    .ToList();

                var linkImgs = listImgsName.Select(imgName =>
                    $"{httpsNginx}/{pathForServerImg}/{imgName}").ToList();

                var chapterResponse = new ChapterResponse()
                {
                    Id = chapter.Id,
                    Name = chapter.Name,
                    ChapterDir = chapter.NameFolder,
                    DateCreated = chapter.DateCreated,
                    DateModified = chapter.DateModified,
                    LinkImgs = linkImgs  // Đây là danh sách các đường dẫn hình ảnh
                };

                return chapterResponse;
            }

            throw new Exception("Directory for images not found.");
        }

        public Task AddChapterByUploadFolder()
        {
            throw new NotImplementedException();
        }
    }
}
