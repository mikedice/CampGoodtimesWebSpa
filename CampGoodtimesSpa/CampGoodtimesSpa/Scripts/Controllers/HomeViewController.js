/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Controller for the home view
// Scope Properties set are:
//   newsItems: collection of newsItem objects to show on the home view
//   newsItemsLoaded: a flag that indicates the loading spinner can stop being shown in the UI
//   bannerImageUrl: a randomly selected banner image to show at the top of the home UI.
var HomeViewController = (function () {

    // default banner images
    var defaultBannerImages = [
        "/Content/images/layout/banner-placeholder1.png",
        "/Content/images/layout/banner-placeholder2.png",
        "/Content/images/layout/banner-placeholder3.png"
    ];

    function selectBannerImage(scope) {
        var date = new Date();
        var index = date.getMilliseconds() % 3;
        scope.bannerImageUrl = defaultBannerImages[index];
    }

    // News items have been loaded. We only show the first three items from the
    // result set in the UI. If an image URL wasn't supplied we show the default
    // image URL
    function processNewsItems(data) {
        var result = [];
        for (var i = 0; i < 3; i++) {
            // if the EventImageUrl wasn't specified we should set it to the default
            if (!data[i].hasOwnProperty("EventImageSmall") || data[i].EventImageSmall == null) {
                data[i]["EventImageSmall"] = "/Content/images/layout/defaultnewsitem.png";
            }

            result.push(data[i]);
        }
        result.reverse();
        return result;
    }

    // Initialization
    function init(scope, newsItemFactory) {
        scope.newsItemsLoaded = false; // loading indicator variable

        selectBannerImage(scope);

        newsItemFactory.getNewItems()
            .success(function (data) {
                scope.newsItemsLoaded = true;
                scope.newsItems = processNewsItems(data);
            })
            .error(function (data, status) {
                scope.newsItemsLoaded = true;
                window.alert("Error retrieving news items " + status);
            });

        // click handlers from UI elements
        scope.becomeAVolunteerClicked = function () {
            window.alert("home partial: volunteer!");
        };

        scope.becomeACamperClicked = function () {
            window.alert("home partial: camper!");
        };

        scope.donateClicked = function () {
            window.alert("home partial: donate!");
        };
    }

    return {
        Name: 'HomeViewController',
        Controller: function ($scope, newsItemFactory) {
            init($scope, newsItemFactory);
        }
    };

})();