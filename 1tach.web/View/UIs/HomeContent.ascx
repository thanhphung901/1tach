<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeContent.ascx.cs" Inherits="OneTach.UIs.HomeContent" %>

<div class="content">
    <div class="container">
        <div class="row">
            <div class="col s5">
                <div class="slide-post">
                    <div class="slidemain">
                        <ul class="slide1">
                            <asp:ListView runat="server" ID="lstFeature">
                                <LayoutTemplate>
                                    <li id="itemPlaceholder" runat="server"></li>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li data-bg='<%#Eval("NEWS_IMAGE1") %>'>
                                        <div class="innerli">
                                            <h3 class="tt-post"><a href='<%#Eval("NEWS_SEO_URL") %>.html'><%#Eval("NEWS_TITLE") %></a></h3>
                                            <p class="des-post"><%#Eval("NEWS_DESC") %></p>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                        </ul>
                    </div>
                    <div id="thumslide" class="clearfix">
                        <a data-slide-index="0" href=""><span class="innerp">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                        </span></a><a data-slide-index="1" href=""><span class="innerp">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                        </span></a><a data-slide-index="2" href=""><span class="innerp">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                        </span></a><a data-slide-index="3" href=""><span class="innerp">
                            <p class="img" style="background-image: url(data/img04.jpg)"></p>
                        </span></a>
                    </div>
                    <!-- end thumslide -->
                </div>
            </div>
            <div class="col s3">
                <h3 class="ttcate">Bài mới</h3>
                <ul class="new_post">
                    <asp:ListView runat="server" ID="lstNewsLastest">
                        <LayoutTemplate>
                            <li id="itemPlaceholder" runat="server"></li>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li><a href='<%#Eval("NEWS_SEO_URL") %>.html'><%#Eval("NEWS_TITLE") %></a></li>
                        </ItemTemplate>
                    </asp:ListView>
                </ul>
            </div>
            <div class="col s4">
                <div class="tabGroup">
                    <input type="radio" name="tabGroup1" id="-tab1" class="tab1" checked="checked">
                    <label for="-tab1" class="first"><i class="material-icons">thumb_up</i>Bầu chọn</label>
                    <input type="radio" name="tabGroup1" id="-tab2" class="tab2">
                    <label for="-tab2"><i class="material-icons">question_answer</i>Tranh luận</label>
                    <div class="tab_index tab1 slide_com slide">
                        <div class="inner_tab">
                            <ul class="slide_comment">
                                <asp:ListView runat="server" ID="lstRightVote">
                                    <LayoutTemplate>
                                        <li id="itemPlaceholder" runat="server"></li>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a href="" class="clearfix"><span class="cmm_img"></span>
                                                <span class="cmm_body">
                                                    <h2><%#Eval("NEWS_TITLE") %></h2>
                                                    <p class="cmm_date">Updated: 23 Hours ago</p>
                                                    <p class="cmm_result">60% chọn YES</p>
                                                </span>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                    <div class="tab_index tab2">
                        <div class="inner_tab slide_com slide">
                            <ul class="slide_comment ">
                                <asp:ListView runat="server" ID="lstDebateRight">
                                    <LayoutTemplate>
                                        <li id="itemPlaceholder" runat="server"></li>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <a href="" class="clearfix">
                                                <span class="cmm_img"></span>
                                                <span class="cmm_body">
                                                    <h2><%#Eval("NEWS_TITLE") %></h2>
                                                    <p class="cmm_date">Updated: 23 Hours ago</p>
                                                    <p class="cmm_result">60% chọn YES</p>
                                                </span>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col s8 posr">
                <h3 class="ttcate2"><span>Bài ngẫu nhiên</span></h3>
                <div class="slide slide_rand">
                    <ul class="sld_rand">
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Cáp quang bảo trì Internet đi quốc tế bị ảnh hưởng..</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img04.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore..</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                    </ul>
                </div>
            </div>
            <div class="col s4">
                <p class="ttcate2"><span>Fanpage</span></p>
                <div class="fanpage">
                    <img src="data/fanpage.jpg" alt="Like fanpage">
                </div>
            </div>
        </div>
        <!-- end row -->
        <!--3 lg row-->
        <div class="row">
            <div class="col s4">
                <p class="ttcate2"><span>Kiến thức kinh doanh</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-post">Chiến thắng của ông Donald Trump trong cuộc bầu cử tổng thống Mỹ có thể làm thay đổi nền kinh tế Trung Quốc..</h2>
                            <p class="info_post clearfix">
                                <span class="date">Jun 11, 2015</span> <span class="cmm_result
							txt_red">71% chọn NO</span>
                            </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img02.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">5 cổ phiếu nào đã tăng giá mạnh nhất từ đầu năm tới nay?</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img03.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Giá dầu đảo chiều giảm do tăng trưởng nhu cầu chậm lại</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->
            <div class="col s4">
                <p class="ttcate2"><span>Không gian đẹp</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-post">Một số ngân hàng đã xin giữ lại cổ tức để tăng vốn, nhằm mục đích bảo đảm hệ số CAR.</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img03.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->

            <div class="col s4">
                <p class="ttcate2"><span>Kiến thức y tế</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-post">Cầu Thủ Thiêm 4 sẽ kết nối Khu đô thị mới Thủ Thiêm với khu trung tâm hiện hữu của TP và quận 4, quận 7. </h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->
        </div>
        <!-- end row -->
        <div class="row rowpost">
            <div class="col s8">
                <p class="ttcate2"><span>Địa điểm du lịch</span></p>
                <div class="row">
                    <div class="col s6">
                        <div class="fist_post">
                            <a href="">
                                <p class="img" style="background-image: url(data/img02.jpg)"></p>
                                <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                                <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                            </a>
                        </div>
                    </div>
                    <div class="col s6">
                        <ul class="orther_post">
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- end col -->
            <div class="col s4  center-align">
                <p class="ads-gg-right">
                    <span>Advertisement<br />
                        336x280 </span>
                </p>
            </div>
            <!-- end col -->
        </div>
        <!-- / row -->
        <div class="row posr">
            <div class="col s12">
                <h3 class="ttcate2"><span>Bài ngẫu nhiên</span></h3>
                <div class="slide slide_rand">
                    <ul class="sld_rand_2">
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img04.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img04.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                    </ul>
                </div>
            </div>
        </div>

        <!--end 3 lg row-->
        <!--3 lg row-->
        <div class="row">
            <div class="col s4">
                <p class="ttcate2"><span>Kiến thức kinh doanh</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img02.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img03.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->
            <div class="col s4">
                <p class="ttcate2"><span>Không gian đẹp</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img03.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->

            <div class="col s4">
                <p class="ttcate2"><span>Kiến thức y tế</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->
        </div>
        <!-- end row -->
        <div class="row rowpost">
            <div class="col s8">
                <p class="ttcate2"><span>Địa điểm du lịch</span></p>
                <div class="row">
                    <div class="col s6">
                        <div class="fist_post">
                            <a href="">
                                <p class="img" style="background-image: url(data/img02.jpg)"></p>
                                <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                                <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                            </a>
                        </div>
                    </div>
                    <div class="col s6">
                        <ul class="orther_post">
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- end col -->
            <div class="col s4  center-align">
                <p class="ads-gg-right"><span>Advertisement</span> </p>
            </div>
            <!-- end col -->
        </div>
        <!-- / row -->
        <div class="row posr">
            <div class="col s12">
                <h3 class="ttcate2"><span>Bài ngẫu nhiên</span></h3>
                <div class="slide slide_rand">
                    <ul class="sld_rand_2">
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img04.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img04.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                    </ul>
                </div>
            </div>
        </div>

        <!--end 3 lg row-->
        <!--3 lg row-->
        <div class="row">
            <div class="col s4">
                <p class="ttcate2"><span>Kiến thức kinh doanh</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img02.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img03.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->
            <div class="col s4">
                <p class="ttcate2"><span>Không gian đẹp</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img03.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->

            <div class="col s4">
                <p class="ttcate2"><span>Kiến thức y tế</span></p>
                <div class="post_col">
                    <div class="fist_post">
                        <a href="">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                        </a>
                    </div>
                    <ul class="orther_post">
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                        <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                            <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                            <p class="date-post">Updated: 23 Hours ago</p>
                            <p class="cmm_result">60% chọn YES</p>
                        </span></a></li>
                    </ul>
                </div>
            </div>
            <!-- end col post -->
        </div>
        <!-- end row -->
        <div class="row rowpost">
            <div class="col s8">
                <p class="ttcate2"><span>Địa điểm du lịch</span></p>
                <div class="row">
                    <div class="col s6">
                        <div class="fist_post">
                            <a href="">
                                <p class="img" style="background-image: url(data/img02.jpg)"></p>
                                <h2 class="tt-post">Lectus non rutrum pulvinar urna leo dignissim lorem rutrum pulvinar urna leo dignissim lorem </h2>
                                <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result txt_red">71% chọn NO</span> </p>
                            </a>
                        </div>
                    </div>
                    <div class="col s6">
                        <ul class="orther_post">
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img04.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                            <li><a href="#" class="clearfix"><span class="post_left" style="background-image: url(data/img01.jpg)"></span><span class="post_body">
                                <h2 class="tt-post">Collectivism ought to be preffered to Individualism</h2>
                                <p class="date-post">Updated: 23 Hours ago</p>
                                <p class="cmm_result">60% chọn YES</p>
                            </span></a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!-- end col -->
            <div class="col s4  center-align">
                <p class="ads-gg-right"><span>Advertisement</span></p>
            </div>
            <!-- end col -->
        </div>
        <!-- / row -->
        <div class="row posr">
            <div class="col s12">
                <h3 class="ttcate2"><span>Bài ngẫu nhiên</span></h3>
                <div class="slide slide_rand">
                    <ul class="sld_rand_2">
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img04.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img02.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img03.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img04.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                        <li><a href="#">
                            <p class="img" style="background-image: url(data/img01.jpg)"></p>
                            <h2 class="tt-pots">Lectus non rutrum pulvinar urna leo dignissim lore...</h2>
                            <p class="info_post clearfix"><span class="date">Jun 11, 2015</span> <span class="cmm_result">70% chọn YES</span> </p>
                        </a></li>
                    </ul>
                </div>
            </div>
        </div>
        <!--end 3 lg row-->

    </div>
</div>
