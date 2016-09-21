(function () {
    'use strict';

    angular
        .module('app.meetings')
        .factory('meetingsService', service);

    service.$inject = ['rest', '$http', 'logger'];

    function service(rest, $http, logger) {
        
        var api = '/api/meetings1';
        var service = rest.api(api);

        service.meetingsForGroup = function(id) {
            return $http.get(api + '/meetingsForGroup/' + id);
        };

        return service;
    }

})();