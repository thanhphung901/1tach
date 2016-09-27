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
using System.IO;

namespace vpro.eshop.cpanel.page
{
    public partial class category_list : System.Web.UI.Page
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
            return "category.aspx?cat_id=" + Utils.CStrDef(obj_id);
        }

        private void SearchResult()
        {
            try
            {
                string keyword = CpanelUtils.ClearUnicode(txtKeyword.Value);

                var AllList = (from g in DB.ESHOP_CATEGORies
                               where ("" == keyword || (DB.fClearUnicode(g.CAT_NAME)).Contains(keyword)) && g.CAT_RANK > 0
                               select new
                               {
                                   g.CAT_ID,
                                   g.CAT_PARENT_ID,
                                   CAT_NAME = (string.IsNullOrEmpty(g.CAT_CODE) ? g.CAT_NAME : g.CAT_NAME + "(" + g.CAT_CODE + ")"),
                                   g.CAT_POSITION,
                                   g.CAT_LANGUAGE,
                                   g.CAT_ORDER,
                                   g.CAT_PERIOD_ORDER,
                                   g.CAT_RANK,
                                   g.CAT_STATUS
                               });

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    //Session["CatList"] = DataUtil.LINQToDataTable(AllList);
                    //DataTable tbl = Session["CatList"] as DataTable;
                    DataTable tbl = DataUtil.LINQToDataTable(AllList);

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);
                    if (IsPostBack)
                    {
                        Rplistcate.DataSource = AllList;
                        Rplistcate.DataBind();
                    }
                    else
                    {
                        Rplistcate.DataSource = dsCat.Tables[0];
                        Rplistcate.DataBind();
                    }
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
                int CatId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == CatId);

                DB.ESHOP_CATEGORies.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathCategory(CatId));
                if (Directory.Exists(fullpath))
                {
                    DeleteAllFilesInFolder(fullpath);
                    Directory.Delete(fullpath);
                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                Response.Redirect("category_list.aspx");
            }
        }

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        public string getPos(object Cat_Pos)
        {
            switch (Utils.CIntDef(Cat_Pos))
            {
                
                case 1: return "Menu Top";
                case 2: return "Menu Theo cấp cha";
            }
            return "Khác";
        }
        public string getStatus(object obj_status)
        {
            return Utils.CIntDef(obj_status) == 0 ? "<img src='../Images/icon-deactive.png' title='Không hiển thị' />" : "<img src='../Images/icon-active.gif' title='Hiển thị'/>";
        }
        public string getLanguage(object Cat_Pos)
        {
            return Utils.CIntDef(Cat_Pos) == 1 ? "Việt Nam" : "English";
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
            int[] items = new int[Rplistcate.Items.Count];

            try
            {
                for (int i = 0; i < Rplistcate.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)Rplistcate.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = Rplistcate.Items[i].FindControl("Hdcatid") as HiddenField;
                    int _catid = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _catid;
                        j++;
                    }
                }
                //delete 
                var g_delete = DB.GetTable<ESHOP_CATEGORy>().Where(g => items.Contains(g.CAT_ID));

                DB.ESHOP_CATEGORies.DeleteAllOnSubmit(g_delete);
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

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Rplistcate.Items.Count; i++)
                {
                    HtmlInputText txtOrder = (HtmlInputText)Rplistcate.Items[i].FindControl("txtOrder");
                    HtmlInputText txtOrderPeriod = (HtmlInputText)Rplistcate.Items[i].FindControl("txtOrderPeriod");
                    HiddenField Hdid = Rplistcate.Items[i].FindControl("Hdcatid") as HiddenField;
                    int _catid = Utils.CIntDef(Hdid.Value);
                    var list = DB.ESHOP_CATEGORies.Where(n => n.CAT_ID == _catid).ToList();
                    if (list.Count > 0)
                    {
                        list[0].CAT_ORDER = Utils.CIntDef(txtOrder.Value);
                        list[0].CAT_PERIOD_ORDER = Utils.CIntDef(txtOrderPeriod.Value);
                        DB.SubmitChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            { SearchResult(); }
        }

      
        #endregion

        protected void Rplistcate_ItemCommand(object source, RepeaterCommandEventArgs e)
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