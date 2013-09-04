if (typeof Medvekoma == "undefined") Medvekoma = {};
if (typeof Medvekoma.Portfotolio == "undefined") Medvekoma.Portfotolio = {};

var delay = 100;        // page load delay in milliseconds
var scrollMargin = 350; // bottom pixels before automatic paging
var historicalScrollPosition = 0;

$(document).ready(function () {
    Medvekoma.Portfotolio.InitializeLoading();
});

Medvekoma.Portfotolio.InitializeLoading = function () {
    $('#nextPageLink').hide();
    $('#nextPageLoading').show();
    
    var requestedScrollPosition = Medvekoma.Portfotolio.CalculateTargetScollPosition();
    Medvekoma.Portfotolio.RestoreScrollPosition(requestedScrollPosition);
    Medvekoma.Portfotolio.ShowNextPageIfNeeded();
    Medvekoma.Portfotolio.SubscribeToScrollEvent();
};

Medvekoma.Portfotolio.CalculateTargetScollPosition = function() {

    var targetScrollPosition = $(window).scrollTop();

    if ($.cookie('scroll_anchor') != null && $.cookie('scroll_anchor') > targetScrollPosition)
        targetScrollPosition = $.cookie('scroll_anchor');

    return targetScrollPosition;
};

Medvekoma.Portfotolio.RestoreScrollPosition = function (scrollPosition) {
    if (scrollPosition > $(document).height())
    {
        Medvekoma.Portfotolio.ShowNextPageIfNeeded();
        $(window).scrollTop(scrollPosition);
    }
};

Medvekoma.Portfotolio.SubscribeToScrollEvent = function () {
    $(window).scroll(function () {
        Medvekoma.Portfotolio.ShowNextPageIfNeeded();
    });
};

Medvekoma.Portfotolio.ShowNextPageIfNeeded = function () {
    var invisiblePixels = $(document).height() - $(window).scrollTop() - $(window).height();
    if (invisiblePixels < scrollMargin || Medvekoma.Portfotolio.CalculateTargetScollPosition() > $(document).height()){
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

                if ($.cookie('scroll_anchor') == null || $.cookie('scroll_anchor') < $(document).height()) {
                    $.cookie('scroll_anchor', $(document).height(), { path: window.location.pathname });
                }
            }
        };
        $.ajax(options);
    }
};

