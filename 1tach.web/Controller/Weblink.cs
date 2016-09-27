using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace Controller
{
    public class Weblink
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public List<ESHOP_WEBLINK> Loadweblink()
        {
            try
            {
                var list = db.ESHOP_WEBLINKs.ToList();
                return list;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<ESHOP_WEBLINK> Loadweblink1(int _order)
        {
            try
            {
                var list = db.ESHOP_WEBLINKs.Where(n => n.WEBSITE_LINKS_ORDER == _order).ToList(); ;
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_WEBLINK> Loadwebid(int webid)
        {
            try
            {
                var list = db.ESHOP_WEBLINKs.Where(n => n.WEBSITE_LINKS_ID==webid).ToList(); ;
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
