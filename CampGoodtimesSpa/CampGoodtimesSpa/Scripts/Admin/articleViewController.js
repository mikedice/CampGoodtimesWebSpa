window.goodtimes.adminApp.controller('articleViewController', ['$http', '$scope', '$location', '$route', function ($http, $scope, $location, $route) {
    var reset = function () {
        $scope.campsArticles = [];
        $scope.eventsArticles = [];
        $scope.newsArticles = [];

        $scope.addNew = function () {
            $location.path('/articles/edit');
        }

        $http({
            method: 'GET',
            url: '/api/data/newsitems'
        })
       .success(function (data, status, headers, config) {
           $scope.newsArticles = data;
       })
       .error(function (data, status, headers, config) {
       });

        $http({
            method: 'GET',
            url: '/api/data/camps'
        })
        .success(function (data, status, headers, config) {
            $scope.campsArticles = data;
        })
        .error(function (data, status, headers, config) {
        });

        $http({
            method: 'GET',
            url: '/api/data/events'
        })
        .success(function (data, status, headers, config) {
            $scope.eventsArticles = data;
        })
        .error(function (data, status, headers, config) {
        });

    }

    var deleteArticle = function (articleId) {
        $http({
            method: 'DELETE',
            url: '/api/data/deletearticle/' + articleId
        })
        .success(function (data, status, headers, config) {
            $route.reload();
        })
        .error(function (data, status, headers, config) {
            window.alert("error " + status + " deleting article");
        });
    }

    $scope.editArticle = function (articleId, title) {
        $location.path('/articles/edit/' + articleId);
    }
    $scope.deleteArticle = function (articleId, title) {
        var r = window.confirm("Really? Delete article with title '" + title + "'");
        if (r == true) {
            deleteArticle(articleId);
        }
    }
    reset();
}]);