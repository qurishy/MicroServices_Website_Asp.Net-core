using Micro.Web.Models;
using Micro.Web.Service.Iservice;
using Micro.Web.Utility;

namespace Micro.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _base;

        public CouponService(IBaseService ba)
        {
            _base = ba;
        }

        public async Task<ResponsDTO> CreateCouponAysnc(CouponDTO obj)
        {
            return await _base.SendAsync(new RequestDto()
            {
                apiType =SD.ApiType.POST,
                Data=obj,
                Url= SD.CouponBaseApi+"/api/Coupon"

            });
        }

        public async Task<ResponsDTO> DeleteCouponAsync(int couponId)
        {
            return await _base.SendAsync(new RequestDto()
            {
                apiType =SD.ApiType.DELETE,
                Url= SD.CouponBaseApi+"/api/Coupon/"+couponId

            });
        }

        public async Task<ResponsDTO> GetAllCouponAsync()
        {
            return await _base.SendAsync(new RequestDto()
            {
                apiType =SD.ApiType.GET,
                Url= SD.CouponBaseApi+"/api/Coupon"

            });
        }

        public async Task<ResponsDTO> GetCouponByCodeAsync(string couponCode)
        {
            return await _base.SendAsync(new RequestDto()
            {
                apiType =SD.ApiType.GET,
                Url= SD.CouponBaseApi+"/api/Coupon/GetByCode/"+couponCode

            });
        }

        public async Task<ResponsDTO> GetCouponByIdAysnc(int couponId)
        {
            return await _base.SendAsync(new RequestDto()
            {
                apiType =SD.ApiType.GET,
                Url= SD.CouponBaseApi+"/api/Coupon/"+couponId

            });
        }

        public async Task<ResponsDTO> UpdateCouponAysnc(CouponDTO obj)
        {
            return await _base.SendAsync(new RequestDto()
            {
                apiType =SD.ApiType.PUT,
                Data=obj,
                Url= SD.CouponBaseApi+"/api/Coupon"

            });
        }
    }
}
