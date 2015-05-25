window.goodtimes = window.goodtimes || {};
window.goodtimes.adminApp = angular.module('GoodtimesAdmin', ['angularFileUpload', 'ngRoute', 'ngAnimate', 'ngSanitize']);

window.goodtimes.adminApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', { templateUrl: '/PartialViews/Admin/adminlandingpage.html' })
        .when('/images', { templateUrl: '/PartialViews/Admin/images.html' })
        .when('/articles', { templateUrl: '/PartialViews/Admin/articles.html' })
        .when('/articles/edit/:articleId?', { templateUrl: '/PartialViews/Admin/articleEdit.html' })
        .when('/camps', { templateUrl: 'PartialViews/Admin/camps.html' })
        .otherwise({ redirectTo: '/' });
});


