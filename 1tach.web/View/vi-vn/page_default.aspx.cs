using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Controller;
using vpro.functions;

namespace OneTach
{
    public partial class page_default : System.Web.UI.Page
    {
        #region Declare
        Get_session getsession = new Get_session();
        checkProperties _checkprt = new checkProperties();
        Config cf = new Config();
        //int _type = 0, lang = 1, _langsearch = 0;
        int _type = 0, lang = 1, _langsearch = 0;
        string _catSeoUrl = string.Empty;
        string _newsSeoUrl = string.Empty;
        #endregion
        #region Page Event
        protected void Page_PreInit(object sender, EventArgs e)
        {
            _type = Utils.CIntDef(Request["type"]);
            _catSeoUrl = Utils.CStrDef(Request.QueryString["curl"]);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            bool err = false;
            try
            {
                var _configs = cf.Config_meta();

                if (_configs.ToList().Count > 0)
                {
                    if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                        ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
                }

                UserControl listNews = Page.LoadControl("../UIs/ListNew.ascx") as UserControl;//Danh sách tin
                UserControl detailsNews = Page.LoadControl("../UIs/DetailNews.ascx") as UserControl;//Chi tiết tin
                UserControl listDebate = Page.LoadControl("../UIs/ListDebate.ascx") as UserControl;//Danh sách tranh luận
                UserControl listVote = Page.LoadControl("../UIs/ListVotes.ascx") as UserControl;//Danh sách vote
                UserControl detailsDebate = Page.LoadControl("../UIs/DetailDebate.ascx") as UserControl;//Danh sách vote
                UserControl listPro = Page.LoadControl("../UIs/ListProfessional.ascx") as UserControl;//Danh sách chuyên gia
                UserControl listBus = Page.LoadControl("../UIs/ListBusiness.ascx") as UserControl;//Danh sách doanh nghiệp

                switch (_catSeoUrl)
                {
                    case "chuyen-gia":
                        phdMain.Controls.Add(listPro);
                        break;
                    case "doanh-nghiep":
                        phdMain.Controls.Add(listBus);
                        break;
                    default:
                        switch (_type)
                        {
                            case 3:
                                if (getsession.check_CatorNews(_catSeoUrl))
                                {
                                    getsession.LoadNewsInfo(_catSeoUrl);
                                    Bind_meta_tags_news();
                                    int iCatType = getsession.Getcat_type(_catSeoUrl);
                                    if (iCatType == 1)
                                    {
                                        //phdMain.Controls.Add(prodetails);
                                    }
                                    else
                                    {
                                        phdMain.Controls.Add(detailsNews);
                                    }
                                }
                                else
                                {
                                    if (!getsession.check_catExits(_catSeoUrl))
                                    {
                                        err = true;
                                    }
                                    else
                                    {
                                        getsession.LoadCatInfo(_catSeoUrl);
                                        Bind_meta_tags_cat();
                                        int iCatType = Utils.CIntDef(Session["Cat_type"]);
                                        if (Utils.CIntDef(Session["Cat_showitem"]) == 1)
                                        {
                                            if (iCatType == 1)
                                            {
                                                //phdMain.Controls.Add(prodetails);
                                            }
                                            else
                                                phdMain.Controls.Add(detailsNews);
                                        }
                                        else if (iCatType == 1)
                                        {
                                            phdMain.Controls.Add(listVote);
                                        }
                                        else if (iCatType == 2)
                                        {
                                            phdMain.Controls.Add(listDebate);
                                        }
                                        else
                                        {
                                            phdMain.Controls.Add(listNews);
                                        }
                                    }
                                }
                                break;
                            default:
                                Response.Redirect("/");
                                break;
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion
        public void Bind_meta_tags_cat()
        {
            #region Bind Meta Tags
            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();

            headerDes.Name = "Description";
            headerKey.Name = "Keywords";

            header.Title = Utils.CStrDef(Session["Cat_seo_title"]);
            headerDes.Content = Utils.CStrDef(Session["Cat_seo_desc"]);
            headerKey.Content = Utils.CStrDef(Session["Cat_seo_keyword"]);


            if (string.IsNullOrEmpty(headerDes.Content))
            {
                headerDes.Content = "";
            }
            header.Controls.Add(headerDes);

            if (string.IsNullOrEmpty(headerKey.Content))
            {
                headerKey.Content = "";
            }

            header.Controls.Add(headerKey);
            #endregion
        }
        public void Bind_meta_tags_news()
        {
            #region Bind Meta Tags
            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();
            //Face tags
            HtmlMeta propety = new HtmlMeta();
            HtmlMeta propetyTitle = new HtmlMeta();
            HtmlMeta propetyDesc = new HtmlMeta();
            //Twitter tags
            HtmlMeta propetyTw = new HtmlMeta();
            HtmlMeta propetyTitleTw = new HtmlMeta();
            HtmlMeta propetyDescTw = new HtmlMeta();

            headerDes.Name = "Description";
            headerKey.Name = "Keywords";
            header.Title = Utils.CStrDef(Session["News_seo_title"]);
            headerDes.Content = Utils.CStrDef(Session["News_seo_desc"]);
            headerKey.Content = Utils.CStrDef(Session["News_seo_keyword"]);
            if (string.IsNullOrEmpty(headerDes.Content))
            {
                headerDes.Content = "";
            }
            header.Controls.Add(headerDes);

            if (string.IsNullOrEmpty(headerKey.Content))
            {
                headerKey.Content = "";
            }

            header.Controls.Add(headerKey);
            //Facebook meta
            propety.Attributes.Add("property", "og:image");
            propety.Attributes.Add("name", "xlarge");
            propety.Content = "" + System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "" + PathFiles.GetPathNews(Utils.CIntDef(Session["News_id"])) + Utils.CStrDef(Session["News_image3"]);
            header.Controls.Add(propety);
            //Title         
            propetyTitle.Attributes.Add("property", "og:title");
            propetyTitle.Content = Utils.CStrDef(Session["News_seo_title"]);
            header.Controls.Add(propetyTitle);
            //Desc
            propetyDesc.Attributes.Add("property", "og:description");
            propetyDesc.Content = Utils.CStrDef(Session["News_seo_desc"]);
            header.Controls.Add(propetyDesc);
            #endregion
        }
        private void Bind_icon()
        {
            var _configs = cf.Config_meta();

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
            }
        }
        public void Bind_meta_tags_index()
        {
            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();
            headerDes.Name = "Description";
            headerKey.Name = "Keywords";

            var _configs = cf.Config_meta();

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";

                header.Title = _configs.ToList()[0].CONFIG_TITLE;

                headerDes.Content = _configs.ToList()[0].CONFIG_DESCRIPTION;
                header.Controls.Add(headerDes);

                headerKey.Content = _configs.ToList()[0].CONFIG_KEYWORD;
                header.Controls.Add(headerKey);
            }
            else
            {
                header.Title = "Enews Standard V1.0";

                headerDes.Content = "Enews Standard V1.0";
                header.Controls.Add(headerDes);

                headerKey.Content = "Enews Standard V1.0";
                header.Controls.Add(headerKey);
            }
        }

    }
}