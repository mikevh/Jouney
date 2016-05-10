(function() {
    'use strict';

    var app = angular.module('app');

    app.controller('indexController', function ($scope, MeetingData) {

        var refreshMeetings = function() {
            MeetingData.all().then(function(result) {
                $scope.meetings = result.data;
            });
        };

        refreshMeetings();
    });

})();
