/// <reference path="dataservice.js" />
(function () {
    'use strict';

    angular
        .module('app.communitygroups')
        .factory('communityGroupService', service);

    service.$inject = ['rest', '$http'];

    function service(rest, $http) {
        var api = '/api/communitygroups1';

        var service = rest.api(api);

        service.latestMeetingDate = function (id) {
            return $http.get(api + '/latestmeetingdate/' + id);
        };

        service.membershipCount = function(id) {
            return $http.get(api + '/membershipcount/' + id);
        };

        service.latestMeetingId = function(id) {
            return $http.get(api + '/latestmeetingid/forgroup/' + id);
        };

        return service;
    }
})();