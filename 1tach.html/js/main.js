// JavaScript Document
$('.icon_f').on('click', function(){
var _text=$(this).children('span').text();
alert('Bầu chọn của bạn - '+ _text);

});
//page to top
$(window).scroll(function(){
	if($(window).scrollTop() >= 10) {
		$('.button_scroll2top').show();
	} else {
		$('.button_scroll2top').hide();
	}
});
	
function page_scroll2top(){
	$('html,body').animate({
		scrollTop: 0
	}, '1000');
}
$("body").append('<div href="javascript:;" class="button_scroll2top" onclick="page_scroll2top()">top</div>');

//icon post
$('.post_chuyen_gia').append('<i class="icon_chuyen_gia" title="Bài viết chuyên gia"></i>');
$('.post_doanh_nghiep').append('<i class="icon_doanh_nghiep" title="Bài viết doanh nghiệp"></i>');
//change color of per comment
// var str = $(".cmm_result").text();
// if(str.match(/no/i)){
//   $(".cmm_result").addClass('txt_red');
// }
//vote btn click
$('.debate-bt .yes-debate').on('click', function(){
$('.form_args >div').hide();
$('#frmYes >div').show();
});

$('.debate-bt .no-debate').on('click', function(){
$('.form_args >div').hide();
$('#frmNo >div').show();
});


//btn xem them
$('.btn_show_dt').on('click', function(){
	if($(this).hasClass('active')){
		$(this).removeClass('active').html('Xem thêm..');
	}
	else{
		$(this).addClass('active').html('Thu gọn');
	}
	$('.content-hide').slideToggle('slow');
});

$('.navi-icon').on('click', function(){
	$('.main_menu ').slideToggle('slow');
	});

//smooth link
$(function() {
  $('a[href*="#"]:not([href="#"])').click(function() {
    if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
      if (target.length) {
        $('html, body').animate({
          scrollTop: target.offset().top
        }, 1000);
        return false;
      }
    }
  });
});