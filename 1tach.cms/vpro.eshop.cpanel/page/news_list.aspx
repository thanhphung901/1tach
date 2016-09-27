<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="news_list.aspx.cs" Inherits="vpro.eshop.cpanel.page.news_list" ValidateRequest="false" %>

<%@ Register Src="../UIs/Popup-Sendemail.ascx" TagName="Popup" TagPrefix="uc1" %>
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
				    
				// -->
    </script>
    <div class="row page-header">
        <asp:HyperLink ID="Hyperaddnew" runat="server" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;Thêm mới</asp:HyperLink>
        <asp:LinkButton ID="Lbsend_email" runat="server" class="btn btn-default btn-sm" OnClick="Lbsend_email_Click"><span class="glyphicon glyphicon-envelope" aria-hidden="true"></span>&nbsp;Gửi email</asp:LinkButton>
        <button type="button" class="btn btn-default btn-sm" data-toggle="modal" data-target=".bs-example-modal-lg">
            <span class="glyphicon glyphicon-envelope" aria-hidden="true"></span>&nbsp;Gửi email
            thông điệp</button>
        <a href="#" onclick="javascript:document.location.reload(true);" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-random" aria-hidden="true"></span>&nbsp;Refresh</a>
        <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="Lbdelete_Click"
            OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
        <div class="navbar-right">
            Danh sách tin tức/sản phẩm</div>
    </div>
    <div class="row page-header">
        <div class="form-inline">
            <div class="form-group">
                <input type="text" class="form-control col-sm-6" id="txtKeyword" placeholder="Từ khóa"
                    runat="server">
            </div>
            <div class="form-group">
                <asp:DropDownList ID="ddlCategory" runat="server" class="form-control col-sm-6">
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" class="btn btn-default"
                OnClick="btnSearch_Click" />
        </div>
    </div>
    <div class="row page-header table-responsive">
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
                    Tiêu đề
                </td>
                <td>
                    Bình luận
                </td>
                <td>
                    Loại tin
                </td>
                <td>
                    Trạng thái
                </td>
                <td>
                    Thứ tự
                </td>
                <td>
                    Ngày tạo
                </td>
                <td>
                    Chỉnh sửa
                </td>
                <td>
                    Xóa
                </td>
            </tr>
            <asp:Repeater ID="RpItemList" runat="server" OnItemCommand="RpItemList_ItemCommand"
                OnItemDataBound="RpItemList_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# getOrder() %>
                            <input type="hidden" value='<%# Eval("news_id") %>' id="newsid" runat="server" />
                        </td>
                        <td>
                            <input id="chkSelect" type="checkbox" name="chkSelect" runat="server">
                        </td>
                        <td>
                            <a href='<%# getLink(Eval("NEWS_ID")) %>'>
                                <%# GetNewsStatus(Eval("NEWS_ID"),Eval("NEWS_TITLE"))%>
                            </a>
                        </td>
                        <td>
                            <a href='<%# getLink_comment(Eval("NEWS_ID")) %>'>
                                <%# Getcount_comment(Eval("NEWS_ID"))%>
                            </a>
                        </td>
                        <td>
                            <%# getTypeNew(Eval("NEWS_TYPE"))%>
                        </td>
                        <td class="text-center">
                            <%# getStatus(Eval("NEWS_SHOWTYPE")) %>
                        </td>
                        <td>
                            <%# Eval("NEWS_ORDER") %>
                        </td>
                        <td>
                            <%# getDate(Eval("NEWS_PUBLISHDATE"))%>
                        </td>
                        <td class="text-center">
                            <a href='<%# getLink(Eval("NEWS_ID")) %>'><span class="glyphicon glyphicon-pencil"></span>
                            </a>
                        </td>
                        <td class="text-center">
                            <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%# Eval("news_id") %>'
                                OnClientClick="return confirm('Bạn có chắc chắn xóa không?');">
                                <img src="../images/delete_icon.gif" title="Xóa" border="0">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="10">
                    <ul class="pagination">
                        <asp:Literal ID="LitPage" runat="server"></asp:Literal>
                    </ul>
                </td>
            </tr>
        </table>
    </div>
    <uc1:Popup ID="Popup1" runat="server" />
</asp:Content>
