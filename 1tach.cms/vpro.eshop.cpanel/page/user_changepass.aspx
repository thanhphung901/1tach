<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="user_changepass.aspx.cs" Inherits="vpro.eshop.cpanel.page.user_changepass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row page-header col-md-12">
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"
            ValidationGroup="G1"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
        <asp:HyperLink ID="Hyperback" runat="server" class="btn btn-default btn-sm"> 
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Đóng</asp:HyperLink>
    </div>
    <div class="row">
        <div class="col-md-8 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Đổi mật khẩu</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mật khẩu cũ</label>
                        <div class="col-sm-10">
                            <input type="password" name="txtPass" id="txtPass" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập mật khẩu cũ"
                                Text="Vui lòng nhập mật khẩu cũ" ControlToValidate="txtPass" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblError" runat="server" Text="" CssClass="label label-danger"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mật khẩu mới</label>
                        <div class="col-sm-10">
                            <input type="password" name="txtPassNew" id="txtPassNew" runat="server" class="form-control"
                                autocomplete="off" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập mật khẩu mới"
                                Text="Vui lòng nhập mật khẩu mới" ControlToValidate="txtPassNew" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Nhập lại mật khẩu mới</label>
                        <div class="col-sm-10">
                            <input type="password" name="txtRePass" id="txtRePass" runat="server" class="form-control"
                                autocomplete="off" />
                            <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtPassNew"
                                ControlToCompare="txtRePass" Operator="Equal" Type="String" ErrorMessage="Mật khẩu mới nhập không đúng!"
                                CssClass="label label-danger" ValidationGroup="G1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Thông tin user</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>
                            Họ và tên</label>
                        <asp:Label ID="Lbfullname" runat="server" Text="" class="form-control"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>
                            Tên đăng nhập</label>
                        <asp:Label ID="Lbuname" runat="server" Text="" class="form-control"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>
                            Nhóm</label>
                        <asp:Label ID="Lbgroup" runat="server" Text="" class="form-control"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
