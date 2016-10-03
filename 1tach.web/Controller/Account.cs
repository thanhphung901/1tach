using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Web;
using System.Web.UI;
using vpro.functions;

namespace Controller
{
    public class Account
    {
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        public bool Login(string Email, string MatKhau)
        {
            var dangnhap = from a in db.ESHOP_CUSTOMERs
                           where a.CUSTOMER_EMAIL == Email && a.CUSTOMER_PW == MatKhau
                           select a;
            if (dangnhap.ToList().Count > 0)
            {
                Load_All_Cuss(Email);
                return true;
            }
            else
                return false;
        }
        private void Load_All_Cuss(string email)
        {
            try
            {
                var _cus = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_EMAIL == email);
                if (_cus.ToList().Count > 0)
                {
                    HttpContext.Current.Session["User_Name"] = _cus.ToList()[0].CUSTOMER_FULLNAME;
                    HttpContext.Current.Session["User_ID"] = _cus.ToList()[0].CUSTOMER_ID;
                    HttpContext.Current.Session["User_Phone"] = _cus.ToList()[0].CUSTOMER_PHONE1;
                    HttpContext.Current.Session["User_Email"] = _cus.ToList()[0].CUSTOMER_EMAIL;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #region Register

        public bool Check_email(string _email)
        {
            try
            {
                var _user = db.GetTable<ESHOP_CUSTOMER>().Where(u => u.CUSTOMER_EMAIL == _email.Trim());

                if (_user.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return true;
            }
        }
        public bool Register(string _sFullName, string _Email, string _Pass,string _sphone,string _add,string _sex,string _idcity, string _iddistrict, DateTime _birth,string _check)
        {
            if (!Check_email(_Email))
            {
                ESHOP_CUSTOMER user = new ESHOP_CUSTOMER();
                user.CUSTOMER_EMAIL = _Email;
                user.CUSTOMER_PW = _Pass;
                user.CUSTOMER_FULLNAME = _sFullName;
                user.CUSTOMER_PUBLISHDATE = DateTime.Now;
                user.CUSTOMER_SHOWTYPE = 0;
                user.CUSTOMER_PHONE1 = _sphone;
                user.CUSTOMER_ADDRESS = _add;
                user.CUSTOMER_FIELD3 = _sex;
                user.CUSTOMER_FIELD1 = _idcity;
                user.CUSTOMER_FIELD2 = _iddistrict;
                user.CUSTOMER_UPDATE = _birth;
                user.CUSTOMER_FIELD4 = _check;
                db.ESHOP_CUSTOMERs.InsertOnSubmit(user);
                db.SubmitChanges();
                Load_All_Cuss(_Email);
                return true;
            }
            return false;
        }
        #endregion
        #region Forgetpass
        public string Name;
        public void Sendpass(string _email, string _pass)
        {
            try
            {
                var list = db.ESHOP_CUSTOMERs.Where(n => n.CUSTOMER_EMAIL == _email);
                foreach (var i in list)
                {
                    Name = i.CUSTOMER_FULLNAME;
                    i.CUSTOMER_PW = _pass;
                    db.SubmitChanges();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
