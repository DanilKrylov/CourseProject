$(document).ready(function () {
    $('.application__control__button').click(function () {
        $(this).parent().children('.application__control__button__delete').toggleClass('_active__delete__button');
    });
});
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