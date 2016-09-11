(function () {
    'use strict';

    angular
        .module('app.attendees')
        .controller('attendeesEditController', controller);

    controller.$inject = ['$location', '$routeParams', 'attendeesService', 'communityGroupService'];

    function controller($location, $routeParams, attendeesService, communityGroupService) {
        var vm = this;
        vm.save = save;
        vm.cancel = cancel;

        activate();

        function activate() {
            var edit = $routeParams.id && $routeParams.id != 0;
            if (edit) {
                attendeesService.get($routeParams.id).then(function (result) {
                    vm.e = result.data;
                    if (vm.e.communityGroupId == null) {
                        vm.e.communityGroupId = 0;
                    }
                });

                communityGroupService.all().then(function(result) {
                    result.data.unshift({ id: 0, name: '--' });
                    vm.groups = result.data;
                });
            } else {
                vm.e = {
                    email: ''
                };
            }
        }

        function save() {
            attendeesService.save(vm.e).then(function (result) {
                $location.path('/Attendees');
            });
        }

        function cancel() {
            $location.path('/Attendees');
        }
    }
})();
