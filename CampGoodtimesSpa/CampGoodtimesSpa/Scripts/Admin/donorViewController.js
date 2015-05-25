window.goodtimes.adminApp.controller('donorViewController', ['$http', '$scope', '$location', '$route', '$upload','$timeout', function ($http, $scope, $location, $route, $upload, $timeout) {
    $scope.donors = [];
    var reset = function () {
        $scope.donors = [];
        $http({ method: 'GET', url: '/api/data/getdonors' })
        .success(function (data, status, headers, config) {
            $scope.donors = data;
        })
        .error(function (data, status, headers, config) {
        });
    }

    $scope.editDonor = function (donorId) { $location.path('/donors/edit/' + donorId); }
    $scope.addNewDonor = function () { $location.path('/donors/edit'); }
    $scope.deleteDonor = function (donorId, donorName) {
        var r = window.confirm("Really? Delete donor '" + donorName + "'?");
        if (r == true) {
            $http({
                method: 'DELETE',
                url: '/api/data/deletedonor/' + donorId
            })
            .success(function (data, status, headers, config) {
                $route.reload();
            })
            .error(function (data, status, headers, config) {
                window.alert("error " + status + " deleting donor");
            });
        }
    }


    var resetForm = function () {
        var frm = $('#donors-upload-form')[0];
        if (frm != 'undefined' && frm != null) {
            frm.reset();
        }
    }

    var checkFinishedUploading = function (files, myUploadAttempts) {
        if (files.length == myUploadAttempts.length) {
            $timeout(function () {
                $scope.uploadResultMessages = myUploadAttempts;
                reset();
                resetForm();
            }, 5, true);
        }
    }

    $scope.onFileSelect = function (files) {
        var uploadAttempts = [];
        if (files) {
            for (var i = 0; i < files.length; i++) {
                var uploadUrl = '/api/data/uploaddonorcsv/asd'

                $upload.upload({
                    method: 'PUT',
                    url: uploadUrl,
                    file: files[i],
                })
                .progress(function (evt) {
                })
                .success(function (data, status, headers, config) {
                    uploadAttempts.push({ result: 'success', filename: config.file.name });
                    checkFinishedUploading(files, uploadAttempts);
                })
                .error(function (data, status, headers, config) {
                    uploadAttempts.push({ result: 'failed', filename: config.file.name });
                    checkFinishedUploading(files, uploadAttempts);
                });
            }
        }
    }

    reset();
}]);

