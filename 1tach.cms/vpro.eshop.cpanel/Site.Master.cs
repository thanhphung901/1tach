using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        eshopdbDataContext db = new eshopdbDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USER_ID"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_ID"]);
            Session["USER_UN"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_UN"]);
            Session["USER_NAME"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_USER_NAME"]);
            Session["GROUP_ID"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_ID"]);
            Session["GROUP_TYPE"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_TYPE"]);

           
            if (Utils.CIntDef(Session["GROUP_TYPE"]) == 0)
                Response.Redirect("login.aspx");

            var _configs = db.GetTable<ESHOP_CONFIG>().OrderBy(c => c.CONFIG_ID).Take(1);

            if (_configs.ToList().Count > 0)
            {
                if (!string.IsNullOrEmpty(_configs.ToList()[0].CONFIG_FAVICON))
                    ltrFavicon.Text = "<link rel='shortcut icon' href='" + PathFiles.GetPathConfigs() + _configs.ToList()[0].CONFIG_FAVICON + "' />";
            }
            UserControl ucmenuMain = Page.LoadControl("../UIs/Menu.ascx") as UserControl;
            UserControl ucmenuDefault = Page.LoadControl("../UIs/MenuDefault.ascx") as UserControl;
            if (Request.RawUrl.Contains("default.aspx"))
                Plmenu.Controls.Add(ucmenuDefault);
            else
                Plmenu.Controls.Add(ucmenuMain);
        }
    }
}
