var WhatController = (function () {

    // default banner images
    var defaultBannerImages = [
        "/Content/images/layout/banner_uke.JPG",
        "/Content/images/layout/banner_superhero_2013.jpg",
        "/Content/images/layout/banner_girls_2013.JPG"
    ];

    var campPartials = [
        { title: "Camp Goodtimes", partial: "/PartialViews/What/camps/goodtimes.html" },
        { title: "Kayak Camp", partial: "/PartialViews/What/camps/kayak.html" },
        { title: "Ski Camp", partial: "/PartialViews/What/camps/ski.html" },
    ];

    function selectBannerImage(scope) {
        var date = new Date();
        var index = date.getMilliseconds() % 3;
        scope.bannerImageUrl = defaultBannerImages[index];
    }

    function SelectCampPartial(campTitle)
    {
        for (var i = 0; i<campPartials.length; i++)
        {
            if (campTitle == campPartials[i].title)
            {
                return campPartials[i].partial;
            }
        }
        return null;
    }

    function processList(scope, route, data)
    {
        var activeEvent = -1;
        var activeCamp = "";

        // determine if camp or event was set on the route
        if (route.current.params.eventNumber !== null)
        {
            if (!isNaN(route.current.params.eventNumber)) {
                activeEvent = route.current.params.eventNumber;
            }
            else {
                activeCamp = route.current.params.eventNumber;
            }
        }

        for (var i = 0; i<data.length; i++)
        {
            // set Camp or Event if it was set on the route.
            if (activeEvent != -1 && activeEvent == data[i].EventNumber)
            {
                scope.SelectedEvent = data[i];
            }
            else if (activeCamp != "" && activeCamp == data[i].Title)
            {
                scope.SelectedCamp = data[i];
                scope.SelectedCampPartial = SelectCampPartial(activeCamp);
            }


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

        scope.eventsLearnMoreClicked = function(eventNumber) {
            location.path("/what/events/" + eventNumber);
        }

        scope.campsLearnMoreClicked = function(camp)
        {
            location.path("what/camps/" + camp);
        }

        scope.goodtimesBecomeACamper = function()
        {
            window.alert("Become a goodtimes camper");
        }

        scope.goodtimesVolunteer = function () {
            window.alert("Volunteer at goodtimes");
        }

        scope.kayakBecomeACamper = function()
        {
            window.alert("Become a kayak camper");
        }

        scope.kayakVolunteer = function() {
            window.alert("Volunteer at kayak camp");
        }

        scope.skiBecomeACamper = function() {
            window.alert("Become a ski camp camper");
        }

        scope.skiVolunteer = function() {
            window.alert("Volunteer at ski camp");
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
                scope.campsList = processList(scope, route, data);
                scope.campsLoaded = true;
            })
            .error(function (data, status) {
                window.alert("Error retrieving list of camps" + status);
                scope.campsLoaded = true; // stops the loading indicator
            });

        whatFactory.getEvents()
            .success(function (data) {
                scope.eventsList = processList(scope, route, data);
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