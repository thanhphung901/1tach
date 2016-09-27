<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="vpro.eshop.cpanel.page._default" %>

<%@ Register Src="../UIs/Ordernow.ascx" TagName="Ordernow" TagPrefix="uc1" %>
<%@ Register Src="../UIs/NewsSee.ascx" TagName="NewsSee" TagPrefix="uc2" %>
<%@ Register Src="../UIs/NewsNew.ascx" TagName="NewsNew" TagPrefix="uc3" %>
<%@ Register Src="../UIs/ContactNew.ascx" TagName="ContactNew" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="category_list.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-categories.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Chuyên mục</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="news_list.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-news.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Bài viết</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="aditem_list.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-ads.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Banner</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="group_list.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-group.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Nhóm quản trị</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="config_meta.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-config.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Cấu hình</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="page-email-send.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-email.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Email</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail" style="display: none">
                <a href="order_list.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-cart.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Đơn hàng</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="user_list.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-user.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Thành viên</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="online_list.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-support.png" /></a>
                <div class="caption text-center">
                    <h4>
                        Hỗ trợ trực tuyến</h4>
                </div>
            </div>
            <div class="col-sm-3 col-md-offset-1 thumbnail">
                <a href="extensionfiles_list.aspx">
                    <img class="img-rounded" src="../Images/Icon/ic-att.png" /></a>
                <div class="caption text-center">
                    <h4>
                        File đính kèm</h4>
                </div>
            </div>
        </div>
        <div class="col-md-4 panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            <div class="panel panel-info">
                <div class="panel-heading" role="tab" id="headingTwo">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo"
                            aria-expanded="false" aria-controls="collapseTwo">Bài viết xem nhiều<span class="glyphicon glyphicon-download navbar-right"
                                aria-hidden="true" style="margin: 0"></span></a>
                    </h4>
                </div>
                <div id="collapseTwo" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingTwo">
                    <div class="panel-body text-center">
                        <uc2:NewsSee ID="NewsSee1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading" role="tab" id="headingThree">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseThree"
                            aria-expanded="false" aria-controls="collapseThree">Liên hệ mới<span class="glyphicon glyphicon-download navbar-right"
                                aria-hidden="true" style="margin: 0"></span></a>
                    </h4>
                </div>
                <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                    <div class="panel-body">
                        <uc4:ContactNew ID="ContactNew1" runat="server" />
                    </div>
                </div>
            </div>
            <div class="panel panel-info">
                <div class="panel-heading" role="tab" id="headingFour">
                    <h4 class="panel-title">
                        <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapseFour"
                            aria-expanded="false" aria-controls="collapseFour">Bài viết mới<span class="glyphicon glyphicon-download navbar-right"
                                aria-hidden="true" style="margin: 0"></span></a>
                    </h4>
                </div>
                <div id="collapseFour" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingFour">
                    <div class="panel-body text-center">
                        <uc3:NewsNew ID="NewsNew1" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
