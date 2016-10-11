<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="OneTach.UIs.Footer" %>

<footer>
    <div class="container">
        <div class="row link_ft">
            <asp:Repeater ID="Ftmenu" runat="server">
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
        <div class="row row2_ft">
            <div class="logo_ft col s3">
                <asp:Repeater ID="Rplogo1" runat="server">
                    <ItemTemplate>
                        <a href="/">
                            <%# Getbanner(Eval("BANNER_TYPE"), Eval("BANNER_ID"), Eval("BANNER_FILE"),Eval("BANNER_DESC"))%>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="col s9">
                <div class="quote">
                    <q>1tach.com không phải báo điện tử, không đăng tin tức. Chúng tôi đem đến những bài viết thú vị có giá trị tri thức.
          Nếu bạn buồn hãy tìm đến 1tach.com, tôi không hứa sẽ làm cho bạn cười, nhưng chắc chắn sẽ có rất nhiều bài viết hay cho bạn đọc đến mức quên buồn.</q>
                </div>
            </div>
        </div>
    </div>
    </div>
</footer>
<div class="bgmain">
    <div class="container">
        <div class="row rowcopy">
            <div class="col s5">
                <p class="copy">© Copyright 2016. Bản quyền website thuộc về 1tach.com</p>
            </div>
            <div class="col s5 right right-align">
                <ul class="navi_ft">
                    <li><a href="#">Hướng dẫn sử dụng</a></li>
                    <li><a href="#">Tuyển dụng </a></li>
                    <li><a href="#">Liên hệ</a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
