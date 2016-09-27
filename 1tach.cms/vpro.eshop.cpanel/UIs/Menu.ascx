<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="vpro.eshop.cpanel.UIs.Menu" %>
<asp:Repeater ID="Rpmenu" runat="server">
    <HeaderTemplate>
        <ul class="nav nav-sidebar">
    </HeaderTemplate>
    <ItemTemplate>
        <li data-toggle="collapse" href="#collapseMenu_<%#Eval("ID") %>" aria-expanded="false" aria-controls="collapseMenu_<%#Eval("ID") %>"><a href="<%#Eval("MENU_LINK") %>" class="showchild">
            <span class="glyphicon glyphicon-star-empty text-info"></span>
            <%#Eval("MENU_NAME") %><span class="glyphicon glyphicon-flash text-success pull-right"></span></a>
        </li>
        <ul id="collapseMenu_<%#Eval("ID") %>" class="collapse <%#getActiveMenu(Eval("ID")) %> ">
            <asp:Repeater ID="Repeater2" runat="server" DataSource='<%#loadmenuRank2(Eval("ID")) %>'>
                <ItemTemplate>
                    <li style="padding: 5px 0 5px 0"><a href="<%#Eval("MENU_LINK") %>">
                        <%#Eval("MENU_NAME") %></a></li></ItemTemplate>
            </asp:Repeater>
        </ul>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>
