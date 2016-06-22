(function () {
    'use strict';

    angular
        .module('app.core')
        .run(run);

    run.$inject = ['$http', '$rootScope'];

    function run($http, $rootScope) {

        $http.get('/api/whoami').then(function(result) {
            $rootScope.profile = result.data;
        });
    }

})();