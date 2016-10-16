<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeContent.ascx.cs" Inherits="OneTach.UIs.HomeContent" %>

<div class="content">
    <div class="container">
        <div class="row">
            <div class="col s5">
                <div class="slide-post">
                    <div class="slidemain">
                        <ul class="slide1">
                            <asp:Repeater ID="rptSlide" runat="server">
                                <ItemTemplate>
                                    <li style="background-image: url(<%#GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3"))%>)">
                                        <div class="innerli">
                                            <h3 class="tt-post"><a href="<%# GetLinkNew(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>"><%#Eval("NEWS_TITLE") %></a> </h3>
                                            <p class="des-post"><%#Eval("NEWS_DESC") %></p>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div id="thumslide" class="clearfix">
                        <asp:Repeater ID="rptFeature" runat="server">
                            <ItemTemplate>
                                <a data-slide-index="<%# GetNumberSlide()%>" href="">
                                    <span class="innerp">
                                        <p class="img" style="background-image: url(<%#GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3"))%>)"></p>
                                    </span>
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- end thumslide -->
                </div>
            </div>
            <div class="col s3">
                <h3 class="ttcate">Bài mới</h3>
                <ul class="new_post">
                    <asp:ListView runat="server" ID="lstNewsLastest">
                        <LayoutTemplate>
                            <li id="itemPlaceholder" runat="server"></li>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li><a href='<%# GetLinkNew(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>' title="<%#Eval("NEWS_TITLE") %>"><%#Eval("NEWS_TITLE") %></a></li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
            <div class="col s4">
                <div class="tabGroup">
                    <input type="radio" name="tabGroup1" id="-tab1" class="tab1" checked="checked">
                    <label for="-tab1" class="first"><i class="material-icons">thumb_up</i>Bầu chọn</label>
                    <input type="radio" name="tabGroup1" id="-tab2" class="tab2">
                    <label for="-tab2"><i class="material-icons">question_answer</i>Tranh luận</label>
                    <div class="tab_index tab1 slide_com slide">
                        <div class="inner_tab">
                            <ul class="slide_comment">
                                <asp:ListView runat="server" ID="lstRightVote">
                                    <LayoutTemplate>
                                        <li id="itemPlaceholder" runat="server"></li>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a href="<%# GetLinkNew(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>" class="clearfix" title="<%#Eval("NEWS_TITLE") %>">
                                                <span class="cmm_img"><img src="<%#GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3"))%>" alt="<%#Eval("NEWS_TITLE") %>"/></span>
                                                <span class="cmm_body">
                                                    <h2><%#Eval("NEWS_TITLE") %></h2>
                                                    <p class="cmm_date"></p>
                                                    
                                                </span>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                    <div class="tab_index tab2">
                        <div class="inner_tab slide_com slide">
                            <ul class="slide_comment ">
                                <asp:ListView runat="server" ID="lstDebateRight">
                                    <LayoutTemplate>
                                        <li id="itemPlaceholder" runat="server"></li>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a href="<%# GetLinkNew(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>" class="clearfix" title="<%#Eval("NEWS_TITLE") %>">
                                                <span class="cmm_img"><img src="<%#GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3"))%>" alt="<%#Eval("NEWS_TITLE") %>"/></span>
                                                <span class="cmm_body">
                                                    <h2><%#Eval("NEWS_TITLE") %></h2>
                                                    <p class="cmm_date"></p>
                                                    <%# GetDebate(Eval("DEBATE_NO"),Eval("DEBATE_YES"))%>
                                                </span>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col s8 posr">
                <h3 class="ttcate2"><span>Bài ngẫu nhiên</span></h3>
                <div class="slide slide_rand">
                    <ul class="sld_rand">
                        <asp:Repeater ID="rptRandoms" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href="<%# GetLinkNew(Eval("NEWS_URL"),Eval("NEWS_SEO_URL")) %>" title="<%#Eval("NEWS_TITLE") %>">
                                        <p class="img" style="background-image: url(<%#GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3"))%>)"></p>
                                        <h2 class="tt-pots"><%#Eval("NEWS_TITLE") %></h2>
                                        <p class="info_post clearfix">
                                            <%# GetInfoType(Eval("NEWS_TYPE"),Eval("DEBATE_NO"),Eval("DEBATE_YES"))%>
                                        </p>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="col s4">
                <p class="ttcate2"><span>Fanpage</span></p>
                <div class="fanpage">
                    <div id="fb-root"></div>
                    <script>
                        (function (d, s, id) {
                            var js, fjs = d.getElementsByTagName(s)[0];
                            if (d.getElementById(id)) return;
                            js = d.createElement(s); js.id = id;
                            js.src = "//connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.5";
                            fjs.parentNode.insertBefore(js, fjs);
                        }(document, 'script', 'facebook-jssdk'));
                    </script>
                    <asp:Literal ID="ltl_fanpage" runat="server"></asp:Literal>
                </div>
            </div>
        </div>

        <asp:Literal ID="liLoadData" runat="server"></asp:Literal>
    </div>
</div>
