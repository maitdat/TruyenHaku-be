using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruyenHakuModels.Entities.Application;

namespace TruyenHakuBusiness.ApplicationService.CategoryService
{
    public interface ICategoryService 
    {
        Category GetCategoryById(int id);
        void AddCategory(Category category);
        IEnumerable<Category> GetCategories();
    }
}
