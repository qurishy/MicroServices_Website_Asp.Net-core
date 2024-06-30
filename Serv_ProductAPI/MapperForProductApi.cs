using AutoMapper;
using Serv_ProductAPI.Models;
using Serv_ProductAPI.Models.DTO;

namespace Serv_ProductAPI
{
    public class MapperForProductApi
    {
        public static MapperConfiguration RegisterMap()
        {
            var mapperconfig = new MapperConfiguration(obj =>
            {
                obj.CreateMap<Product,ProductDto>();
                obj.CreateMap<ProductDto,Product>();

            });
            return mapperconfig;
        }
    }
}
