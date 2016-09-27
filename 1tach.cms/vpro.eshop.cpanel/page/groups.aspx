<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="groups.aspx.cs" Inherits="vpro.eshop.cpanel.page.groups" %>

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
    <div class="row page-header col-md-12">
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"
            ValidationGroup="G1"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
        <asp:LinkButton ID="lbtSaveNew" runat="server" OnClick="lbtSaveNew_Click" class="btn btn-default btn-sm"
            ValidationGroup="G1">
				<span class="glyphicon glyphicon-plus" aria-hidden="true"></span>&nbsp;Lưu &
            Thêm mới
        </asp:LinkButton>
        <asp:LinkButton ID="LbsaveClose" runat="server" class="btn btn-default btn-sm" OnClick="LbsaveClose_Click"
            ValidationGroup="G1">
				<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Lưu & Đóng
        </asp:LinkButton>
        <asp:HyperLink ID="Hyperback" runat="server" class="btn btn-default btn-sm"> 
                <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>&nbsp;Đóng</asp:HyperLink>
        <span id="dvDelete" runat="server">
            <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
                OnClientClick="return confirm('Bạn có chắc chắn xóa không?');" CausesValidation="false"> <span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
        </span>
    </div>
    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="form-horizontal col-md-8">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin nhóm</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mã nhóm</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtCode" id="txtCode" runat="server" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Tên nhóm</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtName" id="txtName" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tên nhóm"
                                Text="Vui lòng nhập tên nhóm" ControlToValidate="txtName" CssClass="label label-danger"
                                ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Loại nhóm</label>
                        <div class="col-sm-10">
                            <asp:RadioButtonList ID="rblType" runat="server" RepeatColumns="5">
                                <asp:ListItem Enabled="true" Selected="False" Text="Administrator" Value="1"></asp:ListItem>
                                <asp:ListItem Enabled="true" Selected="True" Text="Editor" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-2">
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Phân quyền chuyên mục</h3>
                </div>
                <div class="panel-body">
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
                        </tr>
                        <asp:Repeater ID="Rplistcate" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# getOrder() %>
                                        <asp:HiddenField ID="Hdcatid" runat="server" Value='<%#Eval("CAT_ID") %>' />
                                    </td>
                                    <td>
                                        <input id="chkSelect" type="checkbox" name="chkSelect" runat="server" checked='<%#CheckCat(Eval("CAT_ID")) %>'>
                                    </td>
                                    <td>
                                        <%#Eval("CAT_NAME") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H3">
                        Phân quyền menu</h3>
                </div>
                <div class="panel-body">
                    <table class="table table-striped">
                        <tr>
                            <td>
                                #
                            </td>
                            <td>
                                <input type="checkbox" id="Checkbox1" runat="server" onclick="javascript: ToggleAll(this,0);"
                                    name="toggleSign">
                            </td>
                            <td>
                                Menu
                            </td>
                        </tr>
                        <asp:Repeater ID="Rplistmenu" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# getOrder() %>
                                        <asp:HiddenField ID="Hdmenuid" runat="server" Value='<%#Eval("ID") %>' />
                                    </td>
                                    <td>
                                        <input id="chkSelectmenu" type="checkbox" name="chkSelect" runat="server" checked='<%#CheckMenu(Eval("ID")) %>'>
                                    </td>
                                    <td>
                                        <%#Eval("MENU_NAME")%>
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
