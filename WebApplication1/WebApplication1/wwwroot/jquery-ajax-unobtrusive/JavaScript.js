function likeApplication(Id) {
    $.ajax({
        url: "~/Like/Like",
        type: "POST",
        data: { ApplicationId: Id },

        dataType: "json",
        success: function (responce) {
            alert("Ok")
        },

        feilure: function (responce) {
            alert("Fail")
        }
    })
}