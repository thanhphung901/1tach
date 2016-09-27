using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.UIs
{
    public partial class Menu : System.Web.UI.UserControl
    {
        #region Declare
        eshopdbDataContext db = new eshopdbDataContext();
        int _gtype, _gid;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _gid = Utils.CIntDef(Session["GROUP_ID"]);
            _gtype = Utils.CIntDef(Session["GROUP_TYPE"]);
            if (!IsPostBack) Loadmenu();
        }
        private void Loadmenu()
        {
            if (_gtype != 1)
            {
                var list = (from a in db.ESHOP_GROUPs
                            join b in db.GROUP_MENUs on a.GROUP_ID equals b.GROUP_ID
                            join c in db.MENUs on b.MENU_ID equals c.ID
                            where c.MENU_RANK == 1 && c.MENU_ACTIVE == 1
                            select c).OrderByDescending(n => n.MENU_ORDER).ToList();
                if (list.Count > 0)
                {
                    Rpmenu.DataSource = list;
                    Rpmenu.DataBind();
                }
            }
            else
            {
                var list = db.MENUs.Where(n => n.MENU_RANK == 1 && n.MENU_ACTIVE == 1).OrderByDescending(n => n.MENU_ORDER).ToList();
                if (list.Count > 0)
                {
                    Rpmenu.DataSource = list;
                    Rpmenu.DataBind();
                }
            }
        }
        public IQueryable loadmenuRank2(object id)
        {
            int menuid = Utils.CIntDef(id);
            var list = db.MENUs.Where(n => n.MENU_PARENT_ID == menuid && n.MENU_ACTIVE == 1);
            return list.ToList().Count > 0 ? list : null;
        }
        public string getActiveMenu(object catid)
        {
            string url = Request.RawUrl;
            int _menuid = 0;
            int _catidmenu = Utils.CIntDef(catid);
            var list = db.MENUs.Where(n => url.IndexOf(n.MENU_LINK)!=-1).ToList();
            if (list.Count > 0)
            {
                if (list[0].MENU_RANK > 1)
                    _menuid = Utils.CIntDef(list[0].MENU_PARENT_ID);
                else
                    _menuid = list[0].ID;
            }
            if (_menuid==_catidmenu)
                return "in";
            return "";
        }
    }
}