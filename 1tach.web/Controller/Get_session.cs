using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Controller
{
    public class Get_session
    {
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        Product_Details pro_detail = new Product_Details();
        
        public void Load_All_Cuss(string email)
        {
            try
            {
                var _cus = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_EMAIL == email);
                if (_cus.ToList().Count > 0)
                {
                    HttpContext.Current.Session["USER_NAME"] = _cus.ToList()[0].CUSTOMER_FULLNAME;
                    HttpContext.Current.Session["USER_ID"] = _cus.ToList()[0].CUSTOMER_ID;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public void Load_All_Cuss(int cusid)
        {
            try
            {
                var _cus = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == cusid);
                if (_cus.ToList().Count > 0)
                {
                    HttpContext.Current.Session["USER_NAME"] = _cus.ToList()[0].CUSTOMER_FULLNAME;
                    HttpContext.Current.Session["USER_ID"] = _cus.ToList()[0].CUSTOMER_ID;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public void LoadCatInfo(string _catSeoUrl)
        {
            try
            {
                
                var cats = db.GetTable<ESHOP_CATEGORy>().Where(c => c.CAT_SEO_URL == _catSeoUrl);

                if (cats.ToList().Count > 0)
                {

                    #region Bind Cat Info

                    HttpContext.Current.Session["Cat_id"] = cats.ToList()[0].CAT_ID;
                    HttpContext.Current.Session["Cat_title"] = cats.ToList()[0].CAT_NAME;
                    HttpContext.Current.Session["Cat_pageitem"] = cats.ToList()[0].CAT_PAGEITEM;
                    HttpContext.Current.Session["Cat_seo_desc"] = cats.ToList()[0].CAT_SEO_DESC;
                    HttpContext.Current.Session["Cat_seo_keyword"] = cats.ToList()[0].CAT_SEO_KEYWORD;
                    HttpContext.Current.Session["Cat_seo_title"] = cats.ToList()[0].CAT_SEO_TITLE;
                    HttpContext.Current.Session["Cat_showitem"] = cats.ToList()[0].CAT_SHOWITEM;
                    HttpContext.Current.Session["Cat_type"] = cats.ToList()[0].CAT_TYPE;

                    #endregion

                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public void LoadNewsInfo(string News_Seo_Url)
        {
            try
            {

                var news = db.GetTable<ESHOP_NEW>().Where(c => c.NEWS_SEO_URL == News_Seo_Url);

                var list = pro_detail.Load_Product_Menu(News_Seo_Url);
                if (list.Count > 0)
                {
                    HttpContext.Current.Session["Cat_id"] = list[0].CAT_ID;
                } 
                if (news.ToList().Count > 0)
                {
                    #region Bind News Info
                    HttpContext.Current.Session["News_id"] = news.ToList()[0].NEWS_ID;
                    HttpContext.Current.Session["News_seo_keyword"] = news.ToList()[0].NEWS_SEO_KEYWORD;
                    HttpContext.Current.Session["News_seo_desc"] = news.ToList()[0].NEWS_SEO_DESC;
                    HttpContext.Current.Session["News_seo_title"] = news.ToList()[0].NEWS_SEO_TITLE;
                    HttpContext.Current.Session["News_url"] = news.ToList()[0].NEWS_URL;
                    HttpContext.Current.Session["News_image3"] = news.ToList()[0].NEWS_IMAGE3;
                    
                    #endregion


                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public int Getcat_type(string News_Seo_Url)
        {
            int _type=0;
            var list = (from a in db.ESHOP_NEWS_CATs
                        join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                        where b.NEWS_SEO_URL == News_Seo_Url
                        select new { a.ESHOP_CATEGORy.CAT_TYPE }).ToList();
            if (list.Count > 0)
            {
                _type =Utils.CIntDef(list[0].CAT_TYPE);
            }
            return _type;
        }
        public bool check_CatorNews(string url)
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_SEO_URL == url).ToList();
            if (list.Count > 0)
                return true;
            return false;
        }

        public int getLang(string _news_seo, string _catseo)
        {
            int lang = 1;
            if (!String.IsNullOrEmpty(_news_seo))
            {
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            where b.NEWS_SEO_URL == _news_seo
                            select new { a.ESHOP_CATEGORy.CAT_LANGUAGE }).ToList();
                if (list.Count > 0)
                {
                    lang = Utils.CIntDef(list[0].CAT_LANGUAGE);
                }
            }
            else
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == _catseo).Select(n => new { n.CAT_LANGUAGE }).ToList();
                if (list.Count > 0)
                {
                    lang = Utils.CIntDef(list[0].CAT_LANGUAGE);
                }
            }
            return lang;
        }
        public bool check_catExits(string _url)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == _url).ToList();
            if (list.Count > 0)
                return true;
            return false;
        }
    }
}
