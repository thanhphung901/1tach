using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Controller;

namespace OneTach.UIs
{
    public partial class Login : System.Web.UI.UserControl
    {
        Controller.TBL_USER cont = new Controller.TBL_USER();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Name"] != null)
            {
                Response.Redirect("/");
            }
        }

        protected void btnSignIn_OnClick(object sender, EventArgs e)
        {
            if (cont.Login(txtEmail.Text.Trim(), txtPass.Text.Trim()))
            {
                Response.Redirect("/");
            }
        }
    }
}