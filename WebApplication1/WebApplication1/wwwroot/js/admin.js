

$(document).ready(function () {
    submitParamsAdmin();
    getUsers();
})
function submitParamsAdmin() {
    $.ajax({
        url: "/Admin/GetApplications",
        type: 'POST',
        data: {
            minCups: $("#slider-1").val(),
            maxCups: $("#slider-2").val(),
            cupsAscending: $("#cupsAscending").val(),
            searchString: $("#application__searchString").val(),
            onlyLiked: $("#onlyLiked").val()
        },
        success: function (response) {
            $(".admin__items__applications").html(response)
            $('.admin__control__button').click(function () {
                $(this).parent().children('.admin__control__button__delete').toggleClass('_active__delete__button');
            });
        }
    })
}


function removeApplication(id, userName) {
    $.ajax({
        url: "/Admin/RemoveApplication",
        type: "POST",
        data: {
            id: id,
            userName: userName,
        },
        success: function (response) {
            alert(response)
            if (response) {
                $(`#application_${id}`).remove();
            }
        }
    })
}


function getUsers() {
    $.ajax({
        url: "/Admin/GetUsers",
        type: "POST",
        data: {
            searchString: $('#user__searchString').val()
        },
        success: function (response) {
            $(".admin__items__users").html(response)
        }
    })
}

function getUserInfo(userName) {
    $.ajax({
        url: "/Admin/GetUserData",
        type: "POST",
        data: {
            userName: userName
        },
        success: function (response) {
            $(".userInfo__content").html(response)
            $('.admin__control__button').click(function () {
                $(this).parent().children('.admin__control__button__delete').toggleClass('_active__delete__button');
            });
        }
    })
}


function ban(userName) {
    $.ajax({
        type: "POST",
        url: "/Admin/BanUser",
        data: {
            userName: userName
        },
        success: function (response) {
            if (response) {
                var button = $('#banButton')
                button.html("Разбанить")
                button.attr('onclick', "unban('" + userName + "')")
                var text = $(".isBaned")
                text.html("Пользователь забанен")
            }
            else {
                alert("error")
            }
        }
    })
}

function unban(userName) {
    $.ajax({
        type: "POST",
        url: "/Admin/UnbanUser",
        data: {
            userName: userName
        },
        success: function (response) {
            if (response) {
                var button = $('#banButton')
                button.html("Забанить")
                button.attr('onclick', "ban('" + userName + "')")
                var text = $(".isBaned")
                text.html("Пользователь не забанен")
            }
            else {
                alert("error")
            }
        }
    })
}