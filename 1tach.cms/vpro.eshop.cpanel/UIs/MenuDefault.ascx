<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuDefault.ascx.cs"
    Inherits="vpro.eshop.cpanel.UIs.MenuDefault" %>
<style>
    .panel-green
    {
        border-color: #5cb85c;
    }
    .panel-green > .panel-heading
    {
        color: #fff;
        background-color: #5cb85c;
        border-color: #5cb85c;
    }
    .panel-green a
    {
        color: #5cb85c;
    }
    .panel-yellow
    {
        border-color: #f0ad4e;
    }
    .panel-yellow > .panel-heading
    {
        color: #fff;
        background-color: #f0ad4e;
        border-color: #f0ad4e;
    }
    .panel-yellow a
    {
        color: #f0ad4e;
    }
</style>
<div class="panel panel-primary">
    <div class="panel-heading">
        <div class="row">
            <div class="col-xs-3">
                <img src="../Images/IconReport/ic-comment.png" />
            </div>
            <div class="col-xs-9 text-right">
                <h4>
                    <asp:Label ID="Lbcountcomment" runat="server" Text=""></asp:Label></h4>
                <div>
                    Total Comment</div>
            </div>
        </div>
    </div>
    <div class="panel-body">
        <span class="pull-left"><a href="page-comment.aspx">View details</a></span> <span class="pull-right">
            <i class="glyphicon glyphicon-flash"></i></span>
    </div>
</div>
<div class="panel panel-green" style="display:none">
    <div class="panel-heading">
        <div class="row">
            <div class="col-xs-3">
                <img src="../Images/IconReport/ic-cart.png" />
            </div>
            <div class="col-xs-9 text-right">
                <h4>
                    <asp:Label ID="Lbcountcart" runat="server" Text=""></asp:Label></h4>
                <div>
                    Week order</div>
            </div>
        </div>
    </div>
    <div class="panel-body">
        <span class="pull-left"><a href="order_list.aspx">View details</a></span> <span class="pull-right">
            <i class="glyphicon glyphicon-flash"></i></span>
    </div>
</div>
<div class="panel panel-green">
    <div class="panel-heading">
        <div class="row">
            <div class="col-xs-3">
                <img src="../Images/IconReport/email.png" />
            </div>
            <div class="col-xs-9 text-right">
                <h4>
                    <asp:Label ID="Lbcountemail" runat="server" Text=""></asp:Label></h4>
                <div>
                    Email subscribe</div>
            </div>
        </div>
    </div>
    <div class="panel-body">
        <span class="pull-left"><a href="page-email-send.aspx">View details</a></span> <span class="pull-right">
            <i class="glyphicon glyphicon-flash"></i></span>
    </div>
</div>
<div class="panel panel-yellow">
    <div class="panel-heading">
        <div class="row">
            <div class="col-xs-3">
                <img src="../Images/IconReport/ic-online.png" />
            </div>
            <div class="col-xs-9 text-right">
                <h4>
                    <asp:Label ID="LbtotalVisitor" runat="server" Text=""></asp:Label></h4>
                <div>
                    Total visitor</div>
            </div>
        </div>
    </div>
    <div class="panel-body">
        <span class="pull-left"><a href="#">View details</a></span> <span class="pull-right">
            <i class="glyphicon glyphicon-flash"></i></span>
    </div>
</div>
