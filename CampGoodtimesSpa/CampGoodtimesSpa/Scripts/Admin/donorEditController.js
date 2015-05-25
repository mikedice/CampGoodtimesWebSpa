window.goodtimes.adminApp.controller('donorEditController', ['$http', '$scope', '$location', '$routeParams', function ($http, $scope, $location, $routeParams) {
    $scope.donor = {}
    $scope.status = '';
    $scope.cancelEdit = function () { $location.path('/donors'); }
    $scope.editDone = function () { $location.path('/donors'); }

    var reset = function () {
        if ($routeParams.donorId != null) {
            $http({
                method: 'GET',
                url: '/api/data/getdonor/' + $routeParams.donorId
            })
             .success(function (data, status, headers, config) {
                 $scope.donor = data;
             })
             .error(function (data, status, headers, config) {
             });
        }
    }

    $scope.submitDonor = function (donor) {
        if (donor.Id != null) {
            // put
            $http({
                url: '/api/data/putdonor',
                method: 'PUT',
                data: donor
            })
            .success(function (data, status, headers, config) {
                $scope.status = status;
                $location.path('/donors');
            })
            .error(function (data, status, headers, config) {
                $scope.status = 'An error occurred (status code: ' + status + ')';
            });
        }
        else {
            // post
            $http({
                url: '/api/data/postdonor',
                method: 'POST',
                data: donor
            })
            .success(function (data, status, headers, config) {
                $scope.status = status;
                $location.path('/donors');
            })
            .error(function (data, status, headers, config) {
                $scope.status = 'An error occurred (status code: ' + status + ')';
            });
        }
    }

    reset();
}]);