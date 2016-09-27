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
    public partial class contact_config : System.Web.UI.Page
    {
        #region Declare

        string m_pathFile = "";
        eshopdbDataContext DB = new eshopdbDataContext();

        #endregion

        #region Button Events

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            SaveHTMLInfo();
            SaveHTMLInfo1();
            SaveHTMLInfo3();
            SaveHTMLInfo4();
           
        }

        #endregion

        #region My Function

        private void CreateDirectory()
        {
            m_pathFile = PathFiles.GetPathContact();

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }

            Session["FileManager"] = m_pathFile;
        }
        
       
     
       
     
       
    
       
      
        private void showFileHTML1()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/map-vi.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                mrk1.Value = strHTMLContent;
            }
        }

       
        private void showFileHTML()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/contact-vi.htm");

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
        private void showFileHTML4()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/contact-cast.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                mrk4.Value = strHTMLContent;
            }
        }
        private void showFileHTML3()
        {
            string pathFile;
            string strHTMLContent;

            pathFile = Server.MapPath(PathFiles.GetPathContact() + "/contact-banking.htm");

            if ((File.Exists(pathFile)))
            {
                StreamReader objNewsReader;
                //objNewsReader = New StreamReader(pathFile, System.Text.Encoding.Default)
                objNewsReader = new StreamReader(pathFile);
                strHTMLContent = objNewsReader.ReadToEnd();
                objNewsReader.Close();

                mrk3.Value = strHTMLContent;
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

                strFileName = PathFiles.GetPathContact() + "/contact-vi.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = mrk.Value;

                fsoFile = File.CreateText(strHTMLFileLocation);
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SaveHTMLInfo1()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathContact() + "/map-vi.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = mrk1.Value;

                fsoFile = File.CreateText(strHTMLFileLocation);
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void SaveHTMLInfo3()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathContact() + "/contact-banking.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = mrk3.Value;

                fsoFile = File.CreateText(strHTMLFileLocation);
                fsoFile.Write(strHTMLContent);
                fsoFile.Close();


            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        private void SaveHTMLInfo4()
        {
            try
            {
                string strHTMLFileLocation;
                string strFileName;
                string strHTMLContent;
                StreamWriter fsoFile;

                strFileName = PathFiles.GetPathContact() + "/contact-cast.htm";
                strHTMLFileLocation = Server.MapPath(strFileName);
                strHTMLContent = mrk4.Value;

                fsoFile = File.CreateText(strHTMLFileLocation);
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
                showFileHTML1();
                showFileHTML3();
                showFileHTML4();
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

                            string pathfile = Server.MapPath("/data/contact/");
                            string fullpathfile = pathfile + "/" + Path.GetFileName(hpf.FileName);
                            lpath.Add(PathFiles.GetPathContact() + Path.GetFileName(hpf.FileName));
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