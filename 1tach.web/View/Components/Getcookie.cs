using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sanzo.Components
{
    public class Getcookie
    {
        HttpCookie mycookie = new HttpCookie("news_url");

        public Getcookie()
        {
            // Check the Request Cookies collection for the cookie.
            if (System.Web.HttpContext.Current.Request.Cookies["news_url"] != null)
            {
                mycookie = System.Web.HttpContext.Current.Request.Cookies["news_url"];
            }
        }

        public void Addcookie(string Item)
        {
            mycookie.Values["Item_"+Item] = Item;
            mycookie.Expires = DateTime.Now.AddDays(3);

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

            // Remove cookie
            System.Web.HttpContext.Current.Response.Cookies.Remove(Item);

        }
    }
}