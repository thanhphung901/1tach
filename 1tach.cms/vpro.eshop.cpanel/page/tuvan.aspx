<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="tuvan.aspx.cs" Inherits="vpro.eshop.cpanel.page.tuvan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row page-header col-md-12">
        <asp:HyperLink ID="Hyperback" runat="server" class="btn btn-default btn-sm"> 
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Đóng</asp:HyperLink>
        <span id="dvDelete" runat="server">
            <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
                OnClientClick="return confirm('Bạn có chắc chắn xóa không?');" CausesValidation="false"> <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
        </span>
    </div>
    <div class="row">
        <div class="col-md-10 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin liên hệ</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Tên khách hàng</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtName" id="txtName" runat="server" class="form-control"/>

                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Email</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtEmail" id="txtEmail" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            SĐT</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtPhone" id="txtPhone" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Loại công trình</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtLoaiCT" id="txtLoaiCT" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Tên dự án</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtTenDuAn" id="txtTenDuAn" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Diện tích nhà</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtDienTichNha" id="txtDienTichNha" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Thời gian thi công</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtThoiGianThiCong" id="txtThoiGianThiCong" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Kinh phí dự kiến</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtKinhPhi" id="txtKinhPhi" runat="server" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
