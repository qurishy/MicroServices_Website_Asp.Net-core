using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters;
using Serv_CouponAPI.Models;
using Serv_CouponAPI.Models.DTO;

namespace Serv_CouponAPI
{
    public class MapperforCoupon
    {
        public static  MapperConfiguration RegisterMap()
        {
            var mappingconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CouponDTO, Coupon>();
                cfg.CreateMap<Coupon, CouponDTO>();
            });
            return mappingconfig;
        }
    }
}
