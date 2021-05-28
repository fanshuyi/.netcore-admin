$(document).ready(function () {
    $(".page-loader").hide();
})
$(document).ajaxError(function (event, request) {
    if (request.status === 404) {
        $.notify('您访问的页面不存在，请稍后再试！',
            {
                "type": "danger",
                placement: {
                    from: 'bottom',
                    align: 'right'
                }
            });
    } else {
        $.notify(request.responseText,
            {
                "type": "danger",
                placement: {
                    from: 'bottom',
                    align: 'right'
                }
            });
    }
});
$(document).ajaxStart(function () {
    $(".page-loader").show();
});

$(document).ajaxStop(function () {
    $('.select2').select2({ dropdownAutoWidth: true, width: '100%' });
    $(".page-loader").hide();
});


