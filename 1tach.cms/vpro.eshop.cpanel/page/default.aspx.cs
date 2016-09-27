using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.page
{
    public partial class _default : System.Web.UI.Page
    {
        int _gtype = 1;
        eshopdbDataContext db = new eshopdbDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["GROUP_TYPE"] = Utils.CStrDef(HttpContext.Current.Request.Cookies["PITM_NGUOIDUNG_INFO"]["PITM_GROUP_TYPE"]);
            _gtype = Utils.CIntDef(Session["GROUP_TYPE"]);
            if (_gtype == 2)
            {
                Response.Redirect(getLinkIndex0());
            }
        }
        private string getLinkIndex0()
        {
            int _gid = Utils.CIntDef(Session["GROUP_ID"]);
            var list = (from a in db.ESHOP_GROUPs
                        join b in db.GROUP_MENUs on a.GROUP_ID equals b.GROUP_ID
                        join c in db.MENUs on b.MENU_ID equals c.ID
                        where a.GROUP_ID == _gid && c.MENU_RANK == 1
                        select c).ToList();
            if (list.Count > 0)
            {
                return list[0].MENU_LINK;
            }
            return "";
        }
    }
}