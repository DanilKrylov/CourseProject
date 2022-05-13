function Like(Id) {
    Id = Id.toString()
    var formData = new FormData();
    formData.append("applicationId", Id);


    var url
    var str = ".first" + Id
    console.log(str)
    console.log($(str))
    console.log($(str).hasClass("unliked"))
    if ($(str).hasClass("unliked")) {
        url = "/Like/Like"
    } else {
        url = "/Like/RemoveLike"
    }

    $.ajax({
        url: url,
        type: 'POST',
        cache: false,
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            var countClass = ".count" + Id
            var count = $(countClass)
            if ($(str).hasClass("unliked")) {
                $(".first" + Id).removeClass("unliked").addClass("liked")
                $(".second" + Id).removeClass("unliked").addClass("liked")
                count.html(+count.html() + 1)
            } else {
                $(".first" + Id).removeClass("liked").addClass("unliked")
                $(".second" + Id).removeClass("liked").addClass("unliked")
                count.html(+count.html() - 1)
            }
        }
    });
}