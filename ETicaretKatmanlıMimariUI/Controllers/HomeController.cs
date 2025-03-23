using Business.Abstract;
using Entities.Entities;
using ETicaretKatmanlıMimariUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETicaretKatmanlıMimariUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(int id = 0)
        {
            var categories = _categoryService.GetAll();

            // Sadece IsApprovew = true olan ürünleri getir
            var products = id == 0
                ? _productService.GetAll(p => p.IsApprovew == true)
                : _productService.GetAll(p => p.CategoryId == id && p.IsApprovew == true);

            ViewBag.Id = id;
            ViewBag.Categories = categories;
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.Get(p => p.Id == id);
            return View(product);
        }

        public IActionResult List(int? id)
        {
            var categories = _categoryService.GetAll();
            ViewBag.Id = id ?? 0;
            ViewBag.Categories = categories;

            // Ürünleri seçilen kategoriye göre filtrele ve sadece onaylı ürünleri göster
            var products = (id == null || id == 0)
                ? _productService.GetAll(p => p.IsApprovew == true)
                : _productService.GetAll(p => p.CategoryId == id && p.IsApprovew == true);

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}