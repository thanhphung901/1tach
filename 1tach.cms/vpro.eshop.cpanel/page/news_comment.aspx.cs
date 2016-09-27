using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

using System.Data;
using System.Web.UI.HtmlControls;

namespace vpro.eshop.cpanel.page
{
    public partial class news_comment : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        int m_news_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region properties

        public SortDirection sortProperty
        {
            get
            {
                if (ViewState["SortingState"] == null)
                {
                    ViewState["SortingState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortingState"];
            }
            set
            {
                ViewState["SortingState"] = value;
            }
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            m_news_id = Utils.CIntDef(Request["news_id"]);
            int _type = Utils.CIntDef(Request["type"]);
            Hyperback.NavigateUrl = "news.aspx?type=" + _type + "&news_id=" + m_news_id;
            if (!IsPostBack)
            {
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

        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_NEWS_COMMENTs
                               where ("" == keyword || (g.COMMENT_NAME).Contains(keyword) || (g.COMMENT_EMAIL).Contains(keyword) || (g.COMMENT_CONTENT).Contains(keyword))
                               && g.NEWS_ID == m_news_id
                               orderby g.COMMENT_PUBLISHDATE descending
                               select g);

                var _vNewsComment = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(a => a.NEWS_ID == m_news_id);
                foreach (var item in _vNewsComment)
                {
                    item.COMMENT_CHECK = 1;
                    DB.SubmitChanges();
                }

                if (AllList.ToList().Count > 0)
                    Rplistcomment.DataSource = AllList;
                Rplistcomment.DataBind();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void EventDelete(RepeaterCommandEventArgs e)
        {
            try
            {
                int GroupId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => g.COMMENT_ID == GroupId);

                DB.ESHOP_NEWS_COMMENTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("group_list.aspx");
            }
        }
        

        public bool GetCheck(object Obj_Status)
        {
            return Utils.CIntDef(Obj_Status) == 1 ? true : false;
        }

        #endregion

        #region Button Envents

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {


            int[] items = new int[Rplistcomment.Items.Count];
            try
            {
               int j=0;
               for (int i = 0; i < Rplistcomment.Items.Count; i++)
               {

                   HtmlInputCheckBox check = Rplistcomment.Items[i].FindControl("chkSelect") as HtmlInputCheckBox;
                   HiddenField s = Rplistcomment.Items[i].FindControl("Hdcommentid") as HiddenField;
                   int _Commentid = Utils.CIntDef(s.Value);
                   if (check.Checked)
                   {
                       items[j] = _Commentid;
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

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            
            string strLink = "";
            int j = 0;
            int k = 0;

            int[] items_check = new int[Rplistcomment.Items.Count];
            int[] items_uncheck = new int[Rplistcomment.Items.Count];

            try
            {

                for (int i = 0; i < Rplistcomment.Items.Count; i++)
                {

                    HtmlInputCheckBox check = Rplistcomment.Items[i].FindControl("chkDisplay") as HtmlInputCheckBox;
                    HiddenField s = Rplistcomment.Items[i].FindControl("Hdcommentid") as HiddenField;
                    int _Commentid = Utils.CIntDef(s.Value);
                    if (check.Checked)
                    {
                        items_check[j] = _Commentid;
                        j++;
                    }
                    else
                    {
                        items_uncheck[k] = _Commentid;
                        k++;
                    }
                }
                //update check 
                var update_check = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => items_check.Contains(g.COMMENT_ID));
                if (update_check.ToList().Count > 0)
                    update_check.ToList().ForEach(em => em.COMMENT_STATUS = 1);

                //update uncheck 
                var update_uncheck = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(g => items_uncheck.Contains(g.COMMENT_ID));
                if (update_uncheck.ToList().Count > 0)
                    update_uncheck.ToList().ForEach(em => em.COMMENT_STATUS = 0);

                DB.SubmitChanges();

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

        protected void Rplistcomment_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }
    }
}