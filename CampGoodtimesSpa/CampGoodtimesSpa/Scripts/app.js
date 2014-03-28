/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Configure SPA application
var app = angular.module('GoodtimesApp', ['ngRoute', 'ngAnimate']);

// Configure routing
app.config(function ($routeProvider) {
    $routeProvider
        .when('/', { controller: 'HomeViewController', templateUrl: '/PartialViews/Home.html' })
        .when('/who/history', { controller: 'WhoController', templateUrl: '/PartialViews/Who/History.html' })
        .when('/who/staff', { controller: 'WhoController', templateUrl: '/PartialViews/Who/Staff.html' })
        .when('/who/volunteers', { controller: 'WhoController', templateUrl: '/PartialViews/Who/Volunteers.html' })
        .when('/who/donors', { controller: 'WhoController', templateUrl: '/PartialViews/Who/Donors.html' })
        .when('/who', {redirectTo:'/who/history'})
        .otherwise({ redirectTo: '/' });
});

// Inject data factories
app.factory(NewsItemFactory.Name, NewsItemFactory.Factory);

// Inject controllers
app.controller(HomeViewController.Name, HomeViewController.Controller);
app.controller(PageHeaderController.Name, ['$scope', '$location', PageHeaderController.Controller]);
app.controller(WhoController.Name, ['$scope', '$location', '$rootScope', '$route', WhoController.Controller]);
