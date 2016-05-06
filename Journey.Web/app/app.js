(function() {
    'use strict';

    var app = angular.module('app', ['ui.router']);

    app.factory('MeetingData', function($http) {
        var api = '/api/meetings1';
        var all = function() {
            return $http.get(api);
        };

        var get = function(id) {
            return $http.get(api + '/' + id);
        };

        return {
            all: all,
            get: get
        };
    });

    app.factory('CommunityGroupData', function ($http) {
        var api = '/api/communitygroups1';
        var all = function() {
            return $http.get(api);
        };

        var get = function(id) {
            return $http.get(api + '/id');
        };

        return {
            all: all,
            get: get
        };
    });

    

})();