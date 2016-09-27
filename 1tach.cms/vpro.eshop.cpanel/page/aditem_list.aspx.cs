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
    public partial class aditem_list : System.Web.UI.Page
    {
        #region Declare

        private int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        PageIndexChange pagechange = new PageIndexChange();
        int _page = 0;
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

        #region My Function

        public string getImage(object obj_id, object obj_image1)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_id)) && Utils.CIntDef(obj_id) > 0)
                return "<img src='" + PathFiles.GetPathAdItems(Utils.CIntDef(obj_id)) + Utils.CStrDef(obj_image1) + "' border='0' />";
            else
                return "";
        }

        public string getPosition(object obj_position)
        {
            List<string[]> l = CpanelUtils.listAds();
            var list = from obj in l
                       select new
                       {
                           Id = obj[0],
                           Name = obj[1]
                       };
            foreach (var i in list)
            {
                if (Utils.CIntDef(obj_position) == Utils.CIntDef(i.Id))
                    return i.Name;
            }
            return "";

        }


        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        public string getLink(object obj_id)
        {
            return "aditem.aspx?ad_id=" + obj_id;
        }

        public string getLinkImage(object obj_id, object obj_file)
        {
            if (!string.IsNullOrEmpty(Utils.CStrDef(obj_file)) && Utils.CIntDef(obj_id) > 0)
            {
                return "<a href='" + PathFiles.GetPathAdItems(Utils.CIntDef(obj_id)) + Utils.CStrDef(obj_file) + "' target='_blank'>" + Utils.CStrDef(obj_file) + "</a>";
            }

            return "";
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
                var AllList = (from g in DB.ESHOP_AD_ITEMs
                               orderby g.AD_ITEM_PUBLISHDATE descending, g.AD_ITEM_ORDER descending
                               where ("" == keyword || (g.AD_ITEM_CODE).Contains(keyword) || (g.AD_ITEM_DESC).Contains(keyword))
                               select g).OrderByDescending(n => n.AD_ITEM_ID);


                if (AllList.ToList().Count > 0)
                    Session["AdItemList"] = DataUtil.LINQToDataTable(AllList);

                Rplistaditem.DataSource = AllList.Skip(_skip).Take(_limit);
                Rplistaditem.DataBind();
                LitPage.Text = pagechange.changePage("aditem_list.aspx", AllList.ToList().Count, _limit, _page);


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void EventDelete(RepeaterCommandEventArgs e)
        {
            int BannerId = Utils.CIntDef(e.CommandArgument);
            DeleteInfo(BannerId);
        }

        private void DeleteInfo(int ad_id)
        {
            string strLink = "";
            try
            {
                string Banner_File = "";

                var G_info = DB.GetTable<ESHOP_AD_ITEM>().Where(g => g.AD_ITEM_ID == ad_id);

                if (G_info.ToList().Count > 0)
                    Banner_File = Utils.CStrDef(G_info.ToList()[0].AD_ITEM_ID);

                DB.ESHOP_AD_ITEMs.DeleteAllOnSubmit(G_info);
                DB.SubmitChanges();

                //delete file
                if (!string.IsNullOrEmpty(Banner_File))
                {
                    string fullpath = Server.MapPath(PathFiles.GetPathAdItems(ad_id) + Banner_File);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }

                strLink = "aditem_list.aspx";

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

            int[] items = new int[Rplistaditem.Items.Count];

            try
            {
                for (int i = 0; i < Rplistaditem.Items.Count; i++)
                {
                    HtmlInputCheckBox check = (HtmlInputCheckBox)Rplistaditem.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = Rplistaditem.Items[i].FindControl("Hdid") as HiddenField;
                    int _catid = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        items[j] = _catid;
                        try
                        {
                            //delete folder
                            string fullpath = Server.MapPath(PathFiles.GetPathNews(items[j]));
                            if (Directory.Exists(fullpath))
                            {
                                DeleteAllFilesInFolder(fullpath);
                                Directory.Delete(fullpath);
                            }
                        }
                        catch (Exception)
                        { }
                        j++;
                    }
                    
                }
                //delete 
               

                //delete 
                var g_delete = DB.GetTable<ESHOP_AD_ITEM>().Where(g => items.Contains(g.AD_ITEM_ID));

                DB.ESHOP_AD_ITEMs.DeleteAllOnSubmit(g_delete);
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

        protected void Rplistaditem_ItemCommand(object source, RepeaterCommandEventArgs e)
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