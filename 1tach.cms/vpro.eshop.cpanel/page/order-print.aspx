<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order-print.aspx.cs" Inherits="vpro.eshop.cpanel.page.order_print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ĐƠN ĐẶT HÀNG</title>
</head>
<body onload="print();">
    <form id="form1" runat="server">
    <div style="font-family: 'Times New Roman', Times, serif; font-size: 15px; text-align: left;
        width: 800px; margin-top: -10px; margin-left: -20px;">
        <div style="margin-top: 10px; margin-left: 20px">
            <div style="width: 100%; border-bottom: 1px solid #4a4a4a; padding-bottom: 10px;">
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Image ID="Image_Logo" runat="server" />
                        </td>
                        <td>
                            <asp:Literal ID="Litinfo" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <p style="font-size: 26px; text-align: center; font-weight: bold; margin: 0; padding: 15px 0 5px 0;">
                ĐƠN ĐẶT HÀNG</p>
            <p style="text-align: center; font-style: italic; margin: 0; padding: 0 0 10px 0;">
                <asp:Label ID="Lbdate" runat="server" Text=""></asp:Label>
            </p>
            <table width="100%" border="0" cellpadding="1">
                <!--=======================THÔNG TIN NGƯỜI NHẬN HÀNG===============================-->
                <tr>
                    <td colspan="3">
                        <b>THÔNG TIN NGƯỜI NHẬN HÀNG</b>
                    </td>
                </tr>
                <tr>
                    <td width="20%" align="left">
                        - Họ và tên :
                    </td>
                    <td width="70%" align="left">
                        <asp:Label ID="Lbname" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        - Điện thoại :
                    </td>
                    <td>
                        <asp:Label ID="Lbphone" runat="server" Text=""></asp:Label>
                        - Mã đơn hàng :
                        <asp:Label ID="Lbmadonhang" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        - Địa chỉ :
                    </td>
                    <td>
                        <asp:Label ID="Lbadd" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        - Ghi chú :
                    </td>
                    <td>
                        <asp:Label ID="Lbremark" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <!--=======================DANH SÁCH SẢN PHẨM===============================-->
            <table width="100%" border="0" cellpadding="3" cellspacing="0" style="border-left: solid 1px #4a4a4a;
                border-bottom: solid 1px #4a4a4a; margin-top: 10px;">
                <tr>
                    <td style="background: #ccc; border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a;
                        text-align: center; font-weight: bold; width: 40px;">
                        STT
                    </td>
                    <td style="background: #ccc; border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a;
                        text-align: center; font-weight: bold; width: 60px;">
                        MÃ SP
                    </td>
                    <td style="background: #ccc; border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a;
                        text-align: center; font-weight: bold; height: 30px;">
                        TÊN SẢN PHẨM
                    </td>
                    <td style="background: #ccc; border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a;
                        text-align: center; font-weight: bold; width: 110px;">
                        ĐƠN GIÁ<br />
                        (VNĐ)
                    </td>
                    <td style="background: #ccc; border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a;
                        text-align: center; font-weight: bold; width: 90px;">
                        SỐ LƯỢNG
                    </td>
                    <td style="background: #ccc; border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a;
                        text-align: center; font-weight: bold; width: 140px;">
                        THÀNH TIỀN<br />
                        (VNĐ)
                    </td>
                </tr>
                <asp:Repeater ID="Rpdonhang" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: center;">
                                <%# getstt()%>
                            </td>
                            <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: center;">
                                <%# Eval("NEWS_CODE")%>
                            </td>
                            <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: left;">
                                <%# Eval("NEWS_TITLE")%>
                            </td>
                            <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: right;">
                                <%# GetMoney(Eval("ITEM_PRICE"))%>
                            </td>
                            <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: center;">
                                <%# Eval("ITEM_QUANTITY")%>
                            </td>
                            <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: right;">
                                <%# GetMoney(Eval("ITEM_SUBTOTAL"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="5" style="border-top: solid 1px #4a4a4a; text-align: right; height: 25px;">
                        <b>Giảm giá : </b>
                    </td>
                    <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: right;">
                        <b>
                            <asp:Label ID="Lbgiagiam" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="border-top: solid 1px #4a4a4a; text-align: right; height: 25px;">
                        <b>Phí vận chuyển : </b>
                    </td>
                    <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: right;">
                        <b>
                            <asp:Label ID="Lbchiphi" runat="server" Text="0"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="border-top: solid 1px #4a4a4a; text-align: right; height: 25px;">
                        <b>Tổng thanh toán : </b>
                    </td>
                    <td style="border-top: solid 1px #4a4a4a; border-right: solid 1px #4a4a4a; text-align: right;">
                        <b>
                            <asp:Label ID="lblTotal1" runat="server"></asp:Label></b>
                    </td>
                </tr>
            </table>
            <!--=======================CHỮ KÝ===============================-->
            <table width="100%" border="0" cellpadding="3" style="margin-top: 20px;">
                <tr>
                    <td width="40%" style="text-align: center">
                        <b>Người mua hàng</b>
                    </td>
                    <td width="20%">
                        &nbsp;
                    </td>
                    <td width="40%" style="text-align: center">
                        <b>Người bán hàng</b>
                    </td>
                </tr>
            </table>
            <div style="text-align: center; margin-top: 50px;">
                Quý khách vui lòng kiểm tra kỹ hàng trước khi nhận và thanh toán.Cty chỉ bảo hành
                sản phẩm do lỗi nhà sản xuất.</div>
        </div>
    </div>
    </form>
</body>
</html>
