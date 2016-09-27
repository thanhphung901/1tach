<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="aditem_list.aspx.cs" Inherits="vpro.eshop.cpanel.page.aditem_list" %>

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
        <a href="aditem.aspx" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-plus"
            aria-hidden="true"></span>&nbsp;Thêm mới</a> <a href="#" onclick="javascript:document.location.reload(true);"
                class="btn btn-default btn-sm"><span class="glyphicon glyphicon-random" aria-hidden="true">
                </span>&nbsp;Refresh</a>
        <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
            OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
        <div class="navbar-right">
            Danh sách banner</div>
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
                            Mã
                        </td>
                        <td>
                            Mô tả
                        </td>
                        <td class="col-md-2">
                            Ảnh minh họa
                        </td>
                        <td>
                            Vị trí
                        </td>
                        <td>
                            FileName
                        </td>
                        <td>
                            Chỉnh sửa
                        </td>
                        <td>
                            Xóa
                        </td>
                    </tr>
                    <asp:Repeater ID="Rplistaditem" runat="server" OnItemCommand="Rplistaditem_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# getOrder() %>
                                    <asp:HiddenField ID="Hdid" runat="server" Value='<%#Eval("AD_ITEM_ID") %>' />
                                </td>
                                <td>
                                    <input id="chkSelect" type="checkbox" name="chkSelect" runat="server">
                                </td>
                                <td>
                                    <a href='<%# getLink(Eval("AD_ITEM_ID")) %>'>
                                        <%# Eval("AD_ITEM_CODE")%>
                                    </a>
                                </td>
                                <td>
                                    <a href='<%# getLink(Eval("AD_ITEM_ID")) %>'>
                                        <%# Eval("AD_ITEM_DESC")%>
                                    </a>
                                </td>
                                <td class="text-center">
                                    <div class="thumbnail">
                                        <%# getImage(Eval("AD_ITEM_ID"), Eval("AD_ITEM_FILENAME"))%></div>
                                </td>
                                <td>
                                    <%# getPosition(Eval("AD_ITEM_POSITION"))%>
                                </td>
                                <td>
                                    <%# getLinkImage(Eval("AD_ITEM_ID"), Eval("AD_ITEM_FILENAME"))%>
                                </td>
                                <td class="text-center">
                                    <a href='<%# getLink(Eval("AD_ITEM_ID")) %>'><span class="glyphicon glyphicon-pencil">
                                    </span></a>
                                </td>
                                <td class="text-center">
                                    <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%#Eval("AD_ITEM_ID") %>'
                                        OnClientClick="return confirm('Bạn có chắc chắn xóa không?');">
                                <img src="../images/delete_icon.gif" title="Xóa" border="0">
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="9">
                            <ul class="pagination">
                                <asp:Literal ID="LitPage" runat="server"></asp:Literal>
                            </ul>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
