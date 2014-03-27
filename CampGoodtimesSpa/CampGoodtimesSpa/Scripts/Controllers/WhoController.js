/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Controller for the home view
// Scope Properties set are:
//   bannerImageUrl: a randomly selected banner image to show at the top of the home UI.
var WhoController = (function () {

    // TODO: need to revisit this. I wanted to use a simple array of booleans
    // but could not get the window to keep up with the changes to the array.
    var visibleTab0 = { a: 'nav-a-selected', d: 'nav-tab-visible' };
    var visibleTab1 = { a: 'nav-a', d: 'nav-tab-hidden' };
    var visibleTab2 = { a: 'nav-a', d: 'nav-tab-hidden' };
    var visibleTab3 = { a: 'nav-a', d: 'nav-tab-hidden' };

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

    // string helper since there is no string.endsWith
    function endsWith(val, suffix) {
        return val.indexOf(suffix, this.length - suffix.length) !== -1;
    };

    // we set the visible tab after navigation completes. That way
    // the tab gets set correctly whether the user navigates using the
    // back/fwd buttons or by clicking on nav links in the UI.
    function setVisibleTab(loc) {
        if (endsWith(loc, "/who/history")) {
            visibleTab0 = { a: 'nav-a-selected', d: 'nav-tab-visible' };
            visibleTab1 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab2 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab3 = { a: 'nav-a', d: 'nav-tab-hidden' };
        }
        else if (endsWith(loc, "/who/staff")) {
            visibleTab0 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab1 = { a: 'nav-a-selected', d: 'nav-tab-visible' };
            visibleTab2 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab3 = { a: 'nav-a', d: 'nav-tab-hidden' };
        }
        else if (endsWith(loc, "/who/volunteers")) {
            visibleTab0 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab1 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab2 = { a: 'nav-a-selected', d: 'nav-tab-visible' };
            visibleTab3 = { a: 'nav-a', d: 'nav-tab-hidden' };
        }
        else if (endsWith(loc, "/who/donors")) {
            visibleTab0 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab1 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab2 = { a: 'nav-a', d: 'nav-tab-hidden' };
            visibleTab3 = { a: 'nav-a-selected', d: 'nav-tab-visible' };
        }
    }

    function init(scope, location) {
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

        scope.visibleTab0 = visibleTab0;
        scope.visibleTab1 = visibleTab1;
        scope.visibleTab2 = visibleTab2;
        scope.visibleTab3 = visibleTab3;

        selectBannerImage(scope);
        
    };

    return {
        Name: 'WhoController',
        Controller: function ($scope, $location, $rootScope) {
            init($scope, $location);
            $rootScope.$on('$locationChangeSuccess', function (evt, cur, prev) {
                setVisibleTab(cur);
            });
        }
    };

})();