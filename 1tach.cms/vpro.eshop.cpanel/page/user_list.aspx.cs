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
    public partial class user_list : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                SearchResult();

                
            }

        }

        #endregion

        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "user.aspx?user_id=" + Utils.CStrDef(obj_id);
        }

        public string getUserActive(object User_Active)
        {
            return Utils.CIntDef(User_Active) == 0 ? "Chưa kích hoạt" : "Đã kích hoạt";
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_USERs
                               where ("" == keyword || (g.USER_NAME).Contains(keyword) || (g.USER_UN).Contains(keyword))
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["UserList"] = DataUtil.LINQToDataTable(AllList);

                RplistUser.DataSource = AllList;
                RplistUser.DataBind();


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

                var g_delete = DB.GetTable<ESHOP_USER>().Where(g => g.USER_ID == GroupId);

                DB.ESHOP_USERs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("user_list.aspx");
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
            int[] items = new int[RplistUser.Items.Count];

            try
            {

                for (int i = 0; i < RplistUser.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistUser.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistUser.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_USER>().Where(g => items.Contains(g.USER_ID));

                DB.ESHOP_USERs.DeleteAllOnSubmit(g_delete);
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

        #endregion

        protected void RplistUser_ItemCommand(object source, RepeaterCommandEventArgs e)
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