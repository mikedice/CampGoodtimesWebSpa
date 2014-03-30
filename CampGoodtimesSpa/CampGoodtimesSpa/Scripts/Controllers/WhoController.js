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

    function processEmployeeList(data)
    {
        // The purpose of this function is to break up the list
        // into rows of three
        var table = [];
        var tuple = [];
        var r = 0;
        for (var i = 0; i<data.length; i++)
        {
            tuple.push(data[i]);
            r++;
            if (r >= 3)
            {
                table.push(tuple);
                tuple = [];
                r = 0;
            }
        }
        table.push(tuple); // push the last tuple
        return table;
    }


    function init(scope, location, route, donorsFactory, sponsorsFactory, staffAndBoardFactory) {
        // initialize click handlers for nav bar buttons
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

        // The active tab route parameter is defined in the 
        // app route configuration
        scope.activeTab = route.current.params['tab']; 

        // Controls loading indicators for staff and board members
        scope.staffMembersLoaded = false;
        scope.boardMembersLoaded = false;

        // randomly pick a banner image for the page
        selectBannerImage(scope);

        // load async data
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

        staffAndBoardFactory.getStaff()
            .success(function (data) {
                scope.staffList = processEmployeeList(data);
                scope.staffMembersLoaded = true;
            })
            .error(function (data, status) {
                window.alert("Error retrieving list of staff" + status);
                scope.staffMembersLoaded = true; // stops the loading indicator
            });

        staffAndBoardFactory.getBoard()
            .success(function (data) {
                scope.boardList = processEmployeeList(data);
                scope.boardMembersLoaded = true;
            })
            .error(function (data, status) {
                window.alert("Error retrieving list of board" + status);
                scope.boardMembersLoaded = true; // stops the loading... indicator
            });
    };

    return {
        Name: 'WhoController',
        Controller: function ($scope, $location, $route, donorsFactory, sponsorsFactory, staffAndBoardFactory) {
            init($scope, $location, $route, donorsFactory, sponsorsFactory, staffAndBoardFactory);
        }
    };

})();