(function () {
    'use strict';

    angular
        .module('app.attendees')
        .controller('attendeesIndexController', controller);

    controller.$inject = ['$route', 'attendeesService'];

    function controller($route, attendeesService) {
        var vm = this;
        vm.remove = remove;

        activate();

        function activate() {
            attendeesService.all().then(function (result) {
                vm.attendees = result.data;
            });
        }

        function remove(a) {
            attendeesService.remove(a.id).then(function (result) {
                $route.reload();
            });
        }
    }
})();
