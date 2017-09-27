$(document).ready(function () {
});
$(document).ready(function () {
    $('.form-group').on("click", ".remove_field", function (e) { //user click on remove text
        e.preventDefault();
        var s = $(this).parent();
        s.find("input").val('');
        $(this).parent().hide(500);
    })
});
$(document).ready(function () {
    $(document).on("click", "#nav_grid a[href]", function () {
        $.ajax({
            url: $(this).attr("href"),
            type: 'GET',
            cache: false,
            success: function (result) {
                $('.wrapper').html($(result).find("#table"));
                console.log(result);
                $('html, body').animate({
                    scrollTop: $(".wrapper").offset().top
                }, 500);
            }
        });
        return false;
    });
});
//Submit form by id of form
function submitform(id) {
    document.forms[id].submit();
}


//them thong so kt moi
function ThemTSKTMoi(id) {
    var count = parseInt($('#CountFeld').val(), 10);
    ++count;
    $('#CountFeld').val(count);
    var str = '';
    str += "<div class=\"form-group\" id=\"lstkt" + (count - 1) + "\"><div class=\"col-md-2\">";
    str += "<input name=\"lstkt[" + (count - 1) + "].MaSP\" type=\"hidden\" value=\"" + id + "\">";
    str += "<input class=\"form-control\" name=\"lstkt[" + (count - 1) + "].TenTSKT\" type=\"text\" value=\"\">";
    str += "</div><div class=\"col-md-4\">";
    str += "<input class=\"form-control\" name=\"lstkt[" + (count - 1) + "].GiaTri\" type=\"text\" value=\"\">";
    str += "</div></div>";
    $('#thongsoktgrp').append(str);
}

//xoa thong so kt
function XoaTSKT() {
    var count = parseInt($('#CountFeld').val(), 10);
    --count;
    $('#CountFeld').val(count);
    var id = "#lstkt" + count;
    $(id).remove();

}
//Xoa item ajax
function XoaItem(Url, value) {
    if (confirm("Bạn có chắc muốn xóa dữ liệu?") == true) {
        $.ajax({
            url: Url,
            type: 'POST',
            data: { id: value },
            success: function (result) {
                $('.wrapper').html($(result).find("#table"));
                $('#alert-info').html("Xóa dữ liệu thành công");
                $('#alert-info').fadeIn(1000);
                $('#alert-info').fadeOut(3000);
            },
        });
    }


}
