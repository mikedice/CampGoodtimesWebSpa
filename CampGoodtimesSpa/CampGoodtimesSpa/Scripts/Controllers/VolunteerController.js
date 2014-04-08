
var VolunteerController = (function () {

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

    function init(scope, location, route) {
        selectBannerImage(scope);
        scope.campEventClicked = function () {
            location.path("/volunteer/campevent");
        }
        scope.yearRoundClicked = function () {
            location.path("volunteer/yearround");
        }

        // The active tab route parameter is defined in the 
        // app route configuration
        scope.activeTab = route.current.params['tab'];
    }

    return {
        Name: 'VolunteerController',
        Controller: function ($scope, $location, $route) {
            init($scope, $location, $route);
        }
    };

})();