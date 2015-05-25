window.goodtimes.adminApp.directive("signInDirective", ['$http', function ($http) {

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