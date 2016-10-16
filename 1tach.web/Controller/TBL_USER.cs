using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Model;

namespace Controller
{
    public class TBL_USER
    {
        dbVuonRauVietDataContext context = new dbVuonRauVietDataContext();
        Encryption enc = new Encryption();

        public bool Login(string Username, string Password, bool SetCookie)
        {
            Model.TBL_USER tmp = context.TBL_USERs
                .FirstOrDefault(p =>
                (p.Username == Username || p.Email == Username)
                && p.Password == enc.GetMd5HashData(Password));
            if (tmp != null)
            {
                SetSession(tmp);

                if (SetCookie)
                {
                    HttpCookie aCookie = new HttpCookie("LoginOneTach");
                    aCookie.Values["UserName"] = tmp.Username;
                    aCookie.Values["User_Email"] = tmp.Email;
                    aCookie.Values["User_ID"] = tmp.ID.ToString();
                    aCookie.Values["User_Type"] = tmp.Type.ToString();
                    aCookie.Expires = DateTime.Now.AddDays(7);
                    HttpContext.Current.Response.Cookies.Add(aCookie);
                }
                return true;
            }
            return false;
        }

        public bool CreateUser(Model.TBL_USER user)
        {
            string oldpass = user.Password;
            user.Password = enc.GetMd5HashData(user.Password);
            user.IsActive = true;
            user.Type = 1;
            context.TBL_USERs.InsertOnSubmit(user);
            context.SubmitChanges();
            return Login(user.Username, oldpass, true);
        }

        public int CheckExist(string username, string email)
        {
            Model.TBL_USER user = new Model.TBL_USER();

            user = context.TBL_USERs.FirstOrDefault(p => p.Username == username);
            if (user != null)
                return 1;
            else
            {
                user = context.TBL_USERs.FirstOrDefault(p => p.Email == email);
                if (user != null)
                    return 2;
            }

            return 0;
        }

        private void SetSession(Model.TBL_USER user)
        {
            HttpContext.Current.Session["User_Name"] = user.Username;
            HttpContext.Current.Session["User_Email"] = user.Email;
            HttpContext.Current.Session["User_ID"] = user.ID;
            HttpContext.Current.Session["User_Type"] = user.Type;
        }

        public void Logout()
        {
            HttpContext.Current.Session["User_Name"] = null;
            HttpContext.Current.Session["User_Email"] = null;
            HttpContext.Current.Session["User_ID"] = null;
            HttpContext.Current.Session["User_Type"] = null;

            var httpCookie = HttpContext.Current.Response.Cookies["LoginOneTach"];
            if (httpCookie != null)
                httpCookie.Expires = DateTime.Now.AddDays(-1);
        }
    }
}
