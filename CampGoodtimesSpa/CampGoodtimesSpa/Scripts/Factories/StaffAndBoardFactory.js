var StaffAndBoardFactory = (function () {

    return {
        Name: 'staffAndBoardFactory', // factory names have to match exactly the parameter name passed to the controller's init function :(
        Factory: function ($http) {
            var factory = {};

            factory.getStaff = function () { return $http.get("/api/Data/Staff"); };
            factory.getBoard = function () { return $http.get("/api/Data/Board"); };
            return factory;
        }
    };
})();