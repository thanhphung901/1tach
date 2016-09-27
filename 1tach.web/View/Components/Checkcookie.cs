using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sanzo.Components
{
    public class Checkcookie
    {
        Getcookie ck = new Getcookie();
        setCookieLike cklike = new setCookieLike();
        Getcookie_comapre ckcompare = new Getcookie_comapre();
        public List<string> Listcookie_see()
        {
            try
            {
                HttpCookie mycookie = ck.GetCookie();
                List<string> l = new List<string>();
                if (System.Web.HttpContext.Current.Request.Cookies["news_url"] != null)
                {
                    if (mycookie.HasKeys)
                    {
                        for (int j = 0; j < mycookie.Values.Count; j++)
                        {
                            l.Add(mycookie.Values[j]);
                        }
                    }
                }
                return l;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<string> Listcookie_like()
        {
            try
            {
                HttpCookie mycookie = cklike.GetCookie();
                List<string> l = new List<string>();
                if (System.Web.HttpContext.Current.Request.Cookies["news_url_like"] != null)
                {
                    if (mycookie.HasKeys)
                    {
                        for (int j = 0; j < mycookie.Values.Count; j++)
                        {
                            l.Add(mycookie.Values[j]);
                        }
                    }
                }
                return l;

            }
            catch
            {
                return null;
            }
        }
        public List<string> Listcookie_compare()
        {
            try
            {
                HttpCookie mycookie = ckcompare.GetCookie();
                List<string> l = new List<string>();
                if (System.Web.HttpContext.Current.Request.Cookies["news_url_compare"] != null)
                {
                    if (mycookie.HasKeys)
                    {
                        for (int j = 0; j < mycookie.Values.Count; j++)
                        {
                            l.Add(mycookie.Values[j]);
                        }
                    }
                }
                return l;

            }
            catch
            {
                return null;
            }
        }
    }
}