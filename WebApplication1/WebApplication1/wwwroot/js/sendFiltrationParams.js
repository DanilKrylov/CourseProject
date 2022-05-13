function submitParams(url) {
    $.ajax({
        url: url,
        type: 'POST',
        data: {
            minCups: $("#slider-1").val(),
            maxCups: $("#slider-2").val(),
            onlyLiked: $("#onlyLiked").val(),
            cupsAscending: $("#cupsAscending").val(),
            searchString: $("#application__searchString").val(),
        },
        success: function (response) {
            $(".application__content").html(response)
        }
    })
}