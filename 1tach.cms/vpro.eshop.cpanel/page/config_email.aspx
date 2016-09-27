<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_email.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_email" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row page-header col-md-12">
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm" ValidationGroup="G1"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
        <asp:HyperLink ID="Hyperback" runat="server" class="btn btn-default btn-sm"> 
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Đóng</asp:HyperLink>
    </div>
    <div class="row">
        <div class="col-md-6 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin cấu hình</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            STT</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtSTT" id="txtSTT" runat="server" class="form-control"
                                readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mô tả</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtDesc" id="txtDesc" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Gửi đến(To)</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtTo" id="txtTo" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập Email To"
                                Text="Vui lòng nhập Email To" ControlToValidate="txtTo" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="txtTo" ErrorMessage="Invalid Email Format" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Đồng gửi đến(Cc)</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtCc" id="txtCc" runat="server" class="form-control" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="txtCc" ErrorMessage="Invalid Email Format" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Gửi bản sao(Bcc)</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtBcc" id="txtBcc" runat="server" class="form-control" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ControlToValidate="txtBcc" ErrorMessage="Invalid Email Format" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        List email</h3>
                </div>
                <div class="panel-body">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
