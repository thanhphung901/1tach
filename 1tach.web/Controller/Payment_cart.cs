using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using vpro.functions;
using System.Web;
using System.Web.UI;

namespace Controller
{
    public class Payment_cart
    {
        #region Decclare
        dbVuonRauVietDataContext db = new dbVuonRauVietDataContext();
        Propertity per = new Propertity();
        Config cf = new Config();
        public string _Mailbody = string.Empty;
        public static int _idorder = 0;
        #endregion
        // String email
        private string getImgLogo()
        {
            var list = per.Load_logo_or_sologan("1", 1);
            if (list.Count > 0)
            {
                if (!String.IsNullOrEmpty(list[0].BANNER_FILE))
                    return PathFiles.GetPathBanner(list[0].BANNER_ID) + "/" + list[0].BANNER_FILE;
            }
            return string.Empty;
        }
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
        private string Email_info_product_customer(string _mailBody,string _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, string hinhthuc, string _sDesc, string _nameweb, string _url_web)
        {
            string _mail_send = "<table width='100%' border='0' cellspacing='1' cellpadding='10' bgcolor='#CCCCCC'> <tr> <th width='10%' bgcolor='#E3E3E3' class='green'> STT </th> <th width='30%' align='center' bgcolor='#E3E3E3' class='green'> Tên sản phẩm </th> <th width='15%' align='center' bgcolor='#E3E3E3' class='green'> Đơn giá </th> <th width='10%' align='center' bgcolor='#E3E3E3' class='green'> Số lượng </th> <th width='20%' align='center' bgcolor='#E3E3E3' class='green'> Thành tiền </th> </tr>" +
                        _mailBody + " <tr> <th width='5%' align='center' bgcolor='#E3E3E3' class='green'></th></th> <th width='20%' align='center' bgcolor='#E3E3E3' class='green'></th><th width='15%' align='center' bgcolor='#E3E3E3' class='green'> </th><th width='20%' align='center' bgcolor='#E3E3E3' class='red arial14'>TỔNG TIỀN</th><th width='40%' align='right' bgcolor='#E3E3E3' class='red arial14'>" + _totalmoney + "</th></tr></table>";
            string _mail_userinfo = "";
            _mail_userinfo += "<br>Địa chỉ email : " + _sEmail;
            _mail_userinfo += "<br>Tên người mua : " + _sName;
            _mail_userinfo += "<br>Số điện thoại : " + _sPhone;
            _mail_userinfo += "<br>Địa chỉ : " + _sAddress;
            _mail_userinfo += "<br>Ghi chú : " + _sDesc + "<br>";
            string _sMailBody = "";
            _sMailBody += "Cám ơn quý khách: " + _sName + " đã đặt phòng. Đây là email được gửi từ website của " + _nameweb + "<br>";
            _sMailBody += "<br>" + _mail_userinfo;
            _sMailBody += "<br> - Cảm ơn bạn đã đặt phòng tại website " + _url_web + "<br>";
            _sMailBody += "<br><hr>- Để biết thêm thông tin xin liên hệ với chúng tôi trên website: " + _url_web + ". Xin vui lòng không trả lời email này. Cảm ơn!";
            return _sMailBody;

        }
        private string htmlEmalHeader(string _cusName, string _code)
        {
            StringBuilder _res = new StringBuilder();
            string _imglogo = getImgLogo();
            _res.Append("<body style='padding: 0;margin: 0;font: normal 13px/1.5 Arial, Helvetica, sans-serif;color: #333333'>");
            _res.Append("<div>");
            _res.Append("<div class='wrap head' style='background: #f4f3f3; width: 600px;padding: 10px 0; position: relative'> <a href='http://www.thoitrangngocquyen.vn'><img src='http://thoitrangngocquyen.vn" + _imglogo + "' alt='' width='150' /></a> <div class='hotline' style='position: absolute; top: 0; right: 0;color: #FF0000;font-size: 18px;background: url(http://www.thoitrangngocquyen.vn/vi-vn/Images/phone_ico.png) 0 center no-repeat;padding-left: 28px;margin-top: 10px;'><b>0908.169.044</b></div> </div>");
            _res.Append("</div>");
            _res.Append("<div class='wrap' style='width: 600px;margin: 0 auto'>");
            _res.Append("<p><strong>Chào " + _cusName + ",</strong></p>");
            _res.Append("<p>Chúng  tôi đã nhận được đơn đặt hàng của bạn tại &nbsp;<a href='http://www.thoitrangngocquyen.vn'>www.bodonghe.com</a></p>");
            _res.Append("<p><strong>Đơn hàng</strong> " + _code + " mua lúc  " + string.Format("{0: dd/MM/yyyy hh:mm:ss tt}", DateTime.Now) + "</p>");
            _res.Append("<table width='100%' border='0' cellpadding='5' cellspacing='1' bgcolor='#999999'>");
            _res.Append("<tr>");
            _res.Append("<th align='center' valign='bottom' bgcolor='#414141' scope='col'><span style='color: #FFFFFF'>Phiếu  / Sản phẩm</span></th>");
            _res.Append("<th align='center' valign='bottom' bgcolor='#414141' scope='col'><span style='color: #FFFFFF'>Đơn  giá</span></th>");
            _res.Append("<th align='center' valign='bottom' bgcolor='#414141' scope='col'><span style='color: #FFFFFF'>Số  lượng</span></th>");
            _res.Append("<th align='right' valign='bottom' bgcolor='#414141' scope='col'><span style='color: #FFFFFF'>Thành  tiền</span></th>");
            _res.Append("</tr>");
            return _res.ToString();
        }
        private string htmlEmailBody(string _nameproduct, string _pricePro, int _quantity, string _amount)
        {
            StringBuilder _res = new StringBuilder();
            _res.Append("<tr>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>" + _nameproduct + "</td>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>" + _pricePro + "</td>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>" + _quantity + "</td>");
            _res.Append("<td align='right' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>" + _amount + "</td>");
            _res.Append("</tr>");
            _res.Append("<tr>");
            return _res.ToString();
        }
        private string htmlEmailFooter(string _totalamountFirst, string _ship, string _totalfilnal, string _hinhthuc, string _address, string _email, string _phone, string _note)
        {
            StringBuilder _res = new StringBuilder();
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>&nbsp;</td>");
            _res.Append("<td colspan='2' valign='bottom' align='right' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>Tổng tiền</strong></td>");
            _res.Append("<td align='right' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>" + _totalamountFirst + "</strong></td>");
            _res.Append("</tr>");
            _res.Append("<tr>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>&nbsp;</td>");
            _res.Append("<td colspan='2' valign='bottom' align='right' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>Phí vận chuyển</strong></td>");
            _res.Append("<td align='right' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>" + _ship + "</strong></td>");
            _res.Append("</tr>");
            _res.Append("<tr>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>&nbsp;</td>");
            _res.Append("<td colspan='2' valign='bottom' align='right' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>Thành tiền</strong></td>");
            _res.Append("<td align='right' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>" + _totalfilnal + "</strong></td>");
            _res.Append("</tr>");
            _res.Append("<tr>");
            _res.Append("<td align='left' valign='middle' bgcolor='#FFFFFF'>Hình thức thanh toán: " + _hinhthuc + "</td>");
            _res.Append("</tr>");
            _res.Append("</table>");
            _res.Append("<p><strong>Địa  chỉ giao hàng: </strong>" + _address + "</p>");
            _res.Append("<p><strong>Điện thoại:</strong> " + _phone + " - <strong>Email:</strong> <a href='mailto:" + _email + "'>" + _email + "</a></p>");
            _res.Append("<p><strong>Ghi chú:</strong> " + _note + "</p>");
            _res.Append("<p>www.thoitrangngocquyen.vn cảm ơn và rất  mong tiếp tục nhận được sự ủng hộ của bạn</p>");
            _res.Append("</div>");
            _res.Append("<div style='background: #414141; text-align: center; padding: 5px 0; color: #FFFFFF'>");
            _res.Append("<div class='wrap'>");
            _res.Append("<p><strong>Chúng tôi luôn sẵn sàng hỗ trợ bạn qua email <a href='mailto:thoitrangngocquyen@gmail.com' style='color: #FFFFFF'>info@thoitrangngocquyen.vn</a> và hotline: 123456789</strong></p>");
            _res.Append("<p style='font-size:11px'><strong>CÔNG TY CỔ PHẦN CÔNG NGHỆ VÀ TRUYỀN THÔNG ĐẲNG CẤP VIỆT</strong><br>");
            _res.Append("<strong>Trụ sở</strong>: 232/17 Cộng Hòa, P. 12, Q. Tân Bình, TP. Hồ Chí Minh</p>");
            _res.Append("</div>");
            _res.Append("</div>");
            _res.Append("</body>");
            return _res.ToString();
        }
        private string htmlEmalHeaderEN(string _cusName, string _code)
        {
            StringBuilder _res = new StringBuilder();
            string _imglogo = getImgLogo();
            _res.Append("<body style='padding: 0;margin: 0;font: normal 13px/1.5 Arial, Helvetica, sans-serif;color: #333333'>");
            _res.Append("<div>");
            _res.Append("<div class='wrap head' style='background: #f4f3f3; width: 600px;padding: 10px 0; position: relative'> <a href='http://www.thoitrangngocquyen.vn'><img src='http://www.thoitrangngocquyen.vn" + _imglogo + "' alt='' width='150' /></a> <div class='hotline' style='position: absolute; top: 0; right: 0;color: #FF0000;font-size: 18px;background: url(http://www.thoitrangngocquyen.vn/vi-vn/Images/phone_ico.png) 0 center no-repeat;padding-left: 28px;margin-top: 10px;'><b>0908.169.044</b></div> </div>");
            _res.Append("</div>");
            _res.Append("<div class='wrap' style='width: 600px;margin: 0 auto'>");
            _res.Append("<p><strong>Chào " + _cusName + ",</strong></p>");
            _res.Append("<p>We have received your order in &nbsp;<a href='http://www.thoitrangngocquyen.vn'>www.thoitrangngocquyen.com</a></p>");
            _res.Append("<p><strong>Order</strong> " + _code + " buy: " + string.Format("{0: dd/MM/yyyy hh:mm:ss tt}", DateTime.Now) + "</p>");
            _res.Append("<table width='100%' border='0' cellpadding='5' cellspacing='1' bgcolor='#999999'>");
            _res.Append("<tr>");
            _res.Append("<th align='center' valign='bottom' bgcolor='#414141' scope='col'><span style='color: #FFFFFF'>Stamps / Products</span></th>");
            _res.Append("<th align='center' valign='bottom' bgcolor='#414141' scope='col'><span style='color: #FFFFFF'>Price</span></th>");
            _res.Append("<th align='center' valign='bottom' bgcolor='#414141' scope='col'><span style='color: #FFFFFF'>Quantity</span></th>");
            _res.Append("<th align='right' valign='bottom' bgcolor='#414141' scope='col'><span style='color: #FFFFFF'>Amount</span></th>");
            _res.Append("</tr>");
            return _res.ToString();
        }
        private string htmlEmailBodyEN(string _nameproduct, string _pricePro, int _quantity, string _amount)
        {
            StringBuilder _res = new StringBuilder();
            _res.Append("<tr>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>" + _nameproduct + "</td>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>" + _pricePro + "</td>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>" + _quantity + "</td>");
            _res.Append("<td align='right' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>" + _amount + "</td>");
            _res.Append("</tr>");
            _res.Append("<tr>");
            return _res.ToString();
        }
        private string htmlEmailFooterEN(string _totalamountFirst, string _ship, string _totalfilnal, string _hinhthuc, string _address, string _email, string _phone, string _note)
        {
            StringBuilder _res = new StringBuilder();
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>&nbsp;</td>");
            _res.Append("<td colspan='2' valign='bottom' align='right' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>Total money</strong></td>");
            _res.Append("<td align='right' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>" + _totalamountFirst + "</strong></td>");
            _res.Append("</tr>");
            _res.Append("<tr>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>&nbsp;</td>");
            _res.Append("<td colspan='2' valign='bottom' align='right' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>Transport fee</strong></td>");
            _res.Append("<td align='right' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>" + _ship + "</strong></td>");
            _res.Append("</tr>");
            _res.Append("<tr>");
            _res.Append("<td align='center' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'>&nbsp;</td>");
            _res.Append("<td colspan='2' valign='bottom' align='right' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>Amount</strong></td>");
            _res.Append("<td align='right' valign='bottom' bgcolor='#FFFFFF' style='border: 1px solid #999999;'><strong>" + _totalfilnal + "</strong></td>");
            _res.Append("</tr>");
            _res.Append("<tr>");
            _res.Append("<td align='left' valign='middle' bgcolor='#FFFFFF'>Payments: " + _hinhthuc + "</td>");
            _res.Append("</tr>");
            _res.Append("</table>");
            _res.Append("<p><strong>Shipping address: </strong>" + _address + "</p>");
            _res.Append("<p><strong>Phone number:</strong> " + _phone + " - <strong>Email:</strong> <a href='mailto:" + _email + "'>" + _email + "</a></p>");
            _res.Append("<p><strong>Note:</strong> " + _note + "</p>");
            _res.Append("<p>www.thoitrangngocquyen.vn Thanks and look forward to continue to receive your support</p>");
            _res.Append("</div>");
            _res.Append("<div style='background: #414141; text-align: center; padding: 5px 0; color: #FFFFFF'>");
            _res.Append("<div class='wrap'>");
            _res.Append("<p><strong>We are always ready to assist you via email <a href='mailto:thoitrangngocquyen@gmail.com' style='color: #FFFFFF'>info@esell.vn</a> and hotline: 123456789</strong></p>");
            _res.Append("<p style='font-size:11px'><strong>CÔNG TY CỔ PHẦN CÔNG NGHỆ VÀ TRUYỀN THÔNG ĐẲNG CẤP VIỆT</strong><br>");
            _res.Append("<strong>Trụ sở</strong>: 232/17 Cộng Hòa, P. 12, Q. Tân Bình, TP. Hồ Chí Minh</p>");
            _res.Append("</div>");
            _res.Append("</div>");
            _res.Append("</body>");
            return _res.ToString();
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
                if (_vOrderShop.ToList().Count > 0)
                {

                    iNo = _vOrderShop.ToList().Count > 0 ? Utils.CIntDef(_vOrderShop.ToList()[0].ORDER_ID) + 1 : 1;

                    _sMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + Utils.CStrDef(DateTime.Now.Month) : Utils.CStrDef(DateTime.Now.Month);

                    _sDay = DateTime.Now.Day.ToString().Length == 1 ? "0" + Utils.CStrDef(DateTime.Now.Day) : Utils.CStrDef(DateTime.Now.Day);
                }

                string _sOrderCode = DateTime.Now.Year + _sMonth + _sDay + "_" + iNo;
                return _sOrderCode;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //Inser order
        private int Insert_Order(decimal Total_All, string Name, string OrderCode, string Email, string Address, int PaymentID, Guid News_guid, string Desc, DateTime ngaygiao, string Phone, decimal ship,string thoigiangh)
        {
            int _orderID = -1;
            ESHOP_ORDER _order = new ESHOP_ORDER();
            _order.ORDER_TOTAL_ALL = Total_All;
            _order.ORDER_TOTAL_AMOUNT = ship > 0 ? Total_All + ship : Total_All;
            _order.ORDER_SHIPPING_FEE = ship;
            _order.ORDER_NAME = Name;
            _order.ORDER_CODE = OrderCode;
            _order.ORDER_EMAIL = Email;
            _order.ORDER_ADDRESS = Address;
            _order.ORDER_PAYMENT = PaymentID;
            _order.CUSTOMER_OID = News_guid;
            int _iUserID = Utils.CIntDef(HttpContext.Current.Session["USER_ID"]);
            if (_iUserID != 0)
                _order.CUSTOMER_ID = _iUserID;
            _order.ORDER_PUBLISHDATE = DateTime.Now;
            _order.ORDER_STATUS = 0;
            _order.ORDER_FIELD1 = Desc;
            _order.ORDER_UPDATE = ngaygiao;
            _order.ORDER_PHONE = Phone;
            _order.ORDER_FIELD2 = thoigiangh;
            db.ESHOP_ORDERs.InsertOnSubmit(_order);
            db.SubmitChanges();

            var _getOrderID = db.GetTable<ESHOP_ORDER>().Where(a => a.CUSTOMER_OID == News_guid);
            _orderID = _getOrderID.ToList()[0].ORDER_ID;
            return _orderID;
        }

        private int Insert_Ordertour(string Name, string OrderCode, string Email, string Address, Guid News_guid, string Desc, DateTime ngaygiao, string Phone, string thoigiangh, int PaymentID)
        {
            int _orderID = -1;
            ESHOP_ORDER _order = new ESHOP_ORDER();
            _order.ORDER_NAME = Name;
            _order.ORDER_CODE = OrderCode;
            _order.ORDER_EMAIL = Email;
            _order.ORDER_ADDRESS = Address;
            _order.CUSTOMER_OID = News_guid;
            _order.ORDER_PUBLISHDATE = DateTime.Now;
            _order.ORDER_STATUS = 0;
            _order.ORDER_FIELD1 = Desc;
            _order.ORDER_UPDATE = ngaygiao;
            _order.ORDER_PHONE = Phone;
            _order.ORDER_FIELD2 = thoigiangh;

            db.ESHOP_ORDERs.InsertOnSubmit(_order);
            db.SubmitChanges();

            var _getOrderID = db.GetTable<ESHOP_ORDER>().Where(a => a.CUSTOMER_OID == News_guid);
            _orderID = _getOrderID.ToList()[0].ORDER_ID;
            return _orderID;
        }
        //Payment
        public bool Payment_cart_rs(Guid _guid, decimal _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, int _iPaymentID, string hinhthuc, string _sDesc, string _nameweb, string _url_web, decimal ship,string thoigiangiaohang,DateTime ngaygiaohang)
        {
            try
            {
                string _code = Getorder_code();
                int _orderID = Insert_Order(_totalmoney, _sName, _code, _sEmail, _sAddress, _iPaymentID, _guid, _sDesc, ngaygiaohang, _sPhone, ship, thoigiangiaohang);
                //Lấy thông tin sản phẩm trong bảng giỏ hàng 
                _idorder = _orderID;
                var _product = from a in db.ESHOP_BASKETs
                               join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                               where a.CUSTOMER_OID == _guid
                               select new
                               {
                                   a.NEWS_ID,
                                   a.BASKET_PRICE,
                                   a.BASKET_QUANTITY,
                                   b.NEWS_TITLE
                               };

                //Thêm thông tin vào bảng chi tiết đơn hàng
                string _mailbody = string.Empty;
                foreach (var item in _product)
                {
                    decimal _subTotal = Convert.ToDecimal(double.Parse(item.BASKET_PRICE.ToString()) * double.Parse(item.BASKET_QUANTITY.ToString()));
                    ESHOP_ORDER_ITEM _orderItem = new ESHOP_ORDER_ITEM();
                    _orderItem.NEWS_ID = Utils.CIntDef(item.NEWS_ID);
                    _orderItem.ITEM_PRICE = item.BASKET_PRICE;
                    _orderItem.ITEM_PUBLISDATE = DateTime.Now;
                    _orderItem.ITEM_QUANTITY = item.BASKET_QUANTITY;
                    _orderItem.ITEM_SUBTOTAL = _subTotal;
                    _orderItem.ORDER_ID = _orderID;

                    db.ESHOP_ORDER_ITEMs.InsertOnSubmit(_orderItem);
                    db.SubmitChanges();
                    _mailbody += htmlEmailBody(item.NEWS_TITLE, FormatMoney(item.BASKET_PRICE), Utils.CIntDef(item.BASKET_QUANTITY), FormatMoney(_subTotal));
                }
                string noteship = (ship > 0 ? FormatMoney(ship) : (ship == 0 ? "Miễn phí" : "Liên hệ"));
                string _totalFirst = FormatMoney(_totalmoney);
                string total_amount = ship > 0 ? FormatMoney(_totalmoney + ship) : FormatMoney(_totalmoney);
                string _htmlHeader = htmlEmalHeader(_sName, _code);
                string _sMailBody = _htmlHeader + _mailbody + htmlEmailFooter(_totalFirst, noteship, total_amount, hinhthuc, _sAddress, _sEmail, _sPhone, _sDesc);
                _Mailbody = _sMailBody;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Payment_cart_rsEN(Guid _guid, decimal _totalmoney, string _sEmail, string _sName, string _sPhone, string _sAddress, int _iPaymentID, string hinhthuc, string _sDesc, string _nameweb, string _url_web, decimal ship, string thoigiangiaohang, DateTime ngaygiaohang)
        {
            try
            {
                string _code = Getorder_code();
                int _orderID = Insert_Order(_totalmoney, _sName, _code, _sEmail, _sAddress, _iPaymentID, _guid, _sDesc, ngaygiaohang, _sPhone, ship, thoigiangiaohang);
                //Lấy thông tin sản phẩm trong bảng giỏ hàng 
                _idorder = _orderID;
                var _product = from a in db.ESHOP_BASKETs
                               join b in db.ESHOP_NEWs on a.NEWS_ID equals b.NEWS_ID
                               where a.CUSTOMER_OID == _guid
                               select new
                               {
                                   a.NEWS_ID,
                                   a.BASKET_PRICE,
                                   a.BASKET_QUANTITY,
                                   b.NEWS_TITLE
                               };

                //Thêm thông tin vào bảng chi tiết đơn hàng
                string _mailbody = string.Empty;
                foreach (var item in _product)
                {
                    decimal _subTotal = Convert.ToDecimal(double.Parse(item.BASKET_PRICE.ToString()) * double.Parse(item.BASKET_QUANTITY.ToString()));
                    ESHOP_ORDER_ITEM _orderItem = new ESHOP_ORDER_ITEM();
                    _orderItem.NEWS_ID = Utils.CIntDef(item.NEWS_ID);
                    _orderItem.ITEM_PRICE = item.BASKET_PRICE;
                    _orderItem.ITEM_PUBLISDATE = DateTime.Now;
                    _orderItem.ITEM_QUANTITY = item.BASKET_QUANTITY;
                    _orderItem.ITEM_SUBTOTAL = _subTotal;
                    _orderItem.ORDER_ID = _orderID;

                    db.ESHOP_ORDER_ITEMs.InsertOnSubmit(_orderItem);
                    db.SubmitChanges();
                    _mailbody += htmlEmailBodyEN(item.NEWS_TITLE, FormatMoney(item.BASKET_PRICE), Utils.CIntDef(item.BASKET_QUANTITY), FormatMoney(_subTotal));
                }
                string noteship = (ship > 0 ? FormatMoney(ship) : (ship == 0 ? "Free" : "Contact"));
                string _totalFirst = FormatMoney(_totalmoney);
                string total_amount = ship > 0 ? FormatMoney(_totalmoney + ship) : FormatMoney(_totalmoney);
                string _htmlHeader = htmlEmalHeaderEN(_sName, _code);
                string _sMailBody = _htmlHeader + _mailbody + htmlEmailFooterEN(_totalFirst, noteship, total_amount, hinhthuc, _sAddress, _sEmail, _sPhone, _sDesc);
                _Mailbody = _sMailBody;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Delete basket
        public void Delete_basket(Guid _deleteBasket)
        {
            try
            {
                //sau khi thêm, xóa hết sản phẩm trong giỏ hàng của người đó
                var _bas = from a in db.ESHOP_BASKETs
                           where a.CUSTOMER_OID == _deleteBasket
                           select a;
                if (_bas.ToList().Count > 0)
                {
                    db.ESHOP_BASKETs.DeleteAllOnSubmit(_bas);

                    db.SubmitChanges();

                    HttpContext.Current.Session["News_guid"] = Guid.NewGuid();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        //Check cart
        public bool Check_Cart(Guid _guid)
        {
            try
            {
                var _vCheck = db.GetTable<ESHOP_BASKET>().Where(a => a.CUSTOMER_OID == _guid);
                if (_vCheck.ToList().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;
            }
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
       
    }
}
