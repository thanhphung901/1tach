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
    public partial class order : System.Web.UI.Page
    {
        #region Declare

        private int m_order_id = 0;
        private int m_cus_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion
        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_order_id = Utils.CIntDef(Request["order_id"]);
            Hyperprint.NavigateUrl = "order-print.aspx?order_id=" + m_order_id;
            Hyperback.NavigateUrl = "order_list.aspx";
            if (m_order_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
               
                getInfo();
                LoadGridItems();
            }

           

        }

        #endregion

        #region Button Events

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }
        protected void LbsaveClose_Click(object sender, EventArgs e)
        {
            SaveInfo("order_list.aspx");
        }

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {
            SaveInfo("orderexpress.aspx");
        }

        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.ESHOP_ORDERs
                              where g.ORDER_ID == m_order_id
                              select g
                            );

                if (G_info.ToList().Count > 0)
                {
                    txtOrderCode.Value = G_info.ToList()[0].ORDER_CODE;
                    txtOrderDate.Value = string.Format("{0:HH:mm dd-MM-yyyy}", G_info.ToList()[0].ORDER_PUBLISHDATE);
                    txtOrderDesc.Value = G_info.ToList()[0].ORDER_FIELD1;
                    ddlStatus.SelectedValue = Utils.CStrDef(G_info.ToList()[0].ORDER_STATUS, "0");

                    txtName.Value = G_info.ToList()[0].ORDER_NAME;
                    txtEmail.Value = G_info.ToList()[0].ORDER_EMAIL;
                    txtAddress.Value = G_info.ToList()[0].ORDER_ADDRESS;
                    txtPhone.Value = G_info.ToList()[0].ORDER_PHONE;
                    if (G_info.ToList()[0].ORDER_UPDATE != null)
                        txtdateDeli.Value = string.Format("{0:HH:mm dd-MM-yyyy}", G_info.ToList()[0].ORDER_UPDATE);
                    txtpricegiam.Text = Utils.CIntDef(G_info.ToList()[0].ORDER_BILL.ToString()).ToString();
                    //lblTotal.Text = "Tổng tiền : " + GetMoney(G_info.ToList()[0].ORDER_TOTAL_ALL);
                    Txtchiphi.Text = Utils.CIntDef(G_info.ToList()[0].ORDER_SHIPPING_FEE).ToString();
                    lblTotal_amount.Text = GetMoney(G_info.ToList()[0].ORDER_TOTAL_AMOUNT);
                }
               

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SaveInfo(string strLink = "")
        {
           
            try
            {
                var _items = DB.GetTable<ESHOP_ORDER>().Where(o => o.ORDER_ID == m_order_id);

                if (_items.ToList().Count > 0)
                {
                    _items.Single().ORDER_STATUS = Utils.CIntDef(ddlStatus.SelectedValue);
                    _items.Single().ORDER_NAME = Utils.CStrDef(txtName.Value);
                    _items.Single().ORDER_FIELD1 = Utils.CStrDef(txtOrderDesc.Value);
                    _items.Single().ORDER_EMAIL = Utils.CStrDef(txtEmail.Value);
                    _items.Single().ORDER_ADDRESS = Utils.CStrDef(txtAddress.Value);
                    _items.Single().ORDER_PHONE = Utils.CStrDef(txtPhone.Value);

                    if (Utils.CIntDef(ddlStatus.SelectedValue) == 2)
                    {
                        _items.Single().ORDER_UPDATE = DateTime.Now;
                    }
                    DB.SubmitChanges();

                    if(String.IsNullOrEmpty(strLink)) strLink = "order_list.aspx";
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                    Response.Redirect(strLink);
            }
        }
        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<ESHOP_ORDER>().Where(g => g.ORDER_ID == m_order_id);

                DB.ESHOP_ORDERs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("order_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void updateTotal()
        {
            var list = DB.ESHOP_ORDER_ITEMs.Where(n => n.ORDER_ID == m_order_id);
            decimal price = 0;
            foreach (var i in list)
            {
                price += Utils.CDecDef(i.ITEM_SUBTOTAL);
            }
            var updateOrder = DB.ESHOP_ORDERs.Where(n => n.ORDER_ID == m_order_id).ToList();
            if (updateOrder.Count > 0)
            {
                updateOrder[0].ORDER_TOTAL_ALL = price;
                updateOrder[0].ORDER_TOTAL_AMOUNT = price + Utils.CDecDef(updateOrder[0].ORDER_SHIPPING_FEE);
                DB.SubmitChanges();
            }
        }
        private void EventDelete(RepeaterCommandEventArgs e)
        {
            try
            {
                int UnitId = Utils.CIntDef(e);
                var g_delete = DB.GetTable<ESHOP_ORDER_ITEM>().Where(g => g.ITEM_ID == UnitId && g.ORDER_ID == m_order_id);
                DB.ESHOP_ORDER_ITEMs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();
                updateTotal();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("order.aspx?order_id=" + m_order_id);
            }
        }
        private void LoadGridItems()
        {
            try
            {
                var AllList = (from o in DB.ESHOP_ORDER_ITEMs
                               where o.ORDER_ID == m_order_id
                               orderby o.ESHOP_NEW.NEWS_TITLE descending
                               select new
                               {
                                   o.ITEM_ID,
                                   o.ESHOP_NEW.NEWS_TITLE,
                                   o.ESHOP_NEW.NEWS_CODE,
                                   o.ESHOP_NEW.NEWS_SEO_URL,
                                   o.ITEM_QUANTITY,
                                   o.ITEM_PRICE,
                                   o.ITEM_SUBTOTAL,
                                   o.ITEM_FIELD1,
                                   o.ITEM_FIELD2
                               });


                if (AllList.ToList().Count > 0)
                    Session["OrderItems"] = DataUtil.LINQToDataTable(AllList);

                RplistOrderitem.DataSource = AllList;
                RplistOrderitem.DataBind();
                for (int i = 0; i < RplistOrderitem.Items.Count; i++)
                {
                    //TextBox txt = GrdGiohang.Rows[i].FindControl("txtSoLuong") as TextBox;
                    DropDownList dr = RplistOrderitem.Items[i].FindControl("drSoLuong") as DropDownList;
                    HiddenField hdid = RplistOrderitem.Items[i].FindControl("Hdid") as HiddenField;
                    int _sID = Utils.CIntDef(hdid.Value);

                    var g = DB.GetTable<ESHOP_ORDER_ITEM>().Where(gid => gid.ITEM_ID == _sID);
                    if (g.ToList().Count > 0)
                    {
                        dr.SelectedValue = g.ToList()[0].ITEM_QUANTITY.ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        public string GetNewsTitle(object Title, object Color, object Size)
        {
            try
            {
                return Utils.CStrDef(Title) + "(" + Utils.CStrDef(Color) + "/" + Utils.CStrDef(Size) + ")";
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string GetMoney(object obj_value)
        {
            return string.Format("{0:#,#} đ", obj_value).Replace(',', '.');
        }
        public string getLink(object seourl)
        {
            return "http://esell.vn/" + seourl + ".html";
        }
        #endregion


        protected void Lbprint_Click(object sender, EventArgs e)
        {
            Response.Redirect("print-donhang.aspx?order_id=" + m_order_id + "");
        }
        #region index change
        protected void drSoLuong_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < RplistOrderitem.Items.Count; i++)
            {
                //TextBox txt = GrdGiohang.Rows[i].FindControl("txtSoLuong") as TextBox;
                DropDownList dr = RplistOrderitem.Items[i].FindControl("drSoLuong") as DropDownList;
                HiddenField hdid = RplistOrderitem.Items[i].FindControl("Hdid") as HiddenField;
                int _sID = Utils.CIntDef(hdid.Value);

                var g = DB.GetTable<ESHOP_ORDER_ITEM>().Where(gid => gid.ITEM_ID == _sID);
                if (g.ToList().Count > 0)
                {
                    if (Utils.CIntDef(dr.SelectedValue) != 0)
                    {
                        g.ToList()[0].ITEM_QUANTITY = Utils.CIntDef(dr.SelectedValue);
                        g.ToList()[0].ITEM_SUBTOTAL = Utils.CIntDef(dr.SelectedValue) * g.ToList()[0].ITEM_PRICE;
                        DB.SubmitChanges();
                    }


                }

            }
            var listoritem = DB.ESHOP_ORDER_ITEMs.Where(n => n.ORDER_ID == m_order_id);
            var updateorder = DB.ESHOP_ORDERs.Where(n => n.ORDER_ID == m_order_id).ToList();
            foreach (var s in updateorder)
            {
                s.ORDER_TOTAL_ALL = listoritem.Sum(n => n.ITEM_SUBTOTAL);
                s.ORDER_TOTAL_AMOUNT = s.ORDER_TOTAL_ALL + Utils.CIntDef(s.ORDER_SHIPPING_FEE) + Utils.CIntDef(s.ORDER_BILL);
                DB.SubmitChanges();
            }
            LoadGridItems();
            getInfo();
        }
        #endregion

        protected void Txtchiphi_TextChanged(object sender, EventArgs e)
        {
            var updateorder = DB.ESHOP_ORDERs.Where(n => n.ORDER_ID == m_order_id).ToList();
            decimal chiphi = Utils.CDecDef(Txtchiphi.Text);
            foreach (var s in updateorder)
            {

                if (s.ORDER_SHIPPING_FEE != null)
                {
                    if (s.ORDER_SHIPPING_FEE != -1)
                        s.ORDER_TOTAL_AMOUNT = s.ORDER_TOTAL_AMOUNT - Utils.CDecDef(s.ORDER_SHIPPING_FEE) + chiphi;
                    else
                        s.ORDER_TOTAL_AMOUNT = s.ORDER_TOTAL_AMOUNT + chiphi;
                    s.ORDER_SHIPPING_FEE = chiphi;
                }
                else
                {
                    s.ORDER_SHIPPING_FEE = chiphi;
                    s.ORDER_TOTAL_AMOUNT += chiphi;
                }
                DB.SubmitChanges();
            }
            getInfo();
        }

        protected void txtpricegiam_TextChanged(object sender, EventArgs e)
        {
            var updateorder = DB.ESHOP_ORDERs.Where(n => n.ORDER_ID == m_order_id).ToList();
            int giagiam = Utils.CIntDef(txtpricegiam.Text);
            foreach (var s in updateorder)
            {

                if (giagiam != 0)
                {
                    s.ORDER_TOTAL_AMOUNT = s.ORDER_TOTAL_AMOUNT +Utils.CDecDef(s.ORDER_BILL) - giagiam;
                    s.ORDER_BILL = giagiam;
                }
                else
                {
                    s.ORDER_TOTAL_AMOUNT = s.ORDER_TOTAL_ALL + Utils.CDecDef(s.ORDER_SHIPPING_FEE);
                    s.ORDER_BILL = giagiam;
                }
                DB.SubmitChanges();
            }
            getInfo();
        }

        protected void RplistOrderitem_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        
    }
}