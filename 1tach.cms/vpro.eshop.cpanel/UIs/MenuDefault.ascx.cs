using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace vpro.eshop.cpanel.UIs
{
    public partial class MenuDefault : System.Web.UI.UserControl
    {
        #region declare
        eshopdbDataContext DB = new eshopdbDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Lbcountcomment.Text = countCommentNew().ToString();
            Lbcountcart.Text = countOrder().ToString();
            LbtotalVisitor.Text = countVisitor().ToString();
            Lbcountemail.Text = countEmail().ToString();
        }
        private int countCommentNew()
        {
            int  count = DB.ESHOP_NEWS_COMMENTs.ToList().Count;
            return count;
        }
        private int countOrder()
        {
            int count = DB.ESHOP_ORDERs.Where(n =>n.ORDER_PUBLISHDATE>=DateTime.Today.AddDays(-7)).ToList().Count;
            return count;
        }
        private int countVisitor()
        {
            var list = DB.ESHOP_CONFIGs.Select(n => new { n.CONFIG_HITCOUNTER }).ToList();
            if (list.Count > 0)
            {
                return Utils.CIntDef(list[0].CONFIG_HITCOUNTER);
            }
            return 0;
        }
        private int countEmail()
        {
            int count = DB.ESHOP_MAIL_RECIVEs.ToList().Count;
            return count;
        }
    }
}