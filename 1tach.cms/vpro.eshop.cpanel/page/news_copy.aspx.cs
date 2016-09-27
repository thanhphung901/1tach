using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

namespace vpro.eshop.cpanel.page
{
    public partial class news_copy : System.Web.UI.Page
    {
        #region Declare

        private int m_news_id = 0;
        int _type = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        int _gtype, _gid;
        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            _gid = Utils.CIntDef(Session["GROUP_ID"]);
            _gtype = Utils.CIntDef(Session["GROUP_TYPE"]);
            m_news_id = Utils.CIntDef(Request["news_id"]);
            _type = Utils.CIntDef(Request["type"]);
            Hyperback.NavigateUrl = "news_list.aspx?type=" + _type;
            if (m_news_id == 0)
            {
                //dvDelete.Visible = false;
                //trImage1.Visible = false;
                trNewsFunction.Visible = false;
                trImage3.Visible = false;
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
                                // && (_type == 0 ? (t2.CAT_TYPE != 1) : t2.CAT_TYPE == 1)
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
                    if (CatList.ToList()[0].CAT_RANK > 1)
                        ddlCategory.DataSource = CatList;
                    else
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
        //private void Loadhangsx()
        //{
        //    var list = DB.ESHOP_CATEGORies.Where(n => n.CAT_TYPE == 2 && n.CAT_RANK == 2);
        //    Drhangsx.DataValueField = "CAT_ID";
        //    Drhangsx.DataTextField = "CAT_NAME";
        //    Drhangsx.DataSource = list;
        //    Drhangsx.DataBind();
        //    ListItem l = new ListItem("--- Chọn hãng sản xuất ---", "0");
        //    l.Selected = true;
        //    Drhangsx.Items.Insert(0, l);
        //}
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
                    //Kho
                    //txtquantity.Value = G_info.ToList()[0].n.NEWS_QUANTITY.ToString();
                    //txtton.Value = G_info.ToList()[0].n.NEWS_INVENTORY.ToString();

                    txtDesc.Value = G_info.ToList()[0].n.NEWS_DESC;
                    txtUrl.Value = G_info.ToList()[0].n.NEWS_URL;
                    ddlTarget.SelectedValue = G_info.ToList()[0].n.NEWS_TARGET;
                    txtPrice.Value = Utils.CStrDef(Utils.CIntDef(G_info.ToList()[0].n.NEWS_PRICE1));
                    //Txtprice_promos.Value = Utils.CStrDef(Utils.CIntDef(G_info.ToList()[0].n.NEWS_PRICE2));
                    //drNhanHieu.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.UNIT_ID1);
                    //Drhangsx.SelectedValue=G_info.ToList()[0].n.UNIT_ID2.ToString();
                    //txtcountbuy.Value = G_info.ToList()[0].n.UNIT_ID3.ToString();
                    //rblNewsType.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_TYPE);
                    rblStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_SHOWTYPE);
                    rblNewsPeriod.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_PERIOD);
                    //rblField1.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_FIELD1);
                    rblFeefback.SelectedValue = Utils.CStrDef(G_info.ToList()[0].n.NEWS_FEEDBACKTYPE);
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_ORDER, "1");
                    txtOrderPeriod.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_ORDER_PERIOD, "1");
                    //txtCount.Value = Utils.CStrDef(G_info.ToList()[0].n.NEWS_COUNT);
                    //lblCount.Text = string.IsNullOrEmpty(Utils.CStrDef(G_info.ToList()[0].n.NEWS_COUNT)) ? "0" : Utils.CStrDef(G_info.ToList()[0].n.NEWS_COUNT);
                    //lblSendEmail.Text = G_info.ToList()[0].n.NEWS_SENDDATE == null ? "Chưa gửi" : "Gửi lần cuối vào " + string.Format("{0:dd/MM/yyyy HH:mm:ss}", G_info.ToList()[0].n.NEWS_SENDDATE);
                    //txtbaohanh.Value = G_info.ToList()[0].n.NEWS_FIELD2;
                    //Rdstatus.SelectedValue = Utils.CIntDef(G_info.ToList()[0].n.NEWS_FIELD3).ToString();
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

                    ////image2
                    //if (!string.IsNullOrEmpty(G_info.ToList()[0].n.NEWS_IMAGE2))
                    //{
                    //    trUploadImage2.Visible = false;
                    //    trImage2.Visible = true;
                    //    Image2.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE2;
                    //    hplImage2.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].n.NEWS_IMAGE2;
                    //    hplImage2.Text = G_info.ToList()[0].n.NEWS_IMAGE2;
                    //}
                    //else
                    //{
                    //    trUploadImage2.Visible = true;
                    //    trImage2.Visible = false;
                    //}

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

                }
                else
                {
                    //trUploadImage1.Visible = true;
                    //trImage1.Visible = false;
                    //trUploadImage2.Visible = true;
                    //trImage2.Visible = false;
                    trUploadImage3.Visible = true;
                    trImage3.Visible = false;
                    LoadCategoryParent();
                    trCat.Visible = true;
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveInfo(string strLink = "")
        {
            try
            {
                if (m_news_id > 0)
                {

                    //insert
                    ESHOP_NEW news_insert = new ESHOP_NEW();

                    news_insert.NEWS_TITLE = txtTitle.Value;
                    news_insert.NEWS_CODE = txtcode.Value;

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

                    //news_insert.NEWS_TYPE = Utils.CIntDef(rblNewsType.SelectedValue);
                    news_insert.NEWS_SHOWTYPE = Utils.CIntDef(rblStatus.SelectedValue);
                    news_insert.NEWS_PERIOD = Utils.CIntDef(rblNewsPeriod.SelectedValue);
                    //news_insert.NEWS_FIELD1 = Utils.CStrDef(rblField1.SelectedValue);
                    news_insert.NEWS_SHOWINDETAIL = Utils.CIntDef(rblShowDetail.SelectedValue);
                    news_insert.NEWS_FEEDBACKTYPE = Utils.CIntDef(rblFeefback.SelectedValue);
                    //news_insert.NEWS_COUNT = Utils.CIntDef(txtCount.Value);
                    news_insert.NEWS_ORDER = Utils.CIntDef(txtOrder.Value);
                    news_insert.NEWS_ORDER_PERIOD = Utils.CIntDef(txtOrderPeriod.Value);
                    news_insert.NEWS_PRICE1 = Utils.CDecDef(txtPrice.Value);
                    //news_insert.NEWS_PRICE2 = Utils.CDecDef(Txtprice_promos.Value);
                    //news_insert.UNIT_ID2 = Utils.CIntDef(Drhangsx.SelectedValue);
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

                    //save catefory
                    SaveNewsCategory(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsImage.SelectedValue) == 1 || Utils.CIntDef(rblNewsAlbum.SelectedValue) == 1 || Utils.CIntDef(rblNewsContent.SelectedValue) == 1 || Utils.CIntDef(rblNewsAtt.SelectedValue) == 1)
                        copyAllFiles(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsImage.SelectedValue) == 1)
                        copyImage(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsAlbum.SelectedValue) == 1)
                        copyAlbums(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsContent.SelectedValue) == 1)
                        copyHtml(_new.Single().NEWS_ID);

                    if (Utils.CIntDef(rblNewsAtt.SelectedValue) == 1)
                        copyAtt(_new.Single().NEWS_ID);

                    strLink = string.IsNullOrEmpty(strLink) ? "news.aspx?news_id=" + _new.Single().NEWS_ID : strLink;
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
        private bool CheckExitsLink(string strLink)
        {
            try
            {
                var exits = (from c in DB.ESHOP_NEWs where c.NEWS_SEO_URL == strLink select c);

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

        private void copyAllFiles(int id)
        {
            try
            {
                string pathCopy = Server.MapPath(PathFiles.GetPathNews(id));
                string pathOld = Server.MapPath(PathFiles.GetPathNews(m_news_id));

                if (!Directory.Exists(pathCopy))
                {
                    Directory.CreateDirectory(pathCopy);
                }

                //copy all file
                {
                    string fileName = "";
                    string destFile = "";

                    string[] files = Directory.GetFiles(pathOld);
                    foreach (string s in files)
                    {
                        fileName = Path.GetFileName(s);
                        destFile = Path.Combine(pathCopy, fileName);
                        File.Copy(s, destFile);
                    }
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyImage(int id)
        {
            try
            {
                string pathCopy = Server.MapPath(PathFiles.GetPathNews(id));
                string pathOld = Server.MapPath(PathFiles.GetPathNews(m_news_id));

                if (!Directory.Exists(pathCopy))
                {
                    Directory.CreateDirectory(pathCopy);
                }

                var _oldNews = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id);

                if (!string.IsNullOrEmpty(_oldNews.ToList()[0].NEWS_IMAGE1))
                {
                    ////copy file
                    //if (File.Exists(pathOld + _oldNews.ToList()[0].NEWS_IMAGE1))
                    //    File.Copy(pathOld + _oldNews.ToList()[0].NEWS_IMAGE1, pathCopy + _oldNews.ToList()[0].NEWS_IMAGE1);

                    //update database
                    var _copyNews = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == id);
                    _copyNews.Single().NEWS_IMAGE1 = _oldNews.ToList()[0].NEWS_IMAGE1;

                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyHtml(int id)
        {
            try
            {
                var _oldNews = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == m_news_id);

                if (!string.IsNullOrEmpty(_oldNews.ToList()[0].NEWS_FILEHTML))
                {
                    var _copyNews = DB.GetTable<ESHOP_NEW>().Where(n => n.NEWS_ID == id);
                    _copyNews.Single().NEWS_FILEHTML = _oldNews.ToList()[0].NEWS_FILEHTML;

                    DB.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyAlbums(int id)
        {
            try
            {
                var _oldNews = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(n => n.NEWS_ID == m_news_id);

                foreach (var item in _oldNews)
                {
                    ESHOP_NEWS_IMAGE img_insert = new ESHOP_NEWS_IMAGE();

                    img_insert.NEWS_IMG_IMAGE1 = item.NEWS_IMG_IMAGE1;
                    img_insert.NEWS_IMG_IMAGE2 = item.NEWS_IMG_IMAGE2;
                    img_insert.NEWS_IMG_IMAGE3 = item.NEWS_IMG_IMAGE3;
                    img_insert.NEWS_IMG_ORDER = item.NEWS_IMG_ORDER;
                    img_insert.NEWS_IMG_DESC = item.NEWS_IMG_DESC;
                    img_insert.NEWS_IMG_SHOWTYPE = item.NEWS_IMG_SHOWTYPE;
                    img_insert.NEWS_ID = id;

                    DB.ESHOP_NEWS_IMAGEs.InsertOnSubmit(img_insert);
                }

                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void copyAtt(int id)
        {
            try
            {
                var _oldNews = DB.GetTable<ESHOP_NEWS_ATT>().Where(n => n.NEWS_ID == m_news_id);

                foreach (var item in _oldNews)
                {
                    ESHOP_NEWS_ATT att_insert = new ESHOP_NEWS_ATT();

                    att_insert.NEWS_ATT_NAME = item.NEWS_ATT_NAME;
                    att_insert.EXT_ID = item.EXT_ID;
                    att_insert.NEWS_ATT_ORDER = item.NEWS_ATT_ORDER;
                    att_insert.NEWS_ATT_URL = item.NEWS_ATT_URL;
                    att_insert.NEWS_ATT_FILE = item.NEWS_ATT_FILE;
                    att_insert.NEWS_ID = id;

                    DB.ESHOP_NEWS_ATTs.InsertOnSubmit(att_insert);
                }

                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
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
    }
}