/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

var app = angular.module('GoodtimesApp', ['ngRoute', 'ngAnimate']);

app.config(function ($routeProvider) {
    $routeProvider.when('/', { controller: 'HomeViewController', templateUrl: '/Views/HomePartial.html' })
                 .otherwise({ redirectTo: '/' });
});


// Factory object for producing data
app.factory('newsItemFactory', function ($http) {

    var factory = {};
    factory.getNewItems = function () { return $http.get("/api/Data/NewsItems"); }
    return factory;
});


app.controller('HomeViewController', function ($scope, newsItemFactory) {
    init();

    function init() {
        newsItemFactory.getNewItems().success(function (data) {
            $scope.newsItems = data;
        });
    };

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
