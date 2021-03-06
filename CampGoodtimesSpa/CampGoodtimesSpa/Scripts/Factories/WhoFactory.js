﻿var WhoFactory = (function () {

    return {
        Name: 'whoFactory', // factory names have to match exactly the parameter name passed to the controller's init function :(
        Factory: function ($http) {
            var factory = {};

            factory.getDonorsList = function () { return $http.get("/Data/donors.json.txt"); };
            factory.getStaff = function () { return $http.get("/api/Data/Staff"); };
            factory.getBoard = function () { return $http.get("/api/Data/Board"); };
            factory.getVolunteers = function () { return $http.get("/api/Data/Volunteers"); };
            return factory;
        }
    };
})();