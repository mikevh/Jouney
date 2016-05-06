(function() {
    'use strict';

    var app = angular.module('app');

    app.controller('editController', function ($scope, $state, $stateParams, MeetingData, CommunityGroupData) {

        $scope.e = {
            date: new Date()
        };

        var getMeeting = function(id) {
            MeetingData.get(id).then(function (result) {
                result.data.date = new Date(result.data.date);
                $scope.e = result.data;
            });
        };

        var getCommunityGroups = function() {
            CommunityGroupData.all().then(function(result) {
                $scope.communityGroups = result.data;
            });
        };

        $scope.save = function () {
            MeetingData.save($scope.e).then(function(ok) {
                $state.go('index');
            });
        };

        getCommunityGroups();
        getMeeting($stateParams.id);
    });
})();