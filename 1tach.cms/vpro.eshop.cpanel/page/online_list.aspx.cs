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
    public partial class online_list : System.Web.UI.Page
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
            return "online.aspx?online_id=" + Utils.CStrDef(obj_id);
        }

        public string getType(object obj_type)
        {
            string _res = string.Empty;
            switch (Utils.CIntDef(obj_type))
            {
                case 1: _res = "Skype"; break;
                case 2: _res = "Hotline"; break;
                case 3: _res = "Facebook"; break;
                case 4: _res = "Zalo"; break;
                case 5: _res = "Gplus"; break;
                
            }
            return _res;
        }

        private void SearchResult()
        {
            try
            {
                string keyword = txtKeyword.Value;

                var AllList = (from g in DB.ESHOP_ONLINEs
                               where "" == keyword || (g.ONLINE_NICKNAME).Contains(keyword) || (g.ONLINE_DESC).Contains(keyword)
                               select g);


                if (AllList.ToList().Count > 0)
                    Session["OnlineList"] = DataUtil.LINQToDataTable(AllList);

                RplistOnline.DataSource = AllList;
                RplistOnline.DataBind();


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
                int UnitId = Utils.CIntDef(e.CommandArgument);

                var g_delete = DB.GetTable<ESHOP_ONLINE>().Where(g => g.ONLINE_ID == UnitId);

                DB.ESHOP_ONLINEs.DeleteAllOnSubmit(g_delete);
                DB.SubmitChanges();

                //delete folder
                string fullpath = Server.MapPath(PathFiles.GetPathOnline(UnitId));
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
                Response.Redirect("online_list.aspx");
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
            int[] items = new int[RplistOnline.Items.Count];

            try
            {

                for (int i = 0; i < RplistOnline.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)RplistOnline.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = RplistOnline.Items[i].FindControl("Hdid") as HiddenField;
                    int _id = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _id;
                        j++;
                    }
                }

                //delete 
                var g_delete = DB.GetTable<ESHOP_ONLINE>().Where(g => items.Contains(g.ONLINE_ID));

                DB.ESHOP_ONLINEs.DeleteAllOnSubmit(g_delete);
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

        protected void RplistOnline_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
                EventDelete(e);
        }
    }
}