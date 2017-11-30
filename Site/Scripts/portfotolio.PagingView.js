if (typeof Medvekoma == "undefined") Medvekoma = {};
if (typeof Medvekoma.Portfotolio == "undefined") Medvekoma.Portfotolio = {};

var delay = 100;        // page load delay in milliseconds
var scrollMargin = 350; // bottom pixels before automatic paging

$(document).ready(function () {
    Medvekoma.Portfotolio.InitializeLoading();
});

Medvekoma.Portfotolio.InitializeLoading = function () {
    $('#nextPageLink').hide();
    $('#nextPageLoading').show();
    Medvekoma.Portfotolio.SubscribeToScrollEvent();
};

Medvekoma.Portfotolio.SubscribeToScrollEvent = function () {
    $(window).scroll(function () {
        Medvekoma.Portfotolio.ShowNextPageIfNeeded();
    });
};

Medvekoma.Portfotolio.ShowNextPageIfNeeded = function () {
    var invisiblePixels = $(document).height() - $(window).scrollTop() - $(window).height();
    if (invisiblePixels < scrollMargin) {
        $(window).unbind('scroll');
        window.setTimeout(Medvekoma.Portfotolio.ShowNextPage, delay);
    }
};

Medvekoma.Portfotolio.ShowNextPage = function () {
    var nextPageDiv = $('#nextPage');
    var url = $('a', nextPageDiv).attr('href');
    if (url) {
        var options = {
            url: url,
            context: document.body,
            success: function (result) {
                nextPageDiv.html('').replaceWith(result);
                Medvekoma.Portfotolio.InitializeLoading();
            }
        };
        $.ajax(options);
    }
};
