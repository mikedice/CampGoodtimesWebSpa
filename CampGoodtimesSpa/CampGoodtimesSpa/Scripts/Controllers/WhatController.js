var WhatController = (function () {

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

    function processList(data)
    {
        for (var i = 0; i<data.length; i++)
        {
            if (i%2 > 0)
            {
                data[i].side = "right";
            }
            else {
                data[i].side = "left";
            }
        }
        return data;
    }

    function init(scope, location, route, whatFactory)
    {
        // click handlers for the various tabs
        scope.campsClicked = function () {
            location.path("/what/camps");
        }

        scope.eventsClicked = function () {
            location.path("/what/events");
        }

        scope.scholarshipsClicked = function () {
            location.path("/what/scholarships");
        }

        scope.learnMoreClicked = function(subject) {
            window.alert("learn more " + subject);
        }

        // The active tab route parameter is defined in the 
        // app route configuration
        scope.activeTab = route.current.params['tab'];

        // randomly pick a banner image for the page
        selectBannerImage(scope);

        // loading indicator controls
        scope.campsLoaded = false;
        scope.eventsLoaded = false;

        // Fetch the list of camps
        whatFactory.getCamps()
            .success(function (data) {
                scope.campsList = processList(data);
                scope.campsLoaded = true;
            })
            .error(function (data, status) {
                window.alert("Error retrieving list of camps" + status);
                scope.campsLoaded = true; // stops the loading indicator
            });

        whatFactory.getEvents()
            .success(function (data) {
                scope.eventsList = processList(data);
                scope.eventsLoaded = true;
            })
            .error(function (data, status) {
                window.alert("Error retrieving list of events" + status);
                scope.eventsLoaded = true; // stops the loading indicator
            });
    }

    return {
        Name: 'WhatController',
        Controller: function ($scope, $location, $route, whatFactory) {
            init($scope, $location, $route, whatFactory);
        }
    };
})();