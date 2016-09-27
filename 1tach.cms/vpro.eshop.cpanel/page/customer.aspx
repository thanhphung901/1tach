<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="customer.aspx.cs" Inherits="vpro.eshop.cpanel.page.customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row page-header col-md-12">
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"
            ValidationGroup="G1"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
        <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click" class="btn btn-default btn-sm"
            ValidationGroup="G1">
				<span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;Lưu &
            Thêm mới
        </asp:LinkButton>
        <asp:LinkButton ID="LbsaveClose" runat="server" class="btn btn-default btn-sm" OnClick="LbsaveClose_Click"
            ValidationGroup="G1">
				<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Lưu & Đóng
        </asp:LinkButton>
        <asp:HyperLink ID="Hyperback" runat="server" class="btn btn-default btn-sm"> 
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Đóng</asp:HyperLink>
        <span id="dvDelete" runat="server">
            <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
                OnClientClick="return confirm('Bạn có chắc chắn xóa không?');" CausesValidation="false"> <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
        </span>
    </div>
    <div class="row">
        <div class="col-md-7 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin cá nhân</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Họ và tên</label>
                        <div class="col-sm-9">
                            <input type="text" name="txtCustomerName" id="txtCustomerName" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập họ tên !"
                                Text="Vui lòng nhập họ tên !" ControlToValidate="txtCustomerName" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Số điện thoại</label>
                        <div class="col-sm-9">
                            <input type="text" name="txtCustomerPhone1" id="txtCustomerPhone1" runat="server"
                                class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Vui lòng nhập số điện thoại !"
                                Text="Vui lòng nhập số điện thoại !" ControlToValidate="txtCustomerPhone1" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Địa chỉ</label>
                        <div class="col-sm-9">
                            <input type="text" name="txtaddress" id="txtaddress" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập địa chỉ !"
                                Text="Vui lòng nhập địa chỉ !" ControlToValidate="txtaddress" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Thành phố</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="Drcity" runat="server" class="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="Drcity_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập thành phố"
                                ControlToValidate="Drcity" Display="Dynamic" InitialValue="0" CssClass="label label-danger"
                                ValidationGroup="G1">Vui lòng nhập thành phố</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Quận/huyện</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="Drdistric" runat="server" class="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Vui lòng nhập quận/huyện"
                                ControlToValidate="Drdistric" Display="Dynamic" InitialValue="0" CssClass="label label-danger"
                                ValidationGroup="G1">Vui lòng nhập quận/huyện</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Giớ tính</label>
                        <div class="col-sm-9">
                            <asp:RadioButtonList ID="rblsex" runat="server" RepeatColumns="5">
                                <asp:ListItem Text="Nam" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Nữ" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Ngày sinh</label>
                        <div class="col-sm-9">
                            <div class="col-sm-4">
                                <asp:DropDownList ID="Dryear" runat="server" class="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Vui lòng nhập quận/huyện"
                                    ControlToValidate="Dryear" Display="Dynamic" InitialValue="0" CssClass="label label-danger"
                                    ValidationGroup="G1">Vui lòng nhập năm</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="Drmonth" runat="server" class="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Vui lòng nhập quận/huyện"
                                    ControlToValidate="Drmonth" Display="Dynamic" InitialValue="0" CssClass="label label-danger"
                                    ValidationGroup="G1">Vui lòng nhập tháng</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="Drday" runat="server" class="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Vui lòng nhập quận/huyện"
                                    ControlToValidate="Drday" Display="Dynamic" InitialValue="0" CssClass="label label-danger"
                                    ValidationGroup="G1">Vui lòng nhập ngày sinh</asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Thông tin đăng nhập</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>
                            Email</label>
                        <input type="text" name="txtCustomerEmail" id="txtCustomerEmail" runat="server" class="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Vui lòng nhập email !"
                            Text="Vui lòng nhập email !" ControlToValidate="txtCustomerEmail" CssClass="label label-danger"
                            ValidationGroup="G1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Email không đúng định dạng !"
                            ControlToValidate="txtCustomerEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            CssClass="label label-danger" ValidationGroup="G1"></asp:RegularExpressionValidator>
                        <asp:Label ID="lblError" runat="server" Text="" CssClass="label label-danger"></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>
                            Mật khẩu mới</label>
                        <input type="password" name="txtpassword" id="txtpassword" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
