using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using vpro.functions;
using System.Web.UI;
using System.Web;
using Model;
namespace Controller
{
    public class Config
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        #endregion
        //Config meta
        public List<ESHOP_CONFIG> Config_meta()
        {
            try
            {
                var list = db.ESHOP_CONFIGs.OrderBy(c => c.CONFIG_ID).Take(1).ToList();
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Show_File_HTML(string HtmlFile, string path)
        {
            string pathFile;
            string strHTMLContent;
            string _result = string.Empty;
            pathFile = HttpContext.Current.Server.MapPath(path + HtmlFile);

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                _result = strHTMLContent;
            }
            return _result;
        }
        //Insert contact
        public bool Insert_contact(string name, string email, string title, string content, string phone, string diachi)
        {
            ESHOP_CONTACT add = new ESHOP_CONTACT
            {
                CONTACT_NAME = name,
                CONTACT_EMAIL = email,
                CONTACT_TITLE = title,
                CONTACT_CONTENT = content,
                CONTACT_PUBLISHDATE = DateTime.Now,
                CONTACT_PHONE = phone,
                CONTACT_ADDRESS=diachi,
                CONTACT_TYPE = 0,
            };
            db.ESHOP_CONTACTs.InsertOnSubmit(add);
            db.SubmitChanges();
            return true;
        }
        public List<ESHOP_CONTACT> loadlistcontact()
        {
            var list = db.ESHOP_CONTACTs.OrderByDescending(n => n.CONTACT_ID).ToList();
            return list;
        }
        //Config mail
        public List<ESHOP_EMAIL> Getemail(int stt)
        {
            //stt=2 contact
            var _ccMail = db.GetTable<ESHOP_EMAIL>().Where(c => c.EMAIL_STT == stt).ToList();
            return _ccMail;
        }
        //Counter
        public List<ESHOP_HITCOUNTER> counter()
        {
            var list = db.ESHOP_HITCOUNTERs.ToList();
            return list;
        }
        public void Addvister()
        {
            try
            {
                var _hitTotal = db.GetTable<ESHOP_CONFIG>();
                if (_hitTotal.ToList().Count > 0)
                {
                    _hitTotal.ToList()[0].CONFIG_HITCOUNTER = _hitTotal.ToList()[0].CONFIG_HITCOUNTER + 1;
                    db.SubmitChanges();
                }
                var list = db.GetTable<ESHOP_HITCOUNTER>().Where(a => (a.HIT_DATE.Value.Date - DateTime.Now.Date).Days == 0).ToList();

                if (list.Count > 0)
                {
                    list[0].HIT_VALUE += 1;
                    db.SubmitChanges();
                }
                else
                {
                    ESHOP_HITCOUNTER eshop = new ESHOP_HITCOUNTER();
                    eshop.HIT_VALUE = 1;
                    eshop.HIT_DATE = DateTime.Now;
                    db.ESHOP_HITCOUNTERs.InsertOnSubmit(eshop);
                    db.SubmitChanges();
                }
            }
            catch (Exception) { }
           
        }
        public void Deletebasket(Guid _guid)
        {
            var _bas = from a in db.ESHOP_BASKETs
                       where a.CUSTOMER_OID == _guid
                       select a;

            db.ESHOP_BASKETs.DeleteAllOnSubmit(_bas);

            db.SubmitChanges();
        }
        public int getvisittoday()
        {
            try
            {
                int temp = 0;
                var list = db.ESHOP_HITCOUNTERs.Where(n => (n.HIT_DATE.Value.Date - DateTime.Now.Date).Days == 0).ToList();
                if (list.Count > 0)
                {
                    temp = Utils.CIntDef(list[0].HIT_VALUE);
                }
                return temp;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        public int getvisittuannay()
        {
            try
            {
                int temp = 0;
                var list = db.ESHOP_HITCOUNTERs.Where(n => (n.HIT_DATE.Value.Date - DateTime.Now.Date).Days >= -7).Sum(n => n.HIT_VALUE);

                temp = Utils.CIntDef(list);

                return temp;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
    }
}
