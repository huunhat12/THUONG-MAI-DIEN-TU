
$(document).ready(function () {
    //$('.Quantity').blur(function () {
    //    var listProduct = $('.Quantity');
    //    var cartList = [];
    //    $.each(listProduct, function (i, item) {
    //        cartList.push({
    //            Quantity: $(item).val(),
    //            sanpham: {
    //                MaSP: $(item).data('id')
    //            }
    //        });
    //    });

    //    $.ajax({
    //        url: '/Cart/Update',
    //        data: { cartModel: JSON.stringify(cartList) },
    //        dataType: 'json',
    //        type: 'POST',
    //        success: function (res) {
    //            if (res.status == true) {
    //                //$('#cart-page').html(result.toString());
    //                window.location.href = "/Cart/Giohang";
    //            }
    //        }
    //    })
    //});
});
function AddCart(value, sl) {
    if (sl == 0) {
        alert("Xác nhận");
        sl = $('.sl').val();
    }
    var url = "/Cart/AddCart";
    $.ajax({
        url: url,
        type: 'Get',
        cache: false,
        data: { maSp: value, soluong: sl },
        success: function (result) {
            $('.basket').html(result);
            alert("Thêm thành công");
        },
        error: function(data){
        alert("fail");
    }
    });
}
function changequality(value,tt) 
{
    var listProduct = $('.Quantity');
    var cartList = [];
    var id = value;
    var temp;
    var soluong;
    $.each(listProduct, function (i, item) {
        if (id == $(item).data('id'))
        {
            temp = $(item).val();
            soluong = $(item).data('bind');
        }
    });
    if (temp  > soluong)
    {
        alert("Số lượng bạn nhập không đúng");
    }
    else
    {
        if (temp > 0 && tt == 'G')
            temp--;
        if (tt == 'T')
            temp++;
        cartList.push({
            Quantity: temp,
            sanpham: {
                MaSP: id
            }
        });
       
    }
        
        //});
   

    $.ajax({
        url: '/Cart/Update',
        data: { cartModel: JSON.stringify(cartList) },
        dataType: 'json',
        type: 'POST',
        success: function (res) {
            if (res.status == true) {
                loadupdatecart();
            }
        }
    })
}
function loadupdatecart()
{
    $.get("Giohang", function (data) {
        $('#cart-page').html($(data).find("#cart-page"));
        $('.basket').html($(data).find(".basket"));
    });
}
function deletecart(value)
{
    $.ajax({
        url: '/Cart/Delete',
        data: { id : value },
        dataType: 'json',
        type: 'Post',
        success: function (res) {
            if (res.status == true) {
                loadupdatecart();
            }
        }
    })
}
function deleteall() {
    $.ajax({
        url: '/Cart/DeleteAll',
        dataType: 'json',
        type: 'Post',
        success: function (res) {
            if (res.status == true) {
                loadupdatecart();
            }
        }
    })
}