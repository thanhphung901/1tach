<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="order.aspx.cs" Inherits="vpro.eshop.cpanel.page.order" %>

<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Calendar/calendarStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        <asp:HyperLink ID="Hyperprint" runat="server" class="btn btn-default btn-sm"> 
                <span class="glyphicon glyphicon-print" aria-hidden="true"></span>&nbsp;In đơn hàng</asp:HyperLink>
    </div>
    <div class="row">
        <div class="col-md-6 form-horizontal">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H1">
                        Thông tin đơn hàng</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Ngày đặt</label>
                        <div class="col-sm-9">
                            <input type="text" name="txtOrderDate" id="txtOrderDate" runat="server" class="form-control"
                                readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Mã đơn hàng</label>
                        <div class="col-sm-9">
                            <input type="text" name="txtOrderCode" id="txtOrderCode" runat="server" class="form-control"
                                readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Trạng thái</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlStatus" runat="server" class="form-control">
                                <asp:ListItem Value="0" Text="Chưa xử lý"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Đang xử lý"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Đã xác nhận"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Đang giao hàng"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Giao hàng thành công"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Hủy đơn hàng"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Ghi chú</label>
                        <div class="col-sm-9">
                            <textarea id="txtOrderDesc" runat="server" class="form-control"></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Ngày giao hàng</label>
                        <div class="col-sm-9">
                            <input type="text" name="txtdateDeli" id="txtdateDeli" runat="server" class="form-control"
                                readonly="readonly" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H2">
                        Thông tin khách hàng</h3>
                </div>
                <div class="panel-body">
                    <div class="form-group">
                        <label>
                            Tên</label>
                        <input type="text" name="txtName" id="txtName" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>
                            Email</label>
                        <input type="text" name="txtEmail" id="txtEmail" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>
                            Địa chỉ</label>
                        <input type="text" name="txtAddress" id="txtAddress" runat="server" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>
                            Điện thoại</label>
                        <input type="text" name="txtPhone" id="txtPhone" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title" id="H3">
                        Danh sách mặt hàng</h3>
                </div>
                <div class="panel-body table-responsive">
                    <table class="table table-striped">
                        <tr>
                            <td>
                                #
                            </td>
                            <td>
                                Mã sản phẩm
                            </td>
                            <td>
                                Sản phẩm
                            </td>
                            <td>
                                Đơn giá
                            </td>
                            <td>
                                Số lượng
                            </td>
                            <td>
                                Thành tiền
                            </td>
                            <td>
                                Xóa
                            </td>
                        </tr>
                        <asp:Repeater ID="RplistOrderitem" runat="server" OnItemCommand="RplistOrderitem_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# getOrder() %>
                                    </td>
                                    <td>
                                        <a href="<%#getLink(Eval("NEWS_SEO_URL")) %>" target="_blank">
                                            <%# Eval("NEWS_CODE")%></a>
                                    </td>
                                    <td>
                                        <a href="<%#getLink(Eval("NEWS_SEO_URL")) %>" target="_blank"><%# GetNewsTitle(Eval("NEWS_TITLE"), Eval("ITEM_FIELD1"),Eval("ITEM_FIELD2"))%></a>
                                    </td>
                                    <td>
                                        <%# GetMoney(Eval("ITEM_PRICE"))%>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="Hdid" runat="server" Value='<%# Eval("ITEM_ID") %>' />
                                        <asp:DropDownList ID="drSoLuong" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drSoLuong_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Selected="True">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <%# GetMoney(Eval("ITEM_SUBTOTAL"))%>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="Delete" CommandArgument='<%#Eval("ITEM_ID") %>'
                                            OnClientClick="return confirm('Bạn có chắc chắn xóa không?');">
                                        <img src="../images/delete_icon.gif" title="Xóa" border="0">
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <div class="form-horizontal">
                        <div class="col-sm-7">
                        </div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <label class="col-sm-5 control-label">
                                    Giá giảm :</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="txtpricegiam" runat="server" AutoPostBack="true" class="form-control"
                                        Text="0" OnTextChanged="txtpricegiam_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-5 control-label">
                                    Phí vận chuyển :</label>
                                <div class="col-sm-7">
                                    <asp:TextBox ID="Txtchiphi" runat="server" OnTextChanged="Txtchiphi_TextChanged"
                                        AutoPostBack="true" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-5 control-label">
                                    Tổng tiền:</label>
                                <div class="col-sm-7">
                                    <asp:Label ID="lblTotal_amount" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
