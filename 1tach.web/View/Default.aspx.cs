﻿using System;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using Controller;
using vpro.functions;

namespace OneTach
{
    public partial class Default : System.Web.UI.Page
    {
        #region declare
        Config cf = new Config();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();
            headerDes.Name = "Description";
            headerKey.Name = "Keywords";
            var _configs = cf.Config_meta();

            if (_configs != null && _configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
                header.Title = Utils.CStrDef(_configs.ToList()[0].CONFIG_TITLE, "");

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

            if (!IsPostBack)
            {
                CheckCookieLogin();
            }
        }

        private void CheckCookieLogin()
        {
            if (Request.Cookies["LoginOneTach"] != null)
            {
                try
                {
                    string user = Request.Cookies["LoginOneTach"]["UserName"].ToString();

                    HttpContext.Current.Session["User_Name"] = Request.Cookies["LoginOneTach"]["UserName"].ToString();
                    HttpContext.Current.Session["User_Email"] = Request.Cookies["LoginOneTach"]["User_Email"].ToString();
                    HttpContext.Current.Session["User_ID"] = Request.Cookies["LoginOneTach"]["User_ID"].ToString();
                    HttpContext.Current.Session["User_Type"] = Request.Cookies["LoginOneTach"]["User_Type"].ToString();
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
        }
    }
}