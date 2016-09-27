using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.IO;
using System.Net.Mail;

namespace vpro.eshop.cpanel.UIs
{
    public partial class Popup_Sendemail : System.Web.UI.UserControl
    {
        #region Declare
        string m_pathFile = "";
        eshopdbDataContext DB = new eshopdbDataContext();
        int _type = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _type = Utils.CIntDef(Request["type"]);
            CreateDirectory();
        }
        #region My Function

        private void CreateDirectory()
        {
            m_pathFile = PathFiles.GetPathConfigs();

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }

            Session["FileManager"] = m_pathFile;
        }
        public void SendEmailSMTP(string strSubject, string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
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

                    string str = body;
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
        private void Send()
        {
            string _content = mrk.Value;
            string _title = txttitle.Value;
            var list = DB.ESHOP_MAIL_RECIVEs.Where(n => n.MAIL_ACTIVE == 1);
            foreach (var i in list)
            {
                SendEmailSMTP(_title, i.MAIL_NAME, "", "", _content, true, false);
            }
        }
        protected void lbtSave_Click(object sender, EventArgs e)
        {
            Send();
            Response.Write("<script LANGUAGE='JavaScript' >alert('Thông báo: Gửi thành công!');document.location='" + ResolveClientUrl("../Page/news_list.aspx?type=" + _type) + "';</script>");
        }
        //#region Album images
        //protected void Btupmulti_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (FileUpload1.HasFile)
        //        {

        //            HttpFileCollection hfc = Request.Files;
        //            List<string> lpath = new List<string>();
        //            for (int i = 0; i < hfc.Count; i++)
        //            {
        //                HttpPostedFile hpf = hfc[i];
        //                if (hpf.ContentLength > 0)
        //                {

        //                    string pathfile = Server.MapPath("/data/configs/");
        //                    string fullpathfile = pathfile + "/" + Path.GetFileName(hpf.FileName);
        //                    lpath.Add(PathFiles.GetPathConfigs() + Path.GetFileName(hpf.FileName));
        //                    if (!Directory.Exists(pathfile))
        //                    {
        //                        Directory.CreateDirectory(pathfile);
        //                    }
        //                    hpf.SaveAs(fullpathfile);

        //                }
        //            }
        //            string listimg = string.Empty;
        //            for (int s = 0; s < lpath.Count; s++)
        //            {
        //                listimg += "<p><img src='" + lpath[s] + "' alt=''/></p>";
        //            }
        //            mrk.Value += listimg;
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //    }
        //}
        //#endregion
    }
}