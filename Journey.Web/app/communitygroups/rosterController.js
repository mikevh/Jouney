(function () {
    'use strict';

    angular
        .module('app.communitygroups')
        .controller('cgRosterController', controller);

    function controller($route, $location, communityGroupService, meetingsService, $routeParams, $q, logger, _) {
        var vm = this;
        vm.communityGroupId = parseInt($routeParams.id) || 0;
        vm.attended = attended;
        vm.process = process;

        activate(vm);

        function activate(vm) {
            communityGroupService.get(vm.communityGroupId).then(function(result) {
                vm.g = result.data;
            });

            var meetings_p = meetingsService.meetingsForGroup(vm.communityGroupId).then(function (result) {
                vm.meetings = result.data;
            });

            var members_p = communityGroupService.groupMembers(vm.communityGroupId).then(function (result) {
                vm.members = result.data;
            });

            $q.all([meetings_p, members_p]).then(function() {
                vm.process();
            });
        }

        function process() {
            // get all attendees from each meeting
            var meetingAttendees = _.flatMap(vm.meetings, function (x) { return x.attendees });
            // concat those attendees with the group members
            var attendees = _.concat(meetingAttendees, vm.members);
            // dedupe
            attendees = _.uniqBy(attendees, 'id');
            // set isMemberOfThisGroup for sorting 
            attendees = _.forEach(attendees,
                function (x) {
                    x.isMemberOfThisGroup = x.communityGroupId === vm.communityGroupId;
                });
            // sort
            vm.attendees = _.orderBy(attendees, ['isMemberOfThisGroup', 'isMember', 'name'], ['desc', 'desc', 'asc']);
        }

        function attended(attendee, meeting) {
            return _.some(meeting.attendees,
                function (x) {
                    return attendee.id === x.id;
                });
        }
    }
})();
