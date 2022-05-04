var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();
hubConnection.on("Send", function (message, dateTime, name) {
    let border = "<p>------------------------------------------</p>"
    let text = "<p>сообщение от:" + name + "</p><span>" + message + "   </span><span>" + dateTime + "</span>"
    $("#chatroom").html(border + text + border + $("#chatroom").html())
});


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


hubConnection.start()


$(document).ready(function () {
    setTimeout(enter,1000)
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
            $(".current__chat").html(response)
            hubConnection.invoke("Enter", chatId.toString());
        }
    })
}