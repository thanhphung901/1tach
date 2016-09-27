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
    public partial class category_seo_editor : System.Web.UI.Page
    {
        eshopdbDataContext DB = new eshopdbDataContext();
        int _idcat = 0;
        string m_pathFile = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            _idcat = Utils.CIntDef(Request.QueryString["id"]);
            Hyperback.NavigateUrl = "category.aspx?cat_id=" + _idcat;
            CreateDirectory();
            if (!IsPostBack) showFileHTML();
        }
        private void CreateDirectory()
        {
            m_pathFile = PathFiles.GetPathCategory(_idcat);

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }
            Session["FileManager"] = m_pathFile;
        }
        private void showFileHTML()
        {
            string pathFile;
            string strHTMLContent;
            //string strSubweb;

            //if ((HttpContext.Current.Request.ApplicationPath == "/"))
            //{
            //    strSubweb = Request.ApplicationPath;
            //}
            //else
            //{
            //    strSubweb = Request.ApplicationPath + "/";
            //}

            if (_idcat > 0)
            {

                var newsInfo = DB.GetTable<ESHOP_CATEGORy>().Where(n => n.CAT_ID == _idcat);

                pathFile = Server.MapPath(PathFiles.GetPathCategory(_idcat) + "/" + newsInfo.ToList()[0].CAT_FIELD5);

                if ((File.Exists(pathFile)))
                {
                    StreamReader objNewsReader;
                    //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                    objNewsReader = new StreamReader(pathFile);
                    strHTMLContent = objNewsReader.ReadToEnd();
                    objNewsReader.Close();
                    mrk.Value = strHTMLContent;
                }
            }
        }
        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveHTMLInfo();
        }
        private void SaveHTMLInfo()
        {
            try
            {
                if ((_idcat > 0))
                {
                    string strHTMLFileLocation;
                    string strFileName;
                    string strHTMLContent;
                    StreamWriter fsoFile;

                    strFileName = PathFiles.GetPathCategory(_idcat) + _idcat.ToString() + "-vi.htm";
                    strHTMLFileLocation = Server.MapPath(strFileName);
                    strHTMLContent = mrk.Value;

                    fsoFile = File.CreateText(strHTMLFileLocation);
                    fsoFile.Write(strHTMLContent);
                    fsoFile.Close();

                    //update to db
                    var n_update = DB.GetTable<ESHOP_CATEGORy>().Where(g => g.CAT_ID == _idcat);

                    if (n_update.ToList().Count > 0)
                    {
                        n_update.Single().CAT_FIELD5 = _idcat.ToString() + "-vi.htm";

                        DB.SubmitChanges();
                    }

                }

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #region Album images
        protected void Btupmulti_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {

                    HttpFileCollection hfc = Request.Files;
                    List<string> lpath = new List<string>();
                    for (int i = 0; i < hfc.Count; i++)
                    {
                        HttpPostedFile hpf = hfc[i];
                        if (hpf.ContentLength > 0)
                        {

                            string pathfile = Server.MapPath("/data/categories/" + _idcat);
                            string fullpathfile = pathfile + "/" + Path.GetFileName(hpf.FileName);
                            lpath.Add(PathFiles.GetPathCategory(_idcat) + _idcat.ToString()+ "/" + Path.GetFileName(hpf.FileName));
                            if (!Directory.Exists(pathfile))
                            {
                                Directory.CreateDirectory(pathfile);
                            }
                            hpf.SaveAs(fullpathfile);

                        }
                    }
                    string listimg = string.Empty;
                    for (int s = 0; s < lpath.Count; s++)
                    {
                        listimg += "<p><img src='" + lpath[s] + "' alt=''/></p>";
                    }
                    mrk.Value += listimg;
                }



            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion
    }
}