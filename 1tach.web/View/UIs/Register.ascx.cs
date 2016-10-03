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
    public partial class Register : System.Web.UI.UserControl
    {
        Controller.TBL_USER cont = new Controller.TBL_USER();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Name"] != null)
            {
                Response.Redirect("/");
            }
        }

        protected void btnSignUp_OnClick(object sender, EventArgs e)
        {
            Model.TBL_USER user = new Model.TBL_USER();
            user.Password = txtPassword.Text.Trim();
            user.Email = txtEmail.Text.Trim();
            user.IsActive = true;
            user.Type = 1;
            user.Username = txtUserName.Text.Trim();
            cont.CreateUser(user);
        }
    }
}