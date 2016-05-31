(function () {
    'use strict';

    angular
        .module('app.attendees')
        .controller('attendeesEditController', controller);

    controller.$inject = ['$location', '$routeParams', 'attendeesService'];

    function controller($location, $routeParams, attendeesService) {
        var vm = this;
        vm.save = save;
        vm.cancel = cancel;

        activate();

        function activate() {
            var edit = $routeParams.id && $routeParams.id != 0;
            if (edit) {
                attendeesService.get($routeParams.id).then(function (result) {
                    vm.e = result.data;
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
