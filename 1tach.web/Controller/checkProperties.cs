using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class checkProperties
    {
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        Get_session setSession = new Get_session();
        //Default page
        public int Getcat_type(string News_Seo_Url)
        {
            int _type = 0;
            var list = (from a in db.ESHOP_NEWS_CATs
                        join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                        where b.NEWS_SEO_URL == News_Seo_Url
                        select new { a.ESHOP_CATEGORy.CAT_TYPE }).ToList();
            if (list.Count > 0)
            {
                _type = Utils.CIntDef(list[0].CAT_TYPE);
            }
            return _type;
        }
        public bool check_CatorNews(string url)
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_SEO_URL == url).Select(n => new { n.NEWS_ID }).ToList();
            if (list.Count > 0)
                return true;
            return false;
        }
        public bool check_ExitsCategories(string url)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == url).Select(n => new { n.CAT_ID }).ToList();
            if (list.Count > 0)
                return true;
            return false;
        }
        public bool checkDisplay(string url)
        {
            var list = db.ESHOP_CATEGORies.Where(n => n.CAT_SEO_URL == url).Select(n => new { n.CAT_SHOWITEM }).ToList();
            if (list.Count > 0)
            {
                if (list[0].CAT_SHOWITEM == 1)
                    return true;
            }
            return false;
        }
        //EMAIL AND ACCOUNT 
        public bool checkExistEmailCus(string _email)
        {
            var list = db.ESHOP_CUSTOMERs.Where(n => n.CUSTOMER_EMAIL == _email).Select(n => new { n.CUSTOMER_ID }).ToList();
            if (list.Count > 0)
                return true;
            return false;
        }
        public bool checkExistEmailNew(string _email)
        {
            var list = db.ESHOP_MAIL_RECIVEs.Where(n => n.MAIL_NAME == _email).Select(n => new { n.MAIL_ID }).ToList();
            if (list.Count > 0)
                return true;
            return false;
        }
        public bool isLoginSuccess(string _email, string pass)
        {
            //pass = clsFormat.MaHoaMatKhau(pass);
            //var list = db.ESHOP_CUSTOMERs.Where(n => n.CUSTOMER_EMAIL == _email&&n.CUSTOMER_PW==pass).ToList();
            //if (list.Count > 0)
            //{
            //    list[0].CUSTOMER_LASTLOGIN = DateTime.Now;
            //    db.SubmitChanges();
            //    setSession.Load_All_Cuss(_email);
            //    return true;
            //}
            return false;
        }
       
    }
}
