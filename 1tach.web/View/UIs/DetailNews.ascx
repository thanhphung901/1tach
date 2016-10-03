<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailNews.ascx.cs" Inherits="OneTach.UIs.DetailNews" %>

<%@ Register Src="path.ascx" TagName="path" TagPrefix="uc1" %>
<%@ Register Src="RightSide.ascx" TagName="RightSide" TagPrefix="uc2" %>

<div class="content">
    <div class="container">
        <!-- InstanceBeginEditable name="content" -->
        <div class="row">
            <div class="col l9">
                <div class="detail-news">
                    <h1>
                        <asp:Label ID="lbNewsTitle" runat="server"></asp:Label></h1>
                    <div class="top-post-dt clearfix">
                        <uc1:path ID="path1" runat="server" />
                        <div class="right">
                            <span><i class="material-icons">supervisor_account</i><a href="#"> Người đăng</a></span> <span><i class="material-icons">visibility</i>
                                <asp:Literal ID="liCount" runat="server"></asp:Literal></span> <span>
                                    <asp:Literal ID="lbDate" runat="server"></asp:Literal></span>
                        </div>
                    </div>
                    <div class="share-post clearfix">
                        <a href="">
                            <img src="/vi-vn/images/fb.png" alt=""></a> <a href="">
                                <img src="/vi-vn/images/pt.png"></a> <a href="">
                                    <img src="/vi-vn/images/lk.png"></a> <a href="">
                                        <img src="/vi-vn/images/tt.png"></a> <a href="">
                                            <img src="/vi-vn/images/tw.png"></a> <a href="">
                                                <img src="/vi-vn/images/gp.png"></a>
                    </div>
                    <asp:Literal ID="liHtml" runat="server"></asp:Literal>

                    <asp:ListView ID="lstContentNews" runat="server">
                        <LayoutTemplate>
                            <div class="block">
                                <div id="itemPlaceholder" runat="server">
                                </div>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div>
                                <asp:Panel runat="server" Visible='<%#int.Parse(Eval("Type").ToString()) == 0 %>'>
                                    <h2><%# Eval("Paragraph")%></h2>
                                </asp:Panel>
                                <asp:Panel runat="server" Visible='<%#int.Parse(Eval("Type").ToString()) == 1 %>'>
                                    <h3><%# Eval("Title")%></h3>
                                    <p class="fstlt"><%# Eval("Paragraph")%></p>
                                    <p></p>
                                </asp:Panel>
                                <asp:Panel runat="server" Visible='<%#int.Parse(Eval("Type").ToString()) == 2 %>'>
                                    <div class="center">
                                        <p class="center pst-r">
                                            <span class="imgsocial">
                                                <a href="" class="icon_share_img icon_share_img1"></a>
                                                <a href="" class="icon_share_img icon_share_img2"></a>
                                                <a href="" class="icon_share_img icon_share_img3"></a>
                                                <a href="" class="icon_share_img icon_share_img4"></a>
                                                <a href="" class="icon_share_img icon_share_img5"></a>
                                                <a href="" class="icon_share_img icon_share_img6"></a>
                                            </span>
                                            <img src='<%# Eval("Image")%>' alt="">
                                        </p>
                                    </div>
                                </asp:Panel>
                                <asp:Panel runat="server" Visible='<%#int.Parse(Eval("Type").ToString()) == 3 %>'>
                                    <div class="quote-dt">
                                        <p>
                                            <q>
                                                <%# Eval("Box")%>
                                            </q>
                                        </p>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    <asp:Panel runat="server" ID="pnForVote">
                        <div class="block">
                            <div class="detail-vote" style="margin-top: 30px; font-size: 18px">
                                <div class="short-des-dt">
                                    <asp:Literal runat="server" ID="ltr_NewsDesc"></asp:Literal>
                                </div>
                                <%--<div class="content-hide hidden" style="display: none;">
                                        <!-- begin -->
                                        Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nunc euismod purus purus, in commodo risus facilisis in. Vestibulum at enim pulvinar, feugiat metus vitae, consectetur leo. Nulla viverra enim vel eleifend molestie. 
							 
							<!-- end -->
                                    </div>
                                    <p class="center-align"><a class="waves-effect blue darken-1 btn   btn_show_dt">Xem thêm..</a> </p>--%>
                            </div>
                            <asp:ListView ID="lstVoteContent" runat="server">
                                <LayoutTemplate>
                                    <table class="tbl_vote">
                                        <tbody>
                                            <tr>
                                                <th></th>
                                                <th>
                                                    <p class="center">Vote</p>
                                                </th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <p class="center"><span class="ord"><%#Container.DisplayIndex + 1 %></span></p>
                                        </td>
                                        <td>
                                            <p class="center">
                                                <input type="checkbox" id="test5">
                                                <label for="test5"></label>
                                            </p>
                                        </td>
                                        <td>
                                            <img class="a-img" src='<%#Eval("Image") %>'>
                                            <h3 class="tth3"><%#Eval("Content") %></h3>
                                        </td>
                                        <td>
                                            <div class="progress">
                                                <div class="determinate" style="width: 70%">70%</div>
                                            </div>
                                            <div class="comm-count"><i class="material-icons">question_answer</i> <span>3 comments</span></div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>


                </div>
                <div class="share02">
                    <img src="data/share02.jpg" alt="">
                </div>
                <div class="share-post clearfix">
                    <a href="">
                        <img src="/vi-vn/images/fb.png" alt=""></a>
                    <a href="">
                        <img src="/vi-vn/images/pt.png"></a> <a href="">
                            <img src="/vi-vn/images/lk.png"></a> <a href="">
                                <img src="/vi-vn/images/tt.png"></a> <a href="">
                                    <img src="/vi-vn/images/tw.png"></a> <a href="">
                                        <img src="/vi-vn/images/gp.png"></a>
                </div>
                <div class="addbtnews">
                    <img src="data/adsbottom.jpg" alt="">
                </div>
                <div class="feelicon clearfix">
                    <div class="icon_item">
                        <p class="icon_f icon_f1"><span>Rất hữu ích</span></p>
                    </div>
                    <div class="icon_item">
                        <p class="icon_f icon_f2"><span>Yêu thích</span></p>
                    </div>
                    <div class="icon_item">
                        <p class="icon_f icon_f3"><span>Ha ha</span></p>
                    </div>
                    <div class="icon_item">
                        <p class="icon_f icon_f4"><span>Dễ thương</span></p>
                    </div>
                    <div class="icon_item">
                        <p class="icon_f icon_f5"><span>Đáng suy ngẫm</span></p>
                    </div>
                    <div class="icon_item">
                        <p class="icon_f icon_f6"><span>Thú vị</span></p>
                    </div>
                    <div class="icon_item">
                        <p class="icon_f icon_f7"><span>WOW</span></p>
                    </div>
                </div>
                <div class="fbcm">
                    <div id="fb-root"></div>
                    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v2.5";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
                    <asp:Literal ID="ltl_fanpage" runat="server"></asp:Literal>
                </div>
            </div>
            <uc2:RightSide ID="RightSide1" runat="server" />

        </div>
        <div class="orther-post" id="dvOtherNews" runat="server">
            <div class="row row_post2">
                <asp:Repeater ID="Rptinkhac" runat="server">
                    <ItemTemplate>
                        <div class="col l3 m3 s6">
                            <div class="post_item">
                                <a href="<%#GetLinkNews(Eval("NEWS_URL"),Eval("NEWS_SEO_URL"))%>" title="<%#Eval("NEWS_TITLE")%>">
                                    <p class="img" style="background-image: url(<%#GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3"))%>)"></p>
                                    <h2 class="tt-post"><%#Eval("NEWS_TITLE")%></h2>
                                    <p class="info_post clearfix"><span class="date"><%#Eval("NEWS_PUBLISHDATE","{0:dd/MM/yyyy}")%></span> <span class="cmm_result">71% chọn YES(doc ra sau)</span> </p>
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
</div>
