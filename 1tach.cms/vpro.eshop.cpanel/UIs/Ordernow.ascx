<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ordernow.ascx.cs" Inherits="vpro.eshop.cpanel.UIs.Ordernow" %>
<div class="col-sm-6 text-success">
    <h4>
        Mã đơn hàng</h4>
</div>
<div class="col-sm-6 text-success">
    <h4>
        Khách hàng</h4>
</div>
<asp:Repeater ID="Rpordernow" runat="server">
    <ItemTemplate>
        <div class="col-sm-6">
            <a href='<%# getLink(Eval("ORDER_ID"))%>'>
                <%# Eval("ORDER_CODE")%>
            </a>
        </div>
        <div class="col-sm-6">
            <%# Eval("ORDER_NAME")%>
        </div>
    </ItemTemplate>
</asp:Repeater>
