using Business.Abstract;
using Business.Dto;
using Entities.Entities;
using ETicaretKatmanlıMimariUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ETicaretKatmanlıMimariUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IAdminProductService _adminProductService;
        private readonly IAdminOrderService _adminOrderService;
        private readonly IAdminCategoryService _adminCategoryService;

        public AdminPanelController(IAdminProductService adminProductService,
                               IAdminOrderService adminOrderService,
                               IAdminCategoryService adminCategoryService)
        {
            _adminProductService = adminProductService;
            _adminOrderService = adminOrderService;
            _adminCategoryService = adminCategoryService;
        }

        /* ======================= PRODUCT ======================= */

        // 1) Ürün Listeleme (Index)
        public IActionResult ManageProduct()
        {
            var productDtos = _adminProductService.GetAllProducts();
            return View(productDtos);
        }

        // 2) Ürün Ekleme (Opsiyonel)
        [HttpGet]
        public IActionResult ProductAdd()
        {
            var categories = _adminCategoryService.GetAllCategories();


            var viewModel = new ProductViewModel
            {
                CategoryList = new SelectList(categories, "Id", "Name")
            };

            return View(viewModel); 
        }

        
        [HttpPost]
        public IActionResult ProductAdd(ProductViewModel model)
        {
           
            ModelState.Remove("CategoryList");
            ModelState.Remove("CategoryName");

            if (!ModelState.IsValid)
            {
                var categories = _adminCategoryService.GetAllCategories();
                model.CategoryList = new SelectList(categories, "Id", "Name", model.CategoryId);

                return View(model);
            }

            // ViewModel → DTO dönüşümü
            var productDto = new ProductDto
            {
                Name = model.Name,
                Price = model.Price,
                IsApprovew = model.IsApprovew,
                IsHome = model.IsHome,
                Description = model.Description,
                Image = model.Image,
                Stock = model.Stock,
                CategoryId = model.CategoryId
            };

            _adminProductService.AddProduct(productDto);

            return RedirectToAction("ManageProduct");
        }
        [HttpGet]
        public IActionResult ProductUpdate(int id)
        {
            // ID'ye göre DTO'yu al
            var productDto = _adminProductService.GetProductById(id);
            if (productDto == null)
                return NotFound();

            // DTO → ViewModel dönüşümü
            var viewModel = new ProductViewModel
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                IsApprovew = productDto.IsApprovew,
                IsHome = productDto.IsHome,
                Description = productDto.Description,
                Image = productDto.Image,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId
            };

            var categories = _adminCategoryService.GetAllCategories();
            viewModel.CategoryList = new SelectList(categories, "Id", "Name", viewModel.CategoryId);

            return View(viewModel);
        }

        [HttpPost]
        
        public IActionResult ProductUpdate(ProductViewModel model)
        {
            // Remove these properties from ModelState validation
            ModelState.Remove("CategoryList");
            ModelState.Remove("CategoryName");

            if (!ModelState.IsValid)
            {
                // ModelState hatalarını konsola yazdıralım
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }
                // Validasyon hatası → dropdown bozulmasın
                var categories = _adminCategoryService.GetAllCategories();
                model.CategoryList = new SelectList(categories, "Id", "Name", model.CategoryId);

                return View(model);
            }

            // ViewModel → DTO dönüşümü
            var productDto = new ProductDto
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                IsApprovew = model.IsApprovew,
                IsHome = model.IsHome,
                Description = model.Description,
                Image = model.Image,
                Stock = model.Stock,
                CategoryId = model.CategoryId
            };

            _adminProductService.UpdateProduct(productDto);

            return RedirectToAction("ManageProduct");
        }
        // 4) Ürün Silme
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _adminProductService.DeleteProduct(id);
            return RedirectToAction("ManageProduct");
        }

        /* ======================= CATEGORY ======================= */

        // 1) Kategori Listeleme (Index)
        public IActionResult ManageCategory()
        {
            var categories = _adminCategoryService.GetAllCategories();
            return View(categories); 
        }

        // 2) Kategori Ekleme (Opsiyonel)
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult CategoryAdd(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _adminCategoryService.AddCategory(category);
            return RedirectToAction("ManageCategory");
        }

        // 3) Kategori Güncelleme
        [HttpGet]
        public IActionResult CategoryUpdate(int id)
        {
            var category = _adminCategoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return View(category); 
        }

        [HttpPost]
        public IActionResult CategoryUpdate(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _adminCategoryService.UpdateCategory(category);
            return RedirectToAction("ManageCategory");
        }

        // 4) Kategori Silme
        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var category = _adminCategoryService.GetCategoryById(id);
            if (category != null)
            {
                _adminCategoryService.DeleteCategory(category);
            }
            return RedirectToAction("ManageCategory");
        }


        /* ======================= ORDER ======================= */

        // 1) Sipariş Listeleme (Index)
        public IActionResult ManageOrders()
        {
            var orders = _adminOrderService.GetAllOrders();
            return View(orders); 
        }

        // 2) Sipariş Güncelleme
        [HttpGet]
        public IActionResult OrderUpdate(int id)
        {
            var order = _adminOrderService.GetOrderById(id);
            if (order == null) return NotFound();

            // 2) Entity → ViewModel dönüşümü
            var viewModel = new OrderViewModel
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                UserName = order.UserName,
                Total = order.Total,
                orderState = (ETicaretKatmanlıMimariUI.ViewModels.EnumOrderState)order.orderState
            };

            return View(viewModel); 
        }

        [HttpPost]
        public IActionResult OrderUpdate(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1) Eski kaydı bul
            var existingOrder = _adminOrderService.GetOrderById(model.Id);
            if (existingOrder == null) return NotFound();

            // 2) ViewModel → Entity/DTO güncelle
            existingOrder.orderState = (Entities.Enum.EnumOrderState)model.orderState;
            // (OrderNumber, Total, vs. eğer güncellenecekse)

            // 3) Servise güncel kaydı kaydet
            _adminOrderService.UpdateOrder(existingOrder);

            return RedirectToAction("ManageOrders");
        }

        // 3) Sipariş Silme
        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            var order = _adminOrderService.GetOrderById(id);
            if (order != null)
            {
                _adminOrderService.DeleteOrder(order);
            }
            return RedirectToAction("ManageOrders");
        }
    }
}
