using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.Linq.SqlClient;
using vpro.functions;
using System.Text.RegularExpressions;

namespace Controller
{
    public class Search_result
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public List<Pro_details_entity> Load_search_result1(string _txt, int type, int _idcat)
        {
            List<Pro_details_entity> l = new List<Pro_details_entity>();
            var list = (from c in db.ESHOP_NEWS_CATs
                        join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                        join b in db.ESHOP_CATEGORies on c.CAT_ID equals b.CAT_ID
                        where (SqlMethods.Like(db.fClearUnicode(a.NEWS_TITLE), ClearUnicode(_txt)) || a.NEWS_CODE.Contains(_txt.Replace("%", string.Empty)) || "" == _txt || "%%" == _txt)
                          && a.NEWS_TYPE == type && (_idcat != 0 ? (b.CAT_ID == _idcat || b.CAT_PARENT_PATH.Contains(_idcat.ToString())) : _idcat == 0)
                        select new { a.NEWS_ID, a.NEWS_TITLE, a.NEWS_IMAGE3, a.NEWS_PRICE1, a.NEWS_PRICE2, a.NEWS_DESC, a.NEWS_SEO_URL, a.NEWS_URL, a.NEWS_ORDER, a.NEWS_ORDER_PERIOD, a.NEWS_PUBLISHDATE, a.NEWS_FIELD3 }).Distinct().OrderByDescending(n => n.NEWS_ID).OrderByDescending(n => n.NEWS_ORDER);
            foreach (var i in list)
            {
                Pro_details_entity pro = new Pro_details_entity();
                pro.NEWS_ID = i.NEWS_ID;
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
                pro.NEWS_FIELD3 = i.NEWS_FIELD3;
                pro.CAT_SEO_URL = "";
                l.Add(pro);
            }
            return l;

        }
        public List<Pro_details_entity> Load_search_result(string _txt,string _cat_code, int type)
        {
            List<Pro_details_entity> l = new List<Pro_details_entity>();
            var list = (from c in db.ESHOP_NEWS_CATs
                        join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                        join b in db.ESHOP_CATEGORies on c.CAT_ID equals b.CAT_ID
                        where (SqlMethods.Like( a.NEWS_TITLE, _txt)|| "" == _txt || "%%" == _txt)
                        && a.NEWS_TYPE == type
                        select new {a.NEWS_PRICE2, a.NEWS_ID, a.NEWS_TITLE, a.NEWS_IMAGE3, a.NEWS_PRICE1, a.NEWS_DESC, a.NEWS_SEO_URL, a.NEWS_URL, a.NEWS_ORDER, a.NEWS_ORDER_PERIOD, a.NEWS_PUBLISHDATE, b.CAT_SEO_URL }).Distinct().OrderByDescending(n => n.NEWS_ID).OrderByDescending(n => n.NEWS_ORDER);
            foreach (var i in list)
            {
                Pro_details_entity pro = new Pro_details_entity();
                pro.NEWS_ID = i.NEWS_ID;
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
                pro.CAT_SEO_URL = i.CAT_SEO_URL;
                l.Add(pro);
            }
            return l;

        }
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
        public List<Pro_details_entity> Load_search_result1(string _txt, int type)
        {
            List<Pro_details_entity> l = new List<Pro_details_entity>();
            var list = (from c in db.ESHOP_NEWS_CATs
                        join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                        join b in db.ESHOP_CATEGORies on c.CAT_ID equals b.CAT_ID
                        where (SqlMethods.Like(db.fClearUnicode(a.NEWS_TITLE), ClearUnicode(_txt)) || "" == _txt || "%%" == _txt)
                        && a.NEWS_TYPE == type
                        select new {a.NEWS_CODE, a.NEWS_ID, a.NEWS_TITLE, a.NEWS_IMAGE3, a.NEWS_PRICE1, a.NEWS_DESC, a.NEWS_SEO_URL, a.NEWS_URL, a.NEWS_ORDER, a.NEWS_ORDER_PERIOD, a.NEWS_PUBLISHDATE, b.CAT_SEO_URL }).Distinct().OrderByDescending(n => n.NEWS_ID).OrderByDescending(n => n.NEWS_ORDER);
            foreach (var i in list)
            {
                Pro_details_entity pro = new Pro_details_entity();
                pro.NEWS_CODE = i.NEWS_CODE;
                pro.NEWS_ID = i.NEWS_ID;
                pro.NEWS_TITLE = i.NEWS_TITLE;
                pro.NEWS_IMAGE3 = i.NEWS_IMAGE3;
                pro.NEWS_DESC = FormatContentNews( i.NEWS_DESC,100);
                pro.NEWS_SEO_URL = i.NEWS_SEO_URL;
                pro.NEWS_URL = i.NEWS_URL;
                pro.NEWS_ORDER = Utils.CIntDef(i.NEWS_ORDER);
                pro.NEWS_ORDER_PERIOD = Utils.CIntDef(i.NEWS_ORDER_PERIOD);
                pro.NEWS_PRICE1 = Utils.CDecDef(i.NEWS_PRICE1);
                pro.NEWS_PUBLISHDATE = Utils.CDateDef(i.NEWS_PUBLISHDATE, DateTime.Now);
                pro.CAT_SEO_URL = i.CAT_SEO_URL;
                l.Add(pro);
            }
            return l;

        }
        private  string ClearUnicode(string SourceString)
        {

            SourceString = Regex.Replace(SourceString, "[ÂĂÀÁẠẢÃÂẦẤẬẨẪẰẮẶẲẴàáạảãâầấậẩẫăằắặẳẵ]", "a");
            SourceString = Regex.Replace(SourceString, "[ÈÉẸẺẼÊỀẾỆỂỄèéẹẻẽêềếệểễ]", "e");
            SourceString = Regex.Replace(SourceString, "[IÌÍỈĨỊìíịỉĩ]", "i");
            SourceString = Regex.Replace(SourceString, "[ÒÓỌỎÕÔỒỐỔỖỘƠỜỚỞỠỢòóọỏõôồốộổỗơờớợởỡ]", "o");
            SourceString = Regex.Replace(SourceString, "[ÙÚỦŨỤƯỪỨỬỮỰùúụủũưừứựửữ]", "u");
            SourceString = Regex.Replace(SourceString, "[ỲÝỶỸỴỳýỵỷỹ]", "y");
            SourceString = Regex.Replace(SourceString, "[đĐ]", "d");

            return SourceString;
        }
        public virtual List<CategoryEntityComplete> searchComplete(string searchitem)
        {
            List<CategoryEntityComplete> l = new List<CategoryEntityComplete>();
            var list = (from a in db.ESHOP_NEWs
                        join b in db.ESHOP_NEWS_CATs on a.NEWS_ID equals b.NEWS_ID
                        where (db.fClearUnicode(a.NEWS_TITLE).Contains(ClearUnicode(searchitem)) && a.NEWS_TYPE == 1) 
                        select new
                        {
                            a.NEWS_TITLE,
                            a.NEWS_PUBLISHDATE,
                            b.ESHOP_CATEGORy.CAT_NAME
                        }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n=>n.CAT_NAME).Take(10);
            foreach (var i in list)
            {
                CategoryEntityComplete enti = new CategoryEntityComplete();
                enti.catname = i.CAT_NAME;
                enti.title = i.NEWS_TITLE;
                l.Add(enti);
            }
            return l;
        }
    }
}
