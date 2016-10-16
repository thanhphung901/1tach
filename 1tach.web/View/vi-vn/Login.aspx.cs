using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Controller;

namespace OneTach.vi_vn
{
    public partial class Login : System.Web.UI.Page
    {
        Config cf = new Config();
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHead header = base.Header;
            HtmlMeta headerDes = new HtmlMeta();
            HtmlMeta headerKey = new HtmlMeta();
            headerDes.Name = "Description";
            headerKey.Name = "Keywords";
            var _configs = cf.Config_meta();

            if (_configs.ToList().Count > 0)
            {
                //if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                //    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";                //if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                //    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
                header.Title = _configs.ToList()[0].CONFIG_TITLE;

                headerDes.Content = _configs.ToList()[0].CONFIG_DESCRIPTION;
                header.Controls.Add(headerDes);

                headerKey.Content = _configs.ToList()[0].CONFIG_KEYWORD;
                header.Controls.Add(headerKey);
            }
            else
            {
                header.Title = "1 tach | Đăng nhập";

                headerDes.Content = "1 tach | Đăng nhập";
                header.Controls.Add(headerDes);

                headerKey.Content = "1 tach | Đăng nhập";
                header.Controls.Add(headerKey);
            }
        }
    }
}