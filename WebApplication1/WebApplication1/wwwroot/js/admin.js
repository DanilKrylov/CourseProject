

$(document).ready(function () {
    submitParams("/Admin/GetApplications");
    getUsers()
})

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
                let searchstr = "#application" + id
                console.log($(searchstr))
                console.log($(searchstr).html())
                $(searchstr).remove()
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
            console.log(response)
            $(".user__content").html(response)
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
            console.log(response)
            $(".selected__user__data").html(response)
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
            }
            else {
                alert("error")
            }
        }
    })
}