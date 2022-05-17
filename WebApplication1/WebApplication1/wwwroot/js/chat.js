var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();
hubConnection.on("Send", function (message, dateTime, name) {

    let text;
    if (name == userName) {
        text = "<div class='chat__messages__item'><div class='chat__messages__item__my'><div class='text3 chat__messages__item__content'>" + message + "</div ></div ></div > "
    }
    else {
        text = "<div class='chat__messages__item'><div class='chat__messages__item__other'><div class='text3 chat__messages__item__content'>" + message + "</div ></div ></div > "
    }
    
    $(".chat__content__messages").html($(".chat__content__messages").html() + text)
    scroll()
});

var userName

function send() {
    let message = $("#message").val();
    let groupId = $(".chat__info").attr("id")
    $("#message").val('')
    hubConnection.invoke("Send", message, groupId)
}


function removeFromChat() {
    let groupId = $(".chat__info").attr("id")
    if (groupId != undefined) {
        hubConnection.invoke("RemoveFromChat", groupId)
    }
}

function scroll() {
    $('.chat__content__messages').scrollTop($('.chat__content__messages').height())
}

hubConnection.start()



$(document).ready(function () {
    setTimeout(enter, 1000)
    scroll()
    userName = $(".chat").attr("userName")
})


function enter() {
    let groupId = $(".chat__info").attr("id")
    if (groupId != undefined) {
        hubConnection.invoke("Enter", groupId);
    }
}


function getChat(chatId) {
    $.ajax({
        url: "../Chat/GetChat",
        type: "POST",
        data: {
            chatId: chatId
        },
        success: function (response) {
            removeFromChat()
            $(".chat__content__messages").html(response)
            hubConnection.invoke("Enter", chatId.toString());
            scroll()
        }
    })
}