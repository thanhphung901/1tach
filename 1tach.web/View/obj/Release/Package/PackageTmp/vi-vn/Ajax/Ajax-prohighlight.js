$(function () {
    $('.link_tab').click(function () {
        $('.link_tab').removeClass("view_all_P");
        $(this).addClass('view_all_P');
        var perior = $(this).attr("proid");
        if (perior != 1) document.getElementById("div_viewmore").style.display = "none";
        else document.getElementById("div_viewmore").style.display = "block";
        $.ajax({
            type: "POST",
            url: "../vi-vn/Ajax-prohighlight.aspx/loadproduct",
            data: "{perior:'" + perior + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (e) {

                $(this).addClass('view_all_P');
                $('#div_prohighlight').html(e.d);

            }
        });
        return false;
    });
    
});
var skip = 15;
function viewmoreall(perior,count) {
    
    $.ajax({
        type: "POST",
        url: "../vi-vn/Ajax-prohighlight.aspx/viewmore",
        data: "{perior:'" + perior + "',skip:'" + skip + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (e) {
            skip += 15;
            $('#div_prohighlight').append(e.d);
            if (skip >= count) document.getElementById("div_viewmore").style.display = "none";
        }
    });
    
    return false;
}