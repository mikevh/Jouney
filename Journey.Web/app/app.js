(function() {
    'use strict';

    var app = angular.module('app', ['ngAnimate', 'ui.router', 'ui.bootstrap']);

    app.factory('MeetingData', function($http) {
        var api = '/api/meetings1';
        var all = function() {
            return $http.get(api);
        };

        var get = function(id) {
            return $http.get(api + '/' + id);
        };

        var save = function(meeting) {
            if (meeting.id) {
                return $http.put(api + '/' + meeting.id, meeting);
            }
            return $http.post(api, meeting);
        };

        return {
            all: all,
            get: get,
            save: save
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

    app.factory('AttendeeData', function ($http) {
        var api = '/api/attendees1';
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