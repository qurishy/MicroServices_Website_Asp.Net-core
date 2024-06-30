using Micro.Web.Models;
using Micro.Web.Models.DTO;
using Micro.Web.Service.Iservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Micro.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService prod)
        {
            _logger = logger;
            _productService = prod;

        }

        public async Task < IActionResult> Index()
        {
            IEnumerable<ProductDto> prodlist = new List<ProductDto>();

            ResponsDTO responded = await _productService.GetProduct();

            if (responded != null && responded.Issuccess)
            {
                prodlist = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(responded.result));

            }
            else
            {
                TempData["error"]=responded.Message;

            }

            return View(prodlist);
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto? result = new();

            ResponsDTO result2 = await _productService.GetById(productId);
            if (result2 != null)
            {
                result = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString( result2.result));

            }
            else
            {
                TempData["error"]= result2.Message;

            }

            return View(result);

        }
    }
}
