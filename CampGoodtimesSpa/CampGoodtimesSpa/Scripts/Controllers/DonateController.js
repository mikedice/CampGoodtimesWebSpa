
var DonateController = (function () {

    // default banner images
    var defaultBannerImages = [
        "/Content/images/layout/banner_girls_2013.JPG",
        "/Content/images/layout/banner-gauntlet_2013.jpg",
        "/Content/images/layout/banner_backs_2013.JPG"
    ];

    function selectBannerImage(scope) {
        var date = new Date();
        var index = date.getMilliseconds() % 3;
        scope.bannerImageUrl = defaultBannerImages[index];
    }

    function init(scope, location, route) {
        selectBannerImage(scope);
        scope.monetaryClicked = function() {
            location.path("/donate/monetary");
        }
        scope.inKindClicked = function() {
            location.path("/donate/inkind");
        }
        scope.donateClicked = function(type) {
            window.alert(type);
        }

        // The active tab route parameter is defined in the 
        // app route configuration
        scope.activeTab = route.current.params['tab'];
    }

    return {
        Name: 'DonateController',
        Controller: function ($scope, $location, $route) {
            init($scope, $location, $route);
        }
    };

})();