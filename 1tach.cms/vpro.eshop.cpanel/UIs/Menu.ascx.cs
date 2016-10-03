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
        public IQueryable LoadmenuRank2(object id)
        {
            int menuid = Utils.CIntDef(id);
            var list = db.MENUs.Where(n => n.MENU_PARENT_ID == menuid && n.MENU_ACTIVE == 1);
            return list.ToList().Count > 0 ? list : null;
        }
        public string GetActiveMenu(object catid)
        {
            string url = Request.RawUrl;
            int menuid = 0;
            int catidmenu = Utils.CIntDef(catid);
            var list = db.MENUs.Where(n => url.IndexOf(n.MENU_LINK)!=-1).ToList();
            if (list.Count > 0)
            {
                if (list[0].MENU_RANK > 1)
                    menuid = Utils.CIntDef(list[0].MENU_PARENT_ID);
                else
                    menuid = list[0].ID;
            }
            if (menuid==catidmenu)
                return "in";
            return "";
        }
    }
}