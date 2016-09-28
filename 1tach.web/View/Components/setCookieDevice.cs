using System;
using System.Web;

namespace OneTach.Components
{
    public class setCookieDevice
    {
         HttpCookie mycookie = new HttpCookie("deviceck");

         public setCookieDevice()
        {
            // Check the Request Cookies collection for the cookie.
            if (System.Web.HttpContext.Current.Request.Cookies["deviceck"] != null)
            {
                mycookie = System.Web.HttpContext.Current.Request.Cookies["deviceck"];
            }
        }

        public void Addcookie(string Item)
        {
            mycookie.Values["Item"] = Item;
            mycookie.Expires = DateTime.Now.AddMonths(1);

            // Add cookie
            System.Web.HttpContext.Current.Response.Cookies.Add(mycookie);
        }

        public HttpCookie GetCookie()
        {
            return mycookie;
        }

        public void Removecookie(string Item)
        {
            mycookie.Values["Item"] = Item;
            mycookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(mycookie);
        }
    }
}