function deleteApplication(applicationId) {
    $.ajax({
        url: "/Applications/Delete",
        type: "POST",
        data: {
            applicationId: applicationId,
        },
        success: function (response) {
            if (response) {
                $("#" + applicationId).remove()
            }
        }
    })
}