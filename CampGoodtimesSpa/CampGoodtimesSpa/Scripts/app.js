/// <autosync enabled="true" />
/// <reference path="angular.js" />
/// <reference path="angular-route.js" />

// Configure SPA application
var app = angular.module('GoodtimesApp', ['ngRoute', 'ngAnimate']);

// Configure routing
app.config(function ($routeProvider) {
    $routeProvider
        .when('/', { controller: 'HomeViewController', templateUrl: '/PartialViews/Home.html' })
        .when('/who/:tab', {
            controller: 'WhoController', templateUrl: function (p) {
                if (p.tab == 'history') { return '/PartialViews/Who/History.html'; }
                else if (p.tab == 'staff') { return '/PartialViews/Who/Staff.html'; }
                else if (p.tab == 'volunteers') { return '/PartialViews/Who/Volunteers.html'; }
                else if (p.tab == 'donors') { return '/PartialViews/Who/Donors.html'; }
            }
        })
        .when('/who', { redirectTo: '/who/history' })
        .otherwise({ redirectTo: '/' });
});

// Inject data factories
app.factory(NewsItemFactory.Name, NewsItemFactory.Factory);
app.factory(DonorsFactory.Name, DonorsFactory.Factory);
app.factory(SponsorsFactory.Name, SponsorsFactory.Factory);
app.factory(StaffAndBoardFactory.Name, StaffAndBoardFactory.Factory)

// Inject controllers
app.controller(HomeViewController.Name, ['$scope', NewsItemFactory.Name, SponsorsFactory.Name, HomeViewController.Controller]);
app.controller(PageHeaderController.Name, ['$scope', '$location', PageHeaderController.Controller]);
app.controller(WhoController.Name, ['$scope',
                                    '$location',
                                    '$route',
                                    DonorsFactory.Name,
                                    SponsorsFactory.Name,
                                    StaffAndBoardFactory.Name,
                                    WhoController.Controller
                                    ]);
