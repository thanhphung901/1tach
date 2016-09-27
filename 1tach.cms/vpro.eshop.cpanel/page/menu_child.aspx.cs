using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using vpro.eshop.cpanel.Components;

namespace vpro.eshop.cpanel.page
{
    public partial class menu_child : System.Web.UI.Page
    {
        #region Declare

        private int m_news_id = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        #endregion
        #region form event
        protected void Page_Load(object sender, EventArgs e)
        {
            m_news_id = Utils.CIntDef(Request["menu_id"]);
            Hyperback.NavigateUrl = "menu_child_list.aspx";
            if (m_news_id == 0)
            {
                dvDelete.Visible = false;
            }
            if (!IsPostBack)
            {
              
                LoadMenu();
                getInfo();
            }
        }
        #endregion
        #region function
        private void LoadMenu()
        {
            try
            {
                var AllList = DB.MENUs.Where(n => n.MENU_ACTIVE == 1);

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;

                    DataTable tbl = DataUtil.LINQToDataTable(AllList);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["ID"], ds.Tables[0].Columns["MENU_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    CpanelUtils.TransformTableWithSpace2(ref CatTable, dsCat.Tables[0], relCat, null);
                    Drchuyenmuc.DataValueField = "ID";
                    Drchuyenmuc.DataTextField = "MENU_NAME";
                    Drchuyenmuc.DataSource = dsCat.Tables[0];
                    Drchuyenmuc.DataBind();
                }
                ListItem l = new ListItem("------- Root -------", "0");
                l.Selected = true;
                Drchuyenmuc.Items.Insert(0, l);
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void getInfo()
        {
            try
            {
                //LoadUnits();

                var G_info = DB.MENUs.Where(n => n.ID == m_news_id);
                if (G_info.ToList().Count > 0)
                {
                    Txtname.Value = G_info.ToList()[0].MENU_NAME;
                    Txtlink.Value = G_info.ToList()[0].MENU_LINK;
                    Drchuyenmuc.SelectedValue = G_info.ToList()[0].MENU_PARENT_ID.ToString();
                    txtorder.Value = G_info.ToList()[0].MENU_ORDER.ToString();
                    Rdactive.SelectedValue = G_info.ToList()[0].MENU_ACTIVE.ToString();
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
                int idroot = Utils.CIntDef(Drchuyenmuc.SelectedValue);
                int rank = 1;
                var getRank = DB.MENUs.Where(n => n.ID == idroot).ToList();
                if (getRank.Count > 0)
                    rank = Utils.CIntDef(getRank[0].MENU_RANK + 1);
                if (m_news_id == 0)
                {

                    //insert
                    MENU menu = new MENU();
                    menu.MENU_NAME = Txtname.Value;
                    menu.MENU_PARENT_ID = idroot;
                    menu.MENU_RANK = rank;
                    menu.MENU_LINK = Txtlink.Value;
                    menu.MENU_ORDER = Utils.CIntDef(txtorder.Value);
                    menu.MENU_ACTIVE = Utils.CIntDef(Rdactive.SelectedValue);
                    DB.MENUs.InsertOnSubmit(menu);
                    DB.SubmitChanges();
                    var _new = DB.GetTable<MENU>().OrderByDescending(g => g.ID).Take(1);
                    m_news_id = _new.Single().ID;
                    strLink = string.IsNullOrEmpty(strLink) ? "menu_child.aspx?menu_id=" + m_news_id : strLink;
                }
                else
                {
                    //update
                    var c_update = DB.GetTable<MENU>().Where(g => g.ID == m_news_id);

                    if (c_update.ToList().Count > 0)
                    {
                        c_update.ToList()[0].MENU_NAME = Txtname.Value;
                        c_update.ToList()[0].MENU_PARENT_ID = Utils.CIntDef(Drchuyenmuc.SelectedValue);
                        c_update.ToList()[0].MENU_LINK = Txtlink.Value;
                        c_update.ToList()[0].MENU_RANK = rank;
                        c_update.ToList()[0].MENU_ORDER = Utils.CIntDef(txtorder.Value);
                        c_update.ToList()[0].MENU_ACTIVE = Utils.CIntDef(Rdactive.SelectedValue);
                        DB.SubmitChanges();
                        strLink = string.IsNullOrEmpty(strLink) ? "menu_child_list.aspx" : strLink;
                    }

                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                { Response.Redirect(strLink); }
            }
        }
        private void DeleteInfo()
        {
            try
            {
                var G_info = DB.GetTable<MENU>().Where(g => g.ID == m_news_id);

                DB.MENUs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();
                Response.Redirect("menu_child_list.aspx");
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion

        protected void lbtSaveNew_Click(object sender, EventArgs e)
        {

            SaveInfo("menu_child.aspx");
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveInfo();
        }

        protected void LbsaveClose_Click(object sender, EventArgs e)
        {
            SaveInfo("menu_child_list.aspx");
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {

        }
    }
}