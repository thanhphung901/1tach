<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="news_attachment.aspx.cs" Inherits="vpro.eshop.cpanel.page.news_attachment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row page-header">
        <div class="col-sm-5">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
            <asp:HyperLink ID="Hyperback" runat="server" class="btn btn-default btn-sm"> 
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Đóng</asp:HyperLink>
        </div>
        <div class="col-sm-7 navbar-right">
            <div id="trNewsFunction" runat="server">
                <ul class="nav nav-pills">
                    <li><a href="#" id="hplCatNews" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Chuyên mục </a></li>
                    <li><a href="#" id="hplEditorHTMl" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Soạn tin </a></li>
                    <li><a href="#" id="hplNewsAtt" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;File đính kèm </a></li>
                    <li><a href="#" id="hplAlbum" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Album hình </a></li>
                    <li><a href="#" id="hplComment" runat="server"><span class="glyphicon glyphicon-pencil"
                        aria-hidden="true"></span>&nbsp;Phản hồi</a></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin file</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Tiêu đề</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtTitle" id="txtTitle" runat="server" class="form-control" />
                            <asp:Label ID="lblError" runat="server" CssClass="label label-danger" Text="Vui lòng nhập tiêu đề"
                                Visible="false" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Loại file</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlExtFiles" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Thứ tự</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtOrder" id="txtOrder" runat="server" onkeyup="this.value=formatNumeric(this.value);"
                                onblur="this.value=formatNumeric(this.value);" maxlength="4" class="form-control"
                                value="1" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Url</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtUrl" id="txtUrl" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            File</label>
                        <div class="col-sm-10">
                            <div id="trUpload" runat="server">
                                <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                                </span>&nbsp;Chọn file<input id="fileImage1" type="file" name="fileImage1" size="50" runat="server"></span>
                            </div>
                            <div id="trFile" runat="server">
                                <div class="col-sm-9 thumbnail">
                                    <asp:HyperLink runat="server" ID="hplFile" Target="_blank"></asp:HyperLink>
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
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        List file</h3>
                </div>
                <div class="panel-body">
                    <table class="table table-striped">
                        <tr>
                            <td>
                                #
                            </td>
                            <td>
                                Tên
                            </td>
                            <td>
                                Chỉnh sửa
                            </td>
                            <td>
                                Xóa
                            </td>
                        </tr>
                        <asp:Repeater ID="RplistFile" runat="server" OnItemCommand="RplistFile_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# getOrder() %>
                                    </td>
                                    <td>
                                        <a href='<%# getLink(Eval("NEWS_ATT_ID")) %>'>
                                            <%# Eval("NEWS_ATT_NAME")%>
                                        </a>
                                    </td>
                                    <td>
                                        <a href='<%# getLink(Eval("NEWS_ATT_ID")) %>'>Chỉnh sửa </a>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%#Eval("NEWS_ATT_ID") %>' OnClientClick="return confirm('Bạn có chắc chắn xóa không?');">
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
