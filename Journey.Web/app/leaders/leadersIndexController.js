(function () {
    'use strict';

    angular
        .module('app.leaders')
        .controller('leadersIndexController', leaders);

    function leaders($location, $route, leadersService, $q, logger) {
        var vm = this;
        vm.remove = remove;

        activate();

        function activate() {
            leadersService.all().then(function(result) {
                vm.leaders = result.data;
            });
        }

        function remove(l) {
            leadersService.remove(l.id).then(function(result) {
                $route.reload();
            }, function(error) {
                logger.error(error.data.exceptionMessage);
            });
        }
    }
})();
