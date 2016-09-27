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

namespace vpro.eshop.cpanel.page
{
    public partial class page_email_send : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        private int m_mail_id = 0;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                m_mail_id = Utils.CIntDef(Request["MAIL_ID"]);
              
                SearchResult();                
            }

        }

        #endregion
        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_MAIL_RECIVEs
                               where "" == keyword || g.MAIL_NAME.Contains(keyword)
                               orderby g.MAIL_ID descending
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["email_send"] = DataUtil.LINQToDataTable(AllList);

                RplistEmail.DataSource = AllList;
                RplistEmail.DataBind();


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
        public string getactive(object active)
        {
            int id = Utils.CIntDef(active);
            return id == 1 ? "Cho phép nhận tin" : "Không cho phép nhận tin";
        }
        public string getLink(object id)
        {
            return "page-email-send-add.aspx?mail_id=" + id;
        }
        private void EventDelete(RepeaterCommandEventArgs e)
        {
            try
            {
                int NewsId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_MAIL_RECIVE>().Where(g => g.MAIL_ID == NewsId);

                DB.ESHOP_MAIL_RECIVEs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("page-email-send.aspx");
            }
        }
        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void lbtAn_Click(object sender, EventArgs e)
        {
            int j = 0;
            int[] items = new int[RplistEmail.Items.Count];

            try
            {

                for (int i = 0; i < RplistEmail.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistEmail.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistEmail.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }
                var list = DB.ESHOP_MAIL_RECIVEs.Where(g => items.Contains(g.MAIL_ID)).ToList();
                foreach (var i1 in list)
                {
                    i1.MAIL_ACTIVE = 0;
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
            int[] items = new int[RplistEmail.Items.Count];

            try
            {

                for (int i = 0; i < RplistEmail.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistEmail.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistEmail.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }
                var list = DB.ESHOP_MAIL_RECIVEs.Where(g => items.Contains(g.MAIL_ID)).ToList();
                foreach (var i1 in list)
                {
                    i1.MAIL_ACTIVE = 1;
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
            int[] items = new int[RplistEmail.Items.Count];

            try
            {

                for (int i = 0; i < RplistEmail.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistEmail.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistEmail.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_MAIL_RECIVE>().Where(g => items.Contains(g.MAIL_ID));

                DB.ESHOP_MAIL_RECIVEs.DeleteAllOnSubmit(g_delete);
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
     
        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void RplistEmail_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }
    }
}