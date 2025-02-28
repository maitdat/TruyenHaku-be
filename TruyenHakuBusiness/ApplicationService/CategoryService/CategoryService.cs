using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels;
using TruyenHakuModels.Entities.Application;

namespace TruyenHakuBusiness.ApplicationService.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Category.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Category.AsNoTracking().AsEnumerable();
        }

        public void AddCategory(Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
        }


        public void DeleteCategory(int id)
        {
            var category = _context.Category.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Category.Remove(category);
                _context.SaveChanges();
            }
        }
    }


}
