<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Userinfo.ascx.cs" Inherits="vpro.eshop.cpanel.UIs.Userinfo" %>
<div class="btn-group" style="margin-right: 40px !important">
    <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown"
        aria-expanded="false">
        <span class="glyphicon glyphicon-user"></span>
        <asp:Label runat="server" ID="lblUser" EnableViewState="false"></asp:Label>
        <span class="caret"></span>
    </button>
    <ul class="dropdown-menu" role="menu">
        <li><a href="user_changepass.aspx">Đổi mật khẩu</a></li>
        <li><a href="logout.aspx">Thoát</a></li>
    </ul>
</div>
