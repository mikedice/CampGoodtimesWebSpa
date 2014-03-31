var SponsorsFactory = (function () {

    return {
        Name: 'sponsorsFactory', // factory names have to match exactly the parameter name passed to the controller's init function :(
        Factory: function ($http) {
            var factory = {};

            factory.getSponsorsList = function () { return $http.get("/api/Data/Sponsors"); };
            return factory;
        }
    };
})()