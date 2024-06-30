using Micro.Web.Models;
using Micro.Web.Models.DTO;
using Micro.Web.Service.Iservice;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Micro.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _product;

        public ProductsController(IProductService prodc)
        {
            _product = prodc;
        }




        public async Task< IActionResult> ProductIndex()
        {
            List<ProductDto> productlist = new();

            ResponsDTO responded = await _product.GetProduct();

            if(responded != null && responded.Issuccess==true)
            {
                productlist = JsonConvert.DeserializeObject<List<ProductDto>>( Convert.ToString( responded.result));

            }
            else
            {
                TempData["error"]=responded.Message;

            }
            return View(productlist);
        }

        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponsDTO? responded = await _product.AddProduct(model);

                if (responded != null && responded.Issuccess)
                {
                    TempData["success"] = "Created Successfully";
                    return RedirectToAction(nameof(ProductIndex));

                }
                else
                {
                    TempData["error"] = responded.Message;

                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int id)
        {
            ProductDto obj = new();
             ResponsDTO navbar =await _product.GetById(id);

            if (navbar != null && navbar.Issuccess)
            {
                obj = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString( navbar.result));
            }
            else
            {
                TempData["error"]= navbar.Message;
            }
            return View(obj);
        }


        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            


            if (ModelState.IsValid)
            {
                ResponsDTO obj = await _product.UpdateProduct(model);

                if (obj != null && obj.Issuccess)
                {
                    TempData["success"]="Updated";
                    return RedirectToAction(nameof(ProductIndex));

                }
                else
                {
                    TempData["error"]=obj.Message;
                }
                
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(int id)
        {
            ProductDto model = new();

            ResponsDTO respons = await _product.GetById(id);

            if(respons != null && respons.Issuccess)
            {

                model =  JsonConvert.DeserializeObject<ProductDto>(Convert.ToString( respons.result));
             
            }
            else
            {
                TempData["error"]= respons.Message;
            }

            return View(model);

           
        }


        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto obj)
        {

        
                ResponsDTO? respon = await _product.DeleteProduct(obj.ProductId);


                if (respon != null && respon.Issuccess)
                {

                    TempData["success"]= "Deleted Successful";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"]= respon.Message;
                }
            
            return View(obj);
        }
    }
}
