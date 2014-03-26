var NewsItemFactory = (function() {
    
    return {
        Name: 'newsItemFactory', // factory names have to match exactly the parameter name passed to the controller's init function :(
        Factory: function ($http) {
            var factory = {};

            factory.getNewItems = function () { return $http.get("/api/Data/NewsItems"); };
            return factory;
        }
    };
})();