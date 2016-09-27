using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;
using System.Web;
using System.Web.UI;
using System.IO;

namespace Controller
{
    public class Home
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        //Pro or news hien thi trang chu
        public List<ESHOP_CATEGORy> Load_cate_index_all(int limit,int lang)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && n.CAT_PERIOD == 1 && n.CAT_LANGUAGE == lang).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_NEW> Loadindex(int type, int period, int limit)
        {
            try
            {
                var list = db.ESHOP_NEWs.Where(n => n.NEWS_PERIOD == period && n.NEWS_TYPE == type && n.NEWS_SHOWTYPE == 1).OrderByDescending(n => n.NEWS_ID).OrderByDescending(n => n.NEWS_ORDER_PERIOD).Take(limit).ToList();
                return list;

            }
            catch (Exception)
            {

                throw;
            }
        }
        #region[FormatContentNews]
        protected string FormatContentNews(string value, int count)
        {
            string _value = value;
            if (_value.Length >= count)
            {
                string ValueCut = _value.Substring(0, count - 3);
                string[] valuearray = ValueCut.Split(' ');
                string valuereturn = "";
                for (int i = 0; i < valuearray.Length - 1; i++)
                {
                    valuereturn = valuereturn + " " + valuearray[i];
                }
                return valuereturn + "...";
            }
            else
            {
                return _value;
            }
        }
        #endregion
       
        //Getpro cookie
        public List<Pro_details_entity> Loadpro_cookie(int type, List<string> listnews_url,int lang)
        {
            try
            {
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where b.NEWS_TYPE == type && listnews_url.Contains(b.NEWS_SEO_URL) && b.NEWS_LANGUAGE==lang
                            select new {c.CAT_SEO_URL, b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE1, b.NEWS_IMAGE3, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE}).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER_PERIOD).ToList();
                foreach (var i in list)
                {
                    Pro_details_entity pro = new Pro_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE1 = i.NEWS_IMAGE1;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                    pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                   
                    l.Add(pro);
                }
                return l;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Pro_details_entity> Loadpro_new(int type,int lang)
        {
            try
            {
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where b.NEWS_TYPE == type &&b.NEWS_LANGUAGE==lang
                            select new { b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE1, b.NEWS_IMAGE3, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE,c.CAT_SEO_URL }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER_PERIOD).ToList();
                foreach (var i in list)
                {
                    Pro_details_entity pro = new Pro_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE1 = i.NEWS_IMAGE1;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                    pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    l.Add(pro);
                }
                return l;

            }
            catch (Exception)
            {

                throw;
            }
        }
        //Categories hien tri trang chu
        public List<ESHOP_CATEGORy> Load_cate_index(int limit, int per)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && n.CAT_PERIOD == per).OrderByDescending(n => n.CAT_PERIOD_ORDER).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Load ajax cate news
        public List<ESHOP_CATEGORy> Load_cate_ajaxnews(int id,int lang)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_STATUS == 1 && n.CAT_ID == id && n.CAT_LANGUAGE == lang).OrderByDescending(n => n.CAT_PERIOD_ORDER).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<ESHOP_CATEGORy> Load_cate_index_rank2(object cat_id, int limit,int lang)
        {
            int id = Utils.CIntDef(cat_id);
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_PARENT_ID == id && n.CAT_LANGUAGE == lang).OrderByDescending(n => n.CAT_PERIOD_ORDER).Take(limit);
            return list.ToList().Count > 0 ? list : null;
        }
        public IQueryable<Pro_details_entity> Load_pro_index_cate(object catid,int skip,int limit,int lang)
        {
            try
            {
                int id = Utils.CIntDef(catid);
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where (c.CAT_ID == id || c.CAT_PARENT_PATH.Contains(id.ToString()) && c.CAT_LANGUAGE==lang) 
                            select new {c.CAT_SEO_URL, b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_PRICE2, b.NEWS_PRICE1, b.NEWS_URL, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER_PERIOD).Skip(skip).Take(limit).ToList();
                foreach (var i in list)
                {
                    Pro_details_entity pro = new Pro_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                    pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    
                    l.Add(pro);
                }
                return l.AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<Pro_details_entity> Load_news_index_cate(object catid, int skip, int limit,int lang)
        {
            try
            {
                int id = Utils.CIntDef(catid);
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where (c.CAT_ID == id || c.CAT_PARENT_PATH.Contains(id.ToString()) && c.CAT_LANGUAGE==lang)
                            select new {c.CAT_SEO_URL, b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_PRICE2, b.NEWS_PRICE1, b.NEWS_URL, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER_PERIOD).Skip(skip).Take(limit).ToList();
                foreach (var i in list)
                {
                    Pro_details_entity pro = new Pro_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                    pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    l.Add(pro);
                }
                return l.AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Tin hiển thị tức thời trang chủ
        public string Gettitle_Showfilehtml_index(int period,int lang)
        {
            try
            {
                string _result = string.Empty;
                var getnewsid = db.ESHOP_NEWs.Where(n => n.NEWS_PERIOD == period && n.NEWS_LANGUAGE==lang).Select(n => new { n.NEWS_TITLE }).Take(1).ToList();
                if (getnewsid.Count > 0)
                {
                    _result = getnewsid[0].NEWS_TITLE;
                }
                return _result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Getdesc_Showfilehtml_index(int period,int lang)
        {
            try
            {
                string _result = string.Empty;
                var getnewsid = db.ESHOP_NEWs.Where(n => n.NEWS_PERIOD == period && n.NEWS_LANGUAGE==lang).Select(n => new { n.NEWS_DESC }).Take(1).ToList();
                if (getnewsid.Count > 0)
                {
                    _result = getnewsid[0].NEWS_DESC;
                }
                return _result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string getLinkHtml(int period,int lang)
        {
            try
            {
                string _result = "";
                var getnewsid = (from a in db.ESHOP_NEWS_CATs
                                 join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                                 where b.NEWS_PERIOD == period && b.NEWS_LANGUAGE==lang
                                 select new
                                 {
                                    
                                     b.NEWS_SEO_URL,
                                     b.NEWS_URL
                                 }).ToList();
                if (getnewsid.Count > 0)
                {
                    _result = string.IsNullOrEmpty(getnewsid[0].NEWS_URL) ?  "/" + getnewsid[0].NEWS_SEO_URL + ".html" : getnewsid[0].NEWS_URL;
                }
                return _result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string getimgHtml(int period,int lang)
        {
            try
            {
                string _result = string.Empty;
                var list = db.ESHOP_NEWs.Where(n => n.NEWS_PERIOD == period && n.NEWS_LANGUAGE==lang).ToList();
                if (list.Count > 0)
                {
                    if (!String.IsNullOrEmpty(list[0].NEWS_IMAGE3))
                    {
                        _result = PathFiles.GetPathNews(list[0].NEWS_ID) + "/" + list[0].NEWS_IMAGE3;
                    }
                }
                return _result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Showfilehtml_index(int period,int lang)
        {
            try
            {
                int _newsID = 0;
                string _result = string.Empty;
                var getnewsid = db.ESHOP_NEWs.Where(n => n.NEWS_PERIOD == period && n.NEWS_LANGUAGE==lang).Select(n => new { n.NEWS_ID }).Take(1).ToList();
                if (getnewsid.Count > 0)
                {
                    _newsID = getnewsid[0].NEWS_ID;
                }
                string pathFile;
                string strHTMLContent;

                if (_newsID > 0)
                {

                    var newsInfo = db.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == _newsID && n.NEWS_LANGUAGE==lang);

                    if (newsInfo.ToList().Count > 0)
                    {

                        pathFile = HttpContext.Current.Server.MapPath(PathFiles.GetPathNews(_newsID) + "/" + newsInfo.ToList()[0].NEWS_FILEHTML);
                        if ((File.Exists(pathFile)))
                        {
                            StreamReader objNewsReader;
                            objNewsReader = new StreamReader(pathFile);
                            strHTMLContent = objNewsReader.ReadToEnd();
                            objNewsReader.Close();
                            _result = strHTMLContent;
                        }

                    }

                }
                return _result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Intro
        public List<IntroEntity> loadIntro(int period, int lang)
        {
            List<IntroEntity> l = new List<IntroEntity>();
            var list = (from a in db.ESHOP_NEWS_CATs
                        join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                        where b.NEWS_PERIOD == period && a.ESHOP_CATEGORy.CAT_LANGUAGE == lang
                        select new
                        {
                            b.NEWS_TITLE,
                            b.NEWS_ID,
                            b.NEWS_DESC,
                            b.NEWS_IMAGE3,
                            b.NEWS_FILEHTML,
                            b.NEWS_SEO_URL,
                            b.NEWS_URL
                        }).Take(1).ToList();
            if (list.Count > 0)
            {
                IntroEntity intro = new IntroEntity();
                intro._desc = list[0].NEWS_DESC;
                intro._title = list[0].NEWS_TITLE;
                intro._newsid = list[0].NEWS_ID;
                intro._url = string.IsNullOrEmpty(list[0].NEWS_URL) ? "/" + list[0].NEWS_SEO_URL + ".html" : list[0].NEWS_URL;
                if (!String.IsNullOrEmpty(list[0].NEWS_IMAGE3))
                {
                    intro._images = PathFiles.GetPathNews(list[0].NEWS_ID) + "/" + list[0].NEWS_IMAGE3;
                }
                string strHTMLContent = string.Empty;
                string pathFile = HttpContext.Current.Server.MapPath(PathFiles.GetPathNews(list[0].NEWS_ID) + "/" + list[0].NEWS_FILEHTML);
                if ((File.Exists(pathFile)))
                {
                    StreamReader objNewsReader;
                    objNewsReader = new StreamReader(pathFile);
                    strHTMLContent = objNewsReader.ReadToEnd();
                    objNewsReader.Close();
                }
                intro._html = strHTMLContent;
                l.Add(intro);
            }
            return l;
        }
        //Danh muc cate theo vi tri
        public List<ESHOP_CATEGORy> Load_danhmuc(int type, int postion, int limit,int lang)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_TYPE == type && n.CAT_STATUS == 1 && n.CAT_POSITION == postion && n.CAT_LANGUAGE == lang).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IQueryable<ESHOP_AD_ITEM> load_ads_cate(object cat_id, int pos,int lang)
        {
            int id = Utils.CIntDef(cat_id);
            var list = (from a in db.ESHOP_AD_ITEMs
                        join b in db.ESHOP_AD_ITEM_CATs on a.AD_ITEM_ID equals b.AD_ITEM_ID
                        where b.CAT_ID == id && a.AD_ITEM_POSITION == pos && a.AD_ITEM_LANGUAGE==lang
                        select a).OrderByDescending(n => n.AD_ITEM_ID);
            return list.ToList().Count > 0 ? list : null;
        }
        public List<ESHOP_CATEGORy> loadListCateID(int cat_id,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == cat_id && n.CAT_LANGUAGE == lang).ToList();
            return list;
        }
        public List<ESHOP_CATEGORy> loadtopCate(int pos,int lang)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_POSITION == pos && n.CAT_LANGUAGE == lang).ToList();
            return list;
        }
        private string Get_Embed(string s,int lang)
        {
            try
            {
                return s.Substring(s.Length - 11, 11);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
        public string Load_video(int period,int lang)
        {
            string _sResult = "";
            var _vGetVideo = db.GetTable<ESHOP_NEW>().Where(n => n.NEWS_PERIOD == period && n.NEWS_LANGUAGE==lang).Select(n => new { n.NEWS_VIDEO_URL, n.NEWS_ID }).Take(1).OrderByDescending(a => a.NEWS_ID);

            if (_vGetVideo.ToList().Count > 0)
            {
                _sResult += "<iframe style='display: block; margin-left: auto; margin-right: auto; width:100%;height:250px'";
               // _sResult += " src='http://www.youtube.com/embed/" + Get_Embed(_vGetVideo.ToList()[0].NEWS_VIDEO_URL) + "?rel=0' frameborder='0' width='100%'></iframe>";
            }
            return _sResult;
        }
    }
}
