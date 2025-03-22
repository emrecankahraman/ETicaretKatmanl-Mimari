using Business.Abstract;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var products = _adminProductService.GetAllProducts();
            return View(products); // Views/Admin/ManageProduct.cshtml
        }

        // 2) Ürün Ekleme (Opsiyonel)
        [HttpGet]
        public IActionResult ProductAdd()
        {
            return View(); // Views/Admin/ProductAdd.cshtml
        }

        [HttpPost]
        public IActionResult ProductAdd(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _adminProductService.AddProduct(product);
            return RedirectToAction("ManageProduct");
        }

        // 3) Ürün Güncelleme
        [HttpGet]
        public IActionResult ProductUpdate(int id)
        {
            var product = _adminProductService.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(product); // Views/Admin/ProductUpdate.cshtml
        }

        [HttpPost]
        public IActionResult ProductUpdate(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _adminProductService.UpdateProduct(product);
            return RedirectToAction("ManageProduct");
        }

        // 4) Ürün Silme
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var product = _adminProductService.GetProductById(id);
            if (product != null)
            {
                _adminProductService.DeleteProduct(product);
            }
            return RedirectToAction("ManageProduct");
        }


        /* ======================= CATEGORY ======================= */

        // 1) Kategori Listeleme (Index)
        public IActionResult ManageCategory()
        {
            var categories = _adminCategoryService.GetAllCategories();
            return View(categories); // Views/Admin/ManageCategory.cshtml
        }

        // 2) Kategori Ekleme (Opsiyonel)
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View(); // Views/Admin/CategoryAdd.cshtml
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

            return View(category); // Views/Admin/CategoryUpdate.cshtml
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
            return View(orders); // Views/Admin/ManageOrders.cshtml
        }

        // 2) Sipariş Güncelleme
        [HttpGet]
        public IActionResult OrderUpdate(int id)
        {
            var order = _adminOrderService.GetOrderById(id);
            if (order == null)
                return NotFound();

            return View(order); // Views/Admin/OrderUpdate.cshtml
        }

        [HttpPost]
        public IActionResult OrderUpdate(Order order)
        {
            if (!ModelState.IsValid)
                return View(order);

            _adminOrderService.UpdateOrder(order);
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
