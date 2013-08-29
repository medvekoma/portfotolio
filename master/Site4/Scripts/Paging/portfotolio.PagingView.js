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
    if (typeof(Storage) !== "undefined") {
        if (sessionStorage.ScrollPosition != null && sessionStorage.ScrollPosition > targetScrollPosition) //back from 3rd pary page
            targetScrollPosition = sessionStorage.ScrollPosition;
    }

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
                debugger;
                if (typeof(Storage) !== "undefined") {
                    if (sessionStorage.ScrollPosition == null || sessionStorage.ScrollPosition < $(document).height())
                        sessionStorage.ScrollPosition = $(document).height();
                }
            }
        };
        $.ajax(options);
    }
};

Medvekoma.Portfotolio.SupportsHistoryApi = function () {
    return !!(window.history && history.pushState);
};

Medvekoma.Portfotolio.GetUrlParameters = function() {
    var match,
        pl = /\+/g, // Regex for replacing addition symbol with a space
        search = /([^&=]+)=?([^&]*)/g,
        decode = function(s) { return decodeURIComponent(s.replace(pl, " ")); },
        query = window.location.search.substring(1);

    var urlParams = {};
    while (match = search.exec(query))
        urlParams[decode(match[1])] = decode(match[2]);
    return urlParams;
};