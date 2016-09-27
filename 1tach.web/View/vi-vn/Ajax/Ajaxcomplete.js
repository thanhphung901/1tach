$(function () {
    $('#txtsearch').catcomplete({        
        delay: 0,
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "/vi-vn/Complete-Ajax.asmx/autocomplete",
                data: "{'searchitem':'" + request.term + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    response($.map(result.d, function (el) {
                        return {
                            label: el.title,
                            category: el.catname
                        };
                    }));
                }
            });
        }
            , select: function (event, ui) {
                document.location = "/tim-kiem.html?page=0&keyword=" + ui.item.value;
            }

    });
});