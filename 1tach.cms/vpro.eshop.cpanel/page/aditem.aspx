<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="aditem.aspx.cs" Inherits="vpro.eshop.cpanel.page.aditem" %>

<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Calendar/calendarStyle.css" rel="stylesheet" type="text/css" />
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
        <asp:LinkButton ID="lbtSave" runat="server" OnClick="lbtSave_Click" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-floppy-save" aria-hidden="true"></span>&nbsp;Lưu</asp:LinkButton>
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
    </div>
    <div class="row">
        <div class="col-md-6 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin chung</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mã</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtCode" id="txtCode" runat="server" class="form-control" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập mã"
                                Text="Vui lòng nhập mã" ControlToValidate="txtCode" CssClass="form-control" ValidationGroup="G1"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Mô tả</label>
                        <div class="col-sm-10">
                            <textarea id="txtDesc" runat="server" class="form-control"></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Liên kết</label>
                        <div class="col-sm-7">
                            <input type="text" name="txtUrl" id="txtUrl" runat="server" class="form-control" />
                        </div>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlTarget" runat="server" class="form-control">
                            </asp:DropDownList>
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
                            Vị trí</label>
                        <div class="col-sm-10">
                            <asp:RadioButtonList ID="rblAdPos" runat="server">
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-sm-2 control-label">
                            Chiều rộng</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtWidth" id="txtWidth" runat="server" onblur="this.value=formatNumeric(this.value);"
                                maxlength="4" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-sm-2 control-label">
                            Chiều cao</label>
                        <div class="col-sm-10">
                            <input type="text" name="txtHeight" id="txtHeight" runat="server" onblur="this.value=formatNumeric(this.value);"
                                maxlength="4" class="form-control" />
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
                    <div class="form-group" style="display:none;">
                        <label class="col-sm-2 control-label">
                            Ngày bắt đầu</label>
                        <div class="col-sm-10">
                            <uc1:pickerAndCalendar ID="ucFromDate" runat="server" />
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-sm-2 control-label">
                            Ngày kết thúc</label>
                        <div class="col-sm-10">
                            <uc1:pickerAndCalendar ID="ucToDate" runat="server" />
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
                            Lượt Click</label>
                        <div class="col-sm-10">
                            <asp:Label ID="lblCount" runat="server" EnableViewState="false" class="form-control"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-2 control-label">
                            Hình</label>
                        <div class="col-sm-10">
                            <div id="trUpload" runat="server">
                                <span class="btn btn-default btn-file"><span class="glyphicon glyphicon-file" aria-hidden="true">
                                </span>&nbsp;Chọn hình
                                    <input id="fileImage1" type="file" name="fileImage1" size="50" runat="server"></span>
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
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Chuyên mục</h3>
                </div>
                <div class="panel-body">
                    <table class="table table-striped">
                        <tr>
                            <td>
                                #
                            </td>
                            <td>
                                <input type="checkbox" id="toggleSelect" runat="server" onclick="javascript: ToggleAll(this,0);"
                                    style="border-top-style: none; border-right-style: none; border-left-style: none;
                                    border-bottom-style: none" name="toggleSign">
                            </td>
                            <td>
                                Phân quyền chuyên mục
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
                                        <%# Eval("CAT_NAME")%>
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
