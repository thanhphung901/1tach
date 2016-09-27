<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_map.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_map"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../tinymce/js/tinymce/tinymce.min.js"></script>
    <script src="../Scripts/TinymiceEditor.js" type="text/javascript"></script>
    <div class="row page-header col-md-12">
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Bản đồ</h3>
                </div>
                <div class="panel-body">
                    <textarea id="mrk" cols="20" rows="15" class="form-control" runat="server"></textarea>
                </div>
            </div>
        </div>
        <div class="col-md-3" style="display:none;">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Select images</h3>
                </div>
                <div class="panel-body">
                    <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                    </span>&nbsp;Chọn file<asp:FileUpload ID="FileUpload1" runat="server" multiple="true" /></span>
                    <asp:Button ID="Btupmulti" runat="server" Text="Upload" OnClick="Btupmulti_Click"
                        class="btn btn-success btn-sm" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
