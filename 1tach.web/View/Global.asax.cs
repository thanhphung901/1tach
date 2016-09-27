﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Controller;
using vpro.functions;

namespace sanzo
{
    public class Global : System.Web.HttpApplication
    {
        #region Declare
        Config cf = new Config();
        #endregion
        void Application_Start(object sender, EventArgs e)
        {
            Application["Online"] = 0;
            Application["CusOnline"] = 0;

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            Guid _guid = System.Guid.NewGuid();
            Session["news_guid"] = _guid;
            Session["Shipping_Fee"] = 0;
            Application.Lock();
            Application["Online"] = Utils.CIntDef(Application["Online"]) + 1;
            Application.UnLock();

            cf.Addvister();
        }

        void Session_End(object sender, EventArgs e)
        {
            Guid _guid = Guid.Parse(Session["News_guid"].ToString());
            cf.Deletebasket(_guid);

            Session["Login_Email"] = null;
            Application.Lock();

            if (!string.IsNullOrEmpty(Utils.CStrDef(Session["CusId"])))
            {
                Application["CusOnline"] = Utils.CIntDef(Application["CusOnline"]) - 1;
            }
            else
            {
                Application["Online"] = Utils.CIntDef(Application["Online"]) - 1;
            }

            Application.UnLock();
        }

    }
}
