using Business.Dto;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IAdminProductService
    {
        // Ortak işlemler için mevcut servis metodunu kullanıyoruz.
        List<ProductDto> GetAllProducts(Expression<Func<Product, bool>> filter = null);
        ProductDto GetProductById(int id);

        void AddProduct(ProductDto productDto);
        void UpdateProduct(ProductDto productDto);
        void DeleteProduct(int id);
    }
}
