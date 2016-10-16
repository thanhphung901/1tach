<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LikeForNews.ascx.cs" Inherits="OneTach.UIs.LikeForNews" %>

<asp:HiddenField runat="server" ID="hdNewIS" />
<asp:ScriptManager runat="server" ID="script1"></asp:ScriptManager>
<asp:UpdatePanel runat="server" ID="pnLike">
    <ContentTemplate>
        <div class="feelicon clearfix">
            <asp:LinkButton runat="server" ID="btnRatHuuIch" OnClick="btnRatHuuIch_OnClick">
        <div class="icon_item">
            <p class="icon_f icon_f1"><span>Rất hữu ích</span></p>
        </div>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnYeuThich" OnClick="btnYeuThich_OnClick">
        <div class="icon_item">
            <p class="icon_f icon_f2"><span>Yêu thích</span></p>
        </div>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnHAHA" OnClick="btnHAHA_OnClick">
        <div class="icon_item">
            <p class="icon_f icon_f3"><span>Ha ha</span></p>
        </div>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnDeThuong" OnClick="btnDeThuong_OnClick">
        <div class="icon_item">
            <p class="icon_f icon_f4"><span>Dễ thương</span></p>
        </div>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnDangSuyNgam" OnClick="btnDangSuyNgam_OnClick">
        <div class="icon_item">
            <p class="icon_f icon_f5"><span>Đáng suy ngẫm</span></p>
        </div>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnThuVi" OnClick="btnThuVi_OnClick">
        <div class="icon_item">
            <p class="icon_f icon_f6"><span>Thú vị</span></p>
        </div>
            </asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnWOW" OnClick="btnWOW_OnClick">
        <div class="icon_item">
            <p class="icon_f icon_f7"><span>WOW</span></p>
        </div>
            </asp:LinkButton>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
