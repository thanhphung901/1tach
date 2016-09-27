using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class order_list : System.Web.UI.Page
    {
        #region Declare

        int _count = 0;
        int m_cus_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        PageIndexChange pagechange = new PageIndexChange();
        int _page = 0;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {
            m_cus_id = Utils.CIntDef(Request["cus_id"]);
            _page = Utils.CIntDef(Request["page"]);
            if (!IsPostBack)
            {
              
                LoadCategoryParent();

                ucFromDate.returnDate = DateTime.Now.AddMonths(-2);
                ucToDate.returnDate = DateTime.Now;

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

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;
                //int Cat_Id=Utils.CIntDef(ddlCategory.SelectedValue);
                DateTime fromDate = ucFromDate.returnDate;
                DateTime toDate = new DateTime(ucToDate.returnDate.Year, ucToDate.returnDate.Month, ucToDate.returnDate.Day, 23, 59, 59);
                int _limit = 20;
                int _skip = 0;
                if (_page != 0)
                    _skip = _page * _limit - _limit;
                int _status = Utils.CIntDef(ddlStatus.SelectedValue);
                var AllList = (from o in DB.ESHOP_ORDERs
                                   join o_i in DB.ESHOP_ORDER_ITEMs on o.ORDER_ID equals o_i.ORDER_ID
                                   where ("" == keyword || (o.ORDER_CODE).Contains(keyword) || (o.ORDER_FIELD1).Contains(keyword))
                                   && o.ORDER_PUBLISHDATE <= toDate && o.ORDER_PUBLISHDATE >= fromDate
                                   && (_status != 99 ? o.ORDER_STATUS == _status : true)
                                   orderby o.ORDER_PUBLISHDATE descending
                                   select o).Distinct().OrderByDescending(n => n.ORDER_ID);

                    if (AllList.ToList().Count > 0)
                        Session["OrderList"] = DataUtil.LINQToDataTable(AllList);

                    RplistOrder.DataSource = AllList.Skip(_skip).Take(_limit); ;
                    RplistOrder.DataBind();
                    LitPage.Text = pagechange.changePage("order_list.aspx", AllList.ToList().Count, _limit, _page);
                
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
                int ObjtId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_ORDER>().Where(g => g.ORDER_ID == ObjtId);

                DB.ESHOP_ORDERs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("order_list.aspx");
            }
        }

        public string getPublishDate(object obj_date)
        {
            return string.Format("{0:dd-MM-yyyy}", obj_date);
        }

        public string getLink(object obj_id)
        {
            return "order.aspx?order_id=" + Utils.CStrDef(obj_id);
        }
        public string getAddress(object add, int index)
        {
            string _address = Utils.CStrDef(add);
            string[] a = _address.Split('-');
            if (a.Length >= 3)
                return a[index];
            return "";
        }
        private void LoadCategoryParent()
        {
            //try
            //{
            //    var CatList = (
            //                    from t2 in DB.ESHOP_CATEGORies
            //                    select new
            //                    {
            //                        CAT_ID = t2.CAT_NAME == "------- Root -------" ? 0 : t2.CAT_ID,
            //                        CAT_NAME = (string.IsNullOrEmpty(t2.CAT_CODE) ? t2.CAT_NAME : t2.CAT_NAME + "(" + t2.CAT_CODE + ")"),
            //                        CAT_PARENT_ID = t2.CAT_PARENT_ID,
            //                        CAT_RANK = t2.CAT_RANK
            //                    }
            //                );

            //    if (CatList.ToList().Count > 0)
            //    {
            //        DataRelation relCat;
            //        DataTable tbl = DataUtil.LINQToDataTable(CatList);
            //        DataSet ds = new DataSet();
            //        ds.Tables.Add(tbl);

            //        tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
            //        relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

            //        ds.Relations.Add(relCat);
            //        DataSet dsCat = ds.Clone();
            //        DataTable CatTable = ds.Tables[0];

            //        DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

            //        ddlCategory.DataSource = dsCat.Tables[0];
            //        ddlCategory.DataTextField = "CAT_NAME";
            //        ddlCategory.DataValueField = "CAT_ID";
            //        ddlCategory.DataBind();
            //    }
            //    else
            //    {
            //        DataTable dt = new DataTable("Newtable");

            //        dt.Columns.Add(new DataColumn("CAT_ID"));
            //        dt.Columns.Add(new DataColumn("CAT_NAME"));

            //        DataRow row = dt.NewRow();
            //        row["CAT_ID"] = 0;
            //        row["CAT_NAME"] = "--------All--------";
            //        dt.Rows.Add(row);

            //        ddlCategory.DataTextField = "CAT_NAME";
            //        ddlCategory.DataValueField = "CAT_ID";
            //        ddlCategory.DataSource = dt;
            //        ddlCategory.DataBind();

            //    }

            //}
            //catch (Exception ex)
            //{
            //    clsVproErrorHandler.HandlerError(ex);
            //}
        }

        public string getOrderStatus(object obj_status)
        {
            switch (Utils.CIntDef(obj_status))
            {
                case 0:
                    return "<font color='#FF0000'>Chưa xử lý</font>";
                case 1:
                    return "<font color='#0c5cd4'>Đang xử lý</font>";
                case 2:
                    return "<font color='#0c5cd4'>Đã xác nhận</font>";
                case 3:
                    return "<font color='#529214'>Đang giao hàng</font>";
                case 4:
                    return "<font color='#529214'>Giao hàng thành công</font>";
                case 5:
                    return "<font color='#c4670c'>Hủy đơn hàng</font>";
                default:
                    return "Chưa xử lý";
            }
        }

        public string getOrderPayment(object obj_payment)
        {
            switch (Utils.CIntDef(obj_payment))
            {
                case 1:
                    return "<font color='#0c5cd4'>Thanh toán bằng tiền mặt</font>";
                case 2:
                    return "<font color='#529214'>Thanh toán bẳng chuyển khoản</font>";
                default:
                    return "Khác";
            }
        }

        public string GetMoney(object obj_value)
        {
            return string.Format("{0:#,#} đ", obj_value).Replace(',', '.');
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
            int[] items = new int[RplistOrder.Items.Count];

            try
            {

                for (int i = 0; i < RplistOrder.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistOrder.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistOrder.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_ORDER>().Where(g => items.Contains(g.ORDER_ID));

                DB.ESHOP_ORDERs.DeleteAllOnSubmit(g_delete);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchResult();
        }

        protected void RplistOrder_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }

        protected void Lbprint_Click(object sender, EventArgs e)
        {
            
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=donhang.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            RplistOrder.Controls[0].Controls[0].FindControl("count_div").Visible = false;
            RplistOrder.Controls[0].Controls[0].FindControl("check_div").Visible = false;
            RplistOrder.Controls[0].Controls[0].FindControl("delete_div").Visible = false;
            for (int i = 0; i < RplistOrder.Items.Count; i++)
            {
                Control _countmain_div = RplistOrder.Items[i].FindControl("count_main_div") as Control;
                Control _checkmain_div = RplistOrder.Items[i].FindControl("check_main_div") as Control;
                Control _deletemain_div = RplistOrder.Items[i].FindControl("deletemain_div") as Control;
                
                _countmain_div.Visible = false;
                _checkmain_div.Visible = false;
                _deletemain_div.Visible = false;
            }
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            RplistOrder.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        #region Print
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion
    }
}