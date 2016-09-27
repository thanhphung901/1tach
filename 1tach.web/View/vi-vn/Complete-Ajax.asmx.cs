using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Model;
using Controller;

namespace sanzo.vi_vn
{
    /// <summary>
    /// Summary description for Complete_Ajax
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Complete_Ajax : System.Web.Services.WebService
    {
        Search_result search = new Search_result();
        [WebMethod]
        public List<CategoryEntityComplete> autocomplete(string searchitem)
        {
            return search.searchComplete(searchitem);
        }
    }
}
