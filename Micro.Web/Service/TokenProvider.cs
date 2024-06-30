using Micro.Web.Service.Iservice;
using Micro.Web.Utility;

namespace Micro.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor contextAccessor)
        {

            _contextAccessor=contextAccessor;

        }
        public void ClearedToken()
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(SD.TokenCooke);
        }

        public string GetToken()
        {
            string? token = null;

        
            bool hasToken = _contextAccessor.HttpContext.Request.Cookies.TryGetValue(SD.TokenCooke, out token);

            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCooke,token);
        }
    }
}
