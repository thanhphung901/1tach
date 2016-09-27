<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="category_list.aspx.cs" Inherits="vpro.eshop.cpanel.page.category_list" %>

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
        <a href="category.aspx" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-plus"
            aria-hidden="true"></span>&nbsp;Thêm mới</a>
        <asp:LinkButton ID="Lbsave" runat="server" class="btn btn-default btn-sm" OnClick="lbtSave_Click"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
        <a href="#" onclick="javascript:document.location.reload(true);" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-random" aria-hidden="true"></span>&nbsp;Refresh</a>
        <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
            OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
    </div>
    <div class="row page-header">
        <div class="form-inline">
            <div class="form-group">
                <input type="text" class="form-control col-sm-6" id="txtKeyword" placeholder="Từ khóa"
                    runat="server">
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" class="btn btn-default"
                OnClick="btnSearch_Click" />
        </div>
    </div>
    <div class="row">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Danh mục</h3>
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
                            Chuyên mục
                        </td>
                        <td>
                            Vị trí
                        </td>
                        <td>
                            Trạng thái
                        </td>
                        <td>
                            Ngôn ngữ
                        </td>
                        <td>
                            Thứ tự
                        </td>
                        <td>
                            Thứ tự trang chủ
                        </td>
                        <td>
                            Chỉnh sửa
                        </td>
                        <td>
                            Xóa
                        </td>
                    </tr>
                    <asp:Repeater ID="Rplistcate" runat="server" OnItemCommand="Rplistcate_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# getOrder() %>
                                    <asp:HiddenField ID="Hdcatid" runat="server" Value='<%#Eval("CAT_ID") %>' />
                                </td>
                                <td>
                                    <input id="chkSelect" type="checkbox" name="chkSelect" runat="server">
                                </td>
                                <td>
                                    <a href='<%# getLink(Eval("CAT_ID")) %>'>
                                        <%# Eval("CAT_NAME")%>
                                    </a>
                                </td>
                                <td>
                                    <%# getPos(Eval("CAT_POSITION"))%>
                                </td>
                                <td class="text-center">
                                    <%# getStatus(Eval("CAT_STATUS")) %>
                                </td>
                                <td>
                                    <%# getLanguage(Eval("CAT_LANGUAGE"))%>
                                </td>
                                <td>
                                    <input type="text" id="txtOrder" runat="server" maxlength="4" size="6" value='<%# Eval("CAT_ORDER") %>'
                                        onkeyup="this.value=formatNumeric(this.value);" onblur="this.value=formatNumeric(this.value);"
                                        name="txtOrder" class="form-control">
                                </td>
                                <td>
                                    <input type="text" id="txtOrderPeriod" runat="server" maxlength="4" size="6" value='<%# Eval("CAT_PERIOD_ORDER") %>'
                                        onkeyup="this.value=formatNumeric(this.value);" onblur="this.value=formatNumeric(this.value);"
                                        name="txtOrderPeriod" class="form-control">
                                </td>
                                <td class="text-center">
                                    <a href='<%# getLink(Eval("CAT_ID")) %>'><span class="glyphicon glyphicon-pencil"></span>
                                    </a>
                                </td>
                                <td class="text-center">
                                    <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%#Eval("CAT_ID") %>'
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
