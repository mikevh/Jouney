(function() {
    'use strict';

    var app = angular.module('app');

    app.controller('editController', function ($scope, $state, $stateParams, MeetingData, AttendeeData) {
        MeetingData.get($stateParams.id).then(function (result) {
            result.data.date = new Date(result.data.date);
            $scope.e = result.data;
        });

        AttendeeData.all().then(function(result) {
            $scope.attendees = result.data;
        });

        $scope.save = function () {
            MeetingData.save($scope.e).then(function(ok) {
                $state.go('index');
            });
        };

        $scope.cancel = function() {
            $state.go('index');
        };
    });
})();