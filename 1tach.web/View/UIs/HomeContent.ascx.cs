using System;
using System.Linq;
using Controller;
using vpro.functions;

namespace OneTach.UIs
{
    public partial class HomeContent : System.Web.UI.UserControl
    {
        #region Declare
        Propertity per = new Propertity();
        Function fun = new Function();
        Controller.Home index = new Controller.Home();
        List_news lnews = new List_news();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLastestNews();
                LoadRightTab();
                LoadContent();
                LoadSocial();
            }
        }

        #region Data
        private void LoadLastestNews()
        {
            //Bài mới nhất
            var list = lnews.LoadLastestNews(4);
            lstNewsLastest.DataSource = list;
            lstNewsLastest.DataBind();

            rptSlide.DataSource = list;
            rptSlide.DataBind();

            rptFeature.DataSource = list;
            rptFeature.DataBind();
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
        private void LoadContent()
        {

            string str = "";
            //Lấy chuyên mục hiển thị trang chủ trước
            var listCats = index.Load_cate_index(20, 1);
            int _countCats = listCats.Count;

            int _numberLoop = 0;
            if (_countCats % 4 == 0)
            {
                _numberLoop = _countCats / 4;
            }
            else
            {
                _numberLoop = (_countCats / 4) + 1;
            }

            //Lấy bài ngẫu nhiên TOP
            int _numberNewGet = (_numberLoop + 1) * 5;
            var listRandom = lnews.LoadRandom_Content(_numberNewGet);
            var listRandomTOP = listRandom.Take(5).ToList();
            if (listRandomTOP.Count > 0)
            {
                rptRandoms.DataSource = listRandomTOP;
                rptRandoms.DataBind();
            }

            for (int i = 0; i < _numberLoop; i++)
            {
                int iSkip = 4 * i;
                var listCatsCurrent = listCats.Skip(iSkip).Take(4).ToList();

                int _countCurrent = listCatsCurrent.Count;

                if(_countCurrent > 0)
                {
                    if (_countCurrent < 4)
                    {
                        str += "<div class='row'>";
                        for (int k = 0; k < _countCurrent; k++)
                        {
                            int iCatId = Utils.CIntDef(listCatsCurrent[k].CAT_ID);
                            string sCat_Name = listCatsCurrent[k].CAT_NAME;
                            var listNews = lnews.Load_listnewsByCatId(iCatId, 3);
                            int _countNew = listNews.Count;

                            str += String.Format(@"
                            <div class='col s4'>
                                <p class='ttcate2'><span>{0}</span></p>
                                <div class='post_col'>", sCat_Name);

                            for (int x = 0; x < _countNew; x++)
                            {
                                string sTitle = listNews[x].NEWS_TITLE;
                                string sLinkNew = GetLinkNew(listNews[x].NEWS_URL, listNews[x].NEWS_SEO_URL);
                                string sImgNew = GetImageT(listNews[x].NEWS_ID, listNews[x].NEWS_IMAGE3);
                                string sTypeInfo = GetInfoType1(listNews[x].NEWS_TYPE, listNews[x].DEBATE_NO, listNews[x].DEBATE_YES);
                                if (x == 0)
                                {
                                    str += String.Format(@"
                                    <div class='fist_post'>
                                        <a href='{0}' title='{2}'>
                                            <p class='img' style='background-image: url({1})'></p>
                                            <h2 class='tt-post'>{2}</h2>
                                            <p class='info_post clearfix'>
                                                {3}
                                            </p>
                                        </a>
                                    </div>", sLinkNew, sImgNew, sTitle, sTypeInfo);
                                }
                                else {
                                    if (x == 1)
                                        str += "<ul class='orther_post'>";

                                    str += String.Format(@"
                                    <li><a href='{0}' title='{2}' class='clearfix'><span class='post_left' style='background-image: url({1})'></span><span class='post_body'>
                                        <h2 class='tt-post'>{2}</h2>
                                            {3}
                                        </span></a>
                                    </li>", sLinkNew, sImgNew, sTitle, sTypeInfo);

                                    if (x == (_countNew - 1))
                                        str += "</ul>";
                                }
                            }
                            
                            str += "</div></div>";
                        }
                        str += "</div>";
                    }
                    else
                    {
                        int iCatId = Utils.CIntDef(listCatsCurrent[3].CAT_ID);
                        string sCat_Name = listCatsCurrent[3].CAT_NAME;
                        var listNews = lnews.Load_listnewsByCatId(iCatId, 3);
                        int _countNew = listNews.Count;

                        str += String.Format(@"
                            <div class='row rowpost'>
                                <div class='col s8'>
                                    <p class='ttcate2'><span>{0}</span></p>", sCat_Name);

                        str += "<div class='row'>";

                        for (int x = 0; x < _countNew; x++)
                        {
                            string sTitle = listNews[x].NEWS_TITLE;
                            string sLinkNew = GetLinkNew(listNews[x].NEWS_URL, listNews[x].NEWS_SEO_URL);
                            string sImgNew = GetImageT(listNews[x].NEWS_ID, listNews[x].NEWS_IMAGE3);
                            string sTypeInfo = GetInfoType1(listNews[x].NEWS_TYPE, listNews[x].DEBATE_NO, listNews[x].DEBATE_YES);

                            if (x == 0)
                            {
                                str += String.Format(@"
                                    <div class='col s6'>
                                        <div class='fist_post'>
                                            <a href='{0}' title='{2}'>
                                                <p class='img' style='background-image: url({1})'></p>
                                                <h2 class='tt-post'>{2}</h2>
                                                <p class='info_post clearfix'>
                                                    {3}
                                                </p>
                                            </a>
                                        </div>
                                    </div>", sLinkNew, sImgNew, sTitle, sTypeInfo);
                            }
                            else
                            {
                                if (x == 1)
                                {
                                    str += "<div class='col s6'>";
                                    str += "<ul class='orther_post'>";
                                }

                                str += String.Format(@"
                                    <li>
                                        <a href='{0}' title='{2}' class='clearfix'>
                                            <span class='post_left' style='background-image: url({1})'></span><span class='post_body'>
                                                <h2 class='tt-post'>{2}</h2>
                                                {3}
                                            </span>
                                        </a>
                                    </li>", sLinkNew, sImgNew, sTitle, sTypeInfo);

                                if (x == (_countNew - 1))
                                {
                                    str += "</ul>";
                                    str += "</div>";
                                }
                            }
                        }

                        str += "</div>";
                        str += "</div>";

                        var listSlide = per.Load_sliderByCatId(iCatId, 2, 1);
                        if (listSlide.Count > 0)
                        {
                            string sImgSlide = GetImageAd(listSlide[0].AD_ITEM_ID, listSlide[0].AD_ITEM_FILENAME);
                            str += String.Format(@"
                                <div class='col s4 center-align'>
                                    <p class='ads-gg-right'>
                                        {0}
                                    </p>
                                </div>", sImgSlide);
                        }

                        str += "</div>";
                    }
                }

                //Lấy bài ngẫu nhiên
                int iSkipRan = 5 * (i + 1);
                var listRandomCurrent = listRandom.Skip(iSkipRan).Take(5).ToList();
                int _countRandom = listRandomCurrent.Count;
                if (_countRandom > 0)
                {
                    str += "<div class='row posr'>";
                    str += "<div class='col s12'>";
                    str += "<h3 class='ttcate2'><span>Bài ngẫu nhiên</span></h3>";
                    str += "<div class='slide slide_rand'>";
                    str += "<ul class='sld_rand_2'>";

                    for (int j = 0; j < _countRandom; j++)
                    {
                        string sTitle = listRandomCurrent[j].NEWS_TITLE;
                        string sLinkNew = GetLinkNew(listRandomCurrent[j].NEWS_URL, listRandomCurrent[j].NEWS_SEO_URL);
                        string sImgNew = GetImageT(listRandomCurrent[j].NEWS_ID, listRandomCurrent[j].NEWS_IMAGE3);
                        string sTypeInfo = GetInfoType(listRandomCurrent[j].NEWS_TYPE, listRandomCurrent[j].DEBATE_NO, listRandomCurrent[j].DEBATE_YES);
                        str += String.Format(@"
                            <li>
                                <a href='{0}' title='{2}'>
                                    <p class='img' style='background-image: url({1})'></p>
                                    <h2 class='tt-pots'>{2}</h2>
                                    <p class='info_post clearfix'>
                                        {3}
                                    </p>
                                </a>
                            </li>
                            ", sLinkNew, sImgNew, sTitle, sTypeInfo);
                    }

                    str += "</ul></div></div></div>";
                }
            }
            liLoadData.Text = str;
        }
        private void LoadSocial()
        {
            var lface = per.LoadHTTT(3, 1);
            if (lface.Count > 0)
            {
                ltl_fanpage.Text = "<div class='fb-page' data-href='" + lface[0].ONLINE_NICKNAME + "' data-small-header='false' data-adapt-container-width='true' data-hide-cover='false' data-show-facepile='true'><div class='fb-xfbml-parse-ignore'><blockquote cite='" + lface[0].ONLINE_NICKNAME + "'><a href='" + lface[0].ONLINE_NICKNAME + "'>" + lface[0].ONLINE_DESC + "</a></blockquote></div></div>";
            }
        }
        #endregion

        #region function
        int iNumberSlide = -1;
        public int GetNumberSlide()
        {
            iNumberSlide = iNumberSlide + 1;
            return iNumberSlide;
        }
        public string GetLink_Cat(object Cat_Url, object Cat_Seo_Url)
        {
            try
            {
                string temp = fun.Getlink_Cat(Cat_Url, Cat_Seo_Url);
                return temp;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetLinkNew(object News_Url, object News_Seo_Url)
        {
            try
            {
                return fun.Getlink_News(News_Url, News_Seo_Url);
            }
            catch (Exception ex)
            {
                vpro.functions.clsVproErrorHandler.HandlerError(ex);
                return null;
            }
        }
        public string GetImageT(object News_Id, object News_Image1)
        {
            try
            {
                return fun.GetImageT_News(News_Id, News_Image1);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string GetImageAd(object Ad_Id, object Ad_Image1)
        {
            try
            {
                return fun.GetImageAd(Ad_Id, Ad_Image1);
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public string GetDebate(object iDebateNo, object iDebateYes)
        {
            string str = "";
            int _iDebateNo = Utils.CIntDef(iDebateNo);
            int _iDebateYes = Utils.CIntDef(iDebateYes);

            if(_iDebateNo > 0 && _iDebateYes > 0)
            {
                double dDebateNo = Utils.CDblDef(_iDebateNo);
                double dDebateYes = Utils.CDblDef(_iDebateYes);
                double dDebateTotal = dDebateNo + dDebateYes;
                double dPerYes = (dDebateYes / dDebateTotal) * 100;
                double dPerNo = 100 - dPerYes;

                if (_iDebateNo > _iDebateYes)
                {
                    str = String.Format("<p class='cmm_result txt_red'>{0}% chọn NO</p>", dPerNo);
                }
                else
                {
                    str = String.Format("<p class='cmm_result'>{0}% chọn YES</p>", dPerYes);
                }
            }
            return str;
        }
        public string GetInfoType(object NewsType, object DEBATE_NO, object DEBATE_YES)
        {
            string str = "";
            int _NewsType = Utils.CIntDef(NewsType);
            switch (_NewsType)
            {
                case 1:
                    {
                        str = "";
                        break;
                    }
                case 2:
                    {
                        double dDebateNo = Utils.CDblDef(DEBATE_NO);
                        double dDebateYes = Utils.CDblDef(DEBATE_YES);
                        double dDebateTotal = dDebateNo + dDebateYes;
                        double dPerYes = (dDebateYes / dDebateTotal) * 100;
                        double dPerNo = 100 - dPerYes;
                        if (dDebateTotal > 0)
                        {
                            if (dPerYes > dPerNo)
                            {
                                str = "<span class='cmm_result'>" + dPerYes + "% chọn YES</span>";
                            }
                            else
                            {
                                str = "<span class='cmm_result txt_red'>" + dPerNo + "% chọn YES</span>";
                            }
                        }
                        break;
                    }
                default: str = ""; break;
            }
            return str;
        }
        public string GetInfoType1(object NewsType, object DEBATE_NO, object DEBATE_YES)
        {
            string str = "";
            int _NewsType = Utils.CIntDef(NewsType);
            switch (_NewsType)
            {
                case 1:
                    {
                        str = "";
                        break;
                    }
                case 2:
                    {
                        double dDebateNo = Utils.CDblDef(DEBATE_NO);
                        double dDebateYes = Utils.CDblDef(DEBATE_YES);
                        double dDebateTotal = dDebateNo + dDebateYes;
                        double dPerYes = (dDebateYes / dDebateTotal) * 100;
                        double dPerNo = 100 - dPerYes;
                        if (dDebateTotal > 0)
                        {
                            if (dPerYes > dPerNo)
                            {
                                str = "<p class='cmm_result'>" + dPerYes + "% chọn YES</p>";
                            }
                            else
                            {
                                str = "<p class='cmm_result txt_red'>" + dPerNo + "% chọn YES</p>";
                            }
                        }
                        break;
                    }
                default: str = ""; break;
            }
            return str;
        }
        #endregion
    }
}