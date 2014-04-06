/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Controller for the page header
// No data added to the scope
var PageHeaderController = (function () {

    // Initialization
    function init(scope, location) {
        // Click handlers for UI elements
        scope.donateClicked = function () {
            window.alert("page header: donate!");
        };

        scope.connectClicked = function () {
            window.alert("page header: connect!");
        };

        scope.newsClicked = function () {
            location.path("/news");
        };

        scope.whatWeDoClicked = function () {
            location.path("/what");
        };

        scope.whoWeAreClicked = function () {
            location.path("/who");
        };
    }

    return {
        Name: 'PageHeaderController',
        Controller: function ($scope, $location) {
            init($scope, $location);
        }
    };

})();