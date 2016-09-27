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
    public partial class contact : System.Web.UI.Page
    {
        #region Declare

        private int m_contact_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_contact_id = Utils.CIntDef(Request["contact_id"]);
            Hyperback.NavigateUrl = "contact_list.aspx";
            if (m_contact_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
              
                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }
        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }
        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_CONTACTs
                              where g.CONTACT_ID == m_contact_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtName.Value = G_info.ToList()[0].CONTACT_NAME;
                    txtEmail.Value = G_info.ToList()[0].CONTACT_EMAIL;
                    txtadd.Value = G_info.ToList()[0].CONTACT_ADDRESS;
                    txtPhone.Value = G_info.ToList()[0].CONTACT_PHONE;
                    txtTitle.Value = G_info.ToList()[0].CONTACT_TITLE;
                    txtDesc.Value = G_info.ToList()[0].CONTACT_CONTENT;
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SaveInfo()
        {
            var list = DB.ESHOP_CONTACTs.Where(n => n.CONTACT_ID == m_contact_id).ToList();
            if (list.Count > 0)
            {
                list[0].CONTACT_NAME = txtName.Value;
                list[0].CONTACT_EMAIL = txtEmail.Value;
                list[0].CONTACT_ADDRESS = txtadd.Value;
                list[0].CONTACT_PHONE = txtPhone.Value;
                list[0].CONTACT_TITLE = txtTitle.Value;
                list[0].CONTACT_CONTENT = txtDesc.Value;
                DB.SubmitChanges();
            }
            getInfo();
        }
        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_CONTACT>().Where(g => g.CONTACT_ID == m_contact_id);

                DB.ESHOP_CONTACTs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("contact_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion

       
    }
}