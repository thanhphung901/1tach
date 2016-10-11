<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_banner.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_banner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row page-header col-md-12">
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
        <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click" class="btn btn-default btn-sm"
            ValidationGroup="G1">
				<span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;Lưu &
            Thêm mới
        </asp:LinkButton>
        <span id="dvDelete" runat="server">
            <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
                OnClientClick="return confirm('Bạn có chắc chắn xóa không?');" CausesValidation="false"> <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton></span>
    </div>
    <div class="row">
        <div class="col-md-7 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin cấu hình</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mô tả</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtTitle" id="txtTitle" runat="server" class="form-control" />
                            <asp:Label ID="lblError" runat="server" class="label label-danger" Text="Vui lòng nhập mô tả"
                                Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Ngôn ngữ</label>
                        <div class="col-sm-10">
                            <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatColumns="5">
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Loại</label>
                        <div class="col-sm-10">
                            <asp:RadioButtonList ID="rblBannerType" runat="server" RepeatColumns="5">
                                <asp:ListItem Selected="True" Text="Hình" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Flash" Value="1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Logo/Banner</label>
                        <div class="col-sm-10">
                            <asp:RadioButtonList ID="rblLogoBanner" runat="server" RepeatColumns="2">
                                <asp:ListItem Selected="True" Text="Logo" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Banner" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Thứ tự</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtOrder" id="txtOrder" runat="server" onblur="this.value=formatNumeric(this.value);"
                                maxlength="4" class="form-control" value="1" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            File</label>
                        <div class="col-sm-10">
                            <div id="trUpload" runat="server">
                                <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                                </span>&nbsp;Chọn file<input id="fileImage1" type="file" name="fileImage1" size="50"
                                    runat="server"></span>
                            </div>
                            <div id="trFile" runat="server">
                                <div class="col-sm-9 thumbnail">
                                    <asp:HyperLink runat="server" ID="hplFile" Target="_blank"></asp:HyperLink>
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
                </div>
            </div>
        </div>
        <div class="col-md-5">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        List banner</h3>
                </div>
                <div class="panel-body">
                    <table class="table table-striped">
                        <tr>
                            <td>
                                #
                            </td>
                            <td>
                                Mô tả
                            </td>
                            <td>
                                Banner File
                            </td>
                            <td>
                                Chỉnh s
                            </td>
                            <td>
                                Xóa
                            </td>
                        </tr>
                        <asp:Repeater ID="RplistBanner" runat="server" OnItemCommand="RplistBanner_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# getOrder() %>
                                    </td>
                                    <td>
                                        <a href='<%# getLink(Eval("BANNER_ID")) %>'>
                                            <%# Eval("BANNER_DESC")%>
                                        </a>
                                    </td>
                                    <td>
                                        <%# getLinkImage(Eval("BANNER_ID"),Eval("BANNER_FILE")) %>
                                    </td>
                                    <td>
                                        <a href='<%# getLink(Eval("BANNER_ID")) %>'>Chỉnh sửa </a>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%#Eval("BANNER_ID") %>'
                                            OnClientClick="return confirm('Bạn có chắc chắn xóa không?');">
                                <img src="../images/delete_icon.gif" title="Xóa" border="0">
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
