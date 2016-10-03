using System;
using System.Linq;
using System.Web.Services;
using Controller;
using OneTach.Components;
using vpro.functions;

namespace OneTach
{
    public partial class Ajax_customer : System.Web.UI.Page
    {
        #region Declare
        static Account acc = new Account();
        static clsFormat fm = new clsFormat();
        static Checkcookie checkck = new Checkcookie();
        static setCookieLike setck = new setCookieLike();
        static Controller.Home index = new Controller.Home();
        static Function fun = new Function();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static string login(string email, string pass)
        {
            try
            {
                if (acc.Login(email, fm.MaHoaMatKhau(pass)))
                {

                    return "success";
                }
                else
                {
                    return "errors";
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return "";
            }
        }
        [WebMethod]
        public static string regis_mail(string email)
        {
            Register_email rg = new Register_email();
            Config cf = new Config();
            SendMail semail = new SendMail();
            if (rg.Add_email(email))
            {
                string _sMailBody = string.Empty;
                string _sEmailCC = string.Empty;
                _sMailBody += "Cám ơn bạn đã đặt đăng ký nhận tin từ email với chúng tôi.";
                var _ccMail = cf.Getemail(2);
                if (_ccMail.ToList().Count > 0)
                {
                    _sEmailCC = _ccMail.ToList()[0].EMAIL_TO;
                }
                semail.SendEmailSMTP("Thông báo: Bạn đã đăng ký nhận tin thành công", email, _sEmailCC, "", _sMailBody, true, false);
                return "success";
            }
            else return "errors";
        }
        [WebMethod]
        public static string addLike(string seourl)
        {
            if (!checkck.Listcookie_like().Contains(seourl))
            {
                setck.Addcookie(seourl);
                return "success";
            }
            else
                return "error";
        }
        [WebMethod]
        public static string likepro(string seourl)
        {
            string res = string.Empty;
            var list = index.Loadpro_cookie(1, checkck.Listcookie_like(),1);
            foreach (var i in list)
            {
                res += "<li>";

                res += "<div class='product_item_slide'>";
                res += "    <div class='img_general'>";
                res += "       <a href='"+GetLink(i.NEWS_URL,i.NEWS_SEO_URL)+"'>";
                res += "           <img width='40' height='40' src='"+GetImageT(i.NEWS_ID,i.NEWS_IMAGE3)+"'>";
                res += "       </a>";
                res += "</div>";
                res += " <div class='r_product_info'>";
                res += "    <h4>";
                res += "    <a href='" + GetLink(i.NEWS_URL, i.NEWS_SEO_URL) + "'>";
                res += i.NEWS_TITLE + "</a></h4>";
                res += "<div class='info_price'>";
                res += "<span class='f_price'><del>";
                res += GetPrice2(i.NEWS_PRICE1,i.NEWS_PRICE2)+"</del></span> <span class='main_price'>";
                res += GetPrice1(i.NEWS_PRICE1, i.NEWS_PRICE2) + "</span>";
                res += "   </div>";
                res += " </div>";
                res += "</div>";

                res += "</li>";
            }
            return res;
        }

        //[WebMethod]
        //public static string get
        public static string GetPrice1(object News_Price1, object News_Price2)
        {
            try
            {
                return fun.Getprice1(News_Price1, News_Price2);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public static string GetPrice2(object News_Price1, object News_Price2)
        {
            try
            {
                return fun.Getprice2(News_Price1, News_Price2);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }

        public static string GetLink(object News_Url, object News_Seo_Url)
        {
            try
            {
                return fun.Getlink_News(News_Url, News_Seo_Url);
            }
            catch (Exception ex)
            {
                vpro.functions.clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public static string GetImageT(object News_Id, object News_Image1)
        {

            try
            {
                return fun.GetImageT_News(News_Id, News_Image1);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
    }
}