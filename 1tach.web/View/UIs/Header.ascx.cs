using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace sanzo.UIs
{
    public partial class Header : System.Web.UI.UserControl
    {
        #region Declare
        Propertity per = new Propertity();
        Function fun = new Function();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                load_logo();
                Load_Menu1();
            }
        }

        #region Load logo
        protected void load_logo()
        {
            var _logoSlogan = per.Load_logo_or_sologan("1", 1);
            if (_logoSlogan.ToList().Count > 0)
            {
                Rplogo1.DataSource = _logoSlogan;
                Rplogo1.DataBind();
            }
        }
        public string Getbanner(object Banner_type, object Banner_ID, object Banner_Image, object BANNER_DESC)
        {
            return fun.Getbanner(Banner_type, Banner_ID, Banner_Image, Utils.CStrDef(BANNER_DESC));
        }
        #endregion

        #region LoadData
        protected void Load_Menu1()
        {
            try
            {
                var list = per.LoadTopmenu(1, 20, 1);
                if (list.Count > 0)
                {
                    Rpmenu.DataSource = list;
                    Rpmenu.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }

        }
        protected IQueryable Load_Menu2(object cat_parent_id)
        {
            try
            {
                var list = per.Menu2(cat_parent_id, 1);
                return list;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }

        }
        #endregion
        #region funtion
        public string GetLink(object Cat_Url, object Cat_Seo_Url)
        {
            try
            {
                string temp = fun.Getlink_Cat(Cat_Url, Cat_Seo_Url);
                return temp;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetStyleActive(object Cat_Seo_Url, object Cat_Url)
        {
            try
            {
                string _curl = Utils.CStrDef(Request.QueryString["curl"]);
                string _seoUrl = Utils.CStrDef(Request.QueryString["curl"]);
                return per.GetStyleActive(Cat_Seo_Url, Cat_Url, 1);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        #endregion
    }
}