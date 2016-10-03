using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using vpro.functions;

namespace OneTach.UIs
{
    public partial class DetailNews : System.Web.UI.UserControl
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
                getData();
                Addsee();
                LoadSocial();
            }
        }
        #region Addsee
        private void Addsee()
        {
            //cho nay de luu ne khi co nguoi vao xem
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
                    lbDate.Text = String.Format("{0:dd/MM/yyyy hh:mm tt}", Utils.CStrDef(news[0].NEWS_PUBLISHDATE));
                    liHtml.Text = ndetail.Showfilehtm(_iNewId);
                    liCount.Text = news[0].NEWS_COUNT + "";//So luong nguoi da xem bai nay
                    //hplViewmore.HRef = ndetail.Get_ViewMore(_iNewId);
                    Tinkhac(_iNewId);

                    var newsType = news[0].NEWS_TYPE;
                    if (newsType != null) LoadContent(news[0].NEWS_ID, newsType.Value);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Tinkhac(int _newsID)
        {//Co san rui 
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
        private void LoadSocial()
        {
            var lface = per.LoadHTTT(3, 1);
            if (lface.Count > 0)
            {
                ltl_fanpage.Text = "<div class='fb-comments' data-href='" + lface[0].ONLINE_NICKNAME +
                                   "' data-width='100%' data-numposts='5' ></div>";
            }
        }

        private void LoadContent(int _newsId, int _type)
        {
            switch (_type)
            {
                // Load kieu tin tuc
                case 0:
                    List<NewsContent> lst = GetContentNews(_newsId);
                    lstContentNews.Visible = true;
                    lstContentNews.DataSource = lst;
                    lstContentNews.DataBind();

                    pnForVote.Visible = false;
                    break;
                // Load kieu Bau chon
                case 1:
                    List<VoteContent> lst2 = GetVoteNewsContent(_newsId);
                    pnForVote.Visible = true;
                    lstContentNews.Visible = false;
                    lstVoteContent.DataSource = lst2;
                    lstVoteContent.DataBind();
                    break;
                // Load kieu tranh luan
                case 2:
                    break;
                default: break;

            }
        }

        private List<VoteContent> GetVoteNewsContent(int newsId)
        {
            List<VoteContent> lst = new List<VoteContent>();
            List<TBL_VOTE_NEW> lstContent = db.TBL_VOTE_NEWs.Where(t => t.NewsID == newsId).ToList();
            foreach (var value in lstContent)
            {
                VoteContent vote = new VoteContent();
                vote.ID = value.ID;
                vote.Image = value.Image;
                vote.Content = value.Content;
                if (value.Order != null) vote.Order = value.Order.Value;
                lst.Add(vote);
            }
            return lst;
        }

        private List<NewsContent> GetContentNews(int newsId)
        {
            List<NewsContent> lst = new List<NewsContent>();
            List<TBL_NEWS_CONTENT> lstContent = db.TBL_NEWS_CONTENTs.Where(t => t.NewsID == newsId).OrderBy(t => t.ID).ToList();
            foreach (var value in lstContent)
            {
                NewsContent con = new NewsContent(0);
                con.Type = value.Type;
                switch (value.Type)
                {
                    case 0:
                        con.Paragraph = value.Value_;
                        con.Id = value.ID;
                        break;
                    case 1:
                        con.Title = value.Title;
                        con.Id = value.ID;
                        con.Paragraph = value.Value_;
                        break;
                    case 2:
                        con.Image = value.Value_;
                        con.Title = value.Title;
                        con.Id = value.ID;
                        break;
                    case 3:
                        con.Box = value.Value_;
                        con.Id = value.ID;
                        break;
                }
                lst.Add(con);
            }
            return lst;
        }
    }

    class NewsContent
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Paragraph { get; set; }
        public string Image { get; set; }
        public string Box { get; set; }

        public NewsContent(int _type)
        {
            this.Id = 0;
            this.Type = _type;
            this.Title = "";
            this.Box = "";
            this.Image = "";
            this.Paragraph = "";
        }
    }
    class VoteContent
    {
        public int ID { get; set; }
        public int NewsID { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int VotedCount { get; set; }
        public int Order { get; set; }
    }
}