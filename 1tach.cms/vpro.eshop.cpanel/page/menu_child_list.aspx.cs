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
    public partial class menu_child_list : System.Web.UI.Page
    {
        #region Declare
        int _page = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        PageIndexChange pagechange = new PageIndexChange();
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

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "menu_child.aspx?menu_id=" + Utils.CStrDef(obj_id);
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
                var AllList = (from g in DB.MENUs
                               where "" == keyword || g.MENU_NAME.Contains(keyword)
                               orderby g.ID descending
                               select g);

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
                    RplistMenu.DataSource = dsCat.Tables[0];
                    RplistMenu.DataBind();
                    //LitPage.Text = pagechange.changePage("customer_list.aspx", AllList.ToList().Count, _limit, _page);
                }

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
                int Menuid = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<MENU>().Where(g => g.ID == Menuid);

                DB.MENUs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("menu_child_list.aspx");
            }
        }

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
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
            int[] items = new int[RplistMenu.Items.Count];

            try
            {

                for (int i = 0; i < RplistMenu.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistMenu.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistMenu.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }

                //delete 
                var g_delete = DB.GetTable<MENU>().Where(g => items.Contains(Convert.ToInt32(g.ID)));

                DB.MENUs.DeleteAllOnSubmit(g_delete);
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

        protected void RplistMenu_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }
    }
}