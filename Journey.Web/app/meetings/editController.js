(function() {
    'use strict';

    angular
        .module('app.meetings')
        .controller('meetingsEditController', controller);

    controller.$inject = ['$routeParams', '$location', 'meetingsService', 'attendeesService', 'communityGroupService'];

    function controller($routeParams, $location, meetingsService, attendeesService, communityGroupService) {
        var vm = this;

        vm.save = save;
        vm.cancel = cancel;
        vm.attendees = [];
        vm.e = {
            attendees: [],
            date: new Date()
        };
        vm.dateOptions = {
            showWeeks: false,
            formatDay: 'd'
        };
        vm.addIsDisabled = addIsDisabled;
        vm.removeIsDisabled = removeIsDisabled;
        vm.add = add;
        vm.remove = remove;
        vm.alreadySelected = alreadySelected;
        vm.display = display;

        activate();

        function activate() {
            var edit = !!$routeParams.id && $routeParams.id != 0;
            attendeesService.all().then(function (result) {
                vm.attendees = result.data;
            });

            if (edit) {
                meetingsService.get($routeParams.id).then(function (result) {
                    result.data.date = new Date(result.data.date);
                    vm.e = result.data;
                    loadCommunityGroup(vm.e.communityGroupId);
                });

            }
            else if (!!$routeParams.communityGroupId) {
                vm.e.communityGroupId = $routeParams.communityGroupId;
                loadCommunityGroup($routeParams.communityGroupId);
            }
        }

        function loadCommunityGroup(id) {
            communityGroupService.get(id).then(function(result) {
                vm.communityGroup = result.data;
            });
        }

        function display(o) {
            if (o.id > 0) {
                return o.name;
            }
            return 'NEW: ' + o.name;
        }

        function alreadySelected(x) {
            return _.find(vm.e.attendees, function(y) { return x === y; }) === undefined;
        }

        function add() {
            if (vm.attendee) {
                if (!vm.attendee.id) {
                    vm.attendee = {
                        id: 0,
                        name: vm.attendee
                    }
                }
                vm.e.attendees.push(vm.attendee);
                vm.attendee = undefined;
            }
        }

        function remove() {
            angular.forEach(vm.selectedAttendees, function(x) {
                _.remove(vm.e.attendees, function(y) {
                    return x === y;
                });
            });
            vm.selectedAttendees = undefined;
        }

        function addIsDisabled() {
            return !!!vm.attendee;
        }

        function removeIsDisabled() {
            return !!!vm.selectedAttendees;
        }

        function save () {
            meetingsService.save(vm.e).then(function () {
                $location.path('/Meetings');
            });
        };

        function cancel() {
            $location.path('/Meetings');
        };
    }
})();