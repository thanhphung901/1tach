<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="contact.aspx.cs" Inherits="sanzo.vi_vn.contact" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav class="container breadcrumb">
        <ul>
            <li>
                <a href="/" title="trang chủ"><i class="fa fa-home"></i>Trang chủ</a>
            </li>
            <li>
                <a href="lien-he.html" title="liên hệ">Liên hệ</a>
            </li>
        </ul>
    </nav>
    <div id="main" class="pd30">
        <!-- InstanceBeginEditable name="content" -->
        <div class="container">
            <div class="row clearfix">
                <div class="col9">
                    <div class="frm_ct clearfix">
                        <h3 class="ttmain"><b>liên hệ với chúng tôi</b></h3>
                        <input type="text" class="txtmd" id="Txtname" runat="server" placeholder="Họ và tên" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Xin vui lòng nhập họ và tên"
                            ControlToValidate="Txtname" Display="Dynamic" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
                        <input type="text" class="txtmd" id="Txtphone" runat="server" placeholder="Số điện thoại" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Xin vui lòng nhập số điện thoại"
                            ControlToValidate="Txtphone" Display="Dynamic" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
                        <input type="text" class="txtmd" id="txtEmail" runat="server" placeholder="Email" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Xin vui lòng nhập email"
                            ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
                        <textarea cols="30" rows="10" class="txtmd txtarea" id="txtContent" runat="server" placeholder="Bạn cần chúng tôi hỗ trợ gì?"></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Xin vui lòng nhập nội dung"
                            ControlToValidate="txtContent" Display="Dynamic" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
                        <input type="text" class="txtmd w-half" id="txtCapcha" runat="server" placeholder="Mã xác nhận" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Xin vui lòng nhập mã xác nhận"
                            ControlToValidate="txtCapcha" Display="Dynamic" ForeColor="Red" ValidationGroup="G40">*</asp:RequiredFieldValidator>
                        <p class="w-half fr">
                            <script type="text/javascript" language="Javascript">
                                function CatpcSent() {
                                    var img = document.getElementById("icpSent");
                                    img.src = "/vi-vn/captchr.ashx?query=" + Math.random();
                                }
                            </script>
                            <img id="icpSent" align="absmiddle" height="60" src='/vi-vn/captchr.ashx?query=<%= querys() %>' alt="Mã  an toàn" />
                            <a href="javascript:void(0)" onclick="javascript:CatpcSent();">
                                <img title="Refresh" style="vertical-align: middle; border-width: 0px" src="/vi-vn/images/reloadpaf.png" /></a>
                        </p>
                        <asp:LinkButton ID="Gui" CssClass="btn_ct fl over" runat="server" OnClick="Gui_Click" ValidationGroup="G40">Gửi</asp:LinkButton>
                        <a class="btn_ct fr over" onclick="reset();" href="javascript:void(0)"><span>Nhập lại</span></a>
                    </div>
                    <div class=" map w-full mt30">
                        <h3 class="ttmain">Bản đồ</h3>
                        <asp:Literal ID="liLoadMap" runat="server"></asp:Literal>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>
    <div style="text-align: center">
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="G40" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
            ErrorMessage="Email Định Dạng Chưa Đúng" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            ForeColor="Red" ValidationGroup="G40"></asp:RegularExpressionValidator>
        <br />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="Txtphone"
            ErrorMessage="Số điện thoại ít nhất 10 số và không khoảng trống" SetFocusOnError="True"
            ValidationExpression="^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d{9,40}$" ForeColor="Red"
            ValidationGroup="G40" CssClass="errorsval"></asp:RegularExpressionValidator>
        <asp:Label ID="lblresult" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <script type="text/javascript">
        function reset() {
            var name = document.getElementById("<%= Txtname.ClientID %>");
            var email = document.getElementById("<%= txtEmail.ClientID %>");
            var desc = document.getElementById("<%= txtContent.ClientID %>");
            var phone = document.getElementById("<%= Txtphone.ClientID %>");
            var capcha = document.getElementById("<%= txtCapcha.ClientID %>");
            name.value = email.value = desc.value = phone.value = capcha.value = "";
        }
    </script>
</asp:Content>
