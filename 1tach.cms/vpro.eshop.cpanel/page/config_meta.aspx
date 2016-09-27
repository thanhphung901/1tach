<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_meta.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_meta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Karpach.WebControls" Namespace="Karpach.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row page-header">
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"
            ValidationGroup="G1"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
    </div>
    <div class="row">
        <div class="form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin cấu hình</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Title</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtSeoTitle" id="txtSeoTitle" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" label label-dangersage="Vui lòng nhập Seo Title"
                                Text="Vui lòng nhập Seo Title" ControlToValidate="txtSeoTitle" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Title(Tiếng anh)</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtSeoTitleEn" id="txtSeoTitleEn" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" label label-dangersage="Vui lòng nhập Seo Title tiếng anh"
                                Text="Vui lòng nhập Seo Title tiếng anh" ControlToValidate="txtSeoTitleEn" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Meta Description</label>
                        <div class="col-sm-10">
                            <textarea id="txtSeoDesc" runat="server" class="form-control"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" label label-dangersage="Vui lòng nhập Meta Description"
                                Text="Vui lòng nhập Meta Description" ControlToValidate="txtSeoDesc" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Meta Description(Tiếng anh)</label>
                        <div class="col-sm-10">
                            <textarea id="txtSeoDescEn" runat="server" class="form-control"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" label label-dangersage="Vui lòng nhập Meta Description tiếng anh"
                                Text="Vui lòng nhập Meta Description tiếng anh" ControlToValidate="txtSeoDescEn"
                                CssClass="label label-danger" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Meta Keyword</label>
                        <div class="col-sm-10">
                            <textarea id="txtSeoKeyword" runat="server" class="form-control"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" label label-dangersage="Vui lòng nhập Meta Keyword"
                                Text="Vui lòng nhập Meta Keyword" ControlToValidate="txtSeoKeyword" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Meta Keyword(Tiếng anh)</label>
                        <div class="col-sm-10">
                            <textarea id="txtSeoKeywordEn" runat="server" class="form-control"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" label label-dangersage="Vui lòng nhập Meta Keyword tiếng anh"
                                Text="Vui lòng nhập Meta Keyword tiếng anh" ControlToValidate="txtSeoKeywordEn"
                                CssClass="label label-danger" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Favicon</label>
                        <div class="col-sm-10">
                            <div id="trUpload" runat="server">
                                <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                                </span>&nbsp;Chọn file<input id="fileImage1" type="file" name="fileImage1" size="50"
                                    runat="server"></span>
                            </div>
                            <div id="trFile" runat="server">
                                <div class="col-sm-9 thumbnail">
                                    <asp:HyperLink runat="server" ID="hplFile" Target="_blank"></asp:HyperLink><br />
                                    <asp:Literal EnableViewState="false" runat="server" ID="ltrImage"></asp:Literal>
                                </div>
                                <div class="col-sm-3">
                                    <asp:ImageButton ID="btnDelete1" runat="server" ImageUrl="../images/delete_icon.gif"
                                        BorderWidth="0" Width="13px" OnClick="btnDelete1_Click" ToolTip="Xóa file đính kèm">
                                    </asp:ImageButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Nhạc nền</label>
                        <div class="col-sm-10">
                            <div id="trUploadBG" runat="server">
                                <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                                </span>&nbsp;Chọn file<input id="fileImageBG" type="file" name="fileImageBG" size="50"
                                    runat="server"></span>
                            </div>
                            <div id="trFileBG" runat="server">
                                <div class="col-sm-9 thumbnail">
                                    <asp:HyperLink runat="server" ID="hplFileBG" Target="_blank"></asp:HyperLink><br />
                                    <asp:Literal EnableViewState="false" runat="server" ID="ltrImageBG"></asp:Literal>
                                </div>
                                <div class="col-sm-3">
                                    <asp:ImageButton ID="btnDeleteBG" runat="server" ImageUrl="../images/delete_icon.gif"
                                        BorderWidth="0" Width="13px" ToolTip="Xóa file đính kèm" OnClick="btnDeleteBG_Click">
                                    </asp:ImageButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-sm-2 control-label">
                            Chọn màu hover</label>
                        <div class="col-sm-10 inputcolor">
                            <cc1:ColorPicker ID="ColorPicker1" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Lượt truy cập chung</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtvisitor" id="txtvisitor" runat="server" class="form-control" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
