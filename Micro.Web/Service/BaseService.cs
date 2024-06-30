using Micro.Web.Models;
using Micro.Web.Service.Iservice;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Micro.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactorY,ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactorY;
            _tokenProvider = tokenProvider;
        }
        
        public async Task<ResponsDTO?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {

            
            HttpClient client = _httpClientFactory.CreateClient("MangoApi");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            //Token
            if(withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }
               




            message.RequestUri= new Uri(requestDto.Url);
            if(requestDto.Data != null)
            {
                message.Content=new StringContent(JsonConvert.SerializeObject(requestDto.Data),System.Text.Encoding.UTF8,"application/json");
            }
            HttpResponseMessage? ApiResponse = null;

            switch(requestDto.apiType)
            {
                case Utility.SD.ApiType.POST:
                    message.Method= HttpMethod.Post;
                    break;
                   case Utility.SD.ApiType.PUT:
                    message.Method= HttpMethod.Put;
                    break;
                    case Utility.SD.ApiType.DELETE:
                    message.Method= HttpMethod.Delete;
                    break;
                default:
                    message.Method= HttpMethod.Get;
                    break;
            }

            ApiResponse = await client.SendAsync(message);

            switch(ApiResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    return new() { Issuccess = false, Message = "not found" };

                case System.Net.HttpStatusCode.Forbidden:
                    return new() { Issuccess = false, Message = "Acces Denied" };
                case System.Net.HttpStatusCode.Unauthorized:
                    return new() { Issuccess = false, Message = "Unauthorized" };
                case System.Net.HttpStatusCode.InternalServerError:
                    return new() { Issuccess = false, Message = "Internal Server Error" };
                default:
                    var apicontent = await ApiResponse.Content.ReadAsStringAsync();
                    var apiResponseDtp = JsonConvert.DeserializeObject<ResponsDTO>(apicontent);
                    return apiResponseDtp;

            }
            }
            catch (Exception ex) {
                var dto = new ResponsDTO
                {
                    Message = ex.Message.ToString(),
                    Issuccess = false
                };
                return dto;
            }
        }
    }
}
