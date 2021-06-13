var newRecognition = new webkitSpeechRecognition();
newRecognition.continuous = true;
newRecognition.interimResults = true;

newRecognition.lang = "zh-CN";

var enableAssistant = false;

newRecognition.onresult = function (event) {
    for (var i = event.resultIndex; i < event.results.length; ++i) {

        console.log(event.results[i][0].transcript)

        if (event.results[i].isFinal) {

        } else {
            console.log(event.results[i][0].transcript)

            if (event.results[i][0].transcript == "小萌小萌") {
                speak('你好在');
            } else {
                speak('我听不懂您在说什么');
            }
        }
    }
}


$(document).ready(function () {
    $("#assistant").click(function () {
        newRecognition.start();
    })
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

function speak(text) {

    var utterThis = new window.SpeechSynthesisUtterance(text);

    utterThis.onstart = function (event) {
        console.log('开始播放：: ' + event.utterance.text);
        newRecognition.stop();
    }

    utterThis.onend = function (event) {
        console.log('播放结束')
        newRecognition.start();
    }

    window.speechSynthesis.speak(utterThis);

}