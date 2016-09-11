(function () {
    'use strict';

    angular
        .module('app.attendees')
        .controller('attendeesIndexController', controller);

    controller.$inject = ['$route', 'attendeesService', 'communityGroupService'];

    function controller($route, attendeesService, communityGroupService) {
        var vm = this;
        vm.remove = remove;

        vm.clearFilter = function() {
            vm.filterCommunityGroupId = -1;
            vm.filterCommunityGroup();
        };

        vm.filterCommunityGroupId = -1;

        vm.filterCommunityGroup = function () {
            vm.attendees = _.filter(vm.all_attendees,
                function (x) {
                    if (vm.filterCommunityGroupId === -1) {
                        return true;
                    }
                    if (vm.filterCommunityGroupId === 0 && x.communityGroupId == null) {
                        return true;
                    }
                    return x.communityGroupId === vm.filterCommunityGroupId;
                });
        };

        activate();

        function activate() {

            attendeesService.all().then(function (result) {
                vm.all_attendees = result.data;
                vm.attendees = result.data;
            });

            communityGroupService.all().then(function (result) {
                result.data.unshift({ id: 0, name: 'No group' });
                result.data.unshift({ id: -1, name: '' });
                vm.groups = result.data;
            });
        }

        function remove(a) {
            attendeesService.remove(a.id).then(function (result) {
                $route.reload();
            });
        }
    }
})();
