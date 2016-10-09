using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Controller;
using OneTach.Components;
using vpro.functions;

namespace OneTach.vi_vn
{
    public partial class PostNews : System.Web.UI.Page
    {
        #region declare
        Config cf = new Config();
        int device = 0;
        setCookieDevice setckdv = new setCookieDevice();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User_Name"] == null)
            {
                Response.Redirect("/");
            }

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
                header.Title = "1 tach | Đăng bài";

                headerDes.Content = "1 tach | Đăng bài";
                header.Controls.Add(headerDes);

                headerKey.Content = "1 tach | Đăng bài";
                header.Controls.Add(headerKey);
            }
        }

        class NewsContent
        {
            public int Id { get; set; }
            public int Type { get; set; }
            public string Title { get; set; }
            public string Paragraph { get; set; }
            public string Image { get; set; }
            public string Box { get; set; }

            public NewsContent(int _type)
            {
                this.Id = 0;
                this.Type = _type;
                this.Title = "";
                this.Box = "";
                this.Image = "";
                this.Paragraph = "";
            }
        }
        class VoteContent
        {
            public int ID { get; set; }
            public int NewsID { get; set; }
            public string Content { get; set; }
            public string Image { get; set; }
            public int VotedCount { get; set; }
            public int Order { get; set; }
        }

        protected void radType_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnDelete3_OnClick(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnDelete2_OnClick(object sender, ImageClickEventArgs e)
        {

        }
    }
}