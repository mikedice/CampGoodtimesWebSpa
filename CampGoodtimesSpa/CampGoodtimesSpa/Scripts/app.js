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
            controller: 'WhoController',
            templateUrl: function (p) {
                if (p.tab == 'history') { return '/PartialViews/Who/History.html'; }
                else if (p.tab == 'staff') { return '/PartialViews/Who/Staff.html'; }
                else if (p.tab == 'volunteers') { return '/PartialViews/Who/Volunteers.html'; }
                else if (p.tab == 'donors') { return '/PartialViews/Who/Donors.html'; }
            }
        })
        .when('/who', { controller: 'WhoController', redirectTo: '/who/history' })
        .when('/what/:tab', {
            controller: 'WhatController',
            templateUrl: function (p) {
                if (p.tab == 'camps') { return '/PartialViews/What/Camps.html'; }
                else if (p.tab == 'events') { return '/PartialViews/What/Events.html'; }
                else if (p.tab == 'scholarships') { return '/PartialViews/What/Scholarships.html'; }
            }
        })
        .when('/what', { controller: 'WhatController', redirectTo: '/what/camps' })
        .otherwise({ redirectTo: '/' });
});

// Inject data factories
app.factory(NewsItemFactory.Name, NewsItemFactory.Factory);
app.factory(WhoFactory.Name, WhoFactory.Factory);
app.factory(SponsorsFactory.Name, SponsorsFactory.Factory);
app.factory(WhatFactory.Name, WhatFactory.Factory);

// Inject controllers
app.controller(HomeViewController.Name, ['$scope', NewsItemFactory.Name, SponsorsFactory.Name, HomeViewController.Controller]);
app.controller(PageHeaderController.Name, ['$scope', '$location', PageHeaderController.Controller]);
app.controller(WhoController.Name, ['$scope',
                                    '$location',
                                    '$route',
                                    WhoFactory.Name,
                                    SponsorsFactory.Name,
                                    WhoController.Controller]);
app.controller(WhatController.Name, ['$scope',
                                    '$location',
                                    '$route',
                                    WhatFactory.Name,
                                    WhatController.Controller]);

