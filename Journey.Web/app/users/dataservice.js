(function () {
    'use strict';

    angular
        .module('app.users')
        .factory('usersService', factory);

    factory.$inject = ['rest', '$http'];

    function factory(rest, $http) {
        var api = '/api/account1';
        var service = rest.api(api);

        service.roles = roles;

        return service;

        function roles() {
            return $http.get(api + '/roles');
        }

    }
})();