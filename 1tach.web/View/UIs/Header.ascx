<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="OneTach.UIs.Header" %>
<div id="main-menu">
    <div class="container">
        <asp:Repeater ID="Rpmenu" runat="server">
            <HeaderTemplate>
                <ul class="menu-pc">
            </HeaderTemplate>
            <ItemTemplate>
                <li class="<%# GetStyleActive(Eval("cat_seo_url"),Eval("cat_url"))%>">
                    <a href="<%# GetLink(Eval("cat_url"),Eval("cat_seo_url")) %>" title="<%#Eval("cat_name")%>">
                        <h3><%#Eval("cat_name")%></h3>
                    </a>
                    <%--<asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Load_Menu2(Eval("Cat_ID")) %>'>
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <a title="<%#Eval("cat_name")%>" href="<%# GetLink(Eval("cat_url"),Eval("cat_seo_url")) %>">
                                    <h4><%#Eval("cat_name")%></h4>
                                </a>
                                <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# Load_Menu2(Eval("Cat_ID")) %>'>
                                    <HeaderTemplate>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a title="<%#Eval("cat_name")%>" href="<%# GetLink(Eval("cat_url"),Eval("cat_seo_url")) %>">
                                                <h5><%#Eval("cat_name")%></h5>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>--%>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul> 
            </FooterTemplate>
        </asp:Repeater>

    </div>
</div>

<!-- end nav -->
<asp:Repeater ID="Rplogo1" runat="server">
    <ItemTemplate>
        <p class="banner img-full">
            <a href="/">
                <%# Getbanner(Eval("BANNER_TYPE"), Eval("BANNER_ID"), Eval("BANNER_FILE"),Eval("BANNER_DESC"))%>
            </a>
        </p>
    </ItemTemplate>
</asp:Repeater>
<!-- end banner -->
<nav id="breadcrumb">
    <div class="container">
        <div class="row">
            <div class="col s8">
                <div class="row row-brd">
                    <div class="col col1"><a href="#!" class="breadcrumb">Bài ngẫu nhiên</a> </div>
                    <div class="col col2 mq">
                        <marquee>
                            <asp:ListView runat="server" ID="lstRandom">
                                <LayoutTemplate>
                                    <a id="itemPlaceholder" runat="server"></a>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <a href='<%#Eval("NEWS_SEO_URL") %>.html'><span>&raquo;</span><%#Eval("NEWS_TITLE") %></a>
                                </ItemTemplate>
                            </asp:ListView>
                        </marquee>
                    </div>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnNotLogined" Visible="True">
                <div class="col s1 logcol right">
                    <a href="dang-ky.html">Đăng ký</a>
                    <a href="dang-nhap.html">Đăng nhập</a>
                </div>
            </asp:Panel>
            <div class="col s3 right">
                <div class="fr search">
                    <input type="text">
                    <a href="" class="btnsr over"></a>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnLogined" Visible="False">
                <div class="col s10 right pnloged">
                    <span style="display: block">Hi,
                        <b>
                            <asp:Label runat="server" ID="lblEmail"></asp:Label>
                        </b>&nbsp;&nbsp;|&nbsp;&nbsp;
                        <a href="/dang-bai.html" class="btnDangbai"><i class="material-icons">mode_edit</i>&nbsp;Đăng bài mới</a>&nbsp;&nbsp;<%--|&nbsp;&nbsp;--%>
                        <%--<a href="#modalChangePassword" class="btnDoiMatKhau modal-trigger"><i class="material-icons">loop</i>&nbsp;Đổi mật khẩu</a>
                        &nbsp;&nbsp;|&nbsp;&nbsp;--%>
                        <asp:LinkButton runat="server" ID="btnThoat" OnClick="btnThoat_OnClick"><i class="material-icons">power_settings_new</i>&nbsp;Thoát</asp:LinkButton>
                    </span>
                </div>

            </asp:Panel>
            <asp:Panel runat="server" ID="pnDangBai" Visible="False">
                <div class="col s2 logcol right">
                </div>
            </asp:Panel>
        </div>
    </div>
</nav>

<!-- Modal Trigger -->
<%--<div id="modalChangePassword" class="modal">
    <div class="modal-content">
        <div class="row">
        <h1>Đổi mật khẩu</h1>
            <form class="col s12">
                <div class="row">
                    <div class="input-field col s12">
                        <asp:TextBox ID="txtOldPass" TextMode="Password" runat="server" />
                        <label for="<%=txtOldPass.ClientID %>" data-error="wrong" data-success="right">Mật khẩu cũ</label>
                    </div>
                    <div class="input-field col s12">
                        <asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" />
                        <label for="<%=txtNewPass.ClientID %>" data-error="wrong" data-success="right">Mật khẩu mới</label>
                    </div>
                    <div class="input-field col s12">
                        <asp:TextBox ID="txtReNewPass" TextMode="Password" runat="server" />
                        <label for="<%=txtReNewPass.ClientID %>" data-error="wrong" data-success="right">Nhập lại mật khẩu mới</label>
                    </div>
                </div>
            </form>
        </div>

    </div>
    <div class="modal-footer">
        <a href="#!" class=" modal-action modal-close waves-effect waves-green btn-flat">Agree</a>
    </div>
</div>--%>
<div class="container">
    <ul class="row submenu clearfix">
        <li class="col"><a href="/"><i class="material-icons">home</i>Trang chủ</a></li>
        <li class="col"><a href="/chuyen-gia.html"><i class="material-icons">mode_edit</i>Chuyên gia</a></li>
        <li class="col"><a href="/tranh-luan.html"><i class="material-icons">question_answer</i>Tranh luận</a></li>
        <li class="col"><a href="/bau-chon.html"><i class="material-icons">thumb_up</i>Bầu chọn</a></li>
        <li class="col"><a href="/doanh-nghiep.html"><i class="material-icons">mode_edit</i>Doanh nghiệp</a></li>

    </ul>
</div>
<!-- end breadcrumb -->
