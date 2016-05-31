(function() {
    'use strict';

    angular
        .module('app.communitygroups')
        .controller('cgEditController', controller);

    function controller($routeParams, $location, communityGroupService, leadersService) {
        var vm = this;
        vm.save = save;
        vm.cancel = cancel;

        activate();

        function activate() {
            var edit = $routeParams.id && $routeParams.id != 0;

            if (edit) {
                communityGroupService.get($routeParams.id).then(function(result) {
                    vm.e = result.data;
                });
            } else {
                vm.e = {
                    id: 0,
                    name: '',
                    leaderId: 0
                };
            }

            leadersService.all().then(function (result) {
                if (!edit) {
                    result.data.unshift({ id: 0, email: '--Select Name--' });
                }
                vm.leaders = result.data;
            });
        }

        function save() {
            communityGroupService.save(vm.e).then(function () {
                $location.path('/CommunityGroups');
            });
        }

        function cancel() {
            $location.path('/CommunityGroups');
        }
    }
})();
