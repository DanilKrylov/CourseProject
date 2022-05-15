function submitParams(url) {
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            minCups: $("#slider-1").val(),
            maxCups: $("#slider-2").val(),
            cupsAscending: $("#cupsAscending").val(),
            searchString: $("#application__searchString").val(),
            onlyLiked: $("#onlyLiked").val()
        },
        success: function (response) {
            $(".application__content").html(response)
        }
    })
}