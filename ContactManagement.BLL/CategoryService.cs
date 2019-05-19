using ContactManagement.Data;
using ContactManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryManagement.BLL
{
    public static class CategoryService
    {
        public static List<CategoryModels> GetCategorys()
        {
            List<CategoryModels> Categorys = CategoryRepository.GetAll();
            return Categorys.ToList();
        }
        
        public static CategoryModels SaveCategory(int id, CategoryModels model)
        {
            CategoryRepository.Save(id, model);
            return model;
        }
        public static bool DeleteCategory(int id)
        {
            return CategoryRepository.Delete(id);
        }
    }
}
