using Business.Abstract;
using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Concreate
{
    public class AdminProductManager : IAdminProductService
    {
        private readonly IProductService _productService;
        private readonly IProductDal _productDal;

        public AdminProductManager(IProductService productService, IProductDal productDal)
        {
            _productService = productService;
            _productDal = productDal;
        }

        // Ortak listeleme işlemi: IProductService içindeki GetAll metodunu kullanıyoruz.
        public List<Product> GetAllProducts(Expression<Func<Product, bool>> filter = null)
        {
            return _productService.GetAll(filter);
        }

        public Product GetProductById(int id)
        {
            return _productService.GetById(id);
        }

        // Admin'e özgü ek, güncelle ve silme işlemleri IProductDal üzerinden yapılır.
        public void AddProduct(Product product)
        {
            _productDal.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _productDal.Update(product);
        }

        public void DeleteProduct(Product product)
        {
            _productDal.Delete(product);
        }
    }
}
