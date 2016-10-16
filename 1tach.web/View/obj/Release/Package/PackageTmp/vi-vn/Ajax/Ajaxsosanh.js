$(function () {
    $('#Checksosanh').click(function () {
        if (this.checked) {
            $('.pndsosanh').removeClass('hidden');
        }
    });
});

function tatss() {
    $('.pndsosanh').addClass("hidden");
}