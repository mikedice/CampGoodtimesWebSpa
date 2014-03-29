/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Controller for the home view
// Scope Properties set are:
//   bannerImageUrl: a randomly selected banner image to show at the top of the home UI.
//   activeTab: the currently active 'who we are' tab
var WhoController = (function () {

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


    function init(scope, location, route, donorsFactory, sponsorsFactory) {
        scope.historyClicked = function () {
            location.path("/who/history");
        };

        scope.staffAndBoardClicked = function () {
            location.path("/who/staff");
        };

        scope.volunteersClicked = function () {
            location.path("/who/volunteers");
        };

        scope.donorsClicked = function () {
            location.path("/who/donors");
        };

        scope.activeTab = route.current.params['tab']; 

        selectBannerImage(scope);

        donorsFactory.getDonorsList()
            .success(function (data) {
                scope.getDonorsList = data;
            })
            .error(function (data, status) {
                window.alert("Error retrieving list of donors" + status);
            });

        sponsorsFactory.getSponsorsList()
            .success(function (data) {
                scope.sponsorsList = data;
            })
            .error(function (data, status) {
                window.alert("Error retrieving list of sponsors" + status);
            });
    };

    return {
        Name: 'WhoController',
        Controller: function ($scope, $location, $route, donorsFactory, sponsorsFactory) {
            init($scope, $location, $route, donorsFactory, sponsorsFactory);
        }
    };

})();