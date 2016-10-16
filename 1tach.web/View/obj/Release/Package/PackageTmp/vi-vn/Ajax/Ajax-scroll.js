 $(function () {
    var record = 5;
    var pos = 0;
    var count = 0;
    $(window).scroll(function () {
        var heightmain = $('#main').offset().top-300;
        //        var heightfirst = ('#first_cate_main').offset().top;
        //        var heightsecond = ('#second_cate_main').offset().top;
        //        var heightthird = ('#third_cate_main').offset().top;
        //        var heightfourth = ('#fourth_cate_main').offset().top;
        //        var heightfive = ('#five_cate_main').offset().top;
        var mostOfTheWayDown = ($(document).height() - $(window).height());
        if ($(window).scrollTop() >= heightmain && count == 0) {
            //alert(heightmain);
            if (pos < record) {
                //alert(pos);
                $('#loadingmain').show();
                $.ajax({
                    url: '../vi-vn/Ajax-scroll-main.aspx',
                    data: 'pos=' + pos,
                    success: function (e) {
                        setTimeout(function () { getdata(e) }, 1); 
                    }
                });
            }
            count++;
        }

    });
});
function getdata(e) {
    $('#TabsContent').append(e);
    $('#loadingmain').hide();
                       
}