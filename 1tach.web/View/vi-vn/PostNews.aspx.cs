using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Controller;
using Model;
using OneTach.Components;
using vpro.functions;

namespace OneTach.vi_vn
{
    public partial class PostNews : System.Web.UI.Page
    {
        #region declare
        Config cf = new Config();
        int device = 0;
        setCookieDevice setckdv = new setCookieDevice();
        dbVuonRauVietDataContext DB = new dbVuonRauVietDataContext();
        List<NewsContent> _lstNewsContent = new List<NewsContent>();
        List<VoteContent> _lstVote = new List<VoteContent>();
        private int m_news_id = 0;
        int _type = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Name"] == null)
            {
                Response.Redirect("/");
            }

            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();
            headerDes.Name = "Description";
            headerKey.Name = "Keywords";
            var _configs = cf.Config_meta();

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
                header.Title = _configs.ToList()[0].CONFIG_TITLE;

                headerDes.Content = _configs.ToList()[0].CONFIG_DESCRIPTION;
                header.Controls.Add(headerDes);

                headerKey.Content = _configs.ToList()[0].CONFIG_KEYWORD;
                header.Controls.Add(headerKey);
            }
            else
            {
                header.Title = "1 tach | Đăng bài";

                headerDes.Content = "1 tach | Đăng bài";
                header.Controls.Add(headerDes);

                headerKey.Content = "1 tach | Đăng bài";
                header.Controls.Add(headerKey);
            }

