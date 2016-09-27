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
    public partial class news_category : System.Web.UI.Page
    {
        #region Declare

        private int m_news_id = 0;
        int _count = 0;
        eshopdbDataContext DB = new eshopdbDataContext();
        int _type = 0;
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {

            m_news_id = Utils.CIntDef(Request["news_id"]);
            _type = Utils.CIntDef(Request["type"]);
            Hyperback.NavigateUrl = "news.aspx?type=" + _type + "&news_id=" + m_news_id;

            if (!IsPostBack)
            {
                LoadCat();
            }
            hplCatNews.HRef = "news_category.aspx?type=" + _type + "&news_id=" + m_news_id;
            hplEditorHTMl.HRef = "news_editor.aspx?type=" + _type + "&news_id=" + m_news_id;
            hplNewsAtt.HRef = "news_attachment.aspx?type=" + _type + "&news_id=" + m_news_id;
            hplAlbum.HRef = "news_images.aspx?type=" + _type + "&news_id=" + m_news_id;
            //bplNewsCopy.HRef = "news_copy.aspx?news_id=" + m_news_id;
            hplComment.HRef = "news_comment.aspx?type=" + _type + "&news_id=" + m_news_id;
            //hplCatProducts.HRef = "news_news.aspx?news_id=" + m_news_id;

        }

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveNewsCat();
        }

        #endregion

        #region My Functions

        private void SaveNewsCat()
        {
            string strLink = "";

            try
            {
                HtmlInputCheckBox check = new HtmlInputCheckBox();

                var gcdel = (from gp in DB.ESHOP_NEWS_CATs
                             where gp.NEWS_ID == m_news_id
                             select gp);

                DB.ESHOP_NEWS_CATs.DeleteAllOnSubmit(gcdel);
                for (int i = 0; i < Rplistcate.Items.Count; i++)
                {
                    check = new HtmlInputCheckBox();
                    check = (HtmlInputCheckBox)Rplistcate.Items[i].FindControl("chkSelect");
                    HiddenField Hdid = Rplistcate.Items[i].FindControl("Hdcatid") as HiddenField;
                    int _catid = Utils.CIntDef(Hdid.Value);
                    if (check.Checked)
                    {
                        ESHOP_NEWS_CAT grinsert = new ESHOP_NEWS_CAT();
                        grinsert.CAT_ID = _catid;
                        grinsert.NEWS_ID = m_news_id;
                        DB.ESHOP_NEWS_CATs.InsertOnSubmit(grinsert);
                    }
                }
               

                DB.SubmitChanges();
                strLink = "news.aspx?type="+_type+"&news_id=" + m_news_id;
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

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        private void LoadCat()
        {
            try
            {
                var AllList = (from g in DB.ESHOP_CATEGORies
                               where g.CAT_RANK > 0
                               select new
                               {
                                   g.CAT_ID,
                                   g.CAT_PARENT_ID,
                                   g.CAT_RANK,
                                   CAT_NAME = (string.IsNullOrEmpty(g.CAT_CODE) ? g.CAT_NAME : g.CAT_NAME + "(" + g.CAT_CODE + ")")
                               });

                if (AllList.ToList().Count > 0)
                {
                    DataRelation relCat;
                    Session["CatNewsList"] = DataUtil.LINQToDataTable(AllList);
                    DataTable tbl = Session["CatNewsList"] as DataTable;

                    DataSet ds = new DataSet();
                    ds.Tables.Add(tbl);

                    tbl.PrimaryKey = new DataColumn[] { tbl.Columns["CAT_ID"] };
                    relCat = new DataRelation("Category_parent", ds.Tables[0].Columns["CAT_ID"], ds.Tables[0].Columns["CAT_PARENT_ID"], false);

                    ds.Relations.Add(relCat);
                    DataSet dsCat = ds.Clone();
                    DataTable CatTable = ds.Tables[0];

                    DataUtil.TransformTableWithSpace(ref CatTable, dsCat.Tables[0], relCat, null);

                    Rplistcate.DataSource = dsCat.Tables[0];
                    Rplistcate.DataBind();
                }


                
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        public bool CheckCat(object CatId)
        {
            try
            {
                int Cat_Id = Utils.CIntDef(CatId);

                var per = DB.GetTable<ESHOP_NEWS_CAT>().Where(gp => gp.NEWS_ID == m_news_id && gp.CAT_ID == Cat_Id);
                if (per.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;
            }
        }

        #endregion
    }
}