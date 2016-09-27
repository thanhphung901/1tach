<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="contact_list.aspx.cs" Inherits="vpro.eshop.cpanel.page.contact_list" %>

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
        <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
            OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
        <a href="#" onclick="javascript:document.location.reload(true);" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-random" aria-hidden="true"></span>&nbsp;Refresh</a>
        <div class="navbar-right">
            Danh sách liên hệ</div>
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
                    Danh sách liên hệ</h3>
            </div>
            <div class="panel-body table-responsive">
                <table class="table table-striped">
                    <tr>
                        <td>
                            #
                        </td>
                        <td>
                            <input type="checkbox" id="toggleSelect" runat="server" onclick="javascript: ToggleAll(this,0,'chkSelect');"
                                name="toggleSign">
                        </td>
                        <td>
                            Tên
                        </td>
                        <td>
                            Tiêu đề
                        </td>
                        <td>
                            Địa chỉ
                        </td>
                        <td>
                            Email
                        </td>
                        <td>
                            Số điện thoại
                        </td>
                        <td>
                            Nội dung
                        </td>
                         <td>
                            Ngày
                        </td>
                        <td>
                            Chỉnh sửa
                        </td>
                        <td>
                            Xóa
                        </td>
                    </tr>
                    <asp:Repeater ID="RplistContact" runat="server" OnItemCommand="RplistContact_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# getOrder() %>
                                    <asp:HiddenField ID="Hdid" runat="server" Value='<%#Eval("CONTACT_ID") %>' />
                                </td>
                                <td>
                                    <input id="chkSelect" type="checkbox" name="chkSelect" runat="server">
                                </td>
                                <td>
                                    <a href='<%# getLink(Eval("CONTACT_ID")) %>'>
                                        <%# Eval("CONTACT_NAME")%>
                                    </a>
                                </td>
                                <td>
                                    <%# Eval("CONTACT_TITLE")%>
                                </td>
                                <td>
                                    <%# Eval("CONTACT_ADDRESS")%>
                                </td>
                                <td>
                                    <a href='<%# getLink(Eval("CONTACT_ID")) %>'>
                                        <%# Eval("CONTACT_EMAIL")%>
                                    </a>
                                </td>
                                <td>
                                    <%# Eval("CONTACT_PHONE")%>
                                </td>
                                <td>
                                    <%# Eval("CONTACT_CONTENT")%>
                                </td>
                                <td>
                                <%#formatdate(Eval("CONTACT_PUBLISHDATE")) %>
                                </td>
                                <td class="text-center">
                                    <a href='<%# getLink(Eval("CONTACT_ID")) %>'><span class="glyphicon glyphicon-pencil">
                                    </span></a>
                                </td>
                                <td class="text-center">
                                    <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%#Eval("CONTACT_ID") %>'
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
        </div>
    </div>
</asp:Content>
