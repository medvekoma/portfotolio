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

    var targetScrollPosition = $(window).scrollTop();//default case within the page

    if (Medvekoma.Portfotolio.GetUrlParameters()["scroll"] > targetScrollPosition)//use the one from URL when coming back from Flickr(first partialpage load)
    {
        targetScrollPosition = parseInt(Medvekoma.Portfotolio.GetUrlParameters()["scroll"]);
        if(historicalScrollPosition == 0)
            historicalScrollPosition = targetScrollPosition;
    }

    if (historicalScrollPosition > targetScrollPosition)//use the original one when was requested coming back from Flickr for the first time
        targetScrollPosition = historicalScrollPosition;

    return targetScrollPosition;
};

Medvekoma.Portfotolio.RestoreScrollPosition = function (scrollPosition) {
    if (scrollPosition > $(document).height()) {
        //window.setTimeout(function() {
            Medvekoma.Portfotolio.ShowNextPageIfNeeded();
        //}, delay);
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
                history.pushState(null, null, "?scroll=" + $(document).height());
                window.addEventListener("popstate", function () {
                    var urlParams = Medvekoma.Portfotolio.GetUrlParameters();
                    Medvekoma.Portfotolio.RestoreScrollPosition(urlParams["scroll"]);
                });
            }
        };
        $.ajax(options);
    }
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