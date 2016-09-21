(function () {
    'use strict';

    angular
        .module('app.communitygroups')
        .controller('cgRosterController', controller);

    controller.$inject = ['$route', '$location', 'communityGroupService', 'meetingsService', '$routeParams'];

    function controller($route, $location, communityGroupService, meetingsService, $routeParams) {
        var vm = this;
        vm.attended = attended;

        activate();

        function attended(attendee, meeting) {
            return true;
        }

        function activate() {
            meetingsService.meetingsForGroup($routeParams.id).then(function (result) {
                vm.meetings = result.data;
            });
        }
    }
})();
