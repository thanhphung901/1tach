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
    public partial class config_map : System.Web.UI.Page
    {
        #region Declare

        string m_pathFile = "";
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveHTMLInfo();
        }

        #endregion

        #region My Function

        private void CreateDirectory()
        {
            m_pathFile = PathFiles.GetPathFooter();

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }

            Session["FileManager"] = m_pathFile;
        }

        private void showFileHTML()
        {
            try
            {
                string pathFile;
                string strHTMLContent;
                pathFile = Server.MapPath(PathFiles.GetPathFooter() + "/mapfo-vi.htm");
                if ((File.Exists(pathFile)))
                {
                    StreamReader objNewsReader;
                    //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                    objNewsReader = new StreamReader(pathFile);
                    strHTMLContent = objNewsReader.ReadToEnd();
                    objNewsReader.Close();

                    mrk.Value = strHTMLContent;
                    //mrkEn.Value = strHTMLContentEn;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);

            }
        }
        private void SaveHTMLInfo()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathFooter() + "/mapfo-vi.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                fsoFile = File.CreateText(strHTMLFileLocation);
                strHTMLContent = mrk.Value;                
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        #endregion

        #region Page Events

        protected void Page_Load(object sender, EventArgs e)
        {

            CreateDirectory();

            if (!IsPostBack)
            {
               
                showFileHTML();
            }

        }

        #endregion
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

                            string pathfile = Server.MapPath("/data/footer/");
                            string fullpathfile = pathfile + "/" + Path.GetFileName(hpf.FileName);
                            lpath.Add(PathFiles.GetPathFooter() + Path.GetFileName(hpf.FileName));
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