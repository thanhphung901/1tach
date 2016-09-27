using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace vpro.eshop.cpanel
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Response.Cookies["PITM_NGUOIDUNG_INFO"] != null)
                Response.Redirect("page/default.aspx");
            else
                Response.Redirect("page/login.aspx");
        }
    }
}