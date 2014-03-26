/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Configure SPA application
var app = angular.module('GoodtimesApp', ['ngRoute', 'ngAnimate']);

// Configure routing
app.config(function ($routeProvider) {
    $routeProvider.when('/', { controller: 'HomeViewController', templateUrl: '/Views/HomePartial.html' })
                 .otherwise({ redirectTo: '/' });
});

// Inject data factories
app.factory(NewsItemFactory.Name, NewsItemFactory.Factory);

// Inject controllers
app.controller(HomeViewController.Name, HomeViewController.Controller);
app.controller(PageHeaderController.Name, PageHeaderController.Controller);
