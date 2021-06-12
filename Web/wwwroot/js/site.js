$(document).ready(function () {
    var newRecognition = new webkitSpeechRecognition();
    newRecognition.continuous = true;
    newRecognition.interimResults = true;

    newRecognition.lang = "zh-CN";

    newRecognition.start();

    newRecognition.onresult = function (event) {
        for (var i = event.resultIndex; i < event.results.length; ++i) {
            if (event.results[i].isFinal) {
                that.val(that.val() + event.results[i][0].transcript)
            } else {
                console.log(event.results[i][0].transcript)

                if (event.results[i][0].transcript == "小萌小萌") {
                    speak('你好在');
                }
            }
        }
    }
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

    //var newRecognition = new webkitSpeechRecognition();
    //newRecognition.continuous = true;
    //newRecognition.interimResults = true;

    //newRecognition.lang = "zh-CN";

    //$("input[type='text'],textarea").focus(function () {
    //    var that = $(this);

    //    newRecognition.start();

    //    newRecognition.onresult = function (event) {
    //        for (var i = event.resultIndex; i < event.results.length; ++i) {
    //            if (event.results[i].isFinal) {
    //                that.val(that.val() + event.results[i][0].transcript)
    //            } else {
    //                console.log(event.results[i][0].transcript)

    //                if (event.results[i][0].transcript == "小萌小萌") {
    //                    speak('你好在');
    //                }
    //            }
    //        }
    //    }
    //})

    //$("input[type='text'],textarea").blur(function () {
    //    newRecognition.stop();
    //})
    $(".page-loader").hide();
});

function speak(text) {
    console.log("开始播放：" + text)

    var msg = new window.SpeechSynthesisUtterance(text);

    window.speechSynthesis.speak(msg);

    console.log("播放结束")
}