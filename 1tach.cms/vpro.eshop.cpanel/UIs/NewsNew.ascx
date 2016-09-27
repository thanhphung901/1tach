<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsNew.ascx.cs" Inherits="vpro.eshop.cpanel.UIs.NewsNew" %>
<div class="col-sm-12 text-success">
    <h4>
        Tên bài viết</h4>
</div>
<asp:Repeater ID="RpnewsNew" runat="server">
    <ItemTemplate>
        <div class="col-sm-12">
            <a href='<%# getLink(Eval("NEWS_ID"),Eval("NEWS_TYPE"))%>'>
                <%# subTitle(Eval("NEWS_TITLE"))%>
            </a>
        </div>
    </ItemTemplate>
</asp:Repeater>
