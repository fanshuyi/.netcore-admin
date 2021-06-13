var newRecognition = new webkitSpeechRecognition();
newRecognition.continuous = true;
//newRecognition.interimResults = false;// 是否返回临时结果

newRecognition.lang = "zh-CN";

var enableAssistant = false;

newRecognition.onstart = function () {
    enableAssistant = true;
};

newRecognition.onend = function () {
    enableAssistant = false;
};

newRecognition.onerror = function (event) {
    enableAssistant = false;
};

newRecognition.onresult = function (event) {

    for (var i = event.resultIndex; i < event.results.length; ++i) {

        if (event.results[i].isFinal) {
            // 讲完
            // 记录到数据库

            console.log(event.results[i][0].transcript)

            $.ajax({
                type: "POST", url: '/api/Assistant', contentType: "application/json", dataType: "json", data: JSON.stringify({ content: event.results[i][0].transcript }), success: function (data) {

                    if (data.Code == 0) {
                        speak(data.Data.Content)
                    } else {
                        console.log(data.Msg)
                        speak("我听不懂你在说什么，换一种说法试试。")
                    }
                }
            });

        } else {


        }
    }
}


$(document).ready(function () {
    $("#assistant").click(function () {
        if (enableAssistant)
            newRecognition.stop();
        else
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

        if (enableAssistant)
            newRecognition.stop();
    }

    utterThis.onend = function (event) {
        console.log('播放结束')
        if (!enableAssistant)
            newRecognition.start();
    }

    window.speechSynthesis.speak(utterThis);

}