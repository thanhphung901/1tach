using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.UIs
{
    public partial class NewsNew : System.Web.UI.UserControl
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
            var list = DB.ESHOP_NEWs.OrderByDescending(n => n.NEWS_ID).Take(10).ToList();
            if (list.Count > 0)
            {
                RpnewsNew.DataSource = list;
                RpnewsNew.DataBind();
            }
        }
        #region My Functions
        public string getPublishDate(object obj_date)
        {
            return string.Format("{0:dd-MM-yyyy}", obj_date);
        }

        public string getLink(object obj_id, object type)
        {
            int _type = Utils.CIntDef(type);
            return "news.aspx?news_id=" + Utils.CStrDef(obj_id) + "&type=" + _type;
        }
        public string subTitle(object title)
        {
            string _title = Utils.CStrDef(title);
            if (_title.Length > 30)
                _title = _title.Substring(0, 30) + " ...";
            return _title;
        }

        #endregion
    }
}