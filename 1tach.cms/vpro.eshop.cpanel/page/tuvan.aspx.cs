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
    public partial class tuvan : System.Web.UI.Page
    {
        #region Declare

        private int m_contact_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            m_contact_id = Utils.CIntDef(Request["id"]);
            Hyperback.NavigateUrl = "tuvan_list.aspx";
            if (m_contact_id == 0)
            {
                dvDelete.Visible = false;
            }

            if (!IsPostBack)
            {
              
                getInfo();
            }

        }

        #endregion

        #region Button Events

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteInfo();
        }
        #endregion

        #region My Functions

        private void getInfo()
        {
            try
            {
                var G_info = (from g in DB.TU_VANs
                              where g.ID == m_contact_id
                              select g
                            ).ToList();

                if (G_info.Count > 0)
                {
                    txtName.Value = G_info[0].HO_TEN;
                    txtEmail.Value = G_info[0].EMAIL;
                    txtPhone.Value = G_info[0].DIEN_THOAI;
                    txtLoaiCT.Value = G_info[0].LOAI_CT;
                    txtTenDuAn.Value = G_info[0].TEN_DU_AN;
                    txtDienTichNha.Value = G_info[0].DIEN_TICH_NHA;
                    txtThoiGianThiCong.Value = String.Format("{0:dd/MM/yyyy}", G_info[0].THOI_GIAN_THI_CONG);
                    txtKinhPhi.Value = G_info[0].KINH_PHI_DU_KIEN;
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<TU_VAN>().Where(g => g.ID == m_contact_id);

                DB.TU_VANs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                Response.Redirect("tuvan_list.aspx");

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion

       
    }
}