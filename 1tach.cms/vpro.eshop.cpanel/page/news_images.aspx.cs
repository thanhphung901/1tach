using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class news_images : System.Web.UI.Page
    {
        #region Declare

        private int m_img_id = 0;
        private int m_news_id = 0;
        private int _count = 0;
        int _type = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_img_id = Utils.CIntDef(Request["img_id"]);
            m_news_id = Utils.CIntDef(Request["news_id"]);

            _type = Utils.CIntDef(Request["type"]);
            Hyperback.NavigateUrl = "news.aspx?type=" + _type + "&news_id=" + m_news_id;
            if (m_img_id == 0)
            {

                trImage1.Visible = false;
                //trImage2.Visible = false;
            }

            if (!IsPostBack)
            {

                getInfo();
                SearchResult();

            }

            hplCatNews.HRef = "news_category.aspx?type=" + _type + "&news_id=" + m_news_id;
            hplEditorHTMl.HRef = "news_editor.aspx?type=" + _type + "&news_id=" + m_news_id;
            hplNewsAtt.HRef = "news_attachment.aspx?type=" + _type + "&news_id=" + m_news_id;
            hplAlbum.HRef = "news_images.aspx?type=" + _type + "&news_id=" + m_news_id;
            //bplNewsCopy.HRef = "news_copy.aspx?news_id=" + m_news_id;
            hplComment.HRef = "news_comment.aspx?type=" + _type + "&news_id=" + m_news_id;
            //hplCatProducts.HRef = "news_news.aspx?news_id=" + m_news_id;

        }

        #endregion

        #region Button Events

        //protected void lbtSave_Click(object sender, EventArgs e)
        //{
        //    SaveInfo();
        //}

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo(m_img_id);
        }

        protected void btnDelete1_Click(object sender, ImageClickEventArgs e)
        {
            string strLink = "";
            try
            {
                var n_info = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(n => n.NEWS_IMG_ID == m_img_id);

                if (n_info.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(n_info.ToList()[0].NEWS_IMG_IMAGE1))
                    {
                        string imagePath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + n_info.ToList()[0].NEWS_IMG_IMAGE1);
                        n_info.ToList()[0].NEWS_IMG_IMAGE1 = "";
                        DB.SubmitChanges();

                        if (File.Exists(imagePath))
                            File.Delete(imagePath);

                        strLink = "news_images.aspx?type="+_type+"&img_id=" + m_img_id + "&news_id=" + m_news_id;
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

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {

                var G_info = (from g in DB.ESHOP_NEWS_IMAGEs
                              where g.NEWS_IMG_ID == m_img_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtTitle.Value = G_info.ToList()[0].NEWS_IMG_DESC;
                    txtOrder.Value = Utils.CStrDef(G_info.ToList()[0].NEWS_IMG_ORDER);

                    //image 1
                    if (!string.IsNullOrEmpty(G_info.ToList()[0].NEWS_IMG_IMAGE1))
                    {
                        trUpload1.Visible = false;
                        trImage1.Visible = true;
                        hplImage1.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].NEWS_IMG_IMAGE1;
                        hplImage1.Text = G_info.ToList()[0].NEWS_IMG_IMAGE1;
                        Image1.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].NEWS_IMG_IMAGE1;

                    }
                    else
                    {
                        trUpload1.Visible = true;
                        trImage1.Visible = false;
                    }

                    //image 2
                    //if (!string.IsNullOrEmpty(G_info.ToList()[0].NEWS_IMG_IMAGE2))
                    //{
                    //    trUpload2.Visible = false;
                    //    trImage2.Visible = true;
                    //    hplImage2.NavigateUrl = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].NEWS_IMG_IMAGE2;
                    //    hplImage2.Text = G_info.ToList()[0].NEWS_IMG_IMAGE2;
                    //    Image2.Src = PathFiles.GetPathNews(m_news_id) + G_info.ToList()[0].NEWS_IMG_IMAGE2;
                    //}
                    //else
                    //{
                    //    trUpload2.Visible = true;
                    //    trImage2.Visible = false;
                    //}
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void DeleteInfo(int img_id)
        {
            string strLink = "";
            try
            {
                string Img_Image1 = "";
                string Img_Image2 = "";

                var G_info = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(g => g.NEWS_IMG_ID == img_id);

                if (G_info.ToList().Count > 0)
                {
                    Img_Image1 = Utils.CStrDef(G_info.ToList()[0].NEWS_IMG_IMAGE1);
                    Img_Image2 = Utils.CStrDef(G_info.ToList()[0].NEWS_IMG_IMAGE2);
                }

                DB.ESHOP_NEWS_IMAGEs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete file
                if (!string.IsNullOrEmpty(Img_Image1))
                {
                    string fullpath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + Img_Image1);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }
                if (!string.IsNullOrEmpty(Img_Image2))
                {
                    string fullpath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + Img_Image2);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }

                strLink = "news_images.aspx?type="+_type+"&news_id=" + m_news_id;

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

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "news_images.aspx?type="+_type+"&news_id=" + m_news_id + "&img_id=" + Utils.CStrDef(obj_id);
        }

        public string getLinkImage(object obj_id, object obj_image1)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_id)) && Utils.CIntDef(obj_id) > 0)
                return PathFiles.GetPathNews(m_news_id) + Utils.CStrDef(obj_id);

            return "";
        }

        public string getImage(object obj_id, object obj_image1)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_id)) && Utils.CIntDef(obj_id) > 0)
                return "<img src='" + PathFiles.GetPathNews(m_news_id) + Utils.CStrDef(obj_image1) + "' width='40px' border='0' />";

            return "";
        }

        private void SearchResult()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_NEWS_IMAGEs
                               where g.NEWS_ID == m_news_id
                               select g);
                Rplistimg.DataSource = AllList;
                Rplistimg.DataBind();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion
        protected void Lnupload_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_img_id != 0)
                {
                    if (trImage1.Visible)
                    {
                        var list = DB.ESHOP_NEWS_IMAGEs.Where(n => n.NEWS_IMG_ID == m_img_id).ToList();
                        if (list.Count > 0)
                        {
                            list[0].NEWS_IMG_DESC = txtTitle.Value;
                            list[0].NEWS_IMG_ORDER = Utils.CIntDef(txtOrder.Value);
                            DB.SubmitChanges();
                        }

                    }
                    else
                    {
                        HttpFileCollection hfc = Request.Files;
                        for (int i = 0; i < hfc.Count; i++)
                        {
                            HttpPostedFile hpf = hfc[0];
                            if (hpf.ContentLength > 0)
                            {
                                string pathfile = Server.MapPath("/data/news/" + m_news_id);
                                string fullpathfile = pathfile + "/" + Path.GetFileName(hpf.FileName);
                                if (!Directory.Exists(pathfile))
                                {
                                    Directory.CreateDirectory(pathfile);
                                }
                                hpf.SaveAs(fullpathfile);
                                var list = DB.ESHOP_NEWS_IMAGEs.Where(n => n.NEWS_IMG_ID == m_img_id).ToList();
                                if (list.Count > 0)
                                {
                                    list[0].NEWS_IMG_DESC = txtTitle.Value;
                                    list[0].NEWS_IMG_ORDER = Utils.CIntDef(txtOrder.Value);
                                    list[0].NEWS_IMG_IMAGE1 = hpf.FileName;
                                    DB.SubmitChanges();
                                }

                            }

                        }
                    }
                }
                else
                {
                    // Get the HttpFileCollection
                    HttpFileCollection hfc = Request.Files;
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {
                            ESHOP_NEWS_IMAGE g_update = new ESHOP_NEWS_IMAGE();
                            string pathfile = Server.MapPath("/data/news/" + m_news_id);
                            string fullpathfile = pathfile + "/" + Path.GetFileName(hpf.FileName);
                            if (!Directory.Exists(pathfile))
                            {
                                Directory.CreateDirectory(pathfile);
                            }
                            hpf.SaveAs(fullpathfile);
                            g_update.NEWS_IMG_DESC = txtTitle.Value;
                            g_update.NEWS_IMG_ORDER = Utils.CIntDef(txtOrder.Value);
                            g_update.NEWS_ID = m_news_id;
                            g_update.NEWS_IMG_IMAGE1 = hpf.FileName;
                            DB.ESHOP_NEWS_IMAGEs.InsertOnSubmit(g_update);
                            DB.SubmitChanges();

                        }
                    }
                }
                Response.Redirect("news_images.aspx?type=" + _type + "&news_id=" + m_news_id);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        protected void Rplistimg_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string Img_Image1 = "";
                int _sID = Utils.CIntDef(e.CommandArgument.ToString());
                var g_delete = DB.GetTable<ESHOP_NEWS_IMAGE>().Where(g => g.NEWS_IMG_ID == _sID).ToList();
                if (g_delete.Count > 0)
                {
                    Img_Image1 = g_delete[0].NEWS_IMG_IMAGE1;
                }
                DB.ESHOP_NEWS_IMAGEs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                if (!string.IsNullOrEmpty(Img_Image1))
                {
                    string fullpath = Server.MapPath(PathFiles.GetPathNews(m_news_id) + Img_Image1);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }
            }
            SearchResult();
        }
    }
}