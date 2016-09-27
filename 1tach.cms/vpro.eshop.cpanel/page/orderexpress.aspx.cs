using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Configuration;
using System.Net.Mail;
using System.Data;

namespace vpro.eshop.cpanel.page
{
    public partial class orderexpress : System.Web.UI.Page
    {
        eshopdbDataContext db = new eshopdbDataContext();
        int idorder = 0;
        public string _Mailbody = string.Empty;
        public static int _idorder = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            idorder = Utils.CIntDef(Request["id"]);
            Hyperback.NavigateUrl = "order_list.aspx";
            if (!IsPostBack)
            {
                Load_city();
                Load_distric(-1);
            }
        }
        #region Button drop
        private void Load_city()
        {
            var list = db.ESHOP_PROPERTies.Where(n => n.PROP_RANK == 2).ToList();
            Drcity.DataValueField = "PROP_ID";
            Drcity.DataTextField = "PROP_NAME";
            Drcity.DataSource = list;
            Drcity.DataBind();
            ListItem l = new ListItem("--- Thành phố ---", "0");
            l.Selected = true;
            Drcity.Items.Insert(0, l);
        }
        private void Load_distric(int id)
        {
            var list = db.ESHOP_PROPERTies.Where(n => n.PROP_RANK == 3 && n.PROP_PARENT_ID == id).ToList();
            if (list.Count > 0)
            {
                Drdistric.DataValueField = "PROP_ID";
                Drdistric.DataTextField = "PROP_NAME";
                Drdistric.DataSource = list;
                Drdistric.DataBind();
                ListItem l = new ListItem("--- Quận/Huyện ---", "0");
                l.Selected = true;
                Drdistric.Items.Insert(0, l);
            }
            else
            {
                DataTable dt = new DataTable("Newtable");

                dt.Columns.Add(new DataColumn("PROP_ID"));
                dt.Columns.Add(new DataColumn("PROP_NAME"));

                DataRow row = dt.NewRow();
                row["PROP_ID"] = 0;
                row["PROP_NAME"] = "--- Quận/Huyện ---";
                dt.Rows.Add(row);

                Drdistric.DataTextField = "PROP_NAME";
                Drdistric.DataValueField = "PROP_ID";
                Drdistric.DataSource = dt;
                Drdistric.DataBind();

            }
        }
        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            Hynews.Text = getName();
            Hynews.NavigateUrl = "news.aspx?type=1&news_id=" + getIdnews();
        }
        protected void Drcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Utils.CIntDef(Drcity.SelectedValue);
            Load_distric(id);
        }
        #endregion
        #region load data
        private string getCode(int id)
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_ID == id).ToList();
            if (list.Count > 0)
            {
                return list[0].NEWS_CODE;
            }
            return "";
        }
        private string getName()
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_CODE == txtcode.Text.Trim()).ToList();
            if (list.Count > 0)
            {
                return list[0].NEWS_TITLE;
            }
            return "";
        }
        private int getIdnews()
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_CODE == txtcode.Text.Trim()).ToList();
            if (list.Count > 0)
            {
                return list[0].NEWS_ID;
            }
            return 0;
        }
        #endregion
        #region Save
        private void Save(string link = "")
        {
            string _emailreplace=string.Empty;
            var getEMailconfig = db.ESHOP_EMAILs.Where(n => n.EMAIL_STT == 3).ToList();
            if (getEMailconfig.Count > 0)
                _emailreplace = getEMailconfig[0].EMAIL_TO;
            Guid _guid = Guid.NewGuid();
            string _sName = txtname.Value;
            string _sEmail = txtemail.Value;
            if (String.IsNullOrEmpty(_sEmail))
                _sEmail = _emailreplace;
            string _sAddress = txtadd.Value + " - " + getNameArea(Utils.CIntDef(Drdistric.SelectedValue)) + " - " + getNameArea(Utils.CIntDef(Drcity.SelectedValue));
            DateTime _dateDate = DateTime.Now;
            string _sEmailCC = string.Empty;
            string _sPhone = txtphone.Value;
            int quantity = Utils.CIntDef(txtquan.Value);
            string _hinhtuc = "Thanh toán tại văn phòng"; ;
            string _webname = ConfigurationManager.AppSettings["EmailDisplayName"];
            string _url = ConfigurationManager.AppSettings["URLWebsite"];
            var _ccMail = db.GetTable<ESHOP_EMAIL>().Where(c => c.EMAIL_STT == 3).ToList();
            if (_ccMail.ToList().Count > 0)
            {
                _sEmailCC = _ccMail.ToList()[0].EMAIL_TO;
            }
            if (Payment_cart_rs(_guid, getTotal(), _sEmail, _sName, _sPhone, _sAddress, 1, _hinhtuc, "", _webname, _url, quantity))
            {
                SendEmailSMTP("Thông báo: Bạn đã đặt hàng thành công", _sEmail, _sEmailCC, "", _Mailbody, true, false);
            }
            Response.Redirect(link);
        }
        private decimal getTotal()
        {
            var list = db.ESHOP_NEWs.Where(n => n.NEWS_ID == getIdnews()).ToList();
            if (list.Count > 0)
                return Utils.CDecDef(list[0].NEWS_PRICE2 * Utils.CIntDef(txtquan.Value));
            return 0;
        }
        // String email
        private string Email_product(string NEWS_TITLE, string BASKET_PRICE, int BASKET_QUANTITY, string _subTotal)
        {

            string _mailBody = string.Empty;
            int _mailCount = 1;
            _mailBody += "<tr>";
            _mailBody += "<td align='center' valign='middle' bgcolor='#FFFFFF' class='red arial14'>" + _mailCount++;
            _mailBody += "</td>";

            _mailBody += "<td align='left' valign='top' bgcolor='#FFFFFF'>" + NEWS_TITLE;// +"-" + item.BASKET_FIELD1 + "/" + item.BASKET_FIELD2;
            _mailBody += "</td>";

            _mailBody += "<td align='right' valign='middle' bgcolor='#FFFFFF'>" + BASKET_PRICE;
            _mailBody += "</td>";

            _mailBody += "<td align='right' valign='middle' bgcolor='#FFFFFF' class='green'>" + BASKET_QUANTITY;

            _mailBody += "</td>";

            _mailBody += "<td align='right' valign='middle' bgcolor='#FFFFFF' class='green arial14'>";
            _mailBody += "<strong>" + _subTotal;
            _mailBody += "</strong>";
            _mailBody += "</td>";

            _mailBody += "</tr>";

            return _mailBody;

        }
        private string Email_info_product_customer(string _mailBody, string _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, string hinhthuc, string _sDesc, string _nameweb, string _url_web)
        {
            string _mail_send = "<table width='100%' border='0' cellspacing='1' cellpadding='10' bgcolor='#CCCCCC'> <tr> <th width='10%' bgcolor='#E3E3E3' class='green'> STT </th> <th width='30%' align='center' bgcolor='#E3E3E3' class='green'> Tên sản phẩm </th> <th width='15%' align='center' bgcolor='#E3E3E3' class='green'> Đơn giá </th> <th width='10%' align='center' bgcolor='#E3E3E3' class='green'> Số lượng </th> <th width='20%' align='center' bgcolor='#E3E3E3' class='green'> Thành tiền </th> </tr>" +
                        _mailBody + " <tr> <th width='5%' align='center' bgcolor='#E3E3E3' class='green'></th></th> <th width='20%' align='center' bgcolor='#E3E3E3' class='green'></th><th width='15%' align='center' bgcolor='#E3E3E3' class='green'> </th><th width='20%' align='center' bgcolor='#E3E3E3' class='red arial14'>TỔNG TIỀN</th><th width='40%' align='right' bgcolor='#E3E3E3' class='red arial14'>" + _totalmoney + "</th></tr></table>";
            string _mail_userinfo = "";
            _mail_userinfo += "<br>Địa chỉ email : " + _sEmail;
            _mail_userinfo += "<br>Tên người mua : " + _sName;
            _mail_userinfo += "<br>Số điện thoại : " + _sPhone;
            _mail_userinfo += "<br>Địa chỉ : " + _sAddress;
            _mail_userinfo += "<br>Hình thức thanh toán : " + hinhthuc + "";
            _mail_userinfo += "<br>Ghi chú : " + _sDesc + "<br>";
            string _sMailBody = "";
            _sMailBody += "Cám ơn quý khách: " + _sName + " đã đặt hàng. Đây là email được gửi từ website của " + _nameweb + "<br>";
            _sMailBody += "<br>" + _mail_userinfo;
            _sMailBody += "<br>Xin vui lòng kiểm tra chi tiết đơn đặt hàng của bạn dưới đây: <br><br>" + _mail_send;
            _sMailBody += "<br>";
            _sMailBody += "<table width='100%' border='0' cellspacing='1' cellpadding='10' bgcolor='#CCCCCC'>";
            _sMailBody += "<tr>";
            _sMailBody += "<th align='left'>Thành tiền</th>";
            _sMailBody += "<td>" + _totalmoney + "</td>";
            _sMailBody += "</tr></table>";
            _sMailBody += "<br> - Cảm ơn bạn đã mua sắm tại website " + _url_web + ". Chúng tôi sẽ giao hàng cho bạn sớm nhất có thể.<br>";
            _sMailBody += "<br><hr>- Để biết thêm thông tin xin liên hệ với chúng tôi trên website: " + _url_web + ". Xin vui lòng không trả lời email này. Cảm ơn!";
            return _sMailBody;

        }
        //Get oder_code
        private string Getorder_code()
        {
            try
            {
                var _vOrderShop = db.GetTable<ESHOP_ORDER>().OrderByDescending(a => a.ORDER_ID);
                int iNo = 0;
                string _sMonth = string.Empty;
                string _sDay = string.Empty;
                string _year = string.Empty;
                if (_vOrderShop.ToList().Count > 0)
                {

                    iNo = _vOrderShop.ToList().Count > 0 ? Utils.CIntDef(_vOrderShop.ToList()[0].ORDER_ID) + 1 : 1;

                    _sMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + Utils.CStrDef(DateTime.Now.Month) : Utils.CStrDef(DateTime.Now.Month);

                    _sDay = DateTime.Now.Day.ToString().Length == 1 ? "0" + Utils.CStrDef(DateTime.Now.Day) : Utils.CStrDef(DateTime.Now.Day);

                    _year = DateTime.Now.Year.ToString().Substring(DateTime.Now.Year.ToString().Length - 2, 2);

                }

                string _sOrderCode = "HB" + _sDay + _sMonth + _year + iNo;
                return _sOrderCode;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Inser order
        private int Insert_Order(decimal Total_All, string Name, string OrderCode, string Email, string Address, int PaymentID, Guid News_guid, string Desc, DateTime Date, string Phone)
        {
            int _orderID = -1;
            ESHOP_ORDER _order = new ESHOP_ORDER();
            _order.ORDER_TOTAL_ALL = Total_All;
            _order.ORDER_TOTAL_AMOUNT = Total_All;
            _order.ORDER_NAME = Name;
            _order.ORDER_CODE = OrderCode;
            _order.ORDER_EMAIL = Email;
            _order.ORDER_ADDRESS = Address;
            _order.ORDER_PAYMENT = PaymentID;
            _order.CUSTOMER_OID = News_guid;
            _order.ORDER_PUBLISHDATE = DateTime.Now;
            _order.ORDER_STATUS = 0;
            _order.ORDER_FIELD1 = Desc;
            _order.ORDER_UPDATE = Date;
            _order.ORDER_PHONE = Phone;
            db.ESHOP_ORDERs.InsertOnSubmit(_order);
            db.SubmitChanges();

            var _getOrderID = db.GetTable<ESHOP_ORDER>().Where(a => a.CUSTOMER_OID == News_guid);
            _orderID = _getOrderID.ToList()[0].ORDER_ID;
            return _orderID;
        }
        //Payment
        public bool Payment_cart_rs(Guid _guid, decimal _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, int _iPaymentID, string hinhthuc, string _sDesc, string _nameweb, string _url_web, int quantity)
        {
            try
            {
                int _orderID = Insert_Order(_totalmoney, _sName, Getorder_code(), _sEmail, _sAddress, _iPaymentID, _guid, _sDesc, DateTime.Now, _sPhone);
                //Lấy thông tin sản phẩm trong bảng giỏ hàng 
                _idorder = _orderID;
                var _product = db.ESHOP_NEWs.Where(n => n.NEWS_ID == getIdnews());

                //Thêm thông tin vào bảng chi tiết đơn hàng
                string _mailbody = string.Empty;
                foreach (var item in _product)
                {
                    decimal _subTotal = Utils.CDecDef(item.NEWS_PRICE2 * quantity);
                    ESHOP_ORDER_ITEM _orderItem = new ESHOP_ORDER_ITEM();
                    _orderItem.NEWS_ID = Utils.CIntDef(item.NEWS_ID);
                    _orderItem.ITEM_PRICE = item.NEWS_PRICE2;
                    _orderItem.ITEM_PUBLISDATE = DateTime.Now;
                    _orderItem.ITEM_QUANTITY = quantity;
                    _orderItem.ITEM_SUBTOTAL = _subTotal;
                    _orderItem.ORDER_ID = _orderID;

                    db.ESHOP_ORDER_ITEMs.InsertOnSubmit(_orderItem);
                    db.SubmitChanges();
                    _mailbody += Email_product(item.NEWS_TITLE, FormatMoney(item.NEWS_PRICE2), quantity, FormatMoney(_subTotal));
                }
                string _sMailBody = Email_info_product_customer(_mailbody, FormatMoney(_totalmoney), _sEmail, _sName, _sPhone, _sAddress, hinhthuc, _sDesc, _nameweb, _url_web);
                _Mailbody = _sMailBody;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region function
        private string getNameArea(int id)
        {
            var list = db.ESHOP_PROPERTies.Where(n => n.PROP_ID == id).ToList();
            if (list.Count > 0)
                return list[0].PROP_NAME;
            return "";
        }
        private string FormatMoney(object Expression)
        {
            try
            {
                string Money = String.Format("{0:0,0 VNĐ}", Expression);
                return Money.Replace(",", ".");
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        private void SendEmailSMTP(string strSubject, string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]));
                    mail.To.Add(toAddress);
                    if (ccAddress != "")
                    {
                        mail.CC.Add(ccAddress);
                    }
                    if (bccAddress != "")
                    {
                        mail.Bcc.Add(bccAddress);
                    }
                    mail.Subject = strSubject;

                    string str = "<html>" + body + "</html>";
                    mail.Body = str;
                    mail.IsBodyHtml = isHtml;
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = isSSL;
                    client.Host = Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailHost"]);
                    client.Port = Utils.CIntDef(System.Configuration.ConfigurationManager.AppSettings["EmailPort"]);
                    client.Credentials = new System.Net.NetworkCredential(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]));

                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            Save("order_list.aspx");
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            Save("orderexpress.aspx");
        }

        protected void LbsaveClose_Click(object sender, EventArgs e)
        {
            Save("order_list.aspx");
        }
    }
}