using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.UIs
{
    public partial class Orderchart : System.Web.UI.UserControl
    {
        #region declare
        eshopdbDataContext DB = new eshopdbDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            loadInfo();
        }
        private void loadInfo()
        {
            List<ChartOrderEntity> lchart = new List<ChartOrderEntity>();
            int getOrderWeekNow = DB.ESHOP_ORDERs.Where(n => n.ORDER_PUBLISHDATE >= DateTime.Today.AddDays(-7)).ToList().Count;
            int getOrderWeeklastFirst = DB.ESHOP_ORDERs.Where(n => n.ORDER_PUBLISHDATE >= DateTime.Today.AddDays(-14) && n.ORDER_PUBLISHDATE < DateTime.Today.AddDays(-7)).ToList().Count;
            int getOrderWeeklastSeccond = DB.ESHOP_ORDERs.Where(n => n.ORDER_PUBLISHDATE >= DateTime.Today.AddDays(-21) && n.ORDER_PUBLISHDATE < DateTime.Today.AddDays(-14)).ToList().Count;
            int getOrderWeeklastThree = DB.ESHOP_ORDERs.Where(n => n.ORDER_PUBLISHDATE >= DateTime.Today.AddDays(-28) && n.ORDER_PUBLISHDATE < DateTime.Today.AddDays(-21)).ToList().Count;
            lchart.Add(new ChartOrderEntity { CountOrder = getOrderWeeklastThree, DateOrder = "Trước 3 tuần" });
            lchart.Add(new ChartOrderEntity { CountOrder = getOrderWeeklastSeccond, DateOrder = "Trước 2 tuần" });
            lchart.Add(new ChartOrderEntity { CountOrder = getOrderWeeklastFirst, DateOrder = "Trước 1 tuần" });
            lchart.Add(new ChartOrderEntity { CountOrder = getOrderWeekNow, DateOrder = "Tuần này" });
            Chartorder.Series["Series1"].XValueMember = "DateOrder";
            Chartorder.Series["Series1"].YValueMembers = "CountOrder";
            Chartorder.DataSource = lchart;
            Chartorder.DataBind();
        }
        class ChartOrderEntity
        {
            public int CountOrder { get; set; }
            public string DateOrder { get; set; }
            public decimal TotalPrice { get; set; }
        }
        private string formatDate(object date)
        {
            return string.Format("{0:dd/MM/yyy}", date);
        }
    }
}