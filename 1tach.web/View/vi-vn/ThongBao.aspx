<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="ThongBao.aspx.cs" Inherits="sanzo.vi_vn.ThongBao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="ltrFavicon" runat="server" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container">
  <!--Top Main-->
  <!--End Top Main -->
  <!--Main-->
  <div class="cf" id="main">
    <!--Contact-->
    <div class="box">
      <h1 class="hidden"> <asp:Literal ID="Littittle" runat="server"></asp:Literal></h1>
      <div class="tt_cate">
        <h1 class="main_cate"><span>Thông báo</span></h1>
      </div>
      <div class="box_ct whiteBg padd15">
        <!--Info Contact-->
        <div class="eight columns">
          <div id="info_contact">
          </div>
          <!--Form Contact-->
          <section class="content">
     <div class="b404">
        <div class="fright img404" style="width:30%; text-align:center"> <a href="/"><img src="/vi-vn/Images/404-img.jpg" /></a>
          <div class="clearfix"></div>
          KHÔNG TÌM THẤY TRANG BẠN CẦN TÌM<BR />
          QUAY LẠI <a href="/">Trang Chủ</a> </div>
        <div class="fleft" style="width:68%; margin-left:2%; line-height:25px">
          <h2 class="title404" style="color:#A22500; font-size:1.3em; margin-top:15px"><b>Thông báo</b></h2>
          <b>Không tìm thấy thông tin bạn cần tìm. Thông tin không tồn tại hoặc đã được gỡ bỏ khỏi hệ thống. Hãy thử những điều sau đây:</b>
          <ul style="list-style: circle">
            <li style="list-style: circle; margin-left: 20px">Hãy chắc chắn
              rằng địa chỉ trang web hiển thị trong thanh địa chỉ của trình duyệt của bạn được
              viết và định dạng một cách chính xác</li>
            <li style="list-style: circle; margin-left: 20px">Nếu bạn đã đạt
              đến trang này bằng cách nhấp chuột vào một liên kết, liên hệ với chúng tôi để thông
              báo cho chúng tôi liên kết không đúng định dạng</li>
          </ul>
        </div>
      </div>
    </section>
          <!--End Form Contact-->
        </div>
        <!--Map-->
        <!--End Map-->
      </div>
    </div>
    <!--End Contact-->
  </div>
  <!--End Main-->
</div>
</asp:Content>
