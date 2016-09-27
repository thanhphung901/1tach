using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class order_print : System.Web.UI.Page
    {
        #region Declare

        private int m_order_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        int _count = 1;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            m_order_id = Utils.CIntDef(Request["order_id"]);
            if (!IsPostBack)
            {
                LoadGridItems();
                loadLogo();
                getInfo();
                loadInfoaddress();
            }
        }
        #region Loadinfo
        private void loadLogo()
        {
            var list = DB.ESHOP_BANNERs.Where(n => n.BANNER_FIELD1 == "1").OrderByDescending(n => n.BANNER_ID).Take(1).ToList();
            if (list.Count > 0)
                Image_Logo.ImageUrl = PathFiles.GetPathBanner(list[0].BANNER_ID) + "/" + list[0].BANNER_FILE;
        }
        private void loadInfoaddress()
        {
            string pathFile;
            string strHTMLContent=string.Empty;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/contact-printorder.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();
            }
            Litinfo.Text = strHTMLContent;
        }
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
                    Lbmadonhang.Text = G_info.ToList()[0].ORDER_CODE;
                    Lbname.Text = G_info.ToList()[0].ORDER_NAME;
                    Lbadd.Text = G_info.ToList()[0].ORDER_ADDRESS;
                    Lbphone.Text = G_info.ToList()[0].ORDER_PHONE;
                    Lbgiagiam.Text = GetMoney(G_info.ToList()[0].ORDER_BILL);
                    lblTotal1.Text = GetMoney(G_info.ToList()[0].ORDER_TOTAL_AMOUNT);
                    int ship = Utils.CIntDef(G_info.ToList()[0].ORDER_SHIPPING_FEE);
                    Lbchiphi.Text = ship == -1 ? "Liên hệ" : (ship == 0 ? "Miễn phí" : GetMoney(ship));
                    Lbremark.Text = G_info.ToList()[0].ORDER_FIELD1;
                    // Lbdate.Text = string.Format("{0:dd/MM/yyyy}", G_info.ToList()[0].ORDER_PUBLISHDATE);
                    string[] a = string.Format("{0:dd/MM/yyyy}", G_info.ToList()[0].ORDER_PUBLISHDATE).Split('/');
                    string date = "Ngày " + a[0] + " Tháng " + a[1] + " Năm " + a[2];
                    Lbdate.Text = date;
                }


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
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
                                   o.ITEM_QUANTITY,
                                   o.ITEM_PRICE,
                                   o.ITEM_SUBTOTAL,
                                   o.ITEM_FIELD1,
                                   o.ITEM_FIELD2
                               });


                if (AllList.ToList().Count > 0)
                {
                    Rpdonhang.DataSource = AllList;
                    Rpdonhang.DataBind();
                }


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion
        #region Ftion
        public int getstt()
        {
            return _count++;
        }
        public string GetMoney(object obj_value)
        {
            return string.Format("{0:#,#} đ", obj_value).Replace(',', '.');
        }
        #endregion
    }
}