﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class news : System.Web.UI.Page
    {
        #region Declare

        private int m_news_id = 0;
        int _type = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        int _gtype, _gid;
        List<NewsContent> _lstNewsContent = new List<NewsContent>();
        List<VoteContent> _lstVote = new List<VoteContent>();
        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            _gid = Utils.CIntDef(Session["GROUP_ID"]);
            _gtype = Utils.CIntDef(Session["GROUP_TYPE"]);
            m_news_id = Utils.CIntDef(Request["news_id"]);
            _type = Utils.CIntDef(Request["type"]);
            Hyperback.NavigateUrl = "news_list.aspx?type=" + _type;
            if (_type == 1)
            {
                //div_price.Visible = true;
                div_productinfo.Visible = true;
                hangsx.Visible = true;
                tinhtrang.Visible = true;

                btnBox.Visible = false;
                btnThemDoanVan.Visible = false;
                btnThemHinhAnh.Visible = false;

                lstContentNews.Visible = false;
            }
            else if (_type == 2)
            {
                //div_price.Visible = false;
                div_productinfo.Visible = false;
                hangsx.Visible = false;
                tinhtrang.Visible = false;

                pnNoiDung.Visible = false;
            }
            else
            {
                //div_price.Visible = false;
                div_productinfo.Visible = false;
                hangsx.Visible = false;
                tinhtrang.Visible = false;
                lstContentVote.Visible = false;
                btnThemLuaChon.Visible = false;
            }

            if (m_news_id == 0)
            {
                //dvDelete.Visible = false;
                //trImage1.Visible = false;
                trNewsFunction.Visible = false;
                //trImage3.Visible = false;
            }
            else
            {
                hplCatNews.HRef = "news_category.aspx?type=" + _type + "&news_id=" + m_news_id;
                hplEditorHTMl.HRef = "news_editor.aspx?type=" + _type + "&news_id=" + m_news_id;
                hplNewsAtt.HRef = "news_attachment.aspx?type=" + _type + "&news_id=" + m_news_id;
                hplAlbum.HRef = "news_images.aspx?type=" + _type + "&news_id=" + m_news_id;
                //bplNewsCopy.HRef = "news_copy.aspx?news_id=" + m_news_id;
                hplComment.HRef = "news_comment.aspx?type=" + _type + "&news_id=" + m_news_id;
                //hplCatProducts.HRef = "news_news.aspx?news_id=" + m_news_id;
            }

            if (!IsPostBack)
            {
                if (_type == 0)
                    rblNewsPeriod.SelectedValue = "0";
                else
                    rblNewsPeriod.SelectedValue = "1";
                LoadCate(2, ref Drhangsx);
                getInfo();
                LoadCategoryParent();
            }
        }

        #endregion
        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            if (CheckExitsLink(txtSeoUrl.Value))
                lblError.Text = "Đã tồn tại Seo Url, vui lòng nhập Seo Url khác cho tin.";
            else
                SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            if (CheckExitsLink(txtSeoUrl.Value))
                lblError.Text = "Đã tồn tại Seo Url, vui lòng nhập Seo Url khác cho chuyên mục.";
            else
                SaveInfo("news.aspx?type=" + _type);
        }
        protected void LbsaveClose_Click(object sender, EventArgs e)
        {
            if (CheckExitsLink(txtSeoUrl.Value))
                lblError.Text = "Đã tồn tại Seo Url, vui lòng nhập Seo Url khác cho chuyên mục.";
            else
                SaveInfo("news_list.aspx?type=" + _type);
        }
        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        protected void btnDelete1_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].NEWS_IMAGE1))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + n_info.ToList()[0].NEWS_IMAGE1);
                        n_info.ToList()[0].NEWS_IMAGE1 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "news.aspx?type=" + _type + "&news_id=" + m_news_id;
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
                    Response.Redirect(strLink);
            }
        }

        #endregion

        #region My functions
        private List<string> getCatid()
        {
            List<string> l = new List<string>();
            var list = DB.ESHOP_GROUP_CATs.Where(n => n.GROUP_ID == _gid).ToList();
            foreach (var i in list)
            {
                l.Add(Utils.CStrDef(i.CAT_ID));
            }
            return l;
        }
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
        //private void LoadUnits()
        //{
        //    try
        //    {
        //        var units = DB.GetTable<ESHOP_UNIT>();

        //        ddlUnit1.DataSource = units;
        //        ddlUnit1.DataTextField = "UNIT_NAME";
        //        ddlUnit1.DataValueField = "UNIT_ID";
        //        ddlUnit1.DataBind();

        //        ddlUnit2.DataSource = units;
        //        ddlUnit2.DataTextField = "UNIT_NAME";
        //        ddlUnit2.DataValueField = "UNIT_ID";
        //        ddlUnit2.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //    }
        //}

        private void getInfo()
        {
            try
            {
                //LoadUnits();
                Components.CpanelUtils.createItemTarget(ref ddlTarget);
                Components.CpanelUtils.createItemLanguage(ref rblLanguage);
                var G_info = (from n in DB.ESHOP_NEWs
                              join c in DB.ESHOP_NEWS_CATs on n.NEWS_ID equals c.NEWS_ID into t2_join
                              from c in t2_join.DefaultIfEmpty()
                              where n.NEWS_ID == m_news_id
                              select new
                              {
                                  n,
                                  c.CAT_ID
                              }
                            );

                if (G_info.ToList().Count > 0)
                {
                    trCat.Visible = false;
                    txtTitle.Value = G_info.ToList()[0].n.NEWS_TITLE;
                    txtcode.Value = G_info.ToList()[0].n.NEWS_CODE;
                    Drhangsx.SelectedValue = G_info.ToList()[0].n.UNIT_ID2.ToString();
                    //txtVideo.Value = G_info.ToList()[0].n.NEWS_VIDEO_URL;
                    //Kho
                    //txtquantity.Value = G_info.ToList()[0].n.NEWS_QUANTITY.ToString();
                    //txtton.Value = G_info.ToList()[0].n.NEWS_INVENTORY.ToString();
                    rblLanguage.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_LANGUAGE);
                    txtDesc.Value = G_info.ToList()[0].n.NEWS_DESC;
                    txtUrl.Value = G_info.ToList()[0].n.NEWS_URL;
                    ddlTarget.SelectedValue = G_info.ToList()[0].n.NEWS_TARGET;
                    rblStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SHOWTYPE);
                    rblNewsPeriod.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_PERIOD);
                    //rblField1.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_FIELD1);
                    rblFeefback.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_FEEDBACKTYPE);
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_ORDER, "1");
                    txtOrderPeriod.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_ORDER_PERIOD, "1");

                    Rdstatus.SelectedValue = Utils.CIntDef(G_info.ToList()[0].n.NEWS_FIELD3).ToString();
                    //txtdesc_bot.Value = G_info.ToList()[0].n.NEWS_FIELD4;
                    //txtvideo.Value = G_info.ToList()[0].n.NEWS_DIET;
                    //seo
                    txtSeoTitle.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SEO_TITLE);
                    txtSeoKeyword.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SEO_KEYWORD);
                    txtSeoDescription.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SEO_DESC);
                    txtSeoUrl.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SEO_URL);

                    //PRICE

                    //ddlUnit2.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.UNIT_ID2);

                    //Product Properties
                    // txtStatus.Value = G_info.ToList()[0].n.NEWS_FIELD1;
                    // txtWarranty.Value = G_info.ToList()[0].n.NEWS_FIELD2;
                    // txtOrigin.Value = G_info.ToList()[0].n.NEWS_FIELD3;
                    //txtManufacture.Value = G_info.ToList()[0].n.NEWS_FIELD4;
                    // txtWeight.Value = G_info.ToList()[0].n.NEWS_FIELD5;
                    //image
                    //if (!string.IsNullOrEmpty(G_info.ToList()[0].n.NEWS_IMAGE1))
                    //{
                    //    trUploadImage1.Visible = false;
                    //    trImage1.Visible = true;
                    //    Image1.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE1;
                    //    hplImage1.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE1;
                    //    hplImage1.Text = G_info.ToList()[0].n.NEWS_IMAGE1;
                    //}
                    //else
                    //{
                    //    trUploadImage1.Visible = true;
                    //    trImage1.Visible = false;
                    //}

                    //image2
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].n.NEWS_IMAGE2))
                    {
                        trUploadImage2.Visible = false;
                        trImage2.Visible = true;
                        Image2.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE2;
                        hplImage2.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE2;
                        hplImage2.Text = G_info.ToList()[0].n.NEWS_IMAGE2;
                    }
                    else
                    {
                        trUploadImage2.Visible = true;
                        trImage2.Visible = false;
                    }

                    //image3
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].n.NEWS_IMAGE3))
                    {
                        trUploadImage3.Visible = false;
                        trImage3.Visible = true;
                        Image3.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE3;
                        hplImage3.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE3;
                        hplImage3.Text = G_info.ToList()[0].n.NEWS_IMAGE3;
                    }
                    else
                    {
                        trUploadImage3.Visible = true;
                        trImage3.Visible = false;
                    }


                    // Load content
                    var newsType = G_info.ToList()[0].n.NEWS_TYPE;
                    int newsId = G_info.ToList()[0].n.NEWS_ID;
                    if (newsType != null)
                    {
                        LoadContent(newsId, newsType.Value);
                    }
                }
                else
                {
                    //trUploadImage1.Visible = true;
                    //trImage1.Visible = false;
                    //trUploadImage2.Visible = true;
                    //trImage2.Visible = false;
                    //trUploadImage3.Visible = true;
                    //trImage3.Visible = false;
                    LoadCategoryParent();
                    trCat.Visible = true;

                    // Create new list content of news
                    lstContentNews.DataSource = _lstNewsContent;
                    lstContentNews.DataBind();

                    // Create new list content of news
                    lstContentVote.DataSource = _lstVote;
                    lstContentVote.DataBind();
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
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

                        //insert
                        ESHOP_NEW news_insert = new ESHOP_NEW();

                        news_insert.NEWS_TITLE = txtTitle.Value;
                        news_insert.NEWS_CODE = txtcode.Value;
                        news_insert.UNIT_ID2 = Utils.CIntDef(Drhangsx.SelectedValue);
                        //news_insert.NEWS_VIDEO_URL = txtVideo.Value;                        
                        news_insert.NEWS_COUNT = 1;
                        //Kho
                        //news_insert.NEWS_QUANTITY = Utils.CIntDef(txtquantity.Value);
                        //news_insert.NEWS_INVENTORY = Utils.CIntDef(txtton.Value);

                        news_insert.NEWS_DESC = txtDesc.Value;
                        news_insert.NEWS_URL = txtUrl.Value;
                        news_insert.NEWS_TARGET = ddlTarget.SelectedValue;
                        news_insert.NEWS_SEO_URL = txtSeoUrl.Value;
                        news_insert.NEWS_SEO_TITLE = txtSeoTitle.Value;
                        news_insert.NEWS_SEO_KEYWORD = txtSeoKeyword.Value;
                        news_insert.NEWS_SEO_DESC = txtSeoDescription.Value;

                        //news_insert.UNIT_ID1 = Utils.CIntDef(drNhanHieu.SelectedValue);
                        news_insert.NEWS_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);
                        news_insert.NEWS_TYPE = _type;
                        news_insert.NEWS_SHOWTYPE = Utils.CIntDef(rblStatus.SelectedValue);
                        news_insert.NEWS_PERIOD = Utils.CIntDef(rblNewsPeriod.SelectedValue);
                        //news_insert.NEWS_FIELD1 = Utils.CStrDef(rblField1.SelectedValue);
                        news_insert.NEWS_SHOWINDETAIL = Utils.CIntDef(rblShowDetail.SelectedValue);
                        news_insert.NEWS_FEEDBACKTYPE = Utils.CIntDef(rblFeefback.SelectedValue);
                        //news_insert.NEWS_COUNT = Utils.CIntDef(txtCount.Value);
                        news_insert.NEWS_ORDER = Utils.CIntDef(txtOrder.Value);
                        news_insert.NEWS_ORDER_PERIOD = Utils.CIntDef(txtOrderPeriod.Value);
                        //news_insert.NEWS_PRICE1 = Utils.CDecDef(txtPrice.Value);
                        //news_insert.NEWS_PRICE2 = Utils.CDecDef(Txtprice_promos.Value);
                        // news_insert.UNIT_ID2 = Utils.CIntDef(Drhangsx.SelectedValue);
                        //news_insert.UNIT_ID3 = Utils.CIntDef(txtcountbuy.Value);

                        //news_insert.NEWS_IMAGE1 = News_Image1;
                        news_insert.NEWS_IMAGE2 = News_Image2;
                        news_insert.NEWS_IMAGE3 = News_Image3;

                        news_insert.USER_ID = Utils.CIntDef(Session["USER_ID"]);
                        news_insert.NEWS_PUBLISHDATE = DateTime.Now;


                        // news_insert.NEWS_FIELD1 = txtStatus.Value;
                        //news_insert.NEWS_FIELD2 = txtbaohanh.Value;
                        news_insert.NEWS_FIELD3 = Rdstatus.SelectedValue;
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

                        strLink = string.IsNullOrEmpty(strLink) ? "news.aspx?type=" + _type + "&news_id=" + m_news_id : strLink;
                    }
                    else
                    {
                        //update
                        var c_update = DB.GetTable<ESHOP_NEW>().Where(g => g.NEWS_ID == m_news_id);

                        if (c_update.ToList().Count > 0)
                        {
                            c_update.ToList()[0].NEWS_TITLE = txtTitle.Value;
                            c_update.ToList()[0].NEWS_CODE = txtcode.Value;
                            c_update.ToList()[0].UNIT_ID2 = Utils.CIntDef(Drhangsx.SelectedValue);
                            //c_update.ToList()[0].NEWS_VIDEO_URL = txtVideo.Value;
                            c_update.ToList()[0].NEWS_COUNT = 1;
                            //Kho
                            //c_update.ToList()[0].NEWS_QUANTITY =Utils.CIntDef(txtquantity.Value);
                            //c_update.ToList()[0].NEWS_INVENTORY = Utils.CIntDef(txtton.Value);
                            c_update.ToList()[0].NEWS_LANGUAGE = Utils.CIntDef(rblLanguage.SelectedValue);
                            c_update.ToList()[0].NEWS_DESC = txtDesc.Value;
                            c_update.ToList()[0].NEWS_URL = txtUrl.Value;
                            c_update.ToList()[0].NEWS_TARGET = ddlTarget.SelectedValue;
                            c_update.ToList()[0].NEWS_SEO_URL = txtSeoUrl.Value;
                            c_update.ToList()[0].NEWS_SEO_TITLE = txtSeoTitle.Value;
                            c_update.ToList()[0].NEWS_SEO_KEYWORD = txtSeoKeyword.Value;
                            c_update.ToList()[0].NEWS_SEO_DESC = txtSeoDescription.Value;

                            //c_update.ToList()[0].NEWS_FIELD2 = txtbaohanh.Value;
                            c_update.ToList()[0].NEWS_FIELD3 = Rdstatus.SelectedValue;
                            //c_update.ToList()[0].NEWS_FIELD4 = txtdesc_bot.Value;

                            c_update.ToList()[0].NEWS_TYPE = _type;
                            c_update.ToList()[0].NEWS_SHOWTYPE = Utils.CIntDef(rblStatus.SelectedValue);
                            c_update.ToList()[0].NEWS_PERIOD = Utils.CIntDef(rblNewsPeriod.SelectedValue);
                            //c_update.ToList()[0].NEWS_FIELD1 = Utils.CStrDef(rblField1.SelectedValue);
                            c_update.ToList()[0].NEWS_SHOWINDETAIL = Utils.CIntDef(rblShowDetail.SelectedValue);
                            c_update.ToList()[0].NEWS_FEEDBACKTYPE = Utils.CIntDef(rblFeefback.SelectedValue);
                            //c_update.ToList()[0].NEWS_COUNT = Utils.CIntDef(txtCount.Value);
                            c_update.ToList()[0].NEWS_ORDER = Utils.CIntDef(txtOrder.Value);
                            c_update.ToList()[0].NEWS_ORDER_PERIOD = Utils.CIntDef(txtOrderPeriod.Value);
                            //c_update.ToList()[0].NEWS_PRICE1 = Utils.CDecDef(txtPrice.Value);
                            //c_update.ToList()[0].NEWS_PRICE2 = Utils.CDecDef(Txtprice_promos.Value);
                            //c_update.ToList()[0].UNIT_ID2 = Utils.CIntDef(Drhangsx.SelectedValue);
                            //c_update.ToList()[0].UNIT_ID3 = Utils.CIntDef(txtcountbuy.Value);
                            //c_update.ToList()[0].NEWS_IMAGE1 = News_Image1;
                            c_update.ToList()[0].NEWS_IMAGE2 = News_Image2;
                            c_update.ToList()[0].NEWS_IMAGE3 = News_Image3;

                            DB.SubmitChanges();

                            strLink = string.IsNullOrEmpty(strLink) ? "news_list.aspx?type=" + _type + "" : strLink;

                            if (_type == 0)
                            {

                                DB.TBL_NEWS_CONTENTs.DeleteAllOnSubmit(DB.TBL_NEWS_CONTENTs.Where(t => t.NewsID == m_news_id).ToList());
                                DB.SubmitChanges();
                                SaveNews(m_news_id);
                            }
                            else
                            {
                                if (_type == 1)
                                {
                                    DB.TBL_VOTE_NEWs.DeleteAllOnSubmit(DB.TBL_VOTE_NEWs.Where(t => t.NewsID == m_news_id).ToList());
                                    DB.SubmitChanges();
                                    SaveVote(m_news_id);
                                }
                            }

                        }
                    }

                    //update images
                    //if (trUploadImage1.Visible)
                    //{
                    //    if (!string.IsNullOrEmpty(fileImage1.PostedFile.FileName))
                    //    {
                    //        string pathfile = Server.MapPath("/data/news/" + m_news_id);
                    //        string fullpathfile = pathfile + "/" + News_Image1;

                    //        if (!Directory.Exists(pathfile))
                    //        {
                    //            Directory.CreateDirectory(pathfile);
                    //        }

                    //        fileImage1.PostedFile.SaveAs(fullpathfile);
                    //    }

                    //}

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

        private void DeleteInfo()
        {
            string strLink = "";
            try
            {
                var G_info = DB.GetTable<ESHOP_NEW>().Where(g => g.NEWS_ID == m_news_id);

                DB.ESHOP_NEWs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathNews(m_news_id));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

                strLink = "news_list.aspx?type=" + _type;

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                    Response.Redirect(strLink);
            }
        }

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        public string getLink(object GroupId)
        {
            return "news.aspx?type=" + _type + "&news_id=" + Utils.CStrDef(GroupId);
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

        #endregion

        #region button Function
        protected void btnDelete2_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].NEWS_IMAGE2))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + n_info.ToList()[0].NEWS_IMAGE2);
                        n_info.ToList()[0].NEWS_IMAGE2 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "news.aspx?type=" + _type + "&news_id=" + m_news_id;
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
                    Response.Redirect(strLink);
            }
        }
        protected void btnDelete3_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].NEWS_IMAGE3))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + n_info.ToList()[0].NEWS_IMAGE3);
                        n_info.ToList()[0].NEWS_IMAGE3 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "news.aspx?type=" + _type + "&news_id=" + m_news_id;
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
                    Response.Redirect(strLink);
            }
        }
        protected void btnboxChuDe_OnClick(object sender, EventArgs e)
        {
            NewsContent con = new NewsContent(0);
            _lstNewsContent = GetListContent();
            _lstNewsContent.Add(con);
            lstContentNews.DataSource = null;

            lstContentNews.DataSource = _lstNewsContent;
            lstContentNews.DataBind();
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
                int i = Utils.CIntDef(e.Item.DataItemIndex);
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
        public System.Drawing.Image Base64ToImage(string base64String)
        {
            base64String = base64String.Replace("data:image/png;base64,", String.Empty);
            base64String = base64String.Replace("data:image/jpg;base64,", String.Empty);
            base64String = base64String.Replace("data:image/jpeg;base64,", String.Empty);
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
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
                        {                            
                            string sBase64 = Utils.CStrDef(con.Image);
                            if(sBase64 != "")
                            {
                                // Tên file image lấy Id của NewsId + '-' + random
                                string sNameImage = _m_news_ID + "-" + clsFormat.Generate_Random_String(5);
                                if (sBase64.Contains("data:image/png;base64"))
                                {
                                    sNameImage = sNameImage + ".png";
                                }
                                else if (sBase64.Contains("data:image/jpg;base64") || sBase64.Contains("data:image/jpeg;base64"))
                                {
                                    sNameImage = sNameImage + ".jpg";
                                }
                                else if (sBase64.Contains("data:image/gif;base64"))
                                {
                                    sNameImage = sNameImage + ".gif";
                                }
                                string pathfile = Server.MapPath("/data/news/" + m_news_id);
                                string fullpathfile = pathfile + "/" + sNameImage;
                                if (!Directory.Exists(pathfile))
                                {
                                    Directory.CreateDirectory(pathfile);
                                }
                                Base64ToImage(sBase64).Save(fullpathfile);
                                news.Value_ = sNameImage;
                            }
                            break;
                        }
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

        public string getImg(object fileImg)
        {
            if (Utils.CStrDef(fileImg) != "")
                return "/data/news/" + m_news_id + "/" + fileImg;
            else return "";
        }
        #endregion
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