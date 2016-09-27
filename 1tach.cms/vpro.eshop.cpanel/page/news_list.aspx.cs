using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Web.UI.HtmlControls;
using System.IO;
using vpro.eshop.cpanel.Components;
using System.Net.Mail;
using System.Data;
using System.Data.Linq.SqlClient;

namespace vpro.eshop.cpanel.page
{
    public partial class news_list : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        int _type = 0;
        int _gtype, _gid;
        int _page = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        PageIndexChange pagechange = new PageIndexChange();
        #endregion
        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            _gid = Utils.CIntDef(Session["GROUP_ID"]);
            _gtype = Utils.CIntDef(Session["GROUP_TYPE"]);
            _type = Utils.CIntDef(Request["type"]);
            _page = Utils.CIntDef(Request["page"]);
            Hyperaddnew.NavigateUrl = "news.aspx?type=" + _type;
            if (!IsPostBack)
            {
                if (_gtype == 1)
                    Loadchuyenmuc();
                SearchResult();
            }
            

        }

        #endregion
        #region loaddata
        public void Loadchuyenmuc()
        {
            try
            {
                var CatList = (
                                from t2 in DB.ESHOP_CATEGORies
                                where t2.CAT_RANK > 0
                                
                                select new
                                {
                                    CAT_ID = t2.CAT_NAME == "------- Root -------" ? 0 : t2.CAT_ID,
                                    CAT_NAME = (string.IsNullOrEmpty(t2.CAT_CODE) ? t2.CAT_NAME : t2.CAT_NAME + "(" + t2.CAT_CODE + ")"),
                                    CAT_NAME_EN = (string.IsNullOrEmpty(t2.CAT_CODE_EN) ? t2.CAT_NAME_EN : t2.CAT_NAME_EN + "(" + t2.CAT_CODE_EN + ")"),
                                    CAT_PARENT_ID = t2.CAT_PARENT_ID,
                                    CAT_RANK = t2.CAT_RANK
                                }
                            );

                if (CatList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    DataTable tbl = DataUtil.LINQToDataTable(CatList);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    ddlCategory.DataSource = dsCat.Tables[0];
                    ddlCategory.DataTextField = "CAT_NAME";
                    ddlCategory.DataValueField = "CAT_ID";
                    ddlCategory.DataBind();

                }
                ListItem l = new ListItem("------ Chọn chuyên mục ------", "0", true);
                l.Selected = true;
                ddlCategory.Items.Insert(0, l);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private List<string> getCatid()
        {
            List<string> l = new List<string>();
            var list = DB.ESHOP_GROUP_CATs.Where(n => n.GROUP_ID == _gid).ToList();
            foreach (var i in list)
            {
                l.Add(Utils.CStrDef(i.CAT_ID));
            }
            return l;
        }
        private void SearchResult()
        {
            try
            {
                int _limit = 20;
                int _skip = 0;
                string keyword = CpanelUtils.ClearUnicode(txtKeyword.Value);
                int _catid = Utils.CIntDef(ddlCategory.SelectedValue);
                if (_catid != 0 || !String.IsNullOrEmpty(keyword))
                    _limit = 200;

                if (_page != 0)
                    _skip = _page * _limit - _limit;
                var AllList = (from g in DB.ESHOP_NEWs
                               join a in DB.ESHOP_NEWS_CATs on g.NEWS_ID equals a.NEWS_ID into t2_join
                               from d in t2_join.DefaultIfEmpty()
                               where ("" == keyword || SqlMethods.Like(DB.fClearUnicode(g.NEWS_TITLE), "%" + keyword + "%") || g.NEWS_CODE.Contains(keyword))
                               && (_gtype != 1 ? (getCatid().Contains(d.ESHOP_CATEGORy.CAT_ID.ToString()) || getCatid().Contains(d.ESHOP_CATEGORy.CAT_PARENT_ID.ToString())) : true)
                               && (_type == 1 ? g.NEWS_TYPE == 1 : g.NEWS_TYPE != 1)
                               && ((d.ESHOP_CATEGORy.CAT_ID == _catid || d.ESHOP_CATEGORy.CAT_PARENT_PATH.Contains(_catid.ToString())) || 0 == _catid)
                               orderby g.NEWS_ID descending
                               select g).GroupBy(n => n.NEWS_ID).Select(n => new
                               {
                                   NEWS_ID = n.First().NEWS_ID,
                                   NEWS_TITLE = n.First().NEWS_TITLE,
                                   NEWS_TYPE = n.First().NEWS_TYPE,
                                   NEWS_SHOWTYPE = n.First().NEWS_SHOWTYPE,
                                   NEWS_ORDER = n.First().NEWS_ORDER,
                                   NEWS_PUBLISHDATE = n.First().NEWS_PUBLISHDATE,
                               }).OrderByDescending(n => n.NEWS_ID).ToList();


                RpItemList.DataSource = AllList.Skip(_skip).Take(_limit);
                RpItemList.DataBind();
                LitPage.Text = pagechange.changePageNews("news_list.aspx", AllList.Count, _limit, _page, _type);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion
        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "news.aspx?type=" + _type + "&news_id=" + Utils.CStrDef(obj_id);
        }
        public string getLink_comment(object obj_id)
        {
            return "news_comment.aspx?news_id=" + Utils.CStrDef(obj_id);
        }
        public int Getcount_comment(object NewsID)
        {
            try
            {
                int _iNewsID = Utils.CIntDef(NewsID);
                var _vComment = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(a => a.NEWS_ID == _iNewsID);
                return _vComment.ToList().Count;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return 0;
            }
        }
        public string getTypeNew(object obj_id)
        {
            return (Utils.CIntDef(obj_id) == 0) ? "Tin tức" : ((Utils.CIntDef(obj_id) == 1) ? "Sản Phẩm" : "Khác");
        }
        public string GetNewsStatus(object NewsID, object NewsTitle)
        {
            try
            {
                int _iNewsID = Utils.CIntDef(NewsID);
                var _vComment = DB.GetTable<ESHOP_NEWS_COMMENT>().Where(a => a.NEWS_ID == _iNewsID && a.COMMENT_CHECK == 0);
                if (_vComment.ToList().Count > 0)
                {
                    return Utils.CStrDef(NewsTitle) + " - <font color='#FF0000'>Có phản hồi mới</font>";
                }
                else
                {
                    return Utils.CStrDef(NewsTitle);
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }


        public string getStatus(object obj_status)
        {
            return Utils.CIntDef(obj_status) == 0 ? "<img src='../Images/icon-deactive.png' title='Không hiển thị' />" : "<img src='../Images/icon-active.gif' title='Hiển thị'/>";
        }

        public string getLanguage(object News_Language)
        {
            return Utils.CIntDef(News_Language) == 1 ? "Việt Nam" : "Englist";
        }

        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy HH:mm:ss}", News_PublishDate);
        }

        #endregion
        #region function button

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
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
        private void Send_Mail_Content(string MailContent, string Email)
        {
            try
            {
                string strEmailBody = "";

                strEmailBody += getHeader();
                strEmailBody += MailContent;
                strEmailBody += getFooter();

                SendEmailSMTP("Vui lòng ghé thăm website " + System.Configuration.ConfigurationManager.AppSettings["URLWebsite1"] + "", Email, "", "", strEmailBody, true, false);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private string getHeader()
        {
            string pathFile;
            string strHTMLContent = string.Empty;

            pathFile = Server.MapPath(PathFiles.GetPathConfigs() + "/headersendmail.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

            }
            return strHTMLContent;
        }
        private string getFooter()
        {
            string pathFile;
            string strHTMLContent = string.Empty;

            pathFile = Server.MapPath(PathFiles.GetPathConfigs() + "/footersendmail.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

            }
            return strHTMLContent;
        }
        private string setHtmlSendmail(string title, string desc, string img, string price, string link,int _type)
        {
            string _result = string.Empty;
            _result += "<table class='MsoNormalTable' border='0' cellpadding='0'>";
            _result += "  <tr>";
            _result += "<td rowspan='2' style='padding: .75pt .75pt .75pt .75pt' colspan='2'";
            _result += " <p class='MsoNormal' style='margin-top: 10pt'>";
            _result += "   <b><span style='font-family: Arial,sans-serif;'><a target='_blank' href='" + link + "'>";
            _result += " <span style='color: #0792BD; text-decoration: none;'>";
            _result += " <img border='0' width='210' height='140' src='" + img + "'";
            _result += "   alt='" + title + "' title='" + title + "' /></span></a><o:p>";
            _result += "</o:p></span></b></p>";
            _result += "</td>";
            _result += "<td valign='top' style='padding: .75pt .75pt .75pt 3.75pt'";
            _result += " <p class='MsoNormal' style='margin-top: 10pt; text-align: left;'>";
            _result += " <b><span style='font-size: 13.0pt; font-family: Arial,sans-serif;'>";
            _result += "<a target='_blank' href='" + link + "'>";
            _result += "<span style='color: #DD3489;";
            _result += " text-decoration: none;'>" + title + "</span></a><o:p></o:p></span></b></p>";
            _result += "<p class='MsoNormal' style='margin-top: 10pt; font-size: x-small; text-align: left;'>";
            _result += " <b><span style='font-family: Arial,sans-serif; color: #666666'>";
            _result += desc + "</span></b></p>";
            _result += " </td>";
            _result += "</tr>";
            _result += "<tr>";
            _result += " <td valign='bottom' style='padding: .75pt .75pt .75pt 3.75pt'>";
            _result += " <table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%'";
            _result += "style='width: 100.0%; background: #FF6600;'>";
            _result += "<tr>";
            _result += " <td valign='bottom' style='padding: .75pt .75pt .75pt 3.75pt'>";
            _result += " <table class='MsoNormalTable' border='0' cellspacing='0' cellpadding='0' width='100%'";
            _result += "style='width: 100.0%; background: #FF6600;'>";
            _result += "<tr>";
            if (_type == 1)
            {
                _result += "<td style='background: white; padding: 0in 0in 0in 0in'>";
                _result += "<p class='MsoNormal' align='center' style='text-align: center'>";
                _result += "<span style='color: #666666'>Giá<o:p></o:p></span></p>";
                _result += "</td>";
                _result += "<td style='background: white; padding: 0in 0in 0in 0in'>";
                _result += "  <p class='MsoNormal' align='center' style='text-align: center'>";
                _result += "  </p>";
                _result += " </td>";
                _result += " <td style='background: white; padding: 0in 0in 0in 0in'>";
                _result += "<p class='MsoNormal' align='center' style='text-align: center'>";
                _result += " <span style='color: #666666'><o:p></o:p></span></p>";
                _result += "</td>";
                _result += "<td style='background: white; padding: 0in 0in 0in 0in'>";
                _result += "<p class='MsoNormal' align='center' style='text-align: center'>";
                _result += " <span style='color: white'>&nbsp;<o:p></o:p></span></p>";
                _result += " </td>";

                _result += "</tr>";
                _result += "<tr>";
                _result += "<td style='background: #E77500; padding: 0in 0in 0in 0in'>";
                _result += "  <p class='MsoNormal' align='center' style='text-align: center'>";
                _result += "   <b><span style='color: white'>" + price + "<o:p></o:p></span></b></p>";
                _result += " </td>";
                _result += " <td style='background: #E77500; padding: 0in 0in 0in 0in'>";
                _result += "   <p class='MsoNormal' align='center' style='text-align: center'>";
                _result += "         <b><span style='color: white'><o:p></o:p></span></b></p>";
                _result += "</td>";
                _result += " <td style='background: #E77500; padding: 0in 0in 0in 0in'>";
                _result += "  <p class='MsoNormal' align='center' style='text-align: center'>";
                _result += "  <b><span style='color: white'>";
                _result += "      <o:p></o:p>";
                _result += " </span></b>";
                _result += "</p>";
                _result += "</td>";
                _result += "<td style='background: #468107; padding: 3.75pt 3.75pt 3.75pt 3.75pt'>";
                _result += " <p class='MsoNormal' align='center' style='text-align: center'>";
                _result += " <span style='color: white'><a target='_blank' href='" + link + "'>";
                _result += "<b><span style='font-size: 12.0pt; font-family: Arial,sans-serif; color: white; text-decoration: none;'>";
                _result += " MUA NGAY</span></b></a><o:p></o:p></span></p>";
                _result += " </td>";
            }
            _result += " </tr>";
            _result += "  </table>";
            _result += " </td>";
            _result += "</tr>";

            _result += "  </table>";
            _result += " </td>";
            _result += " </tr>";
            _result += "</table>";
            return _result;
        }
        public string Getlink_News(object News_url, object Seo_url, object type)
        {
            return string.IsNullOrEmpty(Utils.CStrDef(News_url)) ? System.Configuration.ConfigurationManager.AppSettings["URLWebsite1"] + "/" + Utils.CStrDef(Seo_url) + ".html" : Utils.CStrDef(News_url);
        }
        public string Getprice(object Price1)
        {
            decimal _dPrice1 = Utils.CDecDef(Price1);
            if (_dPrice1 != 0)
            {
                return String.Format("{0:0,0 VNĐ}", _dPrice1);
            }
            return "Liên hệ";
        }
        public string GetImageT_News(object News_Id, object News_Image1)
        {

            try
            {
                if (Utils.CIntDef(News_Id) > 0 && !string.IsNullOrEmpty(Utils.CStrDef(News_Image1)))
                {
                    return System.Configuration.ConfigurationManager.AppSettings["URLWebsite1"] + "/" + PathFiles.GetPathNews(Utils.CIntDef(News_Id)) + Utils.CStrDef(News_Image1);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        private string subDesc(string desc)
        {
            if (desc.Length > 100)
                return desc.Substring(0, 99) + "...";
            return desc;
        }
        #endregion
        #region Repeater
        protected void RpItemList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int _sID = Utils.CIntDef(e.CommandArgument.ToString());
                var g_delete = DB.GetTable<ESHOP_NEW>().Where(g => g.NEWS_ID == _sID);

                DB.ESHOP_NEWs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathNews(_sID));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }
            }
            SearchResult();
        }
        protected void RpItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
        }
        #endregion
        #region Button event
        protected void Lbdelete_Click(object sender, EventArgs e)
        {
            int[] items = new int[RpItemList.Items.Count];
            try
            {
                int j=0;
                for (int i = 0; i < RpItemList.Items.Count; i++)
                {
                    
                    HtmlInputCheckBox check  = RpItemList.Items[i].FindControl("chkSelect") as HtmlInputCheckBox;
                    HtmlInputHidden s = RpItemList.Items[i].FindControl("newsid") as HtmlInputHidden;
                    int _newid = Utils.CIntDef(s.Value);
                    if (check.Checked)
                    {
                        items[j] = _newid;
                        try
                        {
                            //delete folder
                            string fullpath = Server.MapPath(PathFiles.GetPathNews(items[j]));
                            if (Directory.Exists(fullpath))
                            {
                                DeleteAllFilesInFolder(fullpath);
                                Directory.Delete(fullpath);
                            }
                        }
                        catch (Exception)
                        {
                            
                        }
                        j++;
                    }
                }
                //delete 
                var g_delete = DB.GetTable<ESHOP_NEW>().Where(g => items.Contains(g.NEWS_ID));

                DB.ESHOP_NEWs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges(); 
            }
            catch (Exception ex)
            {
                
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                items = null;
                SearchResult();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }
        protected void Lbsend_email_Click(object sender, EventArgs e)
        {
            int[] items = new int[RpItemList.Items.Count];
            string _sMailContent = string.Empty;
            try
            {
                int j = 0;
                for (int i = 0; i < RpItemList.Items.Count; i++)
                {

                    HtmlInputCheckBox check = RpItemList.Items[i].FindControl("chkSelect") as HtmlInputCheckBox;
                    HtmlInputHidden s = RpItemList.Items[i].FindControl("newsid") as HtmlInputHidden;
                    int _newid = Utils.CIntDef(s.Value);
                    if (check.Checked)
                    {
                        items[j] = _newid;
                        
                        j++;
                    }
                }
                var setMailcontent = DB.ESHOP_NEWs.Where(n => items.Contains(n.NEWS_ID));
                foreach (var s in setMailcontent)
                {
                    _sMailContent += setHtmlSendmail(s.NEWS_TITLE, subDesc(s.NEWS_DESC), GetImageT_News(s.NEWS_ID, s.NEWS_IMAGE3), Getprice(s.NEWS_PRICE1), Getlink_News(s.NEWS_URL, s.NEWS_SEO_URL, s.NEWS_TYPE),Utils.CIntDef(s.NEWS_TYPE));
                }
                //Gửi mail
                var _vEmailReceive = DB.GetTable<ESHOP_MAIL_RECIVE>().Where(a => a.MAIL_ACTIVE == 1);
                foreach (var item in _vEmailReceive)
                {
                    Send_Mail_Content(_sMailContent, item.MAIL_NAME);
                }
                Response.Write("<script LANGUAGE='JavaScript' >alert('Thông báo: Tin đã được gửi thành công!');document.location='" + ResolveClientUrl("/cpanel/page/news_list.aspx?type=" + _type) + "';</script>");
            }
            catch (Exception ex)
            {

                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                items = null;
                SearchResult();
            }
        }
        #endregion

        protected void Lbaddnew_Click(object sender, EventArgs e)
        {

        }
        
    }
}