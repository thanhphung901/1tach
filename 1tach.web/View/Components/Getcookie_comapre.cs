using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sanzo.Components
{
    public class Getcookie_comapre
    {
        HttpCookie mycookie = new HttpCookie("news_url_compare");

        public Getcookie_comapre()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["news_url_compare"] != null)
            {
                mycookie = System.Web.HttpContext.Current.Request.Cookies["news_url_compare"];
            }
        }

        public void Addcookie(string Item)
        {
            mycookie.Values.Add("Item_" + Item, Item);
            mycookie.Expires = DateTime.Now.AddMonths(1);
            System.Web.HttpContext.Current.Response.Cookies.Add(mycookie);
        }

        public HttpCookie GetCookie()
        {
            return mycookie;
        }

        public void Removecookie(string Item)
        {
            mycookie.Values.Remove("Item_" + Item);
            System.Web.HttpContext.Current.Response.Cookies.Add(mycookie);
        }
    }
}