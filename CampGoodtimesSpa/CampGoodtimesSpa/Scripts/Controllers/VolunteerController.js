
var VolunteerController = (function () {

    // default banner images
    var defaultBannerImages = [
        "/Content/images/layout/banner_staff_guys.JPG",
        "/Content/images/layout/banner_staff_q.JPG",
        "/Content/images/layout/banner_staff_act.JPG"
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
        scope.volunteerClicked = function (type) {
            window.alert(type);
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