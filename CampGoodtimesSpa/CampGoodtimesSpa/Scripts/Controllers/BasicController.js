
// BasicController is shared by multiple pages. The pages that share
// this controller don't consume any data and have simple logic needs.
var BasicController = (function () {

    function init(scope) {
        
    }

    return {
        Name: 'BasicController',
        Controller: function ($scope) {
            init($scope);
        }
    };

})();