using Microsoft.AspNetCore.Mvc;
using static Micro.Web.Utility.SD;

namespace Micro.Web.Models
{
    public class RequestDto
    {

        public ApiType  apiType { get; set; } = ApiType.GET;

        public string Url { get; set; }

        public object Data { get; set; }

        public string AccessToken { get; set; }
    }
}
