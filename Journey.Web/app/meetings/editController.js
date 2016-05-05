(function() {
    'use strict';

    var app = angular.module('app');

    app.controller('editController', function ($scope, $stateParams, MeetingData, CommunityGroupData) {

        var getMeeting = function(id) {
            MeetingData.get(id).then(function(result) {
                $scope.e = result.data;
            });
        };

        var getCommunityGroups = function() {
            CommunityGroupData.all().then(function(result) {
                $scope.communityGroups = result.data;
            });
        };

        getCommunityGroups();
        getMeeting($stateParams.id);
    });
})();