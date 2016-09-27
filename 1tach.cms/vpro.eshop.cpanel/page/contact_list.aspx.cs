using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

using System.Data;
using System.Web.UI.HtmlControls;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class contact_list : System.Web.UI.Page
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

        #region My Functions
        public string formatdate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }
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
                int _limit = 20;
                int _skip = 0;
                if (_page != 0)
                    _skip = _page * _limit - _limit;
                var AllList = (from g in DB.ESHOP_CONTACTs
                               where ("" == keyword || (g.CONTACT_NAME).Contains(keyword) || (g.CONTACT_EMAIL).Contains(keyword)
                               || (g.CONTACT_TITLE).Contains(keyword) || (g.CONTACT_CONTENT).Contains(keyword)
                               || g.CONTACT_NAME.Contains(keyword) || g.CONTACT_EMAIL.Contains(keyword)
                               || g.CONTACT_TITLE.Contains(keyword) || g.CONTACT_CONTENT.Contains(keyword))
                               && g.CONTACT_TYPE != 1
                               orderby g.CONTACT_PUBLISHDATE descending
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["ContactList"] = DataUtil.LINQToDataTable(AllList);

                RplistContact.DataSource = AllList.Skip(_skip).Take(_limit);
                RplistContact.DataBind();
                LitPage.Text = pagechange.changePage("contact_list.aspx", AllList.ToList().Count, _limit, _page);


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
                int UnitId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_CONTACT>().Where(g => g.CONTACT_ID == UnitId);

                DB.ESHOP_CONTACTs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("contact_list.aspx?");
            }
        }

        #endregion

        #region Button Envents

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            
            int j = 0;
            int[] items = new int[RplistContact.Items.Count];

            try
            {

                for (int i = 0; i < RplistContact.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistContact.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistContact.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }
                //delete 
                var g_delete = DB.GetTable<ESHOP_CONTACT>().Where(g => items.Contains(g.CONTACT_ID));

                DB.ESHOP_CONTACTs.DeleteAllOnSubmit(g_delete);
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

        public string getLink(object obj_id)
        {
            return "contact.aspx?contact_id=" + Utils.CStrDef(obj_id);
        }

        #endregion

        protected void RplistContact_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }
    }
}