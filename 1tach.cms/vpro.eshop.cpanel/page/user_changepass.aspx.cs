using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using vpro.functions;

namespace vpro.eshop.cpanel.page
{
    public partial class user_changepass : System.Web.UI.Page
    {
        #region Declare

        eshopdbDataContext DB = new eshopdbDataContext();
        int UserID = 0;
        #endregion

        #region Page events

        protected void Page_Load(object sender, EventArgs e)
        {
            UserID = Utils.CIntDef(Session["USER_ID"]);
            Hyperback.NavigateUrl = "default.aspx";
            if (!IsPostBack)
            {
                loadInfo();
            }
        }

        #endregion
        #region loadInfo
        private void loadInfo()
        {
            var list = DB.ESHOP_USERs.Where(n => n.USER_ID == UserID).ToList();
            if (list.Count > 0)
            {
                Lbuname.Text = list[0].USER_UN;
                Lbfullname.Text = list[0].USER_NAME;
                Lbgroup.Text = getNameGroup(Utils.CIntDef(list[0].GROUP_ID));
            }
        }
        private string getNameGroup(int id)
        {
            var list = DB.ESHOP_GROUPs.Where(n => n.GROUP_ID == id).ToList();
            if (list.Count > 0)
                return list[0].GROUP_NAME;
            return "";
        }
        #endregion
        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            if (CheckInfo())
                lblError.Text = "Mật khẩu cũ nhập không đúng.";
            else
                SaveInfo();
        }

        #endregion

        #region My Functions

        private void SaveInfo()
        {
            try
            {
                

                string SALT = Common.CreateSalt();
                string USER_PW = Common.Encrypt(txtPassNew.Value, SALT);

                var g_update = DB.GetTable<ESHOP_USER>().Where(g => g.USER_ID == UserID);

                if (g_update.ToList().Count > 0)
                {

                    if (!string.IsNullOrEmpty(USER_PW))
                    {
                        g_update.Single().USER_PW = USER_PW;
                        g_update.Single().SALT = SALT;
                    }

                    DB.SubmitChanges();

                    Response.Redirect("default.aspx");
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private bool CheckInfo()
        {
            try
            {
                string strPW = Utils.CStrDef(txtPass.Value, "");
               
                if (UserID > 0)
                {
                    var login = DB.GetTable<ESHOP_USER>().Where(u => u.USER_ID == UserID);

                    if (login.ToList().Count > 0)
                    {
                        //decode pass
                        strPW = Common.Encrypt(strPW, Utils.CStrDef(login.ToList()[0].SALT));
                        //kiem tra lai salt bi null

                        if (login.ToList()[0].USER_PW != strPW)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;

            }

        }

        #endregion

    }
}