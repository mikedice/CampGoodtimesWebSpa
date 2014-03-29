var DonorsFactory = (function () {

    return {
        Name: 'donorsFactory', // factory names have to match exactly the parameter name passed to the controller's init function :(
        Factory: function ($http) {
            var factory = {};

            factory.getDonorsList = function () { return $http.get("/Data/donors.json.txt"); };
            return factory;
        }
    };
})();