
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