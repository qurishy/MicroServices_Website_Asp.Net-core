using System.IO.Pipes;

namespace Micro.Web.Utility
{
    public class SD
    {
        public static string CouponBaseApi { get; set; }
        public static string AuthBaseApi { get; set; }

        public static string ProductBaseApi { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCutomer = "CUSTOMER";
        public const string TokenCooke = "JWTToken";
        public enum ApiType
        {
            GET, 
            POST,
            PUT,
            DELETE

        }
    }
}
