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

namespace vpro.eshop.cpanel.page
{
    public partial class extensionfiles_list : System.Web.UI.Page
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
            return "ExtensionFiles.aspx?ext_id=" + Utils.CStrDef(obj_id);
        }

        public string getLinkIcon(object obj_id, object obj_image)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_image)) && Utils.CIntDef(obj_id) > 0)
            {
                return "<img src='" + PathFiles.GetPathExt(Utils.CIntDef(obj_id)) + Utils.CStrDef(obj_image) + "' width='24px' border='0'>";
            }

            return "";
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_EXT_FILEs
                               where "" == keyword || (g.EXT_FILE_NAME).Contains(keyword) || (g.EXT_FILE_DESC).Contains(keyword)
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["ExtList"] = DataUtil.LINQToDataTable(AllList);

                Rplistextensionfiles.DataSource = AllList;
                Rplistextensionfiles.DataBind();


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
                int ObjId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_EXT_FILE>().Where(g => g.EXT_FILE_ID == ObjId);

                DB.ESHOP_EXT_FILEs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathExt(ObjId));
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
                Response.Redirect("extensionfiles_list.aspx");
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
            int[] items = new int[Rplistextensionfiles.Items.Count];

            try
            {

                for (int i = 0; i < Rplistextensionfiles.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)Rplistextensionfiles.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = Rplistextensionfiles.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }
                //delete 
                var g_delete = DB.GetTable<ESHOP_EXT_FILE>().Where(g => items.Contains(g.EXT_FILE_ID));

                DB.ESHOP_EXT_FILEs.DeleteAllOnSubmit(g_delete);
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

        protected void Rplistextensionfiles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }
    }
}