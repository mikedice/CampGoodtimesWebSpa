window.goodtimes.adminApp.directive("adminNavigationDirective", ['$http', function ($http) {
    return {
        restrict: 'A',
        templateUrl: '/PartialViews/Admin/adminNavigationControls.html',
        link: function (scope, element) {
        }
    }
}]);