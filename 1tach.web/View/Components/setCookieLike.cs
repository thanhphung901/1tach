using System;
using System.Web;

namespace OneTach.Components
{
    public class setCookieLike
    {
        HttpCookie mycookie = new HttpCookie("news_url_like");

        public setCookieLike()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["news_url_like"] != null)
            {
                mycookie = System.Web.HttpContext.Current.Request.Cookies["news_url_like"];
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