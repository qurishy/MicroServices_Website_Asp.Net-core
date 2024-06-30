
using Micro.Web.Models;
using Micro.Web.Models.DTO;

namespace Micro.Web.Service.Iservice
{
    public interface IProductService
    {
        Task<ResponsDTO> GetProduct();

        Task<ResponsDTO> GetByName(string prodname);
        Task<ResponsDTO> GetById(int productId);
        Task<ResponsDTO> AddProduct(ProductDto Obj);
        Task<ResponsDTO> UpdateProduct(ProductDto Obj);
        Task<ResponsDTO> DeleteProduct(int productId);

    }
}
