<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="config_email_list.aspx.cs" Inherits="vpro.eshop.cpanel.page.config_email_list" %>

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
        <a href="#" onclick="javascript:document.location.reload(true);" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-random" aria-hidden="true"></span>&nbsp;Refresh</a>
        <div class="navbar-right">
            Danh sách email cấu hình</div>
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
                    Danh sách email</h3>
            </div>
            <div class="panel-body table-responsive">
                <table class="table table-striped">
                    <tr>
                        <td>
                            #
                        </td>
                        <td>
                            STT
                        </td>
                        <td>
                            Mô tả
                        </td>
                        <td>
                            Chỉnh sửa
                        </td>
                    </tr>
                    <asp:Repeater ID="Rplistemail" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# getOrder() %>
                                </td>
                                <td>
                                    <a href='<%# getLink(Eval("EMAIL_ID")) %>'>
                                        <%# Eval("EMAIL_STT")%>
                                    </a>
                                </td>
                                <td>
                                    <a href='<%# getLink(Eval("EMAIL_ID")) %>'>
                                        <%# Eval("EMAIL_DESC")%>
                                    </a>
                                </td>
                                <td class="text-center">
                                    <a href='<%# getLink(Eval("EMAIL_ID")) %>'><span class="glyphicon glyphicon-pencil">
                                    </span></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
