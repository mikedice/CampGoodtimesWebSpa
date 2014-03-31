var WhatFactory = (function () {
    return {
        Name: 'whatFactory', // factory names have to match exactly the parameter name passed to the controller's init function :(
        Factory: function ($http) {
            var factory = {};

            factory.getCamps = function () { return $http.get("/api/Data/Camps"); };
            return factory;
        }
    };
})();