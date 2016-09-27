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
using System.Net.Mail;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class page_comment : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        PageIndexChange pagechange = new PageIndexChange();
        int _page = 0;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            _page = Utils.CIntDef(Request["page"]);
            if (!IsPostBack)
            {

                SearchResult();
               
            }

        }

        #endregion
        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;
                int _limit = 20;
                int _skip = 0;
                if (_page != 0)
                    _skip = _page * _limit - _limit;
                var AllList = (from g in DB.ESHOP_NEWS_COMMENTs
                               where "" == keyword || g.COMMENT_NAME.Contains(keyword)
                               orderby g.COMMENT_ID descending
                               select g);


                if (AllList.ToList().Count > 0)
                {
                    Rplistcomment.DataSource = AllList.Skip(_skip).Take(_limit);
                    Rplistcomment.DataBind();
                    LitPage.Text = pagechange.changePage("page-comment.aspx", AllList.ToList().Count, _limit, _page);
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        private void EventDelete(RepeaterCommandEventArgs e)
        {
            try
            {
                int NewsId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => g.COMMENT_ID == NewsId);

                DB.ESHOP_NEWS_COMMENTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("page-comment.aspx");
            }
        }
        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }
        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void lbtAn_Click(object sender, EventArgs e)
        {
            int j = 0;
            int[] items = new int[Rplistcomment.Items.Count];

            try
            {

                for (int i = 0; i < Rplistcomment.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)Rplistcomment.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = Rplistcomment.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }
                var list = DB.ESHOP_NEWS_COMMENTs.Where(g => items.Contains(g.COMMENT_ID)).ToList();
                foreach (var i1 in list)
                {
                    i1.COMMENT_STATUS = 0;
                    DB.SubmitChanges();
                }
                SearchResult();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }

        }


        protected void Lbhien_Click(object sender, EventArgs e)
        {
            int j = 0;
            int[] items = new int[Rplistcomment.Items.Count];

            try
            {

                for (int i = 0; i < Rplistcomment.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)Rplistcomment.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = Rplistcomment.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }
                var list = DB.ESHOP_NEWS_COMMENTs.Where(g => items.Contains(g.COMMENT_ID)).ToList();
                foreach (var i1 in list)
                {
                    i1.COMMENT_STATUS = 1;
                    DB.SubmitChanges();
                }
                SearchResult();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }

        }
        private void DeleteInfo()
        {
            int j = 0;
            int[] items = new int[Rplistcomment.Items.Count];

            try
            {

                for (int i = 0; i < Rplistcomment.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)Rplistcomment.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = Rplistcomment.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => items.Contains(g.COMMENT_ID));

                DB.ESHOP_NEWS_COMMENTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                items = null;
                SearchResult();
            }

        }
        public string checkactive(object check)
        {
            int active = Utils.CIntDef(check);
            return active == 1 ? "Hiện" : "Ẩn";
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }
        public string getLink(object obj_id)
        {
            return "news.aspx?news_id=" + Utils.CStrDef(obj_id);
        }
        public string getname(object news_id)
        {
            int id = Utils.CIntDef(news_id);
            var list = DB.ESHOP_NEWs.Where(n => n.NEWS_ID == id).Single();
            if (list != null)
            {
                return list.NEWS_TITLE;
            }
            return "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void Rplistcomment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }
    }
}