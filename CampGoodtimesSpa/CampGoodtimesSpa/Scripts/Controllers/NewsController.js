var NewsController = (function () {


    function SetCurrentArticle(scope, data, articleNumber)
    {
        for (var i = 0; i < data.length; i++)
        {
            if (data[i].ArticleNumber == articleNumber)
            {
                scope.currentArticle = data[i];
                break;
            }
        }
    }

    function init(scope, location, route, newsItemFactory)
    {
        scope.newsPageItemsLoaded = false;
        scope.readMore = function (articleNumber) { location.path('news/' + articleNumber) }

        newsItemFactory.getNewItems()
            .success(function (data) {
                scope.newsPageItemsLoaded = true;
                scope.newsItems = data;
                if (route.current.params.articleNumber !== null)
                {
                    SetCurrentArticle(scope, data, route.current.params.articleNumber);
                }
            })
            .error(function (data, status) {
                scope.newsPageItemsLoaded = true;
                window.alert("Error retrieving news items " + status);
            });
    }

    return {
        Name: 'NewsController',
        Controller: function ($scope, $location, $route, newsItemFactory) {
            init($scope, $location, $route, newsItemFactory);
        }
    };

})();