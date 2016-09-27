using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.page
{
    public partial class page_email_send_add : System.Web.UI.Page
    {
        private int m_mail_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            m_mail_id = Utils.CIntDef(Request["mail_id"]);
            Hyperback.NavigateUrl = "page-email-send.aspx";
            if (m_mail_id == 0)
            {
                dvDelete.Visible = false;
            }
            if (!IsPostBack)
                loadInfo();
        }
        #region loadInfo
        private void loadInfo()
        {
            var list = DB.ESHOP_MAIL_RECIVEs.Where(n => n.MAIL_ID == m_mail_id).ToList();
            if (list.Count > 0)
            {
                txtCustomerEmail.Value = list[0].MAIL_NAME;
                Rdstatus.SelectedValue = list[0].MAIL_ACTIVE.ToString();
            }
        }
        #endregion
        #region function
        private void SaveInfo(string strlink="")
        {
            if (m_mail_id != 0)
            {
                var list = DB.ESHOP_MAIL_RECIVEs.Where(n => n.MAIL_ID == m_mail_id).ToList();
                if (list.Count > 0)
                {
                    list[0].MAIL_NAME = txtCustomerEmail.Value;
                    list[0].MAIL_ACTIVE = Utils.CIntDef(Rdstatus.SelectedValue);
                    DB.SubmitChanges();
                }
            }
            else
            {
                ESHOP_MAIL_RECIVE mail = new ESHOP_MAIL_RECIVE();
                mail.MAIL_NAME = txtCustomerEmail.Value;
                mail.MAIL_ACTIVE = Utils.CIntDef(Rdstatus.SelectedValue);
                DB.ESHOP_MAIL_RECIVEs.InsertOnSubmit(mail);
                DB.SubmitChanges();
                var list = DB.ESHOP_MAIL_RECIVEs.OrderByDescending(n => n.MAIL_ID).ToList();
                if (list.Count > 0)
                    m_mail_id = list[0].MAIL_ID;
            }
            if (String.IsNullOrEmpty(strlink))
                strlink = "page-email-send-add.aspx?mail_id=" + m_mail_id;
            Response.Redirect(strlink);
        }
        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_MAIL_RECIVE>().Where(g => g.MAIL_ID == m_mail_id);

                DB.ESHOP_MAIL_RECIVEs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();
                Response.Redirect("page-email-send.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion
        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
             SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            SaveInfo("page-email-send-add.aspx");
        }
        protected void LbsaveClose_Click(object sender, EventArgs e)
        {
            SaveInfo("page-email-send.aspx");
        }


        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        #endregion
    }
}