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

    function processVolunteersList(scope, data)
    {
        var yearRound = [];
        var event = [];
        for (var i = 0; i<data.length; i++)
        {
            if (data[i].Category != null)
            {
                if (data[i].Category == 'YearRound')
                {
                    pushVisible(yearRound, data[i]);
                }
                else if (data[i].Category == 'CampAndEvent')
                {
                    pushVisible(event, data[i]);
                }
                else
                {
                    pushVisisble(yearRound, data[i]);
                }
            }
            else {
                pushVisible(yearRound, data[i]);
            }
        }

        scope.volunteerYearRound = processEmployeeList(yearRound);
        scope.volunteerEvent = processEmployeeList(event);
        scope.hideYearRound = scope.volunteerYearRound.length == 1 && scope.volunteerYearRound[0].length == 0;
        scope.hideEvent = scope.volunteerEvent.length == 1 && scope.volunteerEvent[0].length == 0;
    }

    function pushVisible(list, element)
    {
        if (element != null && element.ShowOnWebsite == true)
        {
            list.push(element);
        }
    }


    function init(scope, location, route, donorsFactory, sponsorsFactory, staffAndBoardFactory, volunteersFactory) {
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
        scope.volunteersLoaded = false;

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
                window.alert("Error retrieving board list" + status);
                scope.boardMembersLoaded = true; // stops the loading... indicator
            });

        volunteersFactory.getVolunteers()
            .success(function (data) {
                processVolunteersList(scope, data);
                scope.volunteersLoaded = true;
            })
            .error(function (data, status) {
                window.alert("Error retrieving list of volunteers" + status);
                scope.volunteersLoaded= true; // stops the loading... indicator
            });
    };

    return {
        Name: 'WhoController',
        Controller: function ($scope, $location, $route, donorsFactory, sponsorsFactory, staffAndBoardFactory, volunteersFactory) {
            init($scope, $location, $route, donorsFactory, sponsorsFactory, staffAndBoardFactory, volunteersFactory);
        }
    };

})();