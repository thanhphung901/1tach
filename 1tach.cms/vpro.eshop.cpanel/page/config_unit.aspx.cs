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
    public partial class config_unit : System.Web.UI.Page
    {
        #region Declare

        eshopdbDataContext db = new eshopdbDataContext();

        #endregion

        #region form event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetInfo();
            }

        }

        #endregion

        #region Data
        private void GetInfo()
        {
            var list = db.ESHOP_UNITs.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                switch (Utils.CStrDef(list[i].UNIT_CODE))
                {
                    case "V1":
                        txtTitleL.Value = list[i].UNIT_NAME;
                        txtTitleN.Value = list[i].UNIT_NAME1;
                        txtDesc.Value = list[i].UNIT_DESC;
                        break;
                    case "V2":
                        txtTitleL1.Value = list[i].UNIT_NAME;
                        txtTitleN1.Value = list[i].UNIT_NAME1;
                        txtDesc1.Value = list[i].UNIT_DESC;
                        break;
                    case "V3":
                        txtTitleL2.Value = list[i].UNIT_NAME;
                        txtTitleN2.Value = list[i].UNIT_NAME1;
                        txtDesc2.Value = list[i].UNIT_DESC;
                        break;
                    case "E1":
                        txtTitleENL.Value = list[i].UNIT_NAME;
                        txtTitleENN.Value = list[i].UNIT_NAME1;
                        txtDescENT.Value = list[i].UNIT_DESC;
                        break;
                    case "E2":
                        txtTitleENL1.Value = list[i].UNIT_NAME;
                        txtTitleENN1.Value = list[i].UNIT_NAME1;
                        txtDescEN1.Value = list[i].UNIT_DESC;
                        break;
                    case "E3":
                        txtTitleENL2.Value = list[i].UNIT_NAME;
                        txtTitleENN2.Value = list[i].UNIT_NAME1;
                        txtDescEN2.Value = list[i].UNIT_DESC;
                        break;
                }
            }
        }
        private void SaveInfo()
        {
            ESHOP_UNIT l1 = db.ESHOP_UNITs.Where(n => n.UNIT_CODE == "V1").Single();
            if (l1 != null)
            {
                l1.UNIT_NAME = Utils.CStrDef(txtTitleL.Value).Trim();
                l1.UNIT_NAME1 = Utils.CStrDef(txtTitleN.Value).Trim();
                l1.UNIT_DESC = Utils.CStrDef(txtDesc.Value).Trim();
                db.SubmitChanges();
            }

            ESHOP_UNIT l2 = db.ESHOP_UNITs.Where(n => n.UNIT_CODE == "V2").Single();
            if (l2 != null)
            {
                l2.UNIT_NAME = Utils.CStrDef(txtTitleL1.Value).Trim();
                l2.UNIT_NAME1 = Utils.CStrDef(txtTitleN1.Value).Trim();
                l2.UNIT_DESC = Utils.CStrDef(txtDesc1.Value).Trim();
                db.SubmitChanges();
            }

            ESHOP_UNIT l3 = db.ESHOP_UNITs.Where(n => n.UNIT_CODE == "V3").Single();
            if (l3 != null)
            {
                l3.UNIT_NAME = Utils.CStrDef(txtTitleL2.Value).Trim();
                l3.UNIT_NAME1 = Utils.CStrDef(txtTitleN2.Value).Trim();
                l3.UNIT_DESC = Utils.CStrDef(txtDesc2.Value).Trim();
                db.SubmitChanges();
            }

            ESHOP_UNIT lE1 = db.ESHOP_UNITs.Where(n => n.UNIT_CODE == "E1").Single();
            if (lE1 != null)
            {
                lE1.UNIT_NAME = Utils.CStrDef(txtTitleENL.Value).Trim();
                lE1.UNIT_NAME1 = Utils.CStrDef(txtTitleENN.Value).Trim();
                lE1.UNIT_DESC = Utils.CStrDef(txtDescENT.Value).Trim();
                db.SubmitChanges();
            }

            ESHOP_UNIT lE2 = db.ESHOP_UNITs.Where(n => n.UNIT_CODE == "E2").Single();
            if (lE2 != null)
            {
                lE2.UNIT_NAME = Utils.CStrDef(txtTitleENL1.Value).Trim();
                lE2.UNIT_NAME1 = Utils.CStrDef(txtTitleENN1.Value).Trim();
                lE2.UNIT_DESC = Utils.CStrDef(txtDescEN1.Value).Trim();
                db.SubmitChanges();
            }

            ESHOP_UNIT lE3 = db.ESHOP_UNITs.Where(n => n.UNIT_CODE == "E3").Single();
            if (lE3 != null)
            {
                lE3.UNIT_NAME = Utils.CStrDef(txtTitleENL2.Value).Trim();
                lE3.UNIT_NAME1 = Utils.CStrDef(txtTitleENN2.Value).Trim();
                lE3.UNIT_DESC = Utils.CStrDef(txtDescEN2.Value).Trim();
                db.SubmitChanges();
            }
        }
        #endregion



        #region Button Events
        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }
        #endregion
    }
}