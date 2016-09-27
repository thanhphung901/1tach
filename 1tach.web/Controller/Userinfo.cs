using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;

namespace Controller
{
    public class Userinfo
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        public List<ESHOP_PROPERTy> Loadcity()
        {
            try
            {
                var list = db.ESHOP_PROPERTies.Where(n => n.PROP_RANK == 2).ToList();
                return list;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<ESHOP_PROPERTy> Loaddistric(int idpro)
        {
            try
            {
                var list = db.ESHOP_PROPERTies.Where(n => n.PROP_RANK == 3&&n.PROP_PARENT_ID==idpro).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ESHOP_PROPERTy> Loaddship(int idpro)
        {
            try
            {
                var list = db.ESHOP_PROPERTies.Where(n => n.PROP_ID == idpro).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal getShip(int id)
        {
            var list = db.ESHOP_PROPERTies.Where(n => n.PROP_ID == id).ToList();
            if (list.Count > 0)
                return Utils.CIntDef(list[0].PROP_SHIPPING_FEE);
            return 0;
        }
        public string getnamePro(int id)
        {
            var list = db.ESHOP_PROPERTies.Where(n => n.PROP_ID == id).ToList();
            if (list.Count > 0)
                return list[0].PROP_NAME;
            return "";
        }
        public List<ESHOP_CUSTOMER> Loaduserinfo(int userid)
        {
            var _vUser = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == userid).ToList();
            return _vUser;
        }
        public bool Updateuser(int userid,string name,string phone,string address,string city,string district,string sex,DateTime time, string pass1,string pass2,string passfm)
        {
            var _vUser = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == userid);
            foreach (var i in _vUser)
            {
                if (!string.IsNullOrEmpty(pass2))
                {
                    if (pass1.Trim() != pass2.Trim())
                    {
                        return false;
                    }
                    i.CUSTOMER_PW = passfm;
                }
                i.CUSTOMER_FULLNAME = name;
                i.CUSTOMER_PHONE1 = phone;
                i.CUSTOMER_ADDRESS = address;
                i.CUSTOMER_FIELD1 = city;
                i.CUSTOMER_FIELD2 = district;
                i.CUSTOMER_FIELD3 = sex;
                i.CUSTOMER_UPDATE = time;
                db.SubmitChanges();
                return true;
            }
            return false;
        }
        public bool Updatepass(int userid,string passold, string passnew, string passfm)
        {
            var _vUser = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == userid && a.CUSTOMER_PW==passold);
            foreach (var i in _vUser)
            {
                if (!string.IsNullOrEmpty(passnew))
                {
                    if (passold.Trim() != passnew.Trim())
                    {
                        i.CUSTOMER_PW = passfm;
                    }                    
                }
                db.SubmitChanges();
                return true;
            }
            return false;
        }
        public bool checkuser(int userid)
        {
            var _vUser = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == userid);
            if(userid!=0)
            {
                return true;
            }
            return false;
        }
        public bool Updateuser1(int userid, string name, string phone, string sex, DateTime _birth,string diachi)
        {
            var _vUser = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == userid);
            foreach (var i in _vUser)
            {            
                i.CUSTOMER_FULLNAME = name;
                i.CUSTOMER_PHONE1 = phone;
                i.CUSTOMER_FIELD3 = sex;
                i.CUSTOMER_UPDATE = _birth;
                i.CUSTOMER_ADDRESS = diachi;
                db.SubmitChanges();
                return true;
            }
            return false;
        }
        public bool UpdatAdd(int userid, string _add,string _idcity, string _iddistrict)
        {
            var _vUser = db.GetTable<ESHOP_CUSTOMER>().Where(a => a.CUSTOMER_ID == userid);
            foreach (var i in _vUser)
            {
                i.CUSTOMER_ADDRESS = _add;
                i.CUSTOMER_FIELD1 = _idcity;
                i.CUSTOMER_FIELD2 = _iddistrict;
                db.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
