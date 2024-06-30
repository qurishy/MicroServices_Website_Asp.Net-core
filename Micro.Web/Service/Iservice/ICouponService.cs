using Micro.Web.Models;

namespace Micro.Web.Service.Iservice
{
    public interface ICouponService
    {

        Task<ResponsDTO> GetCouponByIdAysnc(int couponId);

        Task<ResponsDTO> GetCouponByCodeAsync(string couponCode);

        Task<ResponsDTO> GetAllCouponAsync();

        Task<ResponsDTO> UpdateCouponAysnc(CouponDTO obj);

        Task<ResponsDTO> CreateCouponAysnc(CouponDTO obj);

        Task<ResponsDTO> DeleteCouponAsync(int couponId);

    }
}
