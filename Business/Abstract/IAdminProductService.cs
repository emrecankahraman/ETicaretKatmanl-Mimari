using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IAdminProductService
    {
        // Ortak işlemler için mevcut servis metodunu kullanıyoruz.
        List<Product> GetAllProducts(Expression<Func<Product, bool>> filter = null);
        Product GetProductById(int id);

        // Sadece admin'e özgü ek işlemler
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
