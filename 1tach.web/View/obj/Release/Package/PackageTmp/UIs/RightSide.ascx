<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RightSide.ascx.cs" Inherits="OneTach.UIs.RightSide" %>

<div class="col l3">
    <div class="fbright">
        <img src="data/fbright.jpg" alt="">
    </div>
    <div class="adsright1 hide-on-med-and-down">
        <img src="data/adsright1.jpg" alt="">
    </div>
    <div class="adsright2 hide-on-med-and-down">
        <img src="data/adsright2.jpg" alt="">
    </div>
    <div class="post-right ">
        <asp:ListView runat="server" ID="lstRightSide">
            <ItemTemplate>
                <div class="post_item">
                    <a href="">
                        <p class="img" style="background-image: url(data/img01.jpg)"></p>
                        <h2 class="tt-post"><%#Eval("NEWS_TITLE") %></h2>
                        <p class="info_post clearfix"><span class="date"><%#Eval("NEWS_PUBLISHDATE","{0:dd/MM/yyyy}")%></span> <span class="cmm_result">71% chọn YES</span> </p>
                    </a>
                </div>
            </ItemTemplate>
        </asp:ListView>
        <!--end post-->
    </div>
</div>
