$(function () {
    $('.link_tab_ajaxcate').click(function () {
        var id = $(this).attr("catid");
        var getloading = $(this).parents().find('.loadingajax').first();
        var gethtm = $(this).parents().find('.ajaxprocate').first();
        getloading.css("display", "block");
        $('.link_tab_ajaxcate').removeClass("view_all_P");
        $(this).addClass("view_all_P");
        $.ajax({
            url: '../vi-vn/AjaxcateproductIndex.aspx',
            data: 'id=' + id,
            success: function (e) {
                getloading.css("display", "none");
                gethtm.html(e);
            }
        });
        return false;
    });

});
