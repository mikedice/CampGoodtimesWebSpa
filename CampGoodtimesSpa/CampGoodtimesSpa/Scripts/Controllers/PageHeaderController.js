/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Controller for the page header
// No data added to the scope
var PageHeaderController = (function () {

    // Initialization
    function init(scope) {
        // Click handlers for UI elements
        scope.donateClicked = function () {
            window.alert("page header: donate!");
        };

        scope.connectClicked = function () {
            window.alert("page header: connect!");
        };

        scope.newsClicked = function () {
            window.alert("page header: news!");
        };

        scope.whatWeDoClicked = function () {
            window.alert("page header: what we do!");
        };

        scope.whoWeAreClicked = function () {
            window.alert("page header: who we are!");
        };
    }

    return {
        Name: 'PageHeaderController',
        Controller: function ($scope) {
            init($scope);
        }
    };

})();