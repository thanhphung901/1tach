<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="order_list.aspx.cs" Inherits="vpro.eshop.cpanel.page.order_list"
    EnableEventValidation="false" %>

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
    <div class="row page-header">
        <a href="orderexpress.aspx" class="btn btn-success btn-sm"><span class="glyphicon glyphicon-plus"
            aria-hidden="true"></span>&nbsp;Thêm mới</a>
        <asp:LinkButton ID="Lbprint" runat="server" class="btn btn-default btn-sm" OnClick="Lbprint_Click"><span class="glyphicon glyphicon-print" aria-hidden="true"></span>&nbsp;In đơn hàng</asp:LinkButton>
        <asp:LinkButton ID="Lbdelete" runat="server" class="btn btn-default btn-sm" OnClick="lbtDelete_Click"
            OnClientClick="return confirm('Bạn có chắc chắn xóa không?');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>&nbsp;Xóa</asp:LinkButton>
        <a href="#" onclick="javascript:document.location.reload(true);" class="btn btn-default btn-sm">
            <span class="glyphicon glyphicon-random" aria-hidden="true"></span>&nbsp;Refresh</a>
    </div>
    <div class="row page-header">
        <div class="form-inline">
            <div class="form-group">
                <input type="text" class="form-control col-sm-6" id="txtKeyword" placeholder="Từ khóa"
                    runat="server">
            </div>
            <div class="form-group">
                <asp:DropDownList ID="ddlStatus" runat="server" class="form-control col-sm-6">
                    <asp:ListItem Value="99" Text="Tất cả"></asp:ListItem>
                    <asp:ListItem Value="0" Text="Chưa xử lý"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Đang xử lý"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Đã xác nhận"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Đang giao hàng"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Giao hàng thành công"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Hủy đơn hàng"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <span class="col-sm-4">Từ ngày</span>
                <div class="col-sm-8">
                    <uc1:pickerAndCalendar ID="ucFromDate" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <span class="col-sm-4">Đến ngày</span>
                <div class="col-sm-8">
                    <uc1:pickerAndCalendar ID="ucToDate" runat="server" />
                </div>
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" class="btn btn-default"
                OnClick="btnSearch_Click" />
        </div>
    </div>
    <div class="row">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Danh sách đơn hàng</h3>
            </div>
            <div class="panel-body table-responsive">
                <asp:Repeater ID="RplistOrder" runat="server" OnItemCommand="RplistOrder_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-striped">
                            <tr>
                                <td id="count_div" runat="server">
                                    #
                                </td>
                                <td id="check_div" runat="server">
                                    <input type="checkbox" id="toggleSelect" runat="server" onclick="javascript: ToggleAll(this,0,'chkSelect');"
                                        name="toggleSign">
                                </td>
                                <td>
                                    Mã
                                </td>
                                <td>
                                    Khách hàng
                                </td>
                                <td>
                                    Điện thoại
                                </td>
                                <td>
                                    Địa chỉ
                                </td>
                                <td>
                                    Quận/Huyện
                                </td>
                                <td>
                                    Tỉnh/Thành phố
                                </td>
                                <td>
                                    Ngày đặt hàng
                                </td>
                                <td>
                                    Tổng tiền
                                </td>
                                <td>
                                    Trạng thái
                                </td>
                                <td>
                                    Thanh toán
                                </td>
                                <td>
                                    Ghi chú
                                </td>
                                <td id="delete_div" runat="server">
                                    Xóa
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td id="count_main_div" runat="server">
                                <%# getOrder() %>
                                <asp:HiddenField ID="Hdid" runat="server" Value='<%#Eval("ORDER_ID") %>' />
                            </td>
                            <td id="check_main_div" runat="server">
                                <input id="chkSelect" type="checkbox" name="chkSelect" runat="server">
                            </td>
                            <td>
                                <a href='<%# getLink(Eval("ORDER_ID"))%>'>
                                    <%# Eval("ORDER_CODE")%>
                                </a>
                            </td>
                            <td>
                                <a href='<%# getLink(Eval("ORDER_ID")) %>'>
                                    <%# Eval("ORDER_NAME")%>
                                </a>
                            </td>
                            <td>
                                <%# Eval("ORDER_PHONE")%>
                            </td>
                            <td>
                                <%# getAddress(Eval("ORDER_ADDRESS"),0)%>
                            </td>
                            <td>
                                <%# getAddress(Eval("ORDER_ADDRESS"),1)%>
                            </td>
                            <td>
                                <%# getAddress(Eval("ORDER_ADDRESS"),2)%>
                            </td>
                            <td>
                                <%# getPublishDate(Eval("ORDER_PUBLISHDATE"))%>
                            </td>
                            <td>
                                <%# GetMoney(Eval("ORDER_TOTAL_AMOUNT"))%>
                            </td>
                            <td>
                                <%# getOrderStatus(Eval("ORDER_STATUS"))%>
                            </td>
                            <td>
                                <%# getOrderPayment(Eval("ORDER_PAYMENT"))%>
                            </td>
                            <td>
                                <%# Eval("ORDER_FIELD1")%>
                            </td>
                            <td class="text-center" id="deletemain_div" runat="server">
                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%#Eval("ORDER_ID") %>'
                                    OnClientClick="return confirm('Bạn có chắc
    chắn xóa không?');"> <img src="../images/delete_icon.gif" title="Xóa" border="0">
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <ul class="pagination">
                    <asp:Literal ID="LitPage" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
