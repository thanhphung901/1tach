using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using Model;
using vpro.functions;

namespace OneTach.UIs
{
    public partial class RightSide : System.Web.UI.UserControl
    {
        #region Declare
        Propertity per = new Propertity();
        News_details ndetail = new News_details();
        Function fun = new Function();
        public string _sNews_Seo_Url = string.Empty;
        Attfile att = new Attfile();
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        int _catID = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _catID = Utils.CIntDef(Session["Cat_id"]);
            if (!IsPostBack)
            {
                GetData();
            }
        }


        public void GetData()
        {
            try
            {
                var news = per.GetNewByNewSeoUrl(_sNews_Seo_Url);
                if (news.Count > 0)
                {
                    int _iNewId = Utils.CIntDef(news[0].NEWS_ID);
                    loadRightSide(_iNewId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void loadRightSide(int _newsID)
        {
            try
            {
                var _tinTucKhac = (from c in db.ESHOP_NEWS_CATs
                                   join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                                   join b in db.ESHOP_CATEGORies on c.CAT_ID equals b.CAT_ID
                                   where a.NEWS_ID != _newsID && b.CAT_ID == _catID
                                   select new { a.NEWS_PUBLISHDATE, a.NEWS_ID, a.NEWS_TITLE, a.NEWS_IMAGE3, a.NEWS_DESC, a.NEWS_SEO_URL, a.NEWS_URL, a.NEWS_ORDER, a.NEWS_ORDER_PERIOD }).Distinct()
                                   .OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).Take(8).Distinct();
                if (_tinTucKhac.ToList().Count > 0)
                {
                    lstRightSide.DataSource = _tinTucKhac;
                    lstRightSide.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}