/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Configure SPA application
var app = angular.module('GoodtimesApp', ['ngRoute', 'ngAnimate']);

// Routing
app.config(function ($routeProvider) {
    $routeProvider.when('/', { controller: 'HomeViewController', templateUrl: '/Views/HomePartial.html' })
                 .otherwise({ redirectTo: '/' });
});

// Data Factories
app.factory('newsItemFactory', function ($http) {
    var factory = {};

    factory.getNewItems = function () { return $http.get("/api/Data/NewsItems"); };
    return factory;
});

// Controllers
app.controller('HomeViewController', function ($scope, newsItemFactory) {
    init();

    function init() {
        $scope.newsItemsLoaded = false; // loading indicator variable

        newsItemFactory.getNewItems()
            .success(function (data) {
                $scope.newsItemsLoaded = true;
                $scope.newsItems = data;
            })
            .error(function (data, status) {
                $scope.newsItemsLoaded = true;
                window.alert("Error retrieving news items " + status);
            });
    }

    $scope.becomeAVolunteerClicked = function () {
        window.alert("volunteer!");
    };

    $scope.becomeACamperClicked = function () {
        window.alert("camper!");
    };

    $scope.donateClicked = function () {
        window.alert("donate!");
    };
});
