using System;
using System.Linq;
using Controller;

namespace OneTach.UIs
{
    public partial class HomeContent : System.Web.UI.UserControl
    {
        #region Declare
        Propertity per = new Propertity();
        Function fun = new Function();
        List_news lnews = new List_news();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLastestNews();
                LoadfeatureNews();
                LoadRightTab();
            }
        }

        private void LoadfeatureNews()
        {
            lstFeature.DataSource = lnews.LoadHotNewsAllType(10);
            lstFeature.DataBind();
        }

        private void LoadLastestNews()
        {
            lstNewsLastest.DataSource = lnews.LoadLastestNews(10);
            lstNewsLastest.DataBind();
        }

        private void LoadRightTab()
        {
            var lstVoteRight = lnews.Load_listnews_byTypeInNews(1);
            lstVoteRight = lstVoteRight.OrderByDescending(t => t.NEWS_PUBLISHDATE).ToList();
            lstRightVote.DataSource = lstVoteRight;
            lstRightVote.DataBind();

            var lstdebRight = lnews.Load_listnews_byTypeInNews(2);
            lstdebRight = lstdebRight.OrderByDescending(t => t.NEWS_PUBLISHDATE).ToList();
            lstDebateRight.DataSource = lstdebRight;
            lstDebateRight.DataBind();
        }
    }
}