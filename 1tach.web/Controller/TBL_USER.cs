using System;
using System.Collections.Generic;
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

        public bool Login(string Username, string Password)
        {
            Model.TBL_USER tmp = context.TBL_USERs
                .FirstOrDefault(p => 
                (p.Username == Username || p.Email == Username) 
                && p.Password == enc.GetMd5HashData(Password));
            if (tmp != null)
            {
                SetSession(tmp);
                return true;
            }
            return false;
        }

        public bool CreateUser(Model.TBL_USER user)
        {
            user.Password = enc.GetMd5HashData(user.Password);
            user.IsActive = true;
            user.Type = 1;
            context.TBL_USERs.InsertOnSubmit(user);
            context.SubmitChanges();
            return Login(user.Username, user.Password);
        }

        private void SetSession(Model.TBL_USER user)
        {
            HttpContext.Current.Session["User_Name"] = user.Username;
            HttpContext.Current.Session["User_Email"] = user.Email;
            HttpContext.Current.Session["User_ID"] = user.ID;
            HttpContext.Current.Session["User_Type"] = user.Type;
        }
    }
}
