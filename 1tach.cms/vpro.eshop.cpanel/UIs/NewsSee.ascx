<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsSee.ascx.cs" Inherits="vpro.eshop.cpanel.UIs.NewsSee" %>
<div class="col-sm-6 text-success">
    <h4>
        Tên bài viết</h4>
</div>
<div class="col-sm-6 text-success">
    <h4>
        Lượt xem</h4>
</div>
<asp:Repeater ID="Rpnewssee" runat="server">
    <ItemTemplate>
        <div class="col-sm-6">
            <a href='<%# getLink(Eval("NEWS_ID"),Eval("NEWS_TYPE"))%>'>
                <%# subTitle(Eval("NEWS_TITLE"))%>
            </a>
        </div>
        <div class="col-sm-6">
            <%# Eval("NEWS_COUNT")%>
        </div>
    </ItemTemplate>
</asp:Repeater>
