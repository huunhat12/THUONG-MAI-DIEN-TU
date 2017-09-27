$(document).ready(function () {
    $(document).on("click", ".pagination a[href]", function () {
        $.ajax({
            url: $(this).attr("href"),
            type: 'GET',
            cache: false,
            success: function (result) {
                $('.container').html($(result).find("#phantrang"));
                console.log(result);
                $('html, body').animate({
                    scrollTop: $(".container").offset().top
                }, 500);
            }
        });
        return false;
    });
});