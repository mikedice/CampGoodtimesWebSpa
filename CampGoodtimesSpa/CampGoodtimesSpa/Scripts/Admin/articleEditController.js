﻿window.goodtimes.adminApp.controller('articleEditController', ['$http', '$scope', '$location', '$routeParams', function ($http, $scope, $location, $routeParams) {
    $scope.existingArticles = [];
    $scope.article = {}
    $scope.status = '';

    var reset = function () {
        if ($routeParams.articleId != null) {
            $http({
                method: 'GET',
                url: '/api/data/getarticle/' + $routeParams.articleId
            })
             .success(function (data, status, headers, config) {
                 $scope.article = data;
             })
             .error(function (data, status, headers, config) {
             });
        }
    }

    $scope.submitArticle = function (article) {
        if (article.Id != null) {
            // this is an edit so do a put
            $http({
                url: '/api/data/putarticle',
                method: 'PUT',
                data: article
            })
            .success(function (data, status, headers, config) {
                $scope.status = status;
                $location.path('/articles');
            })
            .error(function (data, status, headers, config) {
                $scope.status = 'An error occurred (status code: ' + status + ')';
            });
        }
        else {
            // post
            $http({
                url: '/api/data/postarticle',
                method: 'POST',
                data: article
            })
            .success(function (data, status, headers, config) {
                $scope.status = status;
                $location.path('/articles');
            })
            .error(function (data, status, headers, config) {
                $scope.status = 'An error occurred (status code: ' + status + ')';
            });
        }

    }

    $scope.cancelEdit = function () {
        $location.path('/articles');
    }

    $scope.doneEdit = function () {
        $location.path('/articles');
    }

    reset();
}]);