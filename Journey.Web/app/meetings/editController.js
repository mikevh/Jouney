(function() {
    'use strict';

    var app = angular.module('app');

    app.controller('editController', function ($scope, $state, $stateParams, MeetingData, CommunityGroupData, AttendeeData) {

        $scope.e = {
            date: new Date()
        };

        var getMeeting = function(id) {
            MeetingData.get(id).then(function (result) {
                result.data.date = new Date(result.data.date);
                $scope.e = result.data;
            });
        };

        var getAttendees = function() {
            AttendeeData.all().then(function(result) {
                $scope.attendees = result.data;
            });
        };

        $scope.save = function () {
            MeetingData.save($scope.e).then(function(ok) {
                $state.go('index');
            });
        };

        getAttendees();
        getMeeting($stateParams.id);
    });
})();