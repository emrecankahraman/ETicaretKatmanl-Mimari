using Business.Abstract;
using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concreate
{
    public class AdminCategoryManager : IAdminCategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public AdminCategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAllCategories(Expression<Func<Category, bool>> filter = null)
        {
            return _categoryDal.GetAll(filter);
        }

        public Category GetCategoryById(int id)
        {
            return _categoryDal.Get(id);
        }

        public void AddCategory(Category category)
        {
            _categoryDal.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryDal.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            _categoryDal.Delete(category);
        }
    }
}
