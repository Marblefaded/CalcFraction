$(document).ready(function () {

    $('#btnSendMessage').click(function (event) {
        event.stopPropagation();
        var data = {
            'message': $('#TextBoxSender').val(),
        }

        $.ajax({
            url: 'MessageHandler',
            type: 'POST',
            data: JSON.stringify(data),
            cache: false,
            dataType: 'json',
            processData: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data == true) {
                    alert("Сообщение доставлено");
                    location.reload();
                }
                else {
                    alert("Внимание! Сообщение не доставлено!");
                }
            },
            error: function () {
                alert("Произошел сбой бла бла бла");
            }
        });
    });
})