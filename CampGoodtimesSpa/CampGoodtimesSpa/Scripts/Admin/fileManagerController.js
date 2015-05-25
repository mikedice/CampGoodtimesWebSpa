window.goodtimes.adminApp.controller('fileManagerController', ['$http', '$scope', '$upload', '$timeout', function ($http, $scope, $upload, $timeout) {

    $scope.helpText = 'Select a file(s) to upload to the site.'
    $scope.uploadResultMessages = null;
    $scope.imageLibraryFiles = null;
    $scope.imageEntries = null;

    var recursiveLoadSubFolders = function (resultList, entry) {
        $http.get('/api/images/GetFolderContents',
            {
                params: {
                    atPath: entry.Path
                }
            })
            .success(function (data, status, headers, config) {
                for (var i = 0; i < data.Items.length; i++) {
                    if (!data.Items[i].IsFolder) {
                        resultList.push(data.Items[i].Path);
                    }
                    else {
                        recursiveLoadSubFolders(resultList, data.Items[i]);
                    }
                }
            })
            .error(function (data, status, headers, config) {
            });
    }

    var reset = function () {
        $scope.imageEntries = [];
        $http.get('/api/images/GetRootFolder')
            .success(function (data, status, headers, config) {
                var entries = [];
                for (var i = 0; i < data.Items.length; i++) {
                    if (!data.Items[i].IsFolder) {
                        entries.push(data.Items[i].Path);
                    }
                    else {
                        recursiveLoadSubFolders(entries, data.Items[i]);
                    }
                }
                $scope.imageEntries = entries;
            })
            .error(function (data, status, headers, config) {
            });
    }

    var resetForm = function () {
        var frm = $('#image-upload-form')[0];
        if (frm != 'undefined' && frm != null) {
            frm.reset();
        }
    }

    var checkFinishedUploading = function (files, myUploadAttempts) {
        if (files.length == myUploadAttempts.length) {
            $timeout(function () {
                $scope.uploadResultMessages = myUploadAttempts;
            }, 5, true);
            resetForm();
            reset();
        }
    }

    $scope.onFileSelect = function (files) {
        var uploadAttempts = [];
        if (files) {
            for (var i = 0; i < files.length; i++) {
                var uploadUrl = '/api/images/UploadImage/asd'
                var currentFile = files[i];

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

    $scope.deleteImage = function (path) {
        $http({
            method: 'DELETE',
            params: {
                atPath: path
            },
            url: '/api/images/Delete'
        })
        .success(function (data, status, headers, config) {
            reset();
        })
        .error(function (data, status, headers, config) {

        });
    }

    reset();
}]);