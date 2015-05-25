window.goodtimes.adminApp.controller('adminPageController', ['$http', '$scope', '$location', function ($http, $scope, $location) {
    $scope.knownSignInState = false;
    $scope.toolImages = function () { $location.path('/images'); }
    $scope.toolArticles = function () { $location.path('/articles'); }
    $scope.toolHome = function () { $location.path('/'); };
}]);