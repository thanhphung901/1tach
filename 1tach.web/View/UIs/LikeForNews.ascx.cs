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
    public partial class LikeForNews : System.Web.UI.UserControl
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
            string[] cut_sNews_Seo_Url = Utils.CStrDef(Request.QueryString["curl"]).Split('/');//Lay duong duong ne, giong toyota e hay lam y
            if (cut_sNews_Seo_Url.Length > 1)
            {
                _sNews_Seo_Url = cut_sNews_Seo_Url[1];
            }
            else
            {
                _sNews_Seo_Url = cut_sNews_Seo_Url[0];
            }
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
                    hdNewIS.Value = _iNewId.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnRatHuuIch_OnClick(object sender, EventArgs e)
        {
            createNewsLike(1);
        }

        private void createNewsLike(int type)
        {
            TBL_NEWS_LIKE obj = new TBL_NEWS_LIKE();
            obj.NewsID = int.Parse(hdNewIS.Value);
            obj.Type = type;
            db.TBL_NEWS_LIKEs.InsertOnSubmit(obj);
            db.SubmitChanges();
        }

        protected void btnYeuThich_OnClick(object sender, EventArgs e)
        {
            createNewsLike(2);
        }

        protected void btnHAHA_OnClick(object sender, EventArgs e)
        {
            createNewsLike(3);
        }

        protected void btnDeThuong_OnClick(object sender, EventArgs e)
        {
            createNewsLike(4);
        }

        protected void btnDangSuyNgam_OnClick(object sender, EventArgs e)
        {
            createNewsLike(5);
        }

        protected void btnThuVi_OnClick(object sender, EventArgs e)
        {
            createNewsLike(6);
        }

        protected void btnWOW_OnClick(object sender, EventArgs e)
        {
            createNewsLike(7);
        }
    }
}