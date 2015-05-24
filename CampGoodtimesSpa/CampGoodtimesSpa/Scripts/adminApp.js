var adminApp = angular.module('GoodtimesAdmin', ['angularFileUpload','ngRoute', 'ngAnimate', 'ngSanitize']);

adminApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', { templateUrl: '/PartialViews/Admin/adminlandingpage.html' })
        .when('/images', { templateUrl: '/PartialViews/Admin/images.html' })
        .when('/news', { templateUrl: '/PartialViews/Admin/news.html' })
        .when('/news/edit', { templateUrl: '/PartialViews/Admin/newsedit.html' })
        .when('/camps', { templateUrl: 'PartialViews/Admin/camps.html' })
        .otherwise({ redirectTo: '/' });
});

adminApp.controller('adminPageController', ['$http', '$scope', '$location', function ($http, $scope, $location) {
    $scope.knownSignInState = false;
    $scope.toolImages = function () { $location.path('/images'); }
    $scope.toolNews = function () { $location.path('/news'); }
    $scope.toolCamps = function () { $location.path('/camps'); }
    $scope.toolHome = function () { $location.path('/'); };
}]);

adminApp.controller('newsArticleController', ['$http', '$scope', '$location', function ($http, $scope, $location) {
    $scope.existingArticles = [];
    $scope.article = {}
    $scope.status = '';

    $scope.addNew = function ()
    {
        $location.path('/news/edit');
    }

    $scope.submitArticle = function(article)
    {
        if (article.id != null)
        {
            // this is an edit so do a put
        }
        else
        {
            // post
            $http({
                url: '/api/data/postnews',
                method: 'POST',
                data:article})
                .success(function (data, status, headers, config) {
                    $scope.status = status;
                })
                .error(function (data, status, headers, config) {

                });
        }

    }

    $scope.cancelEdit = function()
    {
        $location.path('/news');
    }

    $scope.doneEdit = function()
    {
        $location.path('/news');
    }
}]);

adminApp.controller('fileManagerController', ['$http', '$scope', '$upload', '$timeout', function ($http, $scope, $upload, $timeout) {

    $scope.helpText = 'Select a file(s) to upload to the site.'
    $scope.uploadResultMessages = null;
    $scope.imageLibraryFiles = null;
    $scope.imageEntries = null;

    var recursiveLoadSubFolders = function(resultList, entry)
    {
        $http.get('/api/images/GetFolderContents',
            {
                params:{
                    atPath:entry.Path
                }
            })
            .success(function (data, status, headers, config) {
                for (var i = 0; i < data.Items.length; i++)
                {
                    if (!data.Items[i].IsFolder) {
                        resultList.push(data.Items[i].Path);
                    }
                    else {
                        recursiveLoadSubFolders(resultList, data.Items[i]);
                    }
                }
            })
            .error(function(data, status, headers, config){
            });
    }

    var reset = function () {
        $scope.imageEntries = [];
        $http.get('/api/images/GetRootFolder')
            .success(function (data, status, headers, config) {
                var entries = [];
                for (var i = 0; i < data.Items.length; i++)
                {
                    if (!data.Items[i].IsFolder) {
                        entries.push(data.Items[i].Path);
                    }
                    else {
                        recursiveLoadSubFolders(entries, data.Items[i]);
                    }
                }
                $scope.imageEntries = entries;
            })
            .error(function(data, status, headers, config){
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

    $scope.onFileSelect = function(files)
    {
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

    $scope.deleteImage = function (path)
    {
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

adminApp.directive("adminNavigationDirective", ['$http', function ($http) {
    return {
        restrict: 'A',
        templateUrl: '/PartialViews/Admin/adminNavigationControls.html',
        link: function (scope, element) {
        }
    }
}]);

adminApp.directive("signInDirective", ['$http', function ($http) {

    return {
        restrict: 'A',
        templateUrl: '/PartialViews/Admin/signInControls.html',
        link: function (scope, element) {

            var userSignedIn = function (userName) {
                scope.isSignedIn = true;
                scope.userName = userName;
                scope.knownSignInState = true;
            }

            var userSignedOut = function () {
                scope.isSignedIn = false;
                scope.userName = null;
                scope.user = {
                    name: null,
                    password: null
                };
                scope.knownSignInState = true;
            }

            var reset = function () {
                scope.knownSignInState = false;

                $http.get('/api/auth/IsUserSignedIn')
                  .success(function (data, status) {
                      if (status != 200) { userSignedOut(); }
                      else { userSignedIn(data); }
                  })
                  .error(function (data, status, headers, config) { userSignedOut(); });
            }

            scope.signIn = function (user) {
                $http.post('/api/auth/SignUserIn', { 'UserName': user.name, 'Password': user.password })
                     .success(function (data, status) {
                         if (status == 200) {
                             userSignedIn(data);
                         }
                     })
                      .error(function () {
                          userSignedOut();
                      });
            }

            scope.signOut = function () {
                $http.get('/api/auth/SignUserOut')
                  .success(function (data, status) {
                      if (status == 200) {
                          userSignedOut();
                      }
                  });
            }

            reset();
        }
    };

}]);
