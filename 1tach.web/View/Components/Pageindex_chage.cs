using vpro.functions;

namespace OneTach.Components
{
    public class Pageindex_chage
    {
        public string resultListproduct(int tongsotin, int sotin, string cat_seo_url, int idarea, string listid, string price, int typeprice, int _page, int type)
        {
            string _re = string.Empty;
            int kiemtradu = tongsotin % sotin;
            int _sotrang;
            if (_page == 0)
            {
                _page = 1;
            }
            if (kiemtradu != 0)
            {
                _sotrang = (tongsotin / sotin) + 1;
            }
            else
            {
                _sotrang = (tongsotin / sotin);
            }
            if (_sotrang == 1)
            {
                _re = "";
            }
            else
            {
                int s = 1;
                if (_sotrang > 7)
                {
                    if (_page >= 7 && _page < _sotrang)
                    {
                        _sotrang = _page + 1;
                        s = _page - 7 + 2;
                    }
                    else if (_page == _sotrang)
                    {
                        _sotrang = _page;
                        s = _page - 7 + 1;
                    }
                    else _sotrang = 7;
                }
                for (int i = s; i <= _sotrang; i++)
                {
                    if (_page == i)
                    {
                        _re += "<b>" + i + "</b>";
                    }
                    else
                    {
                        if (type == 2)
                        {
                            if (i == _sotrang && _page >= 7)
                            {
                                _re += "<a href='/tim-kiem.html?page=" + (_page + 1) + "&keyword=" + cat_seo_url + "&catid=" + idarea + "'> >> </a>";
                            }
                            else if (i == s && _page >= 7)
                            {
                                _re += "<a href='/tim-kiem.html?page=" + (_page - 1) + "&keyword=" + cat_seo_url + "&catid=" + idarea + "'> >> </a>";
                            }
                            else
                                _re += "<a href='/tim-kiem.html?page=" + i + "&keyword=" + cat_seo_url + "&catid=" + idarea + "'>" + i + "</a>";
                        }
                        else if (type == 1)
                        {
                            string[] a = listid.Split(',');
                            int _sortvl = Utils.CIntDef(a[0]);
                            if (i == _sotrang && _page >= 7)
                            {
                                _re += "<a href='/" + cat_seo_url + ".html?page=" + (_page + 1) + "&sort=" + _sortvl + "&price=" + price + "&typepri=" + typeprice + "'> >> </a>";
                            }
                            else if (i == s && _page >= 7)
                            {
                                _re += "<a href='/" + cat_seo_url + ".html?page=" + (_page - 1) + "&sort=" + _sortvl + "&price=" + price + "&typepri=" + typeprice + "'> << </a>";
                            }
                            else
                                _re += "<a href='/" + cat_seo_url + ".html?page=" + i + "&sort=" + _sortvl + "&price=" + price + "&typepri=" + typeprice + "'>" + i + "</a>";
                        }

                    }
                }
            }
            return _re;
        }
        public string result(int tongsotin, int sotin, string cat_seo_url, int _page, int type)
        {
            string _re = string.Empty;
            int kiemtradu = tongsotin % sotin;
            int _sotrang;
            if (_page == 0)
            {
                _page = 1;
            }
            if (kiemtradu != 0)
            {
                _sotrang = (tongsotin / sotin) + 1;
            }
            else
            {
                _sotrang = (tongsotin / sotin);
            }
            if (_sotrang == 1)
            {
                _re = "";
            }
            else
            {
                int s = 1;
                if (_sotrang > 7)
                {
                    if (_page >= 7 && _page < _sotrang)
                    {
                        _sotrang = _page + 1;
                        s = _page - 7 + 2;
                    }
                    else if (_page == _sotrang)
                    {
                        _sotrang = _page;
                        s = _page - 7 + 1;
                    }
                    else _sotrang = 7;
                }
                for (int i = s; i <= _sotrang; i++)
                {
                    if (_page == i)
                    {
                        _re += "<b>" + i + "</b>";
                    }
                    else
                    {
                        if (type == 2)
                        {
                            if (i == _sotrang && _page >= 7)
                            {
                                _re += "<a href='/tim-kiem.html?page=" + (_page + 1) + "&keyword=" + cat_seo_url + "'> >> </a>";
                            }
                            else if (i == s && _page >= 7)
                            {
                                _re += "<a href='/tim-kiem.html?page=" + (_page - 1) + "&keyword=" + cat_seo_url + "'> >> </a>";
                            }
                            else
                                _re += "<a href='/tim-kiem.html?page=" + i + "&keyword=" + cat_seo_url + "'>" + i + "</a>";
                        }
                        else if (type == 1)
                        {
                            if (i == _sotrang && _page >= 7)
                            {
                                _re += "<a href='/" + cat_seo_url + ".html?page=" + (_page + 1) + "'> >> </a>";
                            }
                            else if (i == s && _page >= 7)
                            {
                                _re += "<a href='/" + cat_seo_url + ".html?page=" + (_page - 1) + "'> << </a>";
                            }
                            else
                                _re += "<a href='/" + cat_seo_url + ".html?page=" + i + "'>" + i + "</a>";
                        }

                    }
                }
            }
            return _re;
        }
        public string result1(int tongsotin, int sotin, string cat_seo_url, int idarea, string listid, int _page, int type)
        {
            string _re = string.Empty;
            int kiemtradu = tongsotin % sotin;
            int _sotrang;
            if (_page == 0)
            {
                _page = 1;
            }
            if (kiemtradu != 0)
            {
                _sotrang = (tongsotin / sotin) + 1;
            }
            else
            {
                _sotrang = (tongsotin / sotin);
            }
            if (_sotrang == 1)
            {
                _re = "";
            }
            else
            {
                int s = 1;
                if (_sotrang > 7)
                {
                    if (_page >= 7 && _page < _sotrang)
                    {
                        _sotrang = _page + 1;
                        s = _page - 7 + 2;
                    }
                    else if (_page == _sotrang)
                    {
                        _sotrang = _page;
                        s = _page - 7 + 1;
                    }
                    else _sotrang = 7;
                }
                for (int i = s; i <= _sotrang; i++)
                {
                    if (_page == i)
                    {
                        _re += "<b>" + i + "</b>";
                    }
                    else
                    {
                        if (type == 2)
                        {
                            if (i == _sotrang && _page >= 7)
                            {
                                _re += "<a href='/tim-kiem.html?page=" + (_page + 1) + "&keyword=" + cat_seo_url + "&catid=" + idarea + "'> >> </a>";
                            }
                            else if (i == s && _page >= 7)
                            {
                                _re += "<a href='/tim-kiem.html?page=" + (_page - 1) + "&keyword=" + cat_seo_url + "&catid=" + idarea + "'> >> </a>";
                            }
                            else
                                _re += "<a href='/tim-kiem.html?page=" + i + "&keyword=" + cat_seo_url + "&catid=" + idarea + "'>" + i + "</a>";
                        }
                        else if (type == 1)
                        {
                            string[] a = listid.Split(',');
                            int _sortvl = Utils.CIntDef(a[0]);
                            if (i == _sotrang && _page >= 7)
                            {
                                _re += "<a href='/" + cat_seo_url + ".html?code=&page=" + (_page + 1) + "&sort=" + _sortvl + "'> >> </a>";
                            }
                            else if (i == s && _page >= 7)
                            {
                                _re += "<a href='/" + cat_seo_url + ".html?code=&page=" + (_page - 1) + "&sort=" + _sortvl + "'> << </a>";
                            }
                            else
                                _re += "<a href='/" + cat_seo_url + ".html?code=&page=" + i + "&sort=" + _sortvl + "'>" + i + "</a>";
                        }

                    }
                }
            }
            return _re;
        }

    }
}