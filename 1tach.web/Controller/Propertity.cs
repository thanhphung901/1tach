using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;
using System.Web;
using System.Web.UI;

namespace Controller
{
    public class Propertity
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        //Menu
        public List<ESHOP_CATEGORy> Loadmenu(int position, int limit, int rank, int catperiod, int lang)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && (n.CAT_POSITION == position || position == -1) && (n.CAT_RANK == rank || rank == -1) && n.CAT_LANGUAGE == lang && (n.CAT_PERIOD == catperiod || catperiod == -1)).OrderBy(n => n.CAT_ID).OrderByDescending(n => n.CAT_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_CATEGORy> Loadmenu1(int position, int limit, int rank, int catperiod, int lang)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && (n.CAT_POSITION == position || position == -1) && (n.CAT_RANK == rank || rank == -1) && n.CAT_LANGUAGE == lang && (n.CAT_PERIOD == catperiod || n.CAT_PERIOD == 7)).OrderBy(n => n.CAT_ID).OrderByDescending(n => n.CAT_PERIOD_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_CATEGORy> LoadTopmenu(int position, int limit, int rank)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && n.CAT_POSITION == position && n.CAT_RANK == rank).OrderBy(n => n.CAT_ID).OrderByDescending(n => n.CAT_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_CATEGORy> Loadmenu_news(int position, int limit, int rank,int cat_type,int lang)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 
                    && n.CAT_POSITION == position 
                    && n.CAT_TYPE == cat_type
                    && n.CAT_LANGUAGE==lang).OrderBy(n => n.CAT_ID).OrderByDescending(n => n.CAT_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_CATEGORy> Loadmenu_news1(int position, int limit, int rank, int cat_type, int lang)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1
                    && n.CAT_POSITION == position
                    && n.CAT_TYPE == cat_type
                    && n.CAT_LANGUAGE == lang).OrderBy(n => n.CAT_ID).OrderByDescending(n => n.CAT_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_CATEGORy> Loadmenu2(object catid, int lang)
        {
            try
            {
                int id = Utils.CIntDef(catid);
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_PARENT_ID == id && n.CAT_LANGUAGE == lang).OrderByDescending(n => n.CAT_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Menu cap 2
        public IQueryable<ESHOP_CATEGORy> Menu2(object catid,int lang)
        {
            int id=Utils.CIntDef(catid);
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_PARENT_ID == id && n.CAT_LANGUAGE==lang).OrderByDescending(n => n.CAT_ORDER);
            return list.ToList().Count>0 ? list:null;
        }
        public int getCountChild(object cat_id,int lang)
        {
            int id = Utils.CIntDef(cat_id);
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_PARENT_ID == id && n.CAT_LANGUAGE==lang).ToList();
            return list.Count;
        }
        public List<ESHOP_CATEGORy> Loadmenu_footer(int pos)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SHOWFOOTER == pos).OrderByDescending(n=>n.CAT_ORDER).ToList();
            return list;
        }
        public List<ESHOP_CATEGORy> Loadmenu_top(int pos,int limit,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_POSITION == pos && n.CAT_LANGUAGE == lang).Take(limit).ToList();
            return list;
        }
        //Active menu
        #region Active menu
        public string Get_Cat_Seo_Url(string _seoUrl,int lang)
        {
            var rausach = from p in db.ESHOP_CATEGORies
                          where p.CAT_SEO_URL == _seoUrl && p.CAT_STATUS == 1 && p.CAT_LANGUAGE == lang
                          select p;
            int _catID = -1;

            if (rausach.ToList().Count > 0)
            {
                string cat_parent_path = rausach.ToList()[0].CAT_PARENT_PATH;

                string[] str = cat_parent_path.Split(',');

                if (str.Count() > 1)
                {
                    _catID = Utils.CIntDef(str[1]);
                }
                else
                {
                    _catID = Utils.CIntDef(rausach.ToList()[0].CAT_ID);
                }
            }

            else
            {
                var rausach1 = (from nc in db.ESHOP_NEWS_CATs
                                join c in db.ESHOP_CATEGORies on nc.CAT_ID equals c.CAT_ID
                                join n in db.ESHOP_NEWs on nc.NEWS_ID equals n.NEWS_ID
                                where n.NEWS_SEO_URL == _seoUrl && c.CAT_STATUS == 1 && c.CAT_LANGUAGE==lang
                                orderby c.CAT_RANK descending
                                select new
                                {
                                    c.CAT_PARENT_PATH,
                                    c.CAT_NAME,
                                    c.CAT_DESC,
                                    c.CAT_ID
                                }).Take(1);

                if (rausach1.ToList().Count > 0)
                {
                    string cat_parent_path_Max = rausach1.ToList()[0].CAT_PARENT_PATH;

                    string[] str = cat_parent_path_Max.Split(',');
                    if (str.Count() > 1)
                    {
                        _catID = Utils.CIntDef(str[1]);
                    }
                    else
                    {
                        _catID = Utils.CIntDef(rausach1.ToList()[0].CAT_ID);
                    }
                }
            }
            var _cat_Seo_Url = db.GetTable<ESHOP_CATEGORy>().Where(a => a.CAT_ID == _catID && a.CAT_STATUS == 1&&a.CAT_LANGUAGE==lang);
            if (_cat_Seo_Url.ToList().Count > 0)
            {
                string _catSeoUrl = _cat_Seo_Url.ToList()[0].CAT_SEO_URL;
                return _catSeoUrl;
            }
            else
            {
                return null;
            }
        }
        public string GetStyleActive(object Cat_Seo_Url, object Cat_Url,int lang)
        {
            try
            {
                if (!string.IsNullOrEmpty(Utils.CStrDef(HttpContext.Current.Request.QueryString["curl"])))
                {
                    string _curl = Utils.CStrDef(HttpContext.Current.Request.QueryString["curl"]);

                    var _cat = db.GetTable<ESHOP_CATEGORy>().Where(a => a.CAT_SEO_URL == _curl && a.CAT_STATUS == 1&& a.CAT_LANGUAGE==lang);
                    if (_cat.ToList().Count > 0)
                    {
                        if (_cat.ToList()[0].CAT_RANK == 1)
                        {
                            if (Utils.CStrDef(HttpContext.Current.Request.QueryString["curl"]) == Utils.CStrDef(Cat_Seo_Url))
                            {
                                return "active";
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            int _catID = -1;
                            string[] str = Utils.CStrDef(_cat.ToList()[0].CAT_PARENT_PATH).Split(',');

                            if (str.Count() > 1)
                            {
                                _catID = Utils.CIntDef(str[1]);

                                var _cat_Seo_Url = db.GetTable<ESHOP_CATEGORy>().Where(a => a.CAT_ID == _catID && a.CAT_STATUS == 1&&a.CAT_LANGUAGE==lang);
                                if (_cat_Seo_Url.ToList().Count > 0)
                                {
                                    string _catSeoUrl = _cat_Seo_Url.ToList()[0].CAT_SEO_URL;

                                    if (_catSeoUrl == Utils.CStrDef(Cat_Seo_Url))
                                    {
                                        return "active";
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    string s ="/"+Utils.CStrDef(Cat_Seo_Url)+".html";
                    //string _seoUrl = fm.CatChuoiURL(Utils.CStrDef(Request.RawUrl));
                    string _seoUrl = Utils.CStrDef(HttpContext.Current.Request.QueryString["curl"]);
                    if (!string.IsNullOrEmpty(_seoUrl))
                    {
                        string _catSeoUrl = Get_Cat_Seo_Url(_seoUrl,lang);
                        if (_catSeoUrl == Utils.CStrDef(Cat_Seo_Url))
                        {
                            return "active";
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        if (Utils.CStrDef(HttpContext.Current.Request.RawUrl) == s)
                        {
                            return "active";
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                //}
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        #endregion
        //Danh muc menu
        public List<ESHOP_CATEGORy> Load_danhmuc(int type, int rank,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_TYPE == type && n.CAT_RANK == rank && n.CAT_STATUS == 1 && n.CAT_LANGUAGE == lang).OrderByDescending(n => n.CAT_ORDER).ToList();
            return list;
        }
        public List<ESHOP_CATEGORy> Load_danhmuchome( int rank, int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n =>n.CAT_PERIOD==1 && n.CAT_RANK == rank && n.CAT_STATUS == 1  && n.CAT_LANGUAGE == lang).OrderByDescending(n => n.CAT_ORDER).ToList();
            return list;
        }
        public List<ESHOP_CATEGORy> Load_danhmuc_position(int type,int postion,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && (n.CAT_POSITION == postion || n.CAT_POSITION == 3) && n.CAT_TYPE == type && n.CAT_LANGUAGE == lang).OrderByDescending(n => n.CAT_ORDER).ToList();
            return list;
        }
        public List<ESHOP_CATEGORy> Load_danhmuc_search(int type ,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_TYPE == type && n.CAT_LANGUAGE == lang && n.CAT_STATUS == 1 && n.CAT_RANK == 1).OrderByDescending(n => n.CAT_ORDER).ToList();
            return list;
        }

        public List<ESHOP_CATEGORy> Load_menu_cat_news_seo(string cat_seo, string news_seo,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == Getactive_menudanhmuc(cat_seo, news_seo,lang)).ToList();
            return list;
        }
        public int Getactive_menudanhmuc(string cat_seo, string news_seo,int lang)
        {
            int cat_id = 0;
            if (!string.IsNullOrEmpty(cat_seo))
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == cat_seo && n.CAT_LANGUAGE == lang).ToList();
                if (list.Count > 0)
                {
                    if (list[0].CAT_RANK > 2)
                    {
                        string[] a = list[0].CAT_PARENT_PATH.Split(',');
                        cat_id = Utils.CIntDef(a[2]);
                    }
                    else cat_id = list[0].CAT_ID;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(news_seo))
                {
                    var list = (from a in db.ESHOP_NEWs
                                join b in db.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                                join c in db.ESHOP_CATEGORies on b.CAT_ID equals c.CAT_ID
                                where a.NEWS_SEO_URL == news_seo && c.CAT_LANGUAGE == lang
                                select new { c.CAT_ID, c.CAT_RANK, c.CAT_PARENT_PATH }).ToList();
                    if (list.Count > 0)
                    {
                        if (list[0].CAT_RANK > 2)
                        {
                            string[] a = list[0].CAT_PARENT_PATH.Split(',');
                            cat_id = Utils.CIntDef(a[2]);
                        }
                        else cat_id = list[0].CAT_ID;
                    }
                }
            }
            return cat_id;
        }
        //Load city-distric
       
        //Logo-sologan
        public List<ESHOP_BANNER> Load_logo_and_sologan(int limit,int lang)
        {
            try
            {
                 var _logoSlogan = (from a in db.ESHOP_BANNERs
                                    where a.BANNER_LANGUAGE==lang
                               select a).Take(limit).ToList();
                 return _logoSlogan;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //Logo or sologan
        public List<ESHOP_BANNER> Load_logo_or_sologan(string field,int limit)
        {
            try
            {
                var list = db.ESHOP_BANNERs.Where(n => n.BANNER_FIELD1 == field).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // Silder
        public List<ESHOP_AD_ITEM> Load_slider(int position, int limit)
        {
            try
            {
                var list = db.ESHOP_AD_ITEMs.Where(n => n.AD_ITEM_POSITION == position).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //Support yahoo+skype
        public List<ESHOP_ONLINE> Load_support(int lang)
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == 0&&n.ONLINE_LANGUAGE==lang).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_ONLINE> Load_support(int type, int lang)
        {
            try
            {
                var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == type).OrderByDescending(n => n.ONLINE_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Holine
        public List<ESHOP_ONLINE> LoadHTTT(int type, int limit)
        {
            var list = db.ESHOP_ONLINEs.Where(n => n.ONLINE_TYPE == type).OrderByDescending(n => n.ONLINE_ORDER).Take(limit).ToList();
            return list;
        }
        //Weblink
        public List<ESHOP_WEBLINK> Load_weblink()
        {
            var list = db.ESHOP_WEBLINKs.ToList();
            return list;
        }
        //Path
        #region Path

        /// <summary>
        /// Lấy đường dẫn và ghi chú về sản phẩm
        /// </summary>
        public string Getpath()
        {
            try
            {
                string _result = string.Empty;
                string requesturl = Utils.CStrDef(HttpContext.Current.Request.RawUrl);
               
                string cat_seo_url = CatChuoiURL(requesturl);
               
                if (cat_seo_url.Contains("html?p"))
                {
                    string[] a = cat_seo_url.Split('?');
                    cat_seo_url = a[0].Substring(0, a[0].Length - 5);
                }
                var rausach = from p in db.ESHOP_CATEGORies
                              where p.CAT_SEO_URL == cat_seo_url && p.CAT_STATUS == 1
                              select p;

                if (rausach.ToList().Count > 0)
                {

                    string cat_parent_path = rausach.ToList()[0].CAT_PARENT_PATH;

                    string[] str = cat_parent_path.Split(',');

                    if (str.Count() > 1)
                    {
                        _result = Convert_Name(str) + " / <a href='/" + rausach.ToList()[0].CAT_SEO_URL + ".html'>" + rausach.ToList()[0].CAT_NAME + "</a>";
                    }
                    else
                    {
                        if (rausach.ToList()[0].CAT_SHOWITEM > 0)
                        {
                            _result = " / <a href='/" + rausach.ToList()[0].CAT_SEO_URL + ".html'>" + rausach.ToList()[0].CAT_NAME + "</a>";
                        }
                        else
                        {
                            _result = " / <a href='/" + rausach.ToList()[0].CAT_SEO_URL + ".html'>" + rausach.ToList()[0].CAT_NAME + "</a>";
                        }
                    }
                }

                else
                {
                    var rausach1 = (from nc in db.ESHOP_NEWS_CATs
                                    join c in db.ESHOP_CATEGORies on nc.CAT_ID equals c.CAT_ID
                                    join n in db.ESHOP_NEWs on nc.NEWS_ID equals n.NEWS_ID
                                    where n.NEWS_SEO_URL == cat_seo_url && c.CAT_STATUS == 1
                                    orderby c.CAT_RANK descending
                                    select c).Take(1);
                    if (rausach1.ToList().Count > 0)
                    {
                        string cat_parent_path_Max = rausach1.ToList()[0].CAT_PARENT_PATH;

                        string[] str = cat_parent_path_Max.Split(',');
                        if (str.Count() > 1)
                        {
                            _result = Convert_Name(str) + " / <a href='/" + rausach1.ToList()[0].CAT_SEO_URL + ".html'>" + rausach1.ToList()[0].CAT_NAME + "</a>";
                        }
                        else
                        {
                            if (rausach1.ToList()[0].CAT_SHOWITEM > 0)
                            {
                                _result = " / <a href='/" + rausach1.ToList()[0].CAT_SEO_URL + ".html'>" + rausach1.ToList()[0].CAT_NAME + "</a>";
                            }
                            else
                            {
                                _result = " / <a href='/" + rausach1.ToList()[0].CAT_SEO_URL + ".html'>" + rausach1.ToList()[0].CAT_NAME + "</a>";
                            }
                        }

                    }
                }
                return _result;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
        /// <summary>
        /// Chuyển chuỗi kiểu số thành chuỗi kiểu chữ
        /// </summary>
        /// <param name="str">mảng chứa đường dẫn kiểu số</param>
        /// <returns>đường dẫn kiểu chữ</returns>
        public string Convert_Name(string[] str)
        {
            string s = "";

            try
            {
                int _value = 0;

                for (int i = 1; i < str.Count(); i++)
                {

                    _value = Utils.CIntDef(str[i]);

                    var rausach = (from r in db.ESHOP_CATEGORies
                                  where r.CAT_ID == _value && r.CAT_STATUS == 1
                                  select r).ToList();
                    //s += rausach.ToList()[0] + " > ";
                    if(rausach.Count>0)
                        s += " / <a href='/" + rausach.ToList()[0].CAT_SEO_URL + ".html'>" + rausach.ToList()[0].CAT_NAME + "</a>";
                }
                return s;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
        private string CatChuoiURL(string s)
        {
            string[] sep = { "/" };
            string[] sep1 = { " " };
            string[] t1 = s.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            string res = "";
            for (int i = (t1.Length>1 ? 1 : 0); i < t1.Length; i++)
            {
                string[] t2 = t1[i].Split(sep1, StringSplitOptions.RemoveEmptyEntries);
                if (t2.Length > 0)
                {
                    if (res.Length > 0)
                    {
                        res += "//";
                    }
                    res += t2[0];
                }
            }
            return res.Substring(0, res.Length - 5);
        }
        #endregion
        //Total product
        public int Total_product(int lang)
        {
            try
            {
                var list = db.ESHOP_NEWs.Where(n => n.NEWS_TYPE == 1 && n.NEWS_LANGUAGE==lang).ToList();
                return list.Count;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public int getCatid(string cat_seo,string _news_Seo,int lang)
        {
            int _catid=0;
            if (!String.IsNullOrEmpty(cat_seo))
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == cat_seo && n.CAT_LANGUAGE==lang).ToList();
                if (list.Count > 0)
                {
                    if (list[0].CAT_RANK > 1)
                    {
                        string[] a = list[0].CAT_PARENT_PATH.Split(',');
                        _catid = Utils.CIntDef(a[1]);
                    }
                    else _catid = list[0].CAT_ID;
                }
                else
                {
                    var list2 = (from a in db.ESHOP_NEWs
                                join b in db.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                                where a.NEWS_SEO_URL == _news_Seo && a.NEWS_LANGUAGE==lang
                                select new { b.ESHOP_CATEGORy.CAT_RANK, b.ESHOP_CATEGORy.CAT_ID, b.ESHOP_CATEGORy.CAT_PARENT_PATH }).ToList();
                    if (list2.Count > 0)
                    {
                        if (list2[0].CAT_RANK > 1)
                        {
                            string[] a = list2[0].CAT_PARENT_PATH.Split(',');
                            _catid = Utils.CIntDef(a[1]);
                        }
                        else _catid = list2[0].CAT_ID;
                    }
                }
            }
            
            return _catid;
        }
        public int getPosition(string cat_seo, string news_seo,int lang)
        {
            if (getType(cat_seo, news_seo,lang) == 0)
                return 4;
            return 5;
        }
        public int getType(string cat_seo, string news_seo,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == getCatid(cat_seo, news_seo,lang)).ToList();
            if (list.Count > 0)
                return Utils.CIntDef(list[0].CAT_TYPE);
            return 0;
        }
        //public string getVideoNews(object news_link)
        //{
        //    string _sResult = "";

        //    if (!String.IsNullOrEmpty(Utils.CStrDef(news_link)))
        //    {
        //        _sResult += "<iframe style='display: block; margin-left: auto; margin-right: auto; width:100%;height:320px'";
        //        _sResult += " src='http://www.youtube.com/embed/" + Get_Embed(Utils.CStrDef(news_link)) + "?rel=0' frameborder='0' width='100%' height='320px'></iframe>";
        //    }
        //    return _sResult;
        //}
        public string getImagesYoutube(object news_link,int lang)
        {
            if (!String.IsNullOrEmpty(Utils.CStrDef(news_link)))
            { 
                string []a=Utils.CStrDef(news_link).Split('=');
                return "http://img.youtube.com/vi/"+a[a.Length-1]+"/0.jpg";
            }
            return "";
        }
        public List<Pro_details_entity> getProinfo(int id,int lang)
        {
            List<Pro_details_entity> l = new List<Pro_details_entity>();
            var list = (from a in db.ESHOP_NEWs
                        join b in db.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                        where a.NEWS_ID == id && a.NEWS_LANGUAGE==lang
                        select new
                        {
                            a.NEWS_ID,
                            a.NEWS_TITLE,
                            a.NEWS_DESC,
                            a.NEWS_SEO_URL,
                            a.NEWS_URL,
                            a.NEWS_PRICE1,
                            a.UNIT_ID1,
                            a.UNIT_ID2,
                            a.NEWS_FIELD2,
                            a.NEWS_IMAGE3,
                            b.ESHOP_CATEGORy.CAT_SEO_URL
                        }).ToList();
            foreach (var i in list)
            {
                Pro_details_entity enti = new Pro_details_entity();
                enti.NEWS_ID = i.NEWS_ID;
                enti.NEWS_TITLE = i.NEWS_TITLE;
                enti.NEWS_DESC = i.NEWS_DESC;
                enti.NEWS_PRICE1 =Utils.CDecDef(i.NEWS_PRICE1);
                enti.NEWS_FIELD2 = i.NEWS_FIELD2;
                enti.NEWS_URL = i.NEWS_URL;
                enti.NEWS_SEO_URL = i.NEWS_SEO_URL;
                enti.CAT_SEO_URL = i.CAT_SEO_URL;
                enti.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                enti.UNIT_ID1 = Utils.CIntDef(i.UNIT_ID1);
                enti.UNIT_ID2 = Utils.CIntDef(i.UNIT_ID2);
                l.Add(enti);
            }
            return l;
        }
        public string getNameCate(int id,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == id && n.CAT_LANGUAGE == lang).ToList();
            if (list.Count > 0)
                return list[0].CAT_NAME;
            return "";
        }
        public bool checkChild(int cat_id,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_PARENT_ID == cat_id && n.CAT_LANGUAGE == lang).ToList();
            if (list.Count > 0)
                return true;
            return false;
        }
        public List<ESHOP_CATEGORy> Load_MenuByCat(object catid,int lang)
        {
            int id = Utils.CIntDef(catid);
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_PARENT_ID == id && n.CAT_LANGUAGE == lang).OrderByDescending(n => n.CAT_ORDER);
            return list.ToList();
        }
        public List<ESHOP_AD_ITEM> Load_slider1(int position, int limit,int lang)
        {
            try
            {
                var list = (from a in db.ESHOP_AD_ITEM_CATs
                            join b in db.ESHOP_AD_ITEMs on a.AD_ITEM_ID equals b.AD_ITEM_ID
                            where b.AD_ITEM_POSITION == position &&b.AD_ITEM_LANGUAGE==lang
                            select b).Distinct().Take(limit).ToList();
                //db.ESHOP_AD_ITEMs.Where(n => n.AD_ITEM_POSITION == position && n.AD_ITEM_LANGUAGE == lang).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
                return null;
            }
        }
        public List<ESHOP_AD_ITEM> Load_sliderByCatId(int catId, int position, int limit)
        {
            try
            {
                var list = (from a in db.ESHOP_AD_ITEMs
                            join b in db.ESHOP_AD_ITEM_CATs on a.AD_ITEM_ID equals b.AD_ITEM_ID
                            where a.AD_ITEM_POSITION == position && b.CAT_ID == catId
                            select a).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_NEW> GetListNewsByCatID(int Cat_Id, int Period)
        {
            var list = (from a in db.ESHOP_NEWS_CATs
                        join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                        where a.CAT_ID == Cat_Id && (b.NEWS_PERIOD == Period || Period == -1)
                        select b).ToList();
            return list;
        }

        public List<ESHOP_CATEGORy> GetCatByCatSeoUrl(string _catSeoUrl)
        {
            var cats = db.ESHOP_CATEGORies.Where(c => c.CAT_SEO_URL == _catSeoUrl).ToList();
            return cats;
        }

        public List<ESHOP_NEW> GetNewByNewSeoUrl(string _newSeoUrl)
        {
            var news = db.ESHOP_NEWs.Where(c => c.NEWS_SEO_URL == _newSeoUrl).ToList();
            return news;
        }
    }
}
