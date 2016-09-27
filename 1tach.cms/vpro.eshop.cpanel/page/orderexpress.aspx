<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="orderexpress.aspx.cs" Inherits="vpro.eshop.cpanel.page.orderexpress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row page-header">
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
    </div>
    <div class="row">
        <div class="col-md-8 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin đơn hàng</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mã sản phẩm</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtcode" runat="server" AutoPostBack="true" OnTextChanged="txtcode_TextChanged"
                                class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên"
                                Text="Vui lòng nhập mã sản phẩm" ControlToValidate="txtcode" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                            <asp:Label ID="Lberros" runat="server" Text="" CssClass="label label-danger"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Tên sản phẩm</label>
                        <div class="col-sm-10">
                            <asp:HyperLink ID="Hynews" runat="server" ForeColor="Blue" Target="_blank" class="form-control"></asp:HyperLink>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Số lượng</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtquan" id="txtquan" runat="server" class="form-control"
                                value="1" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập số lượng"
                                Text="Vui lòng nhập số lượng" ControlToValidate="txtquan" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Thông tin khách hàng</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>
                            Tên khách hàng</label>
                        <input type="text" name="txtname" id="txtname" runat="server" class="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập tên khách hàng"
                            Text="Vui lòng nhập" ControlToValidate="txtname" CssClass="label label-danger"
                            ValidationGroup="G1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>
                            Email</label>
                        <input type="text" name="txtemail" id="txtemail" runat="server" class="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Vui lòng nhập Email"
                            Text="Vui lòng nhập Email" ControlToValidate="txtemail" CssClass="label label-danger"
                            ValidationGroup="G1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ControlToValidate="txtemail" ErrorMessage="Email không đúng định dạng" CssClass="label label-danger"
                            ValidationGroup="G1"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <label>
                            Địa chỉ</label>
                        <input type="text" name="txtadd" id="txtadd" runat="server" class="form-control" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Vui lòng nhập địa chỉ"
                            Text="Vui lòng nhập địa chỉ" ControlToValidate="txtadd" CssClass="label label-danger"
                            ValidationGroup="G1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>
                            Tỉnh/Thành phố</label>
                        <asp:DropDownList ID="Drcity" runat="server" class="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="Drcity_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Vui lòng nhập thành phố"
                            ControlToValidate="Drcity" Display="Dynamic" InitialValue="0" CssClass="label label-danger"
                            ValidationGroup="G1">Vui lòng nhập thành phố</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>
                            Quận/Huyện</label>
                        <asp:DropDownList ID="Drdistric" runat="server" class="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Vui lòng nhập quận/huyện"
                            ControlToValidate="Drdistric" Display="Dynamic" InitialValue="0" CssClass="label label-danger"
                            ValidationGroup="G1">Vui lòng nhập quận/huyện</asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <label>
                            Điện thoại</label>
                        <input type="text" name="txtphone" id="txtphone" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
