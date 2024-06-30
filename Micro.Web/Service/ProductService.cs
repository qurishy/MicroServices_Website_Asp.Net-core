using Micro.Web.Models;
using Micro.Web.Models.DTO;
using Micro.Web.Service.Iservice;
using Micro.Web.Utility;

namespace Micro.Web.Service
{
    public class ProductService : IProductService
    {

        private readonly IBaseService _baseService;
        
        public ProductService(IBaseService baseService)
        {

            _baseService=baseService;

        }



        public async Task<ResponsDTO> AddProduct(ProductDto Obj)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                apiType=SD.ApiType.POST,
                Data = Obj,
                Url= SD.ProductBaseApi+"/api/product"
            });
        }

        public async Task<ResponsDTO> DeleteProduct(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                apiType = SD.ApiType.DELETE,
                Url = SD.ProductBaseApi+"/api/product/"+productId
               
            });
        }

        public async Task<ResponsDTO> GetById(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                apiType  = SD.ApiType.GET,
                Url=SD.ProductBaseApi+"/api/product/"+productId
            });
        }

        public async Task<ResponsDTO> GetByName(string prodname)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                apiType = SD.ApiType.GET,
                Url = SD.ProductBaseApi+"/api/product/"+prodname, 

            });
        }

        public async Task<ResponsDTO> GetProduct()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                apiType=SD.ApiType.GET,
                Url=SD.ProductBaseApi+"/api/product"
            });
            
        }

        public async Task<ResponsDTO> UpdateProduct(ProductDto Obj)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                apiType=SD.ApiType.PUT,
                Url = SD.ProductBaseApi+"/api/product",
                Data= Obj


            });
        }
    }
}