            if (!IsPostBack)
            {
                LoadCategoryParent();
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

        #region My functions
        private void LoadCategoryParent()
        {
            try
            {
                var CatList = (
                                from t2 in DB.ESHOP_CATEGORies
                                where t2.CAT_RANK > 0
                                //&& t2.CAT_TYPE == 0 || ?t2.CAT_TYPE?
                                //&& (_type == 1 ? t2.CAT_TYPE == 1 : t2.CAT_TYPE==0 || t2.CAT_TYPE==2) 
                                && t2.CAT_STATUS == 1
                                //t2.CAT_TYPE==_type

                                //&& (_gtype != 1 ? (getCatid().Contains(t2.CAT_ID.ToString()) || getCatid().Contains(t2.CAT_PARENT_ID.ToString())) : true)
                                select new
                                {
                                    CAT_ID = t2.CAT_NAME == "------- Root -------" ? 0 : t2.CAT_ID,
                                    CAT_NAME = (string.IsNullOrEmpty(t2.CAT_CODE) ? t2.CAT_NAME : t2.CAT_NAME + "(" + t2.CAT_CODE + ")"),
                                    CAT_NAME_EN = (string.IsNullOrEmpty(t2.CAT_CODE_EN) ? t2.CAT_NAME_EN : t2.CAT_NAME_EN + "(" + t2.CAT_CODE_EN + ")"),
                                    CAT_PARENT_ID = t2.CAT_PARENT_ID,
                                    CAT_RANK = t2.CAT_RANK
                                }
                            );

                if (CatList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    DataTable tbl = DataUtil.LINQToDataTable(CatList);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    ddlCategory.DataSource = dsCat.Tables[0];
                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataBind();

                }
                else
                {
                    DataTable dt = new DataTable("Newtable");

                    dt.Columns.Add(new DataColumn("CAT_ID"));
                    dt.Columns.Add(new DataColumn("CAT_NAME"));
                    dt.Columns.Add(new DataColumn("CAT_NAME_EN"));

                    DataRow row = dt.NewRow();
                    row["CAT_ID"] = 0;
                    row["CAT_NAME"] = "--------Root--------";
                    row["CAT_NAME_EN"] = "--------Root--------";
                    dt.Rows.Add(row);

                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataSource = dt;
                    ddlCategory.DataBind();
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void LoadCate(int type, ref DropDownList dr)
        {
            var list = DB.ESHOP_CATEGORies.Where(n => n.CAT_TYPE == type && n.CAT_RANK == 2);
            dr.DataValueField = "CAT_ID";
            dr.DataTextField = "CAT_NAME";
            dr.DataSource = list;
            dr.DataBind();
            if (type == 2)
            {
                ListItem l = new ListItem("--- Chọn hãng sản xuất ---", "0");
                l.Selected = true;
                dr.Items.Insert(0, l);
            }
            else
            {
                ListItem l = new ListItem("--- Chọn xuất xứ ---", "0");
                l.Selected = true;
                dr.Items.Insert(0, l);
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
                    lstContentVote.Visible = false;

                    btnThemLuaChon.Visible = false;
                    break;
                // Load kieu Bau chon
                case 1:
                    List<VoteContent> lst2 = GetVoteNewsContent(_newsId);
                    lstContentVote.Visible = true;
                    lstContentNews.Visible = false;
                    lstContentVote.DataSource = lst2;
                    lstContentVote.DataBind();

                    btnBox.Visible = false;
                    btnThemDoanVan.Visible = false;
                    btnThemHinhAnh.Visible = false;
                    btnboxChuDe.Visible = false;
                    break;
                // Load kieu tranh luan
                case 2:
                    break;
                default: break;

            }
        }
        private void SaveInfo(string strLink = "")
        {
            try
            {
                //get image
                string News_Image2;

                if (trUploadImage2.Visible == true)
                {
                    if (fileImage2.PostedFile != null)
                    {
                        News_Image2 = Path.GetFileName(fileImage2.PostedFile.FileName);
                    }
                    else
                    {
                        News_Image2 = "";
                    }
                }
                else
                {
                    News_Image2 = hplImage2.Text;
                }

                string News_Image3;

                if (trUploadImage3.Visible == true)
                {
                    if (fileImage3.PostedFile != null)
                    {
                        News_Image3 = Path.GetFileName(fileImage3.PostedFile.FileName);
                    }
                    else
                    {
                        News_Image3 = "";
                    }
                }
                else
                {
                    News_Image3 = hplImage3.Text;
                }
                if (CheckError())
                {
                    if (m_news_id == 0)
                    {
                        _type = int.Parse(radType.SelectedValue.ToString());
                        //insert
                        ESHOP_NEW news_insert = new ESHOP_NEW();

                        news_insert.NEWS_TITLE = txtTitle.Value;
                        news_insert.NEWS_CODE = "";
                        //news_insert.UNIT_ID2 = Utils.CIntDef(Drhangsx.SelectedValue);
                        //news_insert.NEWS_VIDEO_URL = txtVideo.Value;                        
                        news_insert.NEWS_COUNT = 1;
                        //Kho
                        //news_insert.NEWS_QUANTITY = Utils.CIntDef(txtquantity.Value);
                        //news_insert.NEWS_INVENTORY = Utils.CIntDef(txtton.Value);

                        news_insert.NEWS_DESC = txtDesc.Value;
                        news_insert.NEWS_URL = "";
                        //news_insert.NEWS_TARGET = ddlTarget.SelectedValue;
                        news_insert.NEWS_SEO_URL = txtSeoUrl.Value;
                        news_insert.NEWS_SEO_TITLE = txtSeoTitle.Value;
                        news_insert.NEWS_SEO_KEYWORD = txtSeoKeyword.Value;
                        news_insert.NEWS_SEO_DESC = txtSeoDescription.Value;

                        //news_insert.UNIT_ID1 = Utils.CIntDef(drNhanHieu.SelectedValue);
                        news_insert.NEWS_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);
                        news_insert.NEWS_TYPE = int.Parse(radType.SelectedValue.ToString());
                        news_insert.NEWS_SHOWTYPE = Utils.CIntDef(rblStatus.SelectedValue);
                        news_insert.NEWS_PERIOD = Utils.CIntDef(rblNewsPeriod.SelectedValue);
                        //news_insert.NEWS_FIELD1 = Utils.CStrDef(rblField1.SelectedValue);
                        news_insert.NEWS_SHOWINDETAIL = Utils.CIntDef(rblShowDetail.SelectedValue);
                        news_insert.NEWS_FEEDBACKTYPE = Utils.CIntDef(rblFeefback.SelectedValue);
                        //news_insert.NEWS_COUNT = Utils.CIntDef(txtCount.Value);
                        //news_insert.NEWS_ORDER = Utils.CIntDef(txtOrder.Value);
                        //news_insert.NEWS_ORDER_PERIOD = Utils.CIntDef(txtOrderPeriod.Value);
                        //news_insert.NEWS_PRICE1 = Utils.CDecDef(txtPrice.Value);
                        //news_insert.NEWS_PRICE2 = Utils.CDecDef(Txtprice_promos.Value);
                        // news_insert.UNIT_ID2 = Utils.CIntDef(Drhangsx.SelectedValue);
                        //news_insert.UNIT_ID3 = Utils.CIntDef(txtcountbuy.Value);

                        //news_insert.NEWS_IMAGE1 = News_Image1;
                        //news_insert.NEWS_IMAGE2 = News_Image2;
                        //news_insert.NEWS_IMAGE3 = News_Image3;

                        news_insert.USER_ID = Utils.CIntDef(Session["USER_ID"]);
                        news_insert.NEWS_PUBLISHDATE = DateTime.Now;


                        // news_insert.NEWS_FIELD1 = txtStatus.Value;
                        //news_insert.NEWS_FIELD2 = txtbaohanh.Value;
                        //news_insert.NEWS_FIELD3 = Rdstatus.SelectedValue;
                        //news_insert.NEWS_FIELD4 = txtdesc_bot.Value;
                        //news_insert.NEWS_FIELD4 = txtManufacture.Value;
                        //news_insert.NEWS_FIELD5 = txtWeight.Value;


                        DB.ESHOP_NEWs.InsertOnSubmit(news_insert);
                        DB.SubmitChanges();

                        //update cat news
                        var _new = DB.GetTable<ESHOP_NEW>().OrderByDescending(g => g.NEWS_ID).Take(1);

                        m_news_id = _new.Single().NEWS_ID;
                        SaveNewsCategory(_new.Single().NEWS_ID);

                        if (_type == 0)
                        {
                            SaveNews(m_news_id);
                        }
                        else
                        {
                            if (_type == 1)
                            {
                                SaveVote(m_news_id);
                            }
                        }

                        //strLink = string.IsNullOrEmpty(strLink) ? "news.aspx?type=" + _type + "&news_id=" + m_news_id : strLink;
                        strLink = txtSeoUrl.Value.Trim() +".html";
                    }
                    if (trUploadImage2.Visible)
                    {
                        if (!string.IsNullOrEmpty(fileImage2.PostedFile.FileName))
                        {
                            string pathfile = Server.MapPath("/data/news/" + m_news_id);
                            string fullpathfile = pathfile + "/" + News_Image2;

                            if (!Directory.Exists(pathfile))
                            {
                                Directory.CreateDirectory(pathfile);
                            }

                            fileImage2.PostedFile.SaveAs(fullpathfile);
                        }
                    }

                    if (trUploadImage3.Visible)
                    {
                        if (!string.IsNullOrEmpty(fileImage3.PostedFile.FileName))
                        {
                            string pathfile = Server.MapPath("/data/news/" + m_news_id);
                            string fullpathfile = pathfile + "/" + News_Image3;

                            if (!Directory.Exists(pathfile))
                            {
                                Directory.CreateDirectory(pathfile);
                            }

                            fileImage3.PostedFile.SaveAs(fullpathfile);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                { Response.Redirect(strLink); }
            }
        }

        private void SaveNewsCategory(int NewsId)
        {
            try
            {
                ESHOP_NEWS_CAT nc = new ESHOP_NEWS_CAT();
                nc.CAT_ID = Utils.CIntDef(ddlCategory.SelectedValue);
                nc.NEWS_ID = NewsId;

                DB.ESHOP_NEWS_CATs.InsertOnSubmit(nc);
                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }


        private bool CheckExitsLink(string strLink)
        {
            try
            {
                var exits = (from c in DB.ESHOP_NEWs where c.NEWS_SEO_URL == strLink && c.NEWS_ID != m_news_id select c);

                if (exits.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;

            }

        }

        private bool CheckError()
        {
            //if (string.IsNullOrEmpty(txtStatus.Value))
            //{
            //    lblError.Text = "Tình trạng sản phẩm không được rỗng!";
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(txtOrigin.Value))
            //{
            //    lblError.Text = "Nhà nhập khẩu sản phẩm không được rỗng!";
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(txtManufacture.Value))
            //{
            //    lblError.Text = "Nhà sản xuất không được rỗng!";
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(txtWeight.Value))
            //{
            //    lblError.Text = "Trọng lượng sản phẩm không được rỗng!";
            //    return false;
            //}
            return true;
        }

        #endregion

        #region button Function
        protected void btnboxChuDe_OnClick(object sender, EventArgs e)
        {
            NewsContent con = new NewsContent(0);

            _lstNewsContent = GetListContent();
            _lstNewsContent.Add(con);
            lstContentNews.DataSource = null;

            lstContentNews.DataSource = _lstNewsContent;
            lstContentNews.DataBind();
            KeepImage();
        }

        private void KeepImage()
        {
            HtmlInputFile image1 = fileImage3;
            fileImage3 = image1;
        }

        protected void btnThemDoanVan_OnClick(object sender, EventArgs e)
        {
            NewsContent con = new NewsContent(1);
            _lstNewsContent = GetListContent();
            _lstNewsContent.Add(con);
            lstContentNews.DataSource = null;

            lstContentNews.DataSource = _lstNewsContent;
            lstContentNews.DataBind();
        }
        protected void btnThemHinhAnh_OnClick(object sender, EventArgs e)
        {
            NewsContent con = new NewsContent(2);
            _lstNewsContent = GetListContent();
            _lstNewsContent.Add(con);
            lstContentNews.DataSource = null;

            lstContentNews.DataSource = _lstNewsContent;
            lstContentNews.DataBind();
        }
        protected void btnBox_OnClick(object sender, EventArgs e)
        {
            NewsContent con = new NewsContent(3);
            _lstNewsContent = GetListContent();
            _lstNewsContent.Add(con);
            lstContentNews.DataSource = null;

            lstContentNews.DataSource = _lstNewsContent;
            lstContentNews.DataBind();
        }
        private List<NewsContent> GetListContent()
        {
            List<NewsContent> lst = new List<NewsContent>();
            int i = 0;
            foreach (ListViewDataItem lv in lstContentNews.Items)
            {
                TextBox txtTitlePara = lv.FindControl("txtTitlePara") as TextBox;
                TextBox txtNoiDung = lv.FindControl("txtNoiDung") as TextBox;
                TextBox txtBoxChuDe = lv.FindControl("txtBoxChuDe") as TextBox;
                TextBox hdImage = lv.FindControl("hdImage") as TextBox;
                TextBox txtBox = lv.FindControl("txtBox") as TextBox;
                HiddenField hdType = lv.FindControl("hdType") as HiddenField;

                NewsContent con = new NewsContent(0);
                if (txtTitlePara != null) con.Title = txtTitlePara.Text;
                if (txtBox != null) con.Box = txtBox.Text;
                con.Id = i;
                if (hdImage != null) con.Image = hdImage.Text.ToString();
                if (hdType != null)
                    con.Type = int.Parse(hdType.Value);
                if (txtNoiDung != null && txtBoxChuDe != null)
                {
                    con.Paragraph = con.Type == 0 ? txtBoxChuDe.Text : txtNoiDung.Text;
                }
                lst.Add(con);
                i++;
            }
            return lst;
        }


        #region ListView content News
        protected void lstContent_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }
        protected void lstContent_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "XoaDong")
            {
                int i = int.Parse(e.CommandArgument.ToString());
                List<NewsContent> lst = GetListContent();
                NewsContent news = lst.FirstOrDefault(p => p.Id == i);
                if (news != null)
                    lst.Remove(news);
                lstContentNews.DataSource = null;

                lstContentNews.DataSource = lst;
                lstContentNews.DataBind();
            }
        }
        #endregion
        #region Listview Vote
        protected void lstContentVote_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
        }

        protected void lstContentVote_OnItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "XoaDong")
            {
                int i = int.Parse(e.CommandArgument.ToString());
                List<VoteContent> lst = GetListVote();
                VoteContent news = lst.FirstOrDefault(p => p.ID == i);
                if (news != null)
                    lst.Remove(news);
                lstContentVote.DataSource = null;

                lstContentVote.DataSource = lst;
                lstContentVote.DataBind();
            }
        }

        private List<VoteContent> GetListVote()
        {
            List<VoteContent> lst = new List<VoteContent>();
            int i = 0;
            foreach (ListViewDataItem lv in lstContentVote.Items)
            {
                TextBox txtContent = lv.FindControl("txtContent") as TextBox;
                HiddenField hdId = lv.FindControl("hdID") as HiddenField;
                TextBox hdImage = lv.FindControl("hdImage") as TextBox;
                HiddenField hdVoteCount = lv.FindControl("hdVoteCount") as HiddenField;

                VoteContent con = new VoteContent();
                if (txtContent != null) con.Content = txtContent.Text;
                if (hdId != null) con.ID = int.Parse(hdId.Value);
                if (hdImage != null) con.Image = hdImage.Text.ToString();
                //if (hdVoteCount != null) con.Type = int.Parse(hdType.Value);
                lst.Add(con);
                i++;
            }
            return lst;
        }
        #endregion

        private List<VoteContent> GetVoteNewsContent(int newsId)
        {
            List<VoteContent> lst = new List<VoteContent>();
            List<TBL_VOTE_NEW> lstContent = DB.TBL_VOTE_NEWs.Where(t => t.NewsID == newsId).ToList();
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
            List<TBL_NEWS_CONTENT> lstContent = DB.TBL_NEWS_CONTENTs.Where(t => t.NewsID == newsId).OrderBy(t => t.ID).ToList();
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
        protected void btnThemLuaChon_OnClick(object sender, EventArgs e)
        {
            VoteContent con = new VoteContent();
            _lstVote = GetListVote();
            _lstVote.Add(con);
            lstContentVote.DataSource = null;

            lstContentVote.DataSource = _lstVote;
            lstContentVote.DataBind();
        }
        #endregion

        #region Save Content function
        private void SaveNews(int _m_news_ID)
        {
            List<NewsContent> lst = GetListContent();
            int i = 0;
            foreach (NewsContent con in lst)
            {
                TBL_NEWS_CONTENT news = new TBL_NEWS_CONTENT();
                //news.Title
                news.NewsID = _m_news_ID;
                news.Title = con.Title;
                news.Order = i;
                news.Type = con.Type;
                switch (con.Type)
                {
                    case 0:
                        news.Value_ = con.Paragraph;
                        break;
                    case 1:
                        news.Value_ = con.Paragraph;
                        break;
                    case 2:
                        news.Value_ = con.Image;
                        break;
                    case 3:
                        news.Value_ = con.Box;
                        break;
                }
                i++;

                DB.TBL_NEWS_CONTENTs.InsertOnSubmit(news);
                DB.SubmitChanges();
            }
        }
        private void SaveVote(int _m_news_ID)
        {
            List<VoteContent> lst = GetListVote();

            int i = 0;
            foreach (VoteContent con in lst)
            {
                TBL_VOTE_NEW news = new TBL_VOTE_NEW
                {
                    NewsID = _m_news_ID,
                    Content = con.Content,
                    Order = i,
                    Image = con.Image
                };
                //news.Title
                //news.Type = con.Type;
                i++;

                DB.TBL_VOTE_NEWs.InsertOnSubmit(news);
                DB.SubmitChanges();
            }
        }
        private void SaveContent()
        {
        }
        #endregion

        protected void radType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radType.SelectedValue)
            {
                case "0":
                    btnBox.Visible = true;
                    btnThemDoanVan.Visible = true;
                    btnThemHinhAnh.Visible = true;
                    btnboxChuDe.Visible = true;
                    btnThemLuaChon.Visible = false;
                    lstContentVote.Visible = false;
                    lstContentNews.Visible = true;
                    break;
                case "1":
                    btnBox.Visible = false;
                    btnThemDoanVan.Visible = false;
                    btnThemHinhAnh.Visible = false;
                    btnboxChuDe.Visible = false;
                    btnThemLuaChon.Visible = true;
                    lstContentVote.Visible = true;
                    lstContentNews.Visible = false;
                    break;
                case "2":
                    break;
                default:
                    break;
            }
        }

        protected void btnDelete2_OnClick(object sender, ImageClickEventArgs e)
        {
        }

        protected void btnDelete3_OnClick(object sender, ImageClickEventArgs e)
        {
        }

        protected void OnClick(object sender, EventArgs e)
        {
            
        }

        protected void btnLuu_OnClick(object sender, EventArgs e)
        {
            if (CheckExitsLink(txtSeoUrl.Value))
                lblError.Text = "Đã tồn tại Seo Url, vui lòng nhập Seo Url khác cho tin.";
            else
                SaveInfo();
        }
    }
}