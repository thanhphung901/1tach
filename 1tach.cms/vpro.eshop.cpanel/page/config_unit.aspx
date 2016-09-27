<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_unit.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_unit" ValidateRequest="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Karpach.WebControls" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row page-header">
        <div class="col-sm-5">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"
                ValidationGroup="G1"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
        </div>
        <div class="col-sm-7 navbar-right">
            TẠI SAO LẠI CHỌN CHÚNG TÔI?
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Tiếng Việt</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>1. Tiêu đề lớn</label>
                        <input type="text" name="txtTitleL" id="txtTitleL" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>1. Tiêu đề nhỏ</label>
                        <input type="text" name="txtTitleN" id="txtTitleN" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>1. Mô tả</label>
                        <textarea id="txtDesc" runat="server" class="form-control" rows="5"></textarea>
                    </div>
                    <div class="form-group">
                        <label>2. Tiêu đề lớn</label>
                        <input type="text" name="txtTitleL1" id="txtTitleL1" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>2. Tiêu đề nhỏ</label>
                        <input type="text" name="txtTitleN1" id="txtTitleN1" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>2. Mô tả</label>
                        <textarea id="txtDesc1" runat="server" class="form-control" rows="5"></textarea>
                    </div>
                    <div class="form-group">
                        <label>3. Tiêu đề lớn</label>
                        <input type="text" name="txtTitleL2" id="txtTitleL2" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>3. Tiêu đề nhỏ</label>
                        <input type="text" name="txtTitleN2" id="txtTitleN2" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>3. Mô tả</label>
                        <textarea id="txtDesc2" runat="server" class="form-control" rows="5"></textarea>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Tiếng Anh</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>1. Tiêu đề lớn</label>
                        <input type="text" name="txtTitleENL" id="txtTitleENL" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>1. Tiêu đề nhỏ</label>
                        <input type="text" name="txtTitleENN" id="txtTitleENN" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>1. Mô tả</label>
                        <textarea id="txtDescENT" runat="server" class="form-control" rows="5"></textarea>
                    </div>
                    <div class="form-group">
                        <label>2. Tiêu đề lớn</label>
                        <input type="text" name="txtTitleENL1" id="txtTitleENL1" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>2. Tiêu đề nhỏ</label>
                        <input type="text" name="txtTitleENN1" id="txtTitleENN1" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>2. Mô tả</label>
                        <textarea id="txtDescEN1" runat="server" class="form-control" rows="5"></textarea>
                    </div>
                    <div class="form-group">
                        <label>3. Tiêu đề lớn</label>
                        <input type="text" name="txtTitleENL2" id="txtTitleENL2" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>3. Tiêu đề nhỏ</label>
                        <input type="text" name="txtTitleENN2" id="txtTitleENN2" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>3. Mô tả</label>
                        <textarea id="txtDescEN2" runat="server" class="form-control" rows="5"></textarea>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
