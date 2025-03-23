using Business.Abstract;
using Business.Dto; 
using DataAccess.Abstract;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ProductDto> GetAllProducts(Expression<Func<Product, bool>> filter = null)
        {
            var products = _productDal.GetAll(filter);

            
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsApprovew = p.IsApprovew,
                IsHome = p.IsHome,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name,
                Description = p.Description,
                Image = p.Image,
                Stock = p.Stock
            }).ToList();
        }

        public ProductDto GetProductById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                IsApprovew = product.IsApprovew,
                IsHome = product.IsHome,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                Description = product.Description,
                Image = product.Image,
                Stock = product.Stock
            };
        }

        public void AddProduct(ProductDto productDto)
        {
            // DTO -> Entity dönüşümü
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                IsApprovew = productDto.IsApprovew,
                IsHome = productDto.IsHome,
                CategoryId = productDto.CategoryId,
                Description = productDto.Description,
                Image = productDto.Image,
                Stock = productDto.Stock
            };

            _productDal.Add(product);
        }

        public void UpdateProduct(ProductDto productDto)
        {
            var existingProduct = _productDal.Get(p => p.Id == productDto.Id);
            if (existingProduct == null) return;

            // Güncelleme
            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;
            existingProduct.IsApprovew = productDto.IsApprovew;
            existingProduct.IsHome = productDto.IsHome;
            existingProduct.CategoryId = productDto.CategoryId;
            existingProduct.Description = productDto.Description;
            existingProduct.Image = productDto.Image;
            existingProduct.Stock = productDto.Stock;

            _productDal.Update(existingProduct);
        }

        public void DeleteProduct(int id)
        {
            var product = _productDal.Get(p => p.Id == id);
            if (product != null)
            {
                _productDal.Delete(product);
            }
        }
    }
}
