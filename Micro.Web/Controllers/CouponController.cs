using Micro.Web.Models;
using Micro.Web.Service.Iservice;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO.Pipes;

namespace Micro.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService coup)
        {
            _couponService = coup;
        }



        public async Task< IActionResult> CouponIndex()
        {
            List<CouponDTO>? lis = new();
            ResponsDTO? respon = await _couponService.GetAllCouponAsync();

            if (respon != null && respon.Issuccess)
            {

                lis = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(respon.result));

            }
            else
            {
                TempData["error"] = respon?.Message;
            }
            return View(lis);
        }


        public async Task< IActionResult> CouponCreate() { 

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model)
        {
            
            if (ModelState.IsValid)
            {
                ResponsDTO? respons = await _couponService.CreateCouponAysnc(model);
                if (respons !=  null && respons.Issuccess)
                {
                    TempData["success"] = "Created success";
                    return RedirectToAction(nameof(CouponIndex));

                }
                else
                {
                    TempData["error"] = respons?.Message;
                }

            }
         

            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {

            ResponsDTO? respon = await _couponService.GetCouponByIdAysnc(couponId);
            if (respon != null && respon.Issuccess)
            {
               
                CouponDTO? data = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(respon.result));
                return View(data);

            }
            else
            {
                TempData["error"] = respon?.Message;
            }


            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO obj)
        {

            ResponsDTO? respon = await _couponService.DeleteCouponAsync(obj.CouponId);

            if (respon != null && respon.Issuccess)
            {
                TempData["success"] = "Deleted success";
                return RedirectToAction(nameof(CouponIndex));

            }
            else
            {
                TempData["error"] = respon?.Message;
            }

            return View(obj);
        }

    }
}
