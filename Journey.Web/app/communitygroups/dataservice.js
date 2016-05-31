(function () {
    'use strict';

    angular
        .module('app.communitygroups')
        .factory('communityGroupService', service);

    service.$inject = ['rest', '$http'];

    function service(rest, $http) {
        var api = '/api/communitygroups1';

        var service = rest.api(api);

        service.latest = function (id) {
            return $http.get(api + '/latest/' + id);
        };

        return service;
    }
})();