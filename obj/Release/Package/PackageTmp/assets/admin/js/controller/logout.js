$(document).ready( function () {
    $.ajax({
        url: '/Login/Logout',
        dataType: 'json',
        type: 'Post',
        success: function (res) {
            if (res.status == true) {
                document.location = '/admin';
            }
        }, error: function () {
            alert('Logout failed');
        }
    });
    return false;
});
