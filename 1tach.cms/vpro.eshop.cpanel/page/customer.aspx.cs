using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using System.Web.UI.HtmlControls;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class customer : System.Web.UI.Page
    {
        #region Declare

        private int m_customer_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        clsFormat fm = new clsFormat();
        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_customer_id = Utils.CIntDef(Request["customer_id"]);
            Hyperback.NavigateUrl = "customer_list.aspx";
            if (m_customer_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
                //pickbirth.returnDate = DateTime.Now;
                loadYear();
                loadMonth();
                loadDay(1);
                loadCity();
                loadDistric(-1);
                getInfo();
            }

        }
        #region load birth
        private void loadYear()
        {
            for (int i = DateTime.Now.Year; i >= 1980; i--)
            {
                ListItem l = new ListItem();
                l.Value = i.ToString();
                l.Text = i.ToString();
                Dryear.Items.Add(l);
            }
            ListItem s = new ListItem("Năm", "0");
            s.Selected = true;
            Dryear.Items.Insert(0, s);
        }
        private void loadMonth()
        {
            for (int i = 1; i <= 12; i++)
            {
                ListItem l = new ListItem();
                l.Value = i.ToString();
                l.Text = i.ToString();
                Drmonth.Items.Add(l);
            }
            ListItem s = new ListItem("Tháng", "0");
            s.Selected = true;
            Drmonth.Items.Insert(0, s);
        }
        private void loadDay(int _month)
        {
            
            int[] leday = new int[] { 1, 3, 5, 7, 8, 10, 12 };
            int limitday = 30;
            if (leday.Contains(_month))
                limitday = 31;
            else if (_month == 2)
                limitday = 28;
            for (int i = 1; i <= limitday; i++)
            {
                ListItem l = new ListItem();
                l.Value = i.ToString();
                l.Text = i.ToString();
                Drday.Items.Add(l);
            }
            ListItem s = new ListItem("Ngày", "0");
            s.Selected = true;
            Drday.Items.Insert(0, s);
        }
        
        protected void Drmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = Utils.CIntDef(Drmonth.SelectedValue);
            loadDay(month);
        }

        #endregion
        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            if (CheckExits(txtCustomerEmail.Value))
                lblError.Text = "Đã tồn tại email, vui lòng nhập email khác.";
            else
            SaveInfo();
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            if (CheckExits(txtCustomerEmail.Value))
                lblError.Text = "Đã tồn tại email, vui lòng nhập email khác.";
            else
                SaveInfo("customer.aspx");
        }
        protected void LbsaveClose_Click(object sender, EventArgs e)
        {
            if (CheckExits(txtCustomerEmail.Value))
                lblError.Text = "Đã tồn tại email, vui lòng nhập email khác.";
            else
                SaveInfo("customer_list.aspx");
        }
        private bool CheckExits(string strUN)
        {
            try
            {
                if (m_customer_id == 0)
                {
                    var exits = (from c in DB.ESHOP_CUSTOMERs where c.CUSTOMER_EMAIL == strUN select c);

                    if (exits.ToList().Count > 0)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;

            }
        }


        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        #endregion
        #region area
        private void loadCity()
        {
            var list = DB.ESHOP_PROPERTies.Where(n => n.PROP_RANK == 2);
            Drcity.DataValueField = "PROP_ID";
            Drcity.DataTextField = "PROP_NAME";
            Drcity.DataSource = list;
            Drcity.DataBind();
            ListItem l = new ListItem("---Thành phố---", "0");
            l.Selected = true;
            Drcity.Items.Insert(0,l);
        }
        private void loadDistric(int id)
        {
            var list = DB.ESHOP_PROPERTies.Where(n => n.PROP_RANK == 3&&n.PROP_PARENT_ID==id);
            Drdistric.DataValueField = "PROP_ID";
            Drdistric.DataTextField = "PROP_NAME";
            Drdistric.DataSource = list;
            Drdistric.DataBind();
            ListItem l = new ListItem("---Quận/huyện---", "0");
            l.Selected = true;
            Drdistric.Items.Insert(0, l);
        }
        protected void Drcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Utils.CIntDef(Drcity.SelectedValue);
            loadDistric(id);
        }

        #endregion
        #region My Functions
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        private string[] setSelectbirth(DateTime birth)
        {
            string []a=new string[3];
            if (!String.IsNullOrEmpty(Utils.CStrDef(birth)))
            {
                string day = birth.Day.ToString();
                string month = birth.Month.ToString();
                string year = birth.Year.ToString();
                a[0] = day;
                a[1] = month;
                a[2] = year;
            }
            return a;
        }
        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_CUSTOMERs
                              where g.CUSTOMER_ID == m_customer_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtCustomerName.Value = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_FULLNAME);
                    txtaddress.Value = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_ADDRESS);
                    txtCustomerPhone1.Value = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_PHONE1);
                    txtCustomerEmail.Value = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_EMAIL);
                    rblsex.SelectedValue = Utils.CStrDef(G_info.ToList()[0].CUSTOMER_FIELD3);
                    //pickbirth.returnDate =Utils.CDateDef(G_info.ToList()[0].CUSTOMER_UPDATE,DateTime.Now);
                    Drcity.SelectedValue = G_info.ToList()[0].CUSTOMER_FIELD1;
                    loadDistric(Utils.CIntDef(Drcity.SelectedValue));
                    Drdistric.SelectedValue = G_info.ToList()[0].CUSTOMER_FIELD2;
                    string[] a = setSelectbirth(Utils.CDateDef(G_info.ToList()[0].CUSTOMER_UPDATE,DateTime.Now));
                    if (a.Length > 0)
                    {
                        Drday.SelectedValue = a[0];
                        Drmonth.SelectedValue = a[1];
                        Dryear.SelectedValue = a[2];
                    }
                    //txtCustomerUN.Value = Utils.CStrDef(G_info.ToList()[0].USER_UN);
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveInfo(string strLink = "")
        {
            try
            {
                string _name = txtCustomerName.Value;
                string _email = txtCustomerEmail.Value;
                string _phone = txtCustomerPhone1.Value;
                string _address = txtaddress.Value;
                string _city = Drcity.SelectedValue;
                string _distric = Drdistric.SelectedValue;
                string _sex = rblsex.SelectedValue;
                string _date = Dryear.SelectedValue + "/" + Drmonth.SelectedValue + "/" + Drday.SelectedIndex;
                DateTime _birth = Utils.CDateDef(_date, DateTime.Now);
                string _pass = txtpassword.Value;
                
                var g_update = DB.GetTable<ESHOP_CUSTOMER>().Where(g => g.CUSTOMER_ID == m_customer_id);

                if (g_update.ToList().Count > 0)
                {

                    g_update.Single().CUSTOMER_FULLNAME = _name;
                    g_update.Single().CUSTOMER_EMAIL = _email;
                    g_update.Single().CUSTOMER_PHONE1 = _phone;
                    g_update.Single().CUSTOMER_ADDRESS = _address;
                    g_update.Single().CUSTOMER_FIELD1 = _city;
                    g_update.Single().CUSTOMER_FIELD2 = _distric;
                    g_update.Single().CUSTOMER_FIELD3 = _sex;
                    g_update.Single().CUSTOMER_UPDATE = _birth;
                    if (!String.IsNullOrEmpty(_pass))
                    {
                        g_update.Single().CUSTOMER_PW = fm.MaHoaMatKhau(_pass);
                    }
                    DB.SubmitChanges();

                    strLink = string.IsNullOrEmpty(strLink) ? "customer_list.aspx" : strLink;
                }
                else
                {
                    ESHOP_CUSTOMER cus = new ESHOP_CUSTOMER();
                    cus.CUSTOMER_FULLNAME = _name;
                    cus.CUSTOMER_EMAIL = _email;
                    cus.CUSTOMER_PHONE1 = _phone;
                    cus.CUSTOMER_ADDRESS = _address;
                    cus.CUSTOMER_FIELD1 = _city;
                    cus.CUSTOMER_FIELD2 = _distric;
                    cus.CUSTOMER_FIELD3 = _sex;
                    cus.CUSTOMER_UPDATE = _birth;
                    if (!String.IsNullOrEmpty(_pass))
                    {
                        cus.CUSTOMER_PW = fm.MaHoaMatKhau(_pass);
                    }
                    DB.ESHOP_CUSTOMERs.InsertOnSubmit(cus);
                    DB.SubmitChanges();
                    var list = DB.ESHOP_CUSTOMERs.OrderByDescending(n => n.CUSTOMER_ID).ToList();
                    if (list.Count > 0)
                        m_customer_id = list[0].CUSTOMER_ID;
                    strLink = string.IsNullOrEmpty(strLink) ? "customer.aspx?customer_id="+m_customer_id : strLink;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                { Response.Redirect(strLink); }
            }
        }

        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_CUSTOMER>().Where(g => g.CUSTOMER_ID == m_customer_id);

                DB.ESHOP_CUSTOMERs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("customer_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion

       
        
    }
}