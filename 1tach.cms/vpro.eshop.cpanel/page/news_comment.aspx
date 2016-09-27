<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="news_comment.aspx.cs" Inherits="vpro.eshop.cpanel.page.news_comment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript">
				<!--
        function ToggleAll(e, action) {
            if (e.checked) {
                CheckAll();
            }
            else {
                ClearAll();
            }
        }

        function CheckAll() {
            var ml = document.forms[0];
            var len = ml.elements.length;
            for (var i = 1; i < len; i++) {
                var e = ml.elements[i];

                if (e.name.toString().indexOf("chkSelect") > 0)
                    e.checked = true;
            }
            ml.MainContent_GridItemList_toggleSelect.checked = true;
        }

        function ClearAll() {
            var ml = document.forms[0];
            var len = ml.elements.length;
            for (var i = 1; i < len; i++) {
                var e = ml.elements[i];
                if (e.name.toString().indexOf("chkSelect") > 0)
                    e.checked = false;
            }
            ml.MainContent_GridItemList_toggleSelect.checked = false;
        }

        function selectChange() {
            var radioButtons = document.getElementsByName("rblType");
            for (var x = 0; x < radioButtons.length; x++) {
                if (radioButtons[x].checked) {
                    if (radioButtons[x].value == 1)
                    { CheckAll(); }
                }
            }

        }
				    
				// -->
    </script>
    <div class="row page-header">
        <div class="col-sm-5">
            <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
            <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
            OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
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
    <div class="row page-header">
        <div class="form-inline">
            <div class="form-group">
                <input type="text" class="form-control col-sm-6" id="txtKeyword" placeholder="Từ khóa"
                    runat="server">
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" class="btn btn-default"
                OnClick="lbtSearch_Click" />
        </div>
    </div>
    <div class="row">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title" id="H1">
                    Danh sách</h3>
            </div>
            <div class="panel-body table-responsive">
                <table class="table table-striped">
                    <tr>
                        <td>
                            #
                        </td>
                        <td>
                            <input type="checkbox" id="toggleSelect" runat="server" onclick="javascript: ToggleAll(this,0);"
                                name="toggleSign">
                        </td>
                        <td>
                            Tên
                        </td>
                        <td>
                            Email
                        </td>
                        <td>
                            Nội dung
                        </td>
                        <td>
                            Ẩn/Hiện
                        </td>
                        <td>
                            Xóa
                        </td>
                    </tr>
                    <asp:Repeater ID="Rplistcomment" runat="server" 
                        onitemcommand="Rplistcomment_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# getOrder() %>
                                    <asp:HiddenField ID="Hdcommentid" runat="server" Value='<%#Eval("COMMENT_ID") %>' />
                                </td>
                                <td>
                                    <input id="chkSelect" type="checkbox" name="chkSelect" runat="server">
                                </td>
                                <td>
                                    <%# Eval("COMMENT_NAME")%>
                                </td>
                                <td>
                                    <%# Eval("COMMENT_EMAIL")%>
                                </td>
                                <td>
                                    <%# Eval("COMMENT_CONTENT")%>
                                </td>
                                <td>
                                    <input id="chkDisplay" type="checkbox" name="chkDisplay" runat="server" checked='<%# GetCheck(Eval("COMMENT_STATUS")) %>'>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%#Eval("COMMENT_ID") %>'
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
</asp:Content>
