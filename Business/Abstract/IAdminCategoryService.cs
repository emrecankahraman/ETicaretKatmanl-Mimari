using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IAdminCategoryService
    {
        List<Category> GetAllCategories(Expression<Func<Category, bool>> filter = null);
        Category GetCategoryById(int id);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
