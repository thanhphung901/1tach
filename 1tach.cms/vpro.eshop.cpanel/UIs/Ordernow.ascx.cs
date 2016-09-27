using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.UIs
{
    public partial class Ordernow : System.Web.UI.UserControl
    {
        #region declare
        eshopdbDataContext DB = new eshopdbDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                loadInfo();
        }
        private void loadInfo()
        {
            var list = DB.ESHOP_ORDERs.OrderByDescending(n => n.ORDER_ID).Take(10).ToList();
            if (list.Count > 0)
            {
                Rpordernow.DataSource = list;
                Rpordernow.DataBind();
            }
        }
        #region My Functions
        public string getPublishDate(object obj_date)
        {
            return string.Format("{0:dd-MM-yyyy}", obj_date);
        }

        public string getLink(object obj_id)
        {
            return "order.aspx?order_id=" + Utils.CStrDef(obj_id);
        }
      
        public string GetMoney(object obj_value)
        {
            return string.Format("{0:#,#} đ", obj_value).Replace(',', '.');
        }

        #endregion
    }
}