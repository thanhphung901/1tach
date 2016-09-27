using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class List_news
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        //public List<News_details_entity> Load_listnews(int _Catid)
        //{
        //    try
        //    {
        //        List<News_details_entity> l = new List<News_details_entity>();
        //        var list = (from a in db.ESHOP_NEWS_CATs
        //                    join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
        //                    join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
        //                    where (c.CAT_ID == _Catid || c.CAT_PARENT_PATH.Contains(_Catid.ToString()))
        //                    select new {c.CAT_SEO_URL, b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).ToList();
        //        foreach (var i in list)
        //        {
        //            News_details_entity pro = new News_details_entity();
        //            pro.NEWS_ID = i.NEWS_ID;
        //            pro.NEWS_TITLE = i.NEWS_TITLE;
        //            pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
        //            pro.NEWS_DESC = i.NEWS_DESC;
        //            pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
        //            pro.NEWS_URL = i.NEWS_URL;
        //            pro.NEWS_SEO_URL = i.CAT_SEO_URL;
        //            pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
        //            pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
        //            pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    
        //            l.Add(pro);
        //        }
        //        return l;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
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
        public List<News_details_entity> Load_listnews(int _Catid)
        {
            try
            {
                List<News_details_entity> l = new List<News_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where (c.CAT_ID == _Catid || c.CAT_PARENT_PATH.Contains(_Catid.ToString()))
                            select new { b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, c.CAT_SEO_URL,c.CAT_ID }).OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).ToList();
                foreach (var i in list)
                {
                    News_details_entity pro = new News_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.CAT_ID = i.CAT_ID;
                    l.Add(pro);
                }

                return l;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<News_details_entity> Load_listnews1(int _Catid)
        {
            try
            {
                List<News_details_entity> l = new List<News_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where (c.CAT_ID == _Catid || c.CAT_PARENT_PATH.Contains(_Catid.ToString())&& c.CAT_TYPE==0)
                            select new { b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, c.CAT_SEO_URL, c.CAT_ID }).OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).ToList();
                foreach (var i in list)
                {
                    News_details_entity pro = new News_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = FormatContentNews(i.NEWS_DESC,100);
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.CAT_ID = i.CAT_ID;
                    l.Add(pro);
                }

                return l;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_CATEGORy> loadlistnews_cate(int catid)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == catid).ToList();
            return list;
        }
        public List<ESHOP_CATEGORy> loadhinhanh(object  CAT_SEO_URL)
        {
            string _catseourl = Utils.CStrDef(CAT_SEO_URL);
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == _catseourl).ToList();
            return list;
        }
        //Load title
        public string Loadtitle(int _id)
        {
            try
            {
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == _id).Select(n => new { n.CAT_NAME }).ToList();
                if (list.Count > 0)
                {
                    return list[0].CAT_NAME;
                }
                return "";
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get sotin
        public int Getsotin(int catid)
        {
            int sotin =db.ESHOP_CATEGORies.Where(n => n.CAT_ID == catid).ToList().Count>0 ? Utils.CIntDef(db.ESHOP_CATEGORies.Where(n => n.CAT_ID == catid).First().CAT_PAGEITEM) : 0;
            return sotin;
        }
        //Thêm chức năng
        public List<Pro_details_entity> LoadHotNews(int type, int period, int limit, int lang)
        {
            try
            {
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where (b.NEWS_PERIOD >= 0 && b.NEWS_PERIOD <= 5) && b.NEWS_TYPE == type && b.NEWS_PERIOD == period && b.NEWS_LANGUAGE==lang
                            select new { b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE1, b.NEWS_IMAGE3, c.CAT_SEO_URL, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, b.NEWS_FIELD2 }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER_PERIOD).Take(limit).ToList();
                foreach (var i in list)
                {
                    Pro_details_entity pro = new Pro_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE1 = i.NEWS_IMAGE1;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                    pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    pro.NEWS_FIELD2 = i.NEWS_FIELD2;

                    l.Add(pro);
                }
                return l;

            }
            catch (Exception)
            {

                throw;
            }
        }
            //Tin Mới
        public List<Pro_details_entity> Loadtinmoi(int type, int limit, int lang)
        {
            try
            {
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where b.NEWS_TYPE == type && b.NEWS_LANGUAGE == lang
                            select new { b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE1, b.NEWS_IMAGE3, c.CAT_SEO_URL, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, b.NEWS_FIELD2 }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).Take(limit).ToList();
                foreach (var i in list)
                {
                    Pro_details_entity pro = new Pro_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE1 = i.NEWS_IMAGE1;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                    pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    pro.NEWS_FIELD2 = i.NEWS_FIELD2;

                    l.Add(pro);
                }
                return l;

            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
