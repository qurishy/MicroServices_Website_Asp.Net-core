using Micro.Web.Models;

namespace Micro.Web.Service.Iservice
{
    public interface IBaseService
    {
       Task<ResponsDTO?>SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
