<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="sanzo.UIs.Header" %>

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
                    <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Load_Menu2(Eval("Cat_ID")) %>'>
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
                    </asp:Repeater>
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
            <div class="col s8 ">
                <div class="row row-brd">
                    <div class="col col1"><a href="#!" class="breadcrumb">Bài ngẫu nhiên</a> </div>
                    <div class="col col2 mq">
                        <marquee>
            <a href="#!" ><span>&raquo;</span> Fusce aliquet non ipsum vitae scelerisque...</a> <a href="#!" ><span>&raquo;</span> Fusce aliquet non ipsum vitae scelerisque...</a>
            </marquee>
                    </div>
                </div>
            </div>
            <div class="col s1 logcol right"><a href="join.htm">Đăng ký</a><a href="login.htm">Đăng nhập</a></div>
            <div class="col s3 right">
                <div class="fr search">
                    <input type="text">
                    <a href="" class="btnsr over"></a>
                </div>
            </div>
        </div>
    </div>
</nav>
<div class="container">
    <ul class="row submenu clearfix">
        <li class="col"><a href="index.htm"><i class="material-icons">home</i>Trang chủ</a></li>
        <li class="col"><a href="#"><i class="material-icons">mode_edit</i>Chuyên gia</a></li>
        <li class="col"><a href="list_debate.htm"><i class="material-icons">question_answer</i>Tranh luận</a></li>
        <li class="col"><a href="list_vote.htm"><i class="material-icons">thumb_up</i>Bầu chọn</a></li>
        <li class="col"><a href="#"><i class="material-icons">mode_edit</i>Doanh nghiệp</a></li>
    </ul>
</div>

<!-- end breadcrumb -->
