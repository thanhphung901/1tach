<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="area.aspx.cs" Inherits="vpro.eshop.cpanel.page.area" %>

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
        <div class="col-md-8 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin khu vực</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Cấp cha</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlCategory" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mã</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtCode" id="txtCode" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Khu vực</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtName" id="txtName" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập tên khu vực !"
                                Text="Vui lòng nhập tên khu vực !" ControlToValidate="txtName" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mô tả</label>
                        <div class="col-sm-10">
                            <textarea id="txtDesc" rows="3" cols="5" runat="server" class="form-control" onkeyup="ParseDesc(this);"
                                onblur="ParseDesc(this);"></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Hiển thị</label>
                        <div class="col-sm-10">
                            <asp:RadioButtonList ID="rblActive" runat="server" RepeatColumns="5">
                                <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Thứ tự</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtOrder" id="txtOrder" runat="server" onblur="this.value=formatNumeric(this.value);"
                                maxlength="4" class="form-control" value="1" onkeyup="this.value=formatNumeric(this.value);" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Thông tin phí</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>
                            Phí vận chuyển</label>
                        <input type="text" name="txtShippingFee" id="txtShippingFee" runat="server" maxlength="18"
                            class="form-control" value="0" />
                    </div>
                    <div class="form-group">
                        <label>
                            Phí vận chuyển mô tả</label>
                        <textarea id="txtHomeFee" runat="server" class="form-control"></textarea>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
