using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using Controller;
using OneTach.Components;
using vpro.functions;

namespace OneTach
{
    public partial class contact : System.Web.UI.Page
    {
        #region Declare
        Config cf = new Config();
        SendMail sm = new SendMail();
        List_news lnews = new List_news();
        Function fun = new Function();
        clsFormat fm = new clsFormat();
        Pageindex_chage change = new Pageindex_chage();
        int _Catid = 0;
        string _cat_seo_url = string.Empty;
        int _page = 0;
        int _typecat = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _Catid = Utils.CIntDef(Session["Cat_id"]);
            if (!IsPostBack)
            {
                Show_File_HTML("contact-vi.htm");
                Show_File_Map();
            }
            var _configs = cf.Config_meta();

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
            }
            else
            {
                
            }
            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();
            headerDes.Name = "Description";
            headerKey.Name = "Keywords";

            header.Title = "Liên hệ";
        }
      
        public string GetImageCat(object Cat_ID, object cat_img)
        {
            try
            {
                return fun.Getimages_Cat(Cat_ID, cat_img);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        private void Show_File_Map()
        {
            liLoadMap.Text = cf.Show_File_HTML("map-vi.htm", "/Data/contact/");
        }
        private void Show_File_HTML(string HtmlFile)
        {
            string pathFile;
            string strHTMLContent;
            pathFile = Server.MapPath("../Data/contact/" + HtmlFile);

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                Literal1.Text = strHTMLContent;
            }
            else
                Literal1.Text = "";

        }
        public string querys()
        {
            return LookCookie();
        }

        private string LookCookie()
        {
            HttpCookie Cookie = new HttpCookie("slmsrcd1");
            Cookie = Request.Cookies["slmsrcd1"];
            string strUser = "";
            if (Cookie != null && Cookie.Value != "" &&
                 Cookie.Value != null)
            {
                strUser = Cookie.Value.ToString();
            }
            return strUser;
        }
        protected void Gui_Click(object sender, EventArgs e)
        {
            try
            {

                string strSecView = LookCookie().ToLower();
                string strSecurity = txtCapcha.Value.ToString().ToLower();
                if (strSecurity != strSecView)
                {
                    Response.Write("<script>alert('Nhập mã bảo mật sai!');</script>");
                    return;
                }
                else
                {

                    string _sEmailCC = string.Empty;
                    string _sEmail = txtEmail.Value;
                    string _sName = Txtname.Value;
                    string _phone = Txtphone.Value;
                    //string _tittle = txttitle.Value;
                    string _content = txtContent.Value;
                    string _address = "";
                    cf.Insert_contact(_sName, _sEmail, "", _content, _phone, _address);
                    string _mailBody = string.Empty;
                    _mailBody += "<br/><br/><strong>Tên khách hàng</strong>: " + _sName;
                    _mailBody += "<br/><br/><strong>Email</strong>: " + _sEmail;
                    _mailBody += "<br/><br/><strong>Số điện thoại</strong>: " + _phone;
                    //_mailBody += "<br/><br/><strong>Tiêu Đề</strong>: " + _tittle;
                    _mailBody += "<br/><br/><strong>Nội dung</strong>: " + _content + "<br/><br/>";
                    string _sMailBody = string.Empty;
                    _sMailBody += "Cám ơn quý khách: " + _sName + " đã đặt liên hệ với chúng tôi. Đây là email được gửi từ website của " + System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"] + " <br>" + _mailBody;
                    _sEmailCC = cf.Getemail(2).Count > 0 ? cf.Getemail(2)[0].EMAIL_TO : "";
                    sm.SendEmailSMTP("Thông báo: Bạn đã liên hệ thành công", _sEmail, _sEmailCC, "", _sMailBody, true, false);
                    string strScript = "<script>";
                    strScript += "alert(' Đã gửi thành công!');";
                    strScript += "window.location='/trang-chu.html';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public string GetImageT(object News_Id, object News_Image1, object tittle)
        {
            try
            {
                return fun.GetImageT_News_Hasclass(News_Id, News_Image1, tittle, "");
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
 
    }
}