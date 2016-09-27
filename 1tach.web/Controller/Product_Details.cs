using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;
using System.Web;
using System.IO;
using System.Web.UI;

namespace Controller
{
    public class Product_Details
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public List<Pro_details_entity> Load_Product_Detail(string _News_seo_url)
        {
            try
            {
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where (b.NEWS_SEO_URL==_News_seo_url)
                            select new {c.CAT_ID, c.CAT_SEO_URL,b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, b.NEWS_CODE,b.NEWS_FIELD2,b.NEWS_FIELD3,b.NEWS_FIELD4,b.NEWS_COUNT,b.UNIT_ID1,b.UNIT_ID2,b.UNIT_ID3 }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).ToList();
                foreach (var i in list)
                {
                    Pro_details_entity pro = new Pro_details_entity();
                    pro.CAT_ID = i.CAT_ID;
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.NEWS_ORDER =Utils.CIntDef(i.NEWS_ORDER);
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                    pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                    pro.NEWS_PUBLISHDATE =Utils.CDateDef(i.NEWS_PUBLISHDATE,DateTime.Now);
                    pro.NEWS_CODE = i.NEWS_CODE;
                    pro.NEWS_FIELD2 = i.NEWS_FIELD2;
                    pro.NEWS_FIELD3 = i.NEWS_FIELD3;
                    pro.NEWS_FIELD4 = i.NEWS_FIELD4;
                    pro.NEWS_COUNT =Utils.CIntDef(i.NEWS_COUNT);
                    pro.UNIT_ID1 = Utils.CIntDef(i.UNIT_ID1);
                    pro.UNIT_ID2 = Utils.CIntDef(i.UNIT_ID2);
                    pro.UNIT_ID3 = Utils.CIntDef(i.UNIT_ID3);
                    l.Add(pro);
                }
                return l;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<Pro_details_entity> Load_Product_Menu(string _News_seo_url)
        {
            try
            {
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where (b.NEWS_SEO_URL == _News_seo_url)
                            select new {c.CAT_PARENT_ID, c.CAT_ID, c.CAT_SEO_URL, b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, b.NEWS_CODE, b.NEWS_FIELD2, b.NEWS_FIELD3, b.NEWS_FIELD4, b.NEWS_COUNT, b.UNIT_ID1, b.UNIT_ID2, b.UNIT_ID3 }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).ToList();
                if (list.Count > 0)
                {
                    var list1 = (from a in db.ESHOP_NEWS_CATs
                                 join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                                 join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                                 where ( c.CAT_ID==list[0].CAT_PARENT_ID)
                                 select new { c.CAT_ID, c.CAT_SEO_URL, b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, b.NEWS_CODE, b.NEWS_FIELD2, b.NEWS_FIELD3, b.NEWS_FIELD4, b.NEWS_COUNT, b.UNIT_ID1, b.UNIT_ID2, b.UNIT_ID3 }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).ToList();
                    foreach (var i in list1)
                    {
                        Pro_details_entity pro = new Pro_details_entity();
                        pro.CAT_ID = i.CAT_ID;
                        pro.NEWS_ID = i.NEWS_ID;
                        pro.CAT_SEO_URL = i.CAT_SEO_URL;
                        pro.NEWS_TITLE = i.NEWS_TITLE;
                        pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                        pro.NEWS_DESC = i.NEWS_DESC;
                        pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                        pro.NEWS_URL = i.NEWS_URL;
                        pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
                        pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                        pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                        pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                        pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                        pro.NEWS_CODE = i.NEWS_CODE;
                        pro.NEWS_FIELD2 = i.NEWS_FIELD2;
                        pro.NEWS_FIELD3 = i.NEWS_FIELD3;
                        pro.NEWS_FIELD4 = i.NEWS_FIELD4;
                        pro.NEWS_COUNT = Utils.CIntDef(i.NEWS_COUNT);
                        pro.UNIT_ID1 = Utils.CIntDef(i.UNIT_ID1);
                        pro.UNIT_ID2 = Utils.CIntDef(i.UNIT_ID2);
                        pro.UNIT_ID3 = Utils.CIntDef(i.UNIT_ID3);
                        l.Add(pro);
                    }
                }
                return l;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // Show html
        public string Show_File_HTML(int _newsID,int type)
        {
            try
            {
                string pathFile;
                string strHTMLContent;
                string _result=string.Empty;
                if (_newsID > 0)
                {
                    var newsInfo = db.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == _newsID);

                    if (newsInfo.ToList().Count > 0)
                    {
                        if(type==1)
                        pathFile = HttpContext.Current.Server.MapPath(PathFiles.GetPathNews(_newsID) + "/" + newsInfo.ToList()[0].NEWS_FILEHTML);
                        else if(type==2) pathFile = HttpContext.Current.Server.MapPath(PathFiles.GetPathNews(_newsID) + "/" + newsInfo.ToList()[0].NEWS_FIELD1);
                        else if (type == 3) pathFile = HttpContext.Current.Server.MapPath(PathFiles.GetPathNews(_newsID) + "/" + newsInfo.ToList()[0].NEWS_FIELD2);
                        else pathFile = HttpContext.Current.Server.MapPath(PathFiles.GetPathNews(_newsID) + "/" + newsInfo.ToList()[0].NEWS_FIELD5);
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
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
        //Load similar
        public List<Pro_details_entity> Load_Similar_Product(string News_Seo_Url,int limit)
        {
            try
            {
                List<Pro_details_entity> l=new List<Pro_details_entity>();
                var _vCatID = from a in db.ESHOP_NEWS_CATs
                              join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                              join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                              where b.NEWS_SEO_URL == News_Seo_Url
                              select new { c.CAT_ID, c.CAT_PARENT_ID, b.NEWS_TYPE };
                if (_vCatID.ToList().Count > 0)
                {
                    var _vProduct = (from c in db.ESHOP_NEWS_CATs
                                     join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                                     where a.NEWS_TYPE == _vCatID.ToList()[0].NEWS_TYPE && (c.CAT_ID == _vCatID.ToList()[0].CAT_ID || c.CAT_ID == _vCatID.ToList()[0].CAT_PARENT_ID)
                                     && a.NEWS_SHOWINDETAIL == 1 && a.NEWS_SEO_URL != News_Seo_Url
                                     select new { a.NEWS_ID, a.NEWS_TITLE, a.NEWS_IMAGE2, a.NEWS_IMAGE3, a.NEWS_DESC, a.NEWS_SEO_URL, a.NEWS_URL, a.NEWS_ORDER, a.NEWS_ORDER_PERIOD, a.NEWS_PUBLISHDATE, a.NEWS_PRICE1, a.NEWS_PRICE2, a.NEWS_FIELD3 }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).Take(limit);
                    foreach (var i in _vProduct)
                    {
                        Pro_details_entity pro = new Pro_details_entity();
                        pro.NEWS_ID = i.NEWS_ID;
                        pro.NEWS_TITLE = i.NEWS_TITLE;
                        pro.NEWS_IMAGE2 = i.NEWS_IMAGE2;
                        pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                        pro.NEWS_DESC = i.NEWS_DESC;
                        pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                        pro.NEWS_URL = i.NEWS_URL;
                        pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
                        pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                        pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                        pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                        pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                        pro.NEWS_FIELD3 = i.NEWS_FIELD3;
                        
                        l.Add(pro);
                    }
                }
                return l;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        //Load random
        public List<Pro_details_entity> loadPro_Random(string _new_seo,int limit)
        {
            List<int> lpos = new List<int>();
            List<Pro_details_entity> l = new List<Pro_details_entity>();
            Random rdom = new Random();
            var getallList = (from a in db.ESHOP_NEWS_CATs
                        join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                        join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                              where b.NEWS_SEO_URL!=_new_seo && b.NEWS_TYPE==1
                        select new {c.CAT_SEO_URL, b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, b.NEWS_CODE, b.NEWS_FIELD2, b.NEWS_FIELD3 }) .Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).ToList();
            if (getallList.Count > 0)
            {
                for (int i = 0; i < limit; i++)
                {
                    int pos = 0;
                    if (getallList.Count > 1)
                    {
                         pos = rdom.Next(1, getallList.Count - 1);
                    }
                    if (!lpos.Contains(pos))
                    {
                        lpos.Add(pos);
                        Pro_details_entity pro = new Pro_details_entity();
                        pro.NEWS_ID = getallList[pos].NEWS_ID;
                        pro.CAT_SEO_URL = getallList[pos].CAT_SEO_URL;
                        pro.NEWS_TITLE = getallList[pos].NEWS_TITLE;
                        pro.NEWS_IMAGE3 = getallList[pos].NEWS_IMAGE3;
                        pro.NEWS_DESC = getallList[pos].NEWS_DESC;
                        pro.NEWS_SEO_URL = getallList[pos].NEWS_SEO_URL;
                        pro.NEWS_URL = getallList[pos].NEWS_URL;
                        pro.NEWS_ORDER = Utils.CIntDef(getallList[pos].NEWS_ORDER);
                        pro.NEWS_ORDER_PERIOD = Utils.CIntDef(getallList[pos].NEWS_ORDER_PERIOD);
                        pro.NEWS_PUBLISHDATE = Utils.CDateDef(getallList[pos].NEWS_PUBLISHDATE, DateTime.Now);
                        pro.NEWS_PRICE1 = Utils.CDecDef(getallList[pos].NEWS_PRICE1);
                        pro.NEWS_PRICE2 = Utils.CDecDef(getallList[pos].NEWS_PRICE2);

                        l.Add(pro);
                    }
                   
                }
            }
            return l;

        }
        //Load album img
        public List<ESHOP_NEWS_IMAGE> Load_albumimg(int _Newsid,int limit)
        {
            try
            {
                var list = db.ESHOP_NEWS_IMAGEs.Where(n => n.NEWS_ID == _Newsid).Take(limit).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Load comment
        public List<ESHOP_NEWS_COMMENT> Load_comment(int _Newsid)
        {
            try
            {
                var list = db.ESHOP_NEWS_COMMENTs.Where(n => n.NEWS_ID == _Newsid).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Insert comment
        public bool Insert_comment(int _Newsid, string name, string email, string content)
        {
            ESHOP_NEWS_COMMENT cm = new ESHOP_NEWS_COMMENT();
            cm.NEWS_ID = _Newsid;
            cm.COMMENT_CHECK = 0;
            cm.COMMENT_STATUS = 0;
            cm.COMMENT_NAME = name;
            cm.COMMENT_EMAIL = email;
            cm.COMMENT_CONTENT = content;
            cm.COMMENT_PUBLISHDATE = DateTime.Now;
            db.ESHOP_NEWS_COMMENTs.InsertOnSubmit(cm);
            db.SubmitChanges();
            return true;
        }
        //Get name categories
        public bool checkCateImg(object id)
        {
            int idcat = Utils.CIntDef(id);
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == idcat).ToList();
            if (list.Count > 0)
            {
                if (!String.IsNullOrEmpty(list[0].CAT_IMAGE1))
                    return true;
            }
            return false;
        }
        public string getnamecate(object id)
        {
            try
            {
                int idcat=Utils.CIntDef(id);
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == idcat).ToList();
                if (list.Count > 0)
                    return list[0].CAT_NAME;
                return "";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string getNameCateWithNews(object news_id)
        {
            int _id = Utils.CIntDef(news_id);
            var list = (from a in db.ESHOP_NEWS_CATs
                        join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                        where b.NEWS_ID == _id
                        select new
                        {
                            a.ESHOP_CATEGORy.CAT_NAME
                        }).ToList();
            if (list.Count > 0)
                return list[0].CAT_NAME;
            return "";
        }
        public string getCatUrlWithNews(object news_id)
        {
            int _id = Utils.CIntDef(news_id);
            var list = (from a in db.ESHOP_NEWS_CATs
                        join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                        where b.NEWS_ID == _id
                        select new
                        {
                            a.ESHOP_CATEGORy.CAT_SEO_URL
                        }).ToList();
            if (list.Count > 0)
                return "/" + list[0].CAT_SEO_URL + ".html";
            return "";
        }
        public string getimgCate(object id)
        {
            try
            {
                int idcat = Utils.CIntDef(id);
                var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == idcat).ToList();
                if (list.Count > 0)
                    return PathFiles.GetPathCategory(list[0].CAT_ID) + "/" + list[0].CAT_IMAGE1;
                return "";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string getSeourl(int idth)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_ID == idth).Select(n => new { n.CAT_SEO_URL}).ToList();
            if (list.Count > 0)
            {
                return "/"+list[0].CAT_SEO_URL+".html";
            }
            return "";
        }
        public string Getnameattfile(int news_id)
        {
            return PathFiles.GetPathNews(news_id) + "baogia_" + news_id + ".doc";
        }
        public List<ESHOP_CATEGORy> getListid_color(int news_id)
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_ID == news_id).ToList();
            string[] a = null;
            foreach (var i in list)
            {
                if(i.NEWS_FIELD2!=null)
                a = i.NEWS_FIELD2.Split(',');
            }
            if (a != null)
            {
                var listcolor = db.ESHOP_CATEGORies.Where(n => a.Contains(n.CAT_ID.ToString())).ToList();
                return listcolor;
            }
            return null;
        }
        public List<ESHOP_CATEGORy> getListid_size(int news_id)
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_ID == news_id).ToList();
            string[] a = null;
            foreach (var i in list)
            {
                if(i.NEWS_FIELD2!=null)
                a = i.NEWS_FIELD3.Split(',');
            }
            if (a != null)
            {
                var listsize = db.ESHOP_CATEGORies.Where(n => a.Contains(n.CAT_ID.ToString())).ToList();
                return listsize;
            }
            return null;
        }
        public void Addsee(string _news_seo)
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_SEO_URL == _news_seo).ToList();
            if (list.Count > 0)
            {
                if (String.IsNullOrEmpty(Utils.CStrDef(HttpContext.Current.Session["seeIP_" + _news_seo + ""])))
                {
                    if (list[0].NEWS_COUNT == null)
                        list[0].NEWS_COUNT = 1;
                    else
                        list[0].NEWS_COUNT++;
                    db.SubmitChanges();
                    HttpContext.Current.Session["seeIP_" + _news_seo + ""] = HttpContext.Current.Request.UserHostAddress;
                }
            }
           
        }
        public List<ESHOP_PROPERTy> loadArea()
        {
            var list = db.ESHOP_PROPERTies.Where(n => n.PROP_RANK == 2).ToList();
            return list;
        }
        public List<ESHOP_PROPERTy> loadAreaDis(int id)
        {
            var list = db.ESHOP_PROPERTies.Where(n => n.PROP_PARENT_ID == id).ToList();
            return list;
        }
        public string getGiaohang(int id)
        {
            string s = "";
            var list = db.ESHOP_PROPERTies.Where(n => n.PROP_ID==id).ToList();
            if (list.Count > 0)
            {
                string fee = list[0].PROP_SHIPPING_FEE > 0 ? String.Format("{0:0,0 VNĐ}", list[0].PROP_SHIPPING_FEE) : (list[0].PROP_SHIPPING_FEE==0 ? "miễn phí" : "Liên hệ");
                s += "<div class='ship'>Phí vận chuyển: " + fee +"</div>";
                s += "<div class='timedelivery'>" + list[0].PROP_DESC + "</div>";
            }
            return s;
        }
        public List<Pro_details_entity> loadSosanh(string _newseo,int limit)
        {
            int _catid = 0;
            var getcatid = (from a in db.ESHOP_NEWs
                           join b in db.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                           where a.NEWS_SEO_URL==_newseo
                           select new{b.CAT_ID}).ToList();
            if (getcatid.Count > 0)
                _catid =Utils.CIntDef(getcatid[0].CAT_ID);
            if (_catid != 0)
            {
                List<Pro_details_entity> l = new List<Pro_details_entity>();
                var list = (from a in db.ESHOP_NEWS_CATs
                            join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                            join c in db.ESHOP_CATEGORies on a.CAT_ID equals c.CAT_ID
                            where b.NEWS_SEO_URL != _newseo && c.CAT_ID==_catid
                            select new {c.CAT_SEO_URL, b.NEWS_ID, b.NEWS_TITLE, b.NEWS_IMAGE3, b.NEWS_PRICE1, b.NEWS_PRICE2, b.NEWS_DESC, b.NEWS_SEO_URL, b.NEWS_URL, b.NEWS_ORDER, b.NEWS_ORDER_PERIOD, b.NEWS_PUBLISHDATE, b.NEWS_CODE, b.NEWS_FIELD2, b.NEWS_FIELD3, b.NEWS_FIELD4, b.NEWS_COUNT, b.UNIT_ID1, b.UNIT_ID2, b.UNIT_ID3 }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).Take(limit).ToList();
                foreach (var i in list)
                {
                    Pro_details_entity pro = new Pro_details_entity();
                    pro.NEWS_ID = i.NEWS_ID;
                    pro.CAT_SEO_URL = i.CAT_SEO_URL;
                    pro.NEWS_TITLE = i.NEWS_TITLE;
                    pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                    pro.NEWS_DESC = i.NEWS_DESC;
                    pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                    pro.NEWS_URL = i.NEWS_URL;
                    pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
                    pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                    pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                    pro.NEWS_PRICE2 = Utils.CDecDef(i.NEWS_PRICE2);
                    pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                    pro.NEWS_CODE = i.NEWS_CODE;
                    
                    pro.NEWS_FIELD2 = i.NEWS_FIELD2;
                    pro.NEWS_FIELD3 = i.NEWS_FIELD3;
                    pro.NEWS_FIELD4 = i.NEWS_FIELD4;
                    pro.NEWS_COUNT = Utils.CIntDef(i.NEWS_COUNT);
                    pro.UNIT_ID1 = Utils.CIntDef(i.UNIT_ID1);
                    pro.UNIT_ID2 = Utils.CIntDef(i.UNIT_ID2);
                    pro.UNIT_ID3 = Utils.CIntDef(i.UNIT_ID3);
                    l.Add(pro);
                }
                return l;
            }
            return new List<Pro_details_entity>();
        }
        //Thêm Chức năng
    }
}
