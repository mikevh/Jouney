(function() {
    'use strict';

    var app = angular.module('app', ['ngAnimate', 'ui.router', 'ui.bootstrap']);

    app.factory('REST', function($http) {
        var resource = function (api) {
            
            var all = function () {
                return $http.get(api);
            };

            var get = function (id) {
                return $http.get(api + '/' + id);
            };

            var save = function (obj) {
                if (obj.id) {
                    return $http.put(api + '/' + obj.id, obj);
                }
                return $http.post(api, obj);
            };

            return {
                all: all,
                get: get,
                save: save
            };
        };
        return {
            api: resource
        };
    });

    app.factory('MeetingData', function (REST) {
        return REST.api('/api/meetings1');
    });

    app.factory('CommunityGroupData', function (REST) {
        return REST.api('/api/communitygroups1');
    });

    app.factory('AttendeeData', function (REST) {
        return REST.api('/api/attendees1');
    });
    
})();