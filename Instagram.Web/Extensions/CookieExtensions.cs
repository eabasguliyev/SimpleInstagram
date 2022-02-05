using Microsoft.AspNetCore.Http;
using System;

namespace Instagram.Web.Extensions
{
    public static class CookieExtensions
    {
        public static void Set(this IResponseCookies cookies, string key, string value, int? expireTime = null)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
            {
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }

            cookies.Append(key, value, option);
        }
    }
}