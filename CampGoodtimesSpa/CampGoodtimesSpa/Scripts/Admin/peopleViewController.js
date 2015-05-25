window.goodtimes.adminApp.controller('peopleViewController', ['$http', '$scope', '$location', '$route', function ($http, $scope, $location, $route) {

    $scope.editPerson = function (personId) { $location.path('/people/edit/' + personId); }
    $scope.deletePerson = function (personId, name) {
        var r = window.confirm("Really? Delete person '" + name + "'?");
        if (r == true) {
            $http({
                method: 'DELETE',
                url: '/api/data/deleteperson/' + personId
            })
            .success(function (data, status, headers, config) {
                $route.reload();
            })
            .error(function (data, status, headers, config) {
                window.alert("error " + status + " deleting article");
            });
        }

    }
    $scope.addNewPerson = function () { $location.path('/people/edit'); }

    var reset = function () {
        $scope.people = {
            employee: { Title: 'Employee', Data: [] },
            staff: { Title: 'Staff', Data: [] },
            volunteer: { Title: 'Volunteers', Data: [] },
            board: { Title: 'Board', Data: [] },
            sponsor: { Title: 'Sponsors', Data: [] }
        };

        $http({ method: 'GET', url: '/api/data/employee' })
        .success(function (data, status, headers, config) {
            $scope.people.employee.Data = data;
        })
        .error(function (data, status, headers, config) {

        });

        $http({ method: 'GET', url: '/api/data/sponsors' })
        .success(function (data, status, headers, config) {
            $scope.people.sponsor.Data = data;
        })
        .error(function (data, status, headers, config) {

        });

        $http({ method: 'GET', url: '/api/data/staff' })
        .success(function (data, status, headers, config) {
            $scope.people.staff.Data = data;
        })
        .error(function (data, status, headers, config) {

        });

        $http({ method: 'GET', url: '/api/data/board' })
        .success(function (data, status, headers, config) {
            $scope.people.board.Data = data;
        })
        .error(function (data, status, headers, config) {

        });

        $http({ method: 'GET', url: '/api/data/volunteers' })
        .success(function (data, status, headers, config) {
            $scope.people.volunteer.Data = data;
        })
        .error(function (data, status, headers, config) {

        });


    }

    reset()




}]);