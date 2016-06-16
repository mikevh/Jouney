(function () {
    'use strict';

    angular
        .module('app.leaders')
        .controller('leadersEditController', controller);

    controller.$inject = ['$location', '$routeParams', 'leadersService', 'usersService', 'logger'];

    function controller($location, $routeParams, leadersService, usersService, logger) {
        var vm = this;
        vm.save = save;
        vm.cancel = cancel;

        activate();

        function activate() {
            getRoles();
            var edit = $routeParams.id && $routeParams.id != 0;
            if (edit) {
                leadersService.get($routeParams.id).then(function(result) {
                    vm.e = result.data;
                });
            } else {
                vm.e = {
                    email: ''
                };
            }
        }

        function save() {
            leadersService.save(vm.e).then(function(result) {
                $location.path('/Leaders');
            });
        }

        function cancel() {
            $location.path('/Leaders');
        }
    }
})();
