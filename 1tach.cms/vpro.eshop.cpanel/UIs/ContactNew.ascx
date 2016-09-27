<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactNew.ascx.cs"
    Inherits="vpro.eshop.cpanel.UIs.ContactNew" %>
<div class="col-sm-4 text-success">
    <h4>
        Tên</h4>
</div>
<div class="col-sm-8 text-success">
    <h4>
        Tiêu đề</h4>
</div>
<asp:Repeater ID="RpNewContact" runat="server">
    <ItemTemplate>
        <div class="col-sm-4">
            <a href='<%# getLink(Eval("CONTACT_ID"))%>'>
                <%# (Eval("CONTACT_NAME"))%>
            </a>
        </div>
        <div class="col-sm-8">
            <%# subTitle(Eval("CONTACT_TITLE"))%>
        </div>
    </ItemTemplate>
</asp:Repeater>
