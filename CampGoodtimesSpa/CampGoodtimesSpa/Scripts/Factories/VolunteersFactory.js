var VolunteersFactory = (function () {

    return {
        Name: 'volunteerFactory', // factory names have to match exactly the parameter name passed to the controller's init function :(
        Factory: function ($http) {
            var factory = {};

            factory.getVolunteers = function () { return $http.get("/api/Data/Volunteers"); };
            return factory;
        }
    };
})();