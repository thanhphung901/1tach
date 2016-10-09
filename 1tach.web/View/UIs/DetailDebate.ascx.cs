using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace OneTach.UIs
{
    public partial class DetailDebate : System.Web.UI.UserControl
    {
        #region Declare
        Product_Details pro_detail = new Product_Details();
        Propertity per = new Propertity();
        News_details ndetail = new News_details();
        Function fun = new Function();
        public string _sNews_Seo_Url = string.Empty;
        Attfile att = new Attfile();
        Comment cm = new Comment();
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        int _catID = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] cut_sNews_Seo_Url = Utils.CStrDef(Request.QueryString["curl"]).Split('/');
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
                getData();
                Addsee();
            }
        }

        #region Album
        public void Loadimgalbum(int _newsID, int limit, ref Repeater rp)
        {
            try
            {
                var list = pro_detail.Load_albumimg(_newsID, limit).ToList();
                if (list.Count > 0)
                {
                    rp.DataSource = list;
                    rp.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Loadimgalbum(int _newsID, int limit)
        {
            try
            {
                var list = pro_detail.Load_albumimg(_newsID, limit).ToList();
                if (list.Count > 0)
                {
                    re_hinh1.DataSource = list;
                    re_hinh1.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Addsee
        private void Addsee()
        {
            ndetail.Addsee(_sNews_Seo_Url);
        }
        #endregion
        #region My Function
        public string FomatDate(object News_publishdate)
        {
            return string.Format("{0:dd/MM/yyyy hh:mm tt}", News_publishdate);
        }
        public string Get_url()
        {
            return System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + _sNews_Seo_Url + ".html";
        }
        public void getData()
        {
            try
            {
                var news = per.GetNewByNewSeoUrl(_sNews_Seo_Url);
                if (news.Count > 0)
                {
                    int _iNewId = Utils.CIntDef(news[0].NEWS_ID);
                    lbNewsTitle.Text = Utils.CStrDef(news[0].NEWS_TITLE);
                    lblDesc.Text = Utils.CStrDef(news[0].NEWS_DESC);
                    lbDate.Text = String.Format("{0:dd/MM/yyyy hh:mm tt}", Utils.CStrDef(news[0].NEWS_PUBLISHDATE));
                    liHtml.Text = ndetail.Showfilehtm(_iNewId);
                    liCount.Text = news[0].NEWS_COUNT + "";

                    double dDebateNo = Utils.CDblDef(news[0].DEBATE_NO);
                    double dDebateYes = Utils.CDblDef(news[0].DEBATE_YES);
                    double dDebateTotal = dDebateNo + dDebateYes;
                    double dPerYes = (dDebateTotal / 100) * dDebateYes;
                    double dPerNo = 100 - dPerYes;

                    liDebate_Yes.Text = dPerYes + "%";
                    liDebate_No.Text = dPerNo + "%";
                    liDebate_Per.Text = "<div class='determinate' style='width: " + dPerYes + "%'></div>";

                    Tinkhac(_iNewId);
                    Loadimgalbum(_iNewId, 1);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Tinkhac(int _newsID)
        {
            try
            {
                var _tinTucKhac = (from c in db.ESHOP_NEWS_CATs
                                   join a in db.ESHOP_NEWs on c.NEWS_ID equals a.NEWS_ID
                                   join b in db.ESHOP_CATEGORies on c.CAT_ID equals b.CAT_ID
                                   where a.NEWS_ID != _newsID && b.CAT_ID == _catID
                                   select new { a.NEWS_PUBLISHDATE, a.NEWS_ID, a.NEWS_TITLE, a.NEWS_IMAGE3, a.NEWS_DESC, a.NEWS_SEO_URL, a.NEWS_URL, a.NEWS_ORDER, a.NEWS_ORDER_PERIOD }).Distinct().OrderByDescending(n => n.NEWS_PUBLISHDATE).OrderByDescending(n => n.NEWS_ORDER).Take(9).Distinct();
                if (_tinTucKhac.ToList().Count > 0)
                {
                    Rptinkhac.DataSource = _tinTucKhac;
                    Rptinkhac.DataBind();
                }
                else
                    dvOtherNews.Visible = false;
            }
            catch (Exception ex)
            {
            }
        }
        public string GetImageT(object News_Id, object News_Image1)
        {

            try
            {
                return fun.GetImageT_News(News_Id, News_Image1);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetLinkNews(object News_Url, object News_Seo_Url)
        {
            try
            {
                return fun.Getlink_News(News_Url, News_Seo_Url);
            }
            catch (Exception ex)
            {
                vpro.functions.clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string getDate(object News_PublishDate)
        {
            return fun.getDate(News_PublishDate);
        }
        #endregion

        #region Comment
        public string querys()
        {
            return LookCookie();
        }
        private string LookCookie()
        {
            HttpCookie Cookie = new HttpCookie("slmsrcd1");
            Cookie = Request.Cookies["slmsrcd1"];
            string strUser = "";
            if (Cookie != null && Cookie.Value != "" &&
                 Cookie.Value != null)
            {
                strUser = Cookie.Value.ToString();
            }
            return strUser;
        }
        protected void lbtnComment_Yes_Click(object sender, EventArgs e)
        {
            string sTitle = txtCmtTieuDe_Yes.Value.Trim();
            string sDesc = txtCmtNoiDung_Yes.Value.Trim();
            if (sTitle == "")
            {
                Response.Write("<script>alert('Xin vui lòng nhập tiêu đề!');</script>");
                return;
            }
            else if (sDesc == "")
            {
                Response.Write("<script>alert('Xin vui lòng nhập bình luận!');</script>");
                return;
            }
            else if (sDesc.Length < 10 || sDesc.Length > 1000)
            {
                Response.Write("<script>alert('Xin vui lòng nhập trong khoảng từ 10 đến 1000 ký tự!');</script>");
                return;
            }
            else
            {
                var news = per.GetNewByNewSeoUrl(_sNews_Seo_Url);
                if (news.Count > 0)
                {
                    int _iNewId = Utils.CIntDef(news[0].NEWS_ID);
                    bool b = cm.Addcomment(_iNewId, sTitle, sDesc, 1);
                    if (b)
                    {
                        LoadcommentYes(_iNewId);

                        txtCmtTieuDe_Yes.Value = txtCmtNoiDung_Yes.Value = "";

                        string strScript = "<script>";
                        strScript += "alert('Bạn đã gửi bình luận thành công!');";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
            }
        }
        protected void lbtnComment_No_Click(object sender, EventArgs e)
        {
            string sTitle = txtCmtTieuDe_No.Value.Trim();
            string sDesc = txtCmtNoiDung_No.Value.Trim();
            if (sTitle == "")
            {
                Response.Write("<script>alert('Xin vui lòng nhập tiêu đề!');</script>");
                return;
            }
            else if (sDesc == "")
            {
                Response.Write("<script>alert('Xin vui lòng nhập bình luận!');</script>");
                return;
            }
            else if (sDesc.Length < 10 || sDesc.Length > 1000)
            {
                Response.Write("<script>alert('Xin vui lòng nhập trong khoảng từ 10 đến 1000 ký tự!');</script>");
                return;
            }
            else
            {
                var news = per.GetNewByNewSeoUrl(_sNews_Seo_Url);
                if (news.Count > 0)
                {
                    int _iNewId = Utils.CIntDef(news[0].NEWS_ID);
                    bool b = cm.Addcomment(_iNewId, sTitle, sDesc, 0);
                    if (b)
                    {
                        LoadcommentNo(_iNewId);

                        txtCmtTieuDe_No.Value = txtCmtTieuDe_No.Value = "";

                        string strScript = "<script>";
                        strScript += "alert('Bạn đã gửi bình luận thành công!');";
                        strScript += "</script>";
                        Page.RegisterClientScriptBlock("strScript", strScript);
                    }
                }
            }
        }
        public void LoadcommentYes(int news_id)
        {
            var showYes = cm.Load_comment(news_id, 1, 100).ToList();
            int _countYes = showYes.Count;
            if (_countYes > 0)
            {
                rptComments_Yes.DataSource = showYes.OrderByDescending(n => n.COMMENT_ID);
                rptComments_Yes.DataBind();
            }
        }
        public void LoadcommentNo(int news_id)
        {
            var showNo = cm.Load_comment(news_id, 0, 100).ToList();
            int _counNo = showNo.Count;
            if (_counNo > 0)
            {
                rptComments_No.DataSource = showNo.OrderByDescending(n => n.COMMENT_ID);
                rptComments_No.DataBind();
            }
        }
        public string getCountParent(object idComment)
        {
            int _idComment = Utils.CIntDef(idComment);
            var count = db.ESHOP_NEWS_COMMENTs.Where(n => n.COMMENT_PARENT_ID == _idComment).Count();
            return count + "";
        }
        #endregion
    }
}