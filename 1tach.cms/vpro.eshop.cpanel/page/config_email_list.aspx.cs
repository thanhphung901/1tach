using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

using System.Data;
using System.Web.UI.HtmlControls;

namespace vpro.eshop.cpanel.page
{
    public partial class config_email_list : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region properties

        public SortDirection sortProperty
        {
            get
            {
                if (ViewState["SortingState"] == null)
                {
                    ViewState["SortingState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortingState"];
            }
            set
            {
                ViewState["SortingState"] = value;
            }
        }

        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
                SearchResult();

                
            }

        }

        #endregion

        #region My Functions

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "config_email.aspx?email_id=" + Utils.CStrDef(obj_id);
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_EMAILs
                               where "" == keyword || (g.EMAIL_DESC).Contains(keyword) || (g.EMAIL_FROM).Contains(keyword) || (g.EMAIL_TO).Contains(keyword) || (g.EMAIL_CC).Contains(keyword) || (g.EMAIL_BCC).Contains(keyword)
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["EmailList"] = DataUtil.LINQToDataTable(AllList);

                Rplistemail.DataSource = AllList;
                Rplistemail.DataBind();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion

        #region Button Envents

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

      

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }
    }
}