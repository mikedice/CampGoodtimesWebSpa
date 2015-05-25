window.goodtimes = window.goodtimes || {};
window.goodtimes.adminApp = angular.module('GoodtimesAdmin', ['angularFileUpload', 'ngRoute', 'ngAnimate', 'ngSanitize']);

window.goodtimes.adminApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', { templateUrl: '/PartialViews/Admin/adminlandingpage.html' })
        .when('/images', { templateUrl: '/PartialViews/Admin/images.html' })
        .when('/articles', { templateUrl: '/PartialViews/Admin/articles.html' })
        .when('/articles/edit/:articleId?', { templateUrl: '/PartialViews/Admin/articleEdit.html' })
        .when('/people', { templateUrl: '/PartialViews/Admin/people.html' })
        .when('/people/edit/:personId?', { templateUrl: 'PartialViews/Admin/peopleEdit.html' })
        .when('/donors', { templateUrl: '/PartialViews/Admin/donors.html' })
        .when('/donors/edit/:donorId?', { templateUrl: 'PartialViews/Admin/donorsEdit.html' })
        .when('/camps', { templateUrl: 'PartialViews/Admin/camps.html' })
        .otherwise({ redirectTo: '/' });
});


