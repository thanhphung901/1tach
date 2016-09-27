using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

using System.Data;
using System.Web.UI.HtmlControls;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class customer_list : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        PageIndexChange pagechange = new PageIndexChange();
        int _page = 0;
        int _countbirth = 0;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            _page = Utils.CIntDef(Request["page"]);
            if (!IsPostBack)
            {

                SearchResult();
               
            }

        }

        #endregion

        #region My Functions
        public string getNameArea(object areaid)
        {
            int id = Utils.CIntDef(areaid);
            var list = DB.ESHOP_PROPERTies.Where(n => n.PROP_ID == id).ToList();
            if (list.Count > 0)
                return list[0].PROP_NAME;
            return "Chưa nhập thông tin";
        }
        public string Check_birth(object birth)
        {
            if (String.IsNullOrEmpty(Utils.CStrDef(birth)))
                return "";
            string[] a = Utils.CStrDef(birth).Split('/');
            int _mouthbir = Utils.CIntDef(a[0]);
            int _daybir = Utils.CIntDef(a[1]);
            int[] timenow = new int[] { DateTime.Now.Day, DateTime.Now.Month };
            int _daysub = _daybir - timenow[0];
            int _daysubmin = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - timenow[0] + _daybir;
            int _mouthsub = _mouthbir - timenow[1];
            if (_mouthbir == timenow[1] && _daysub <= 7 && _daysub >= 0)
            {
                _countbirth++;
                return "<img src='../Images/birth.gif' /><br/>";
            }
            else if (_mouthsub == 1 && _daysubmin <= 7 && _daysubmin > 0)
            {
                _countbirth++;
                return "<img src='../Images/birth.gif' /> <br/>";
            }
            return "";
        }
        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "customer.aspx?customer_id=" + Utils.CStrDef(obj_id);
        }
        public string getsex(object sexid)
        {
            int id = Utils.CIntDef(sexid);
            switch (id)
            {
                case 0: return "Nam";
                case 1: return "Nữ";
                default: return "Khác";
            }
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;
                int _limit = 20;
                int _skip = 0;
                if (_page != 0)
                    _skip = _page * _limit - _limit;
                var AllList = (from g in DB.ESHOP_CUSTOMERs
                               where ("" == keyword || (g.CUSTOMER_FULLNAME.ToString()).Contains(keyword))
                               select g).OrderByDescending(n=>n.CUSTOMER_ID);


                if (AllList.ToList().Count > 0)
                    Session["CustomerList"] = DataUtil.LINQToDataTable(AllList);

                RplistCustomer.DataSource = AllList.Skip(_skip).Take(_limit);
                RplistCustomer.DataBind();
                LitPage.Text = pagechange.changePage("customer_list.aspx", AllList.ToList().Count, _limit, _page);


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void EventDelete(RepeaterCommandEventArgs e)
        {
            try
            {
                int CustomerId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_CUSTOMER>().Where(g => g.CUSTOMER_ID == CustomerId);

                DB.ESHOP_CUSTOMERs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("customer_list.aspx");
            }
        }

        #endregion

        #region Button Envents

        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            int j = 0;
            int[] items = new int[RplistCustomer.Items.Count];

            try
            {

                for (int i = 0; i < RplistCustomer.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistCustomer.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistCustomer.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_CUSTOMER>().Where(g => items.Contains(g.CUSTOMER_ID));

                DB.ESHOP_CUSTOMERs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                items = null;
                SearchResult();
            }

        }
        #endregion

        protected void RplistCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }
    }
}