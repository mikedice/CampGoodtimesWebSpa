﻿window.goodtimes.adminApp.controller('peopleEditController', ['$http', '$scope', '$location', '$routeParams', function ($http, $scope, $location, $routeParams) {
    $scope.person = {}
    $scope.status = '';
    $scope.cancelEdit = function () { $location.path('/people'); }
    $scope.editDone = function () { $location.path('/people'); }

    var reset = function () {
        if ($routeParams.personId != null) {
            $http({
                method: 'GET',
                url: '/api/data/getperson/' + $routeParams.personId
            })
             .success(function (data, status, headers, config) {
                 $scope.person = data;
             })
             .error(function (data, status, headers, config) {
             });
        }
    }

    $scope.submitPerson = function (person) {
        if (person.Id != null) {
            // put
            $http({
                url: '/api/data/putperson',
                method: 'PUT',
                data: person
            })
            .success(function (data, status, headers, config) {
                $scope.status = status;
                $location.path('/people');
            })
            .error(function (data, status, headers, config) {
                $scope.status = 'An error occurred (status code: ' + status + ')';
            });
        }
        else {
            // post
            $http({
                url: '/api/data/postPerson',
                method: 'POST',
                data: person
            })
            .success(function (data, status, headers, config) {
                $scope.status = status;
                $location.path('/people');
            })
            .error(function (data, status, headers, config) {
                $scope.status = 'An error occurred (status code: ' + status + ')';
            });
        }
    }

    reset();
}]);
