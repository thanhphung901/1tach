<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailDebate.ascx.cs" Inherits="OneTach.UIs.DetailDebate" %>

<%@ Register Src="path.ascx" TagName="path" TagPrefix="uc1" %>
<%@ Register Src="RightSide.ascx" TagName="RightSide" TagPrefix="uc2" %>

<div class="content">
    <div class="container">
        <div class="row">
            <div class="col l9">
                <div class=" debate">
                    <div class="top-post-dt clearfix">
                        <uc1:path ID="path1" runat="server" />
                        <div class="right"><span><i class="material-icons">supervisor_account</i><a href="#"> Người đăng</a></span> 
                            <span><i class="material-icons">visibility</i> <asp:Literal ID="liCount" runat="server"></asp:Literal></span> 
                            <span><asp:Literal ID="lbDate" runat="server"></asp:Literal></span> </div>
                    </div>
                    <div class="row">
                        <div class="col s4">
                            <p class="img-dt-debate">
                                <asp:Repeater ID="re_hinh1" runat="server">
                                    <ItemTemplate>
                                        <img src='<%# GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMG_IMAGE1")) %>'>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </p>
                            <p class="debate-bt clearfix"><a class="yes-debate" href="#frmYes"><i class="material-icons">thumb_up</i>yes</a> <a class="no-debate" href="#frmNo">no<i class="material-icons">thumb_down</i></a> <span class="icon-debate-or">or</span> </p>
                        </div>
                        <div class="col s8">
                            <h1><asp:Label ID="lbNewsTitle" runat="server"></asp:Label></h1>
                            <span class="imgsocial imgsocial2">
                                <p><b>Share</b></p>
                                <a href="" class="icon_share_img icon_share_img1"></a><a href="" class="icon_share_img icon_share_img2"></a><a href="" class="icon_share_img icon_share_img3"></a><a href="" class="icon_share_img icon_share_img4"></a><a href="" class="icon_share_img icon_share_img5"></a><a href="" class="icon_share_img icon_share_img6"></a></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="sum-debate clearfix">
                            <a class="yes-debate"><i class="material-icons">thumb_up</i><asp:Literal ID="liDebate_Yes" runat="server"></asp:Literal> Say Yes</a> <a class="no-debate"><asp:Literal ID="liDebate_No" runat="server"></asp:Literal> Say No<i class="material-icons">thumb_down</i></a>
                            <div class="sum-debate-bar">
                                <div class="progress">
                                    <asp:Literal ID="liDebate_Per" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="block detail-debate">
                        <div class="short-des-dt"><asp:Literal ID="lblDesc" runat="server"></asp:Literal></div>
                        <div class="content-hide hidden">
                            <asp:Literal ID="liHtml" runat="server"></asp:Literal>
                        </div>
                        <p class="center-align"><a class="waves-effect blue darken-1 btn   btn_show_dt">Xem thêm..</a> </p>
                    </div>
                    <div class="row arguments_box">
                        <div class="col s6">
                            <div class="inner-yes">
                                <ul class="arguments args-yes">
                                    <asp:Repeater ID="rptComments_Yes" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <p class="tt-args"><b><%# Eval("COMMENT_NAME")%></b></p>
                                                <p class="args-mg"><%# Eval("COMMENT_CONTENT")%></p>
                                                <p class="report right-align"><a href="#">Report Post</a></p>
                                                <div class="args-ft clearfix">
                                                    <div class="left"><a href="#" class="like">Thích</a> <a href="#" class="reply">Trả lời</a> </div>
                                                    <div class="right">
                                                        <a href="#"><i class="material-icons">thumb_up</i> <span><%# Eval("COMMENT_LIKE")%></span></a>
                                                        <a href="#"><i class="material-icons">question_answer</i><span><%# getCountParent(Eval("COMMENT_ID"))%></span></a>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <div class="form_args" id="frmYes">
                                    <div class="inner-frm">
                                        <p><b>Nêu ý kiến ​​của bạn</b></p>
                                        <p>Tiêu đề</p>
                                        <p>
                                            <input type="text" class="txtarg" placeholder="Tiêu đề" id="txtCmtTieuDe_Yes" runat="server"/>
                                        </p>
                                        <p>Nội dung</p>
                                        <textarea id="txtCmtNoiDung_Yes" class="txtarg" style="min-height: 100px;" runat="server" placeholder="Nội dung"></textarea>
                                        <p class="center-align btn-debate-frm">
                                            <asp:LinkButton ID="lbtnComment_Yes" runat="server" CssClass="btn yes-debate" OnClick="lbtnComment_Yes_Click"><i class="material-icons">thumb_down</i>yes</asp:LinkButton>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                        <div class="col s6">
                            <div class="inner-no">
                                <ul class="arguments args-no">
                                    <asp:Repeater ID="rptComments_No" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <p class="tt-args"><b><%# Eval("COMMENT_NAME")%></b></p>
                                                <p class="args-mg"><%# Eval("COMMENT_CONTENT")%></p>
                                                <p class="report right-align"><a href="#">Report Post</a></p>
                                                <div class="args-ft clearfix">
                                                    <div class="left"><a href="#" class="like">Thích</a> <a href="#" class="reply">Trả lời</a> </div>
                                                    <div class="right">
                                                        <a href="#"><i class="material-icons">thumb_up</i> <span><%# Eval("COMMENT_LIKE")%></span></a>
                                                        <a href="#"><i class="material-icons">question_answer</i><span><%# getCountParent(Eval("COMMENT_ID"))%></span></a>
                                                    </div>
                                                </div>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>

                                <div class="form_args" id="frmNo">
                                    <div class="inner-frm">
                                        <p><b>Nêu ý kiến ​​của bạn</b></p>
                                        <p>Tiêu đề</p>
                                        <p>
                                            <input type="text" class="txtarg" placeholder="Tiêu đề" id="txtCmtTieuDe_No" runat="server"/>
                                        </p>
                                        <p>Nội dung</p>
                                        <textarea id="txtCmtNoiDung_No" class="txtarg" style="min-height: 100px;" runat="server" placeholder="Nội dung"></textarea>
                                        <p class="center-align btn-debate-frm">
                                            <asp:LinkButton ID="lbtnComment_No" runat="server" CssClass="btn no-debate" OnClick="lbtnComment_No_Click">No<i class="material-icons">thumb_down</i></asp:LinkButton>
                                        </p>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!-- end col -->


                    </div>
                    <div class="share-post clearfix"><a href="">
                        <img src="images/fb.png" alt=""></a> <a href="">
                            <img src="images/pt.png"></a> <a href="">
                                <img src="images/lk.png"></a> <a href="">
                                    <img src="images/tt.png"></a> <a href="">
                                        <img src="images/tw.png"></a> <a href="">
                                            <img src="images/gp.png"></a> </div>
                    <div class="addbtnews">
                        <img src="data/adsbottom.jpg" alt="">
                    </div>
                    <div class="fbcm">
                        <img src="data/fbcm.jpg" alt="">
                    </div>
                </div>
            </div>
            <!-- end col9 -->
            <uc2:RightSide ID="RightSide1" runat="server" />
        </div>
        <div class="orther-post" id="dvOtherNews" runat="server">
            <div class="row row_post2">
                <asp:Repeater ID="Rptinkhac" runat="server">
                    <ItemTemplate>
                        <div class="col s3">
                            <div class="post_item">
                                <a href="<%#GetLinkNews(Eval("NEWS_URL"),Eval("NEWS_SEO_URL"))%>" title="<%#Eval("NEWS_TITLE")%>">
                                    <p class="img" style="background-image: url(<%#GetImageT(Eval("NEWS_ID"),Eval("NEWS_IMAGE3"))%>)"></p>
                                    <h2 class="tt-post"><%#Eval("NEWS_TITLE")%></h2>
                                    <p class="info_post clearfix"><span class="date"><%#Eval("NEWS_PUBLISHDATE","{0:dd/MM/yyyy}")%></span> <span class="cmm_result">71% chọn YES</span> </p>
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
