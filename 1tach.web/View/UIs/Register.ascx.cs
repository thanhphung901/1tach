using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (CheckSuccess())
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

        private bool CheckSuccess()
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                lblError.Text = "Username is empty!";
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblError.Text = "Email is empty!";
                return false;
            }
            bool isEmail = Regex.IsMatch(txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail)
            {
                lblError.Text = "Email is not correct format!";
                return false;
            }

            int exist = cont.CheckExist(txtUserName.Text, txtEmail.Text);
            if (exist == 1)
            {
                lblError.Text = "Username already exist!";
                return false;
            }
            else
            {
                if (exist == 2)
                {
                    lblError.Text = "Email already exist!";
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                lblError.Text = "Password is empty!";
                return false;
            }
            if (string.IsNullOrEmpty(txtRePassword.Text))
            {
                lblError.Text = "Re-Password is empty!";
                return false;
            }
            if (txtRePassword.Text.Trim() != txtPassword.Text.Trim())
            {
                lblError.Text = "Password and Re-Password is not same!";
                return false;
            }
            return true;
        }
    }
}