using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controller;
using OneTach.Components;
using vpro.functions;

namespace OneTach.UIs
{
    public partial class ListVotes : System.Web.UI.UserControl
    {
        #region Declare
        List_news lnews = new List_news();
        Function fun = new Function();
        Pageindex_chage change = new Pageindex_chage();
        int _Catid = 0;
        string _cat_seo_url = string.Empty;
        int _page = 0;
        int _typecat = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _Catid = Utils.CIntDef(Session["Cat_id"]);
            _cat_seo_url = Request.QueryString["curl"];
            _page = Utils.CIntDef(Request.QueryString["page"]);
            _typecat = Utils.CIntDef(Request.QueryString["typecat"]);
            if (!IsPostBack)
            {
                Loadlist();
                ltl_CatName.Text = Utils.CStrDef(Session["Cat_title"]);
            }
        }
        public void Loadlist()
        {
            try
            {
                int sotin = Utils.CIntDef(Session["Cat_Pageitem"]);
                if (sotin == 0)
                {
                    sotin = 20;
                }
                var list = lnews.Load_listnews_byTypeInNews(1);
                if (list.Count > 0)
                {
                    if (_page != 0)
                    {
                        Re_New.DataSource = list.Skip(sotin * _page - sotin).Take(sotin);
                        Re_New.DataBind();
                    }
                    else
                    {
                        Re_New.DataSource = list.Take(sotin);
                        Re_New.DataBind();
                    }
                    ltrPage.Text = ltrPage.Text = change.result(list.Count, sotin, _cat_seo_url, _page, 1);
                    if (string.IsNullOrEmpty(ltrPage.Text))
                    {
                        ipage.Visible = false;
                    }
                    else
                    {
                        ipage.Visible = true;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        #region function
        public string GetLink(object News_Url, object News_Seo_Url)
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
        public string GetImageT(object News_Id, object News_Image1)
        {
            try
            {
                return fun.GetImageT_News(News_Id, News_Image1);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}