(function() {
    'use strict';

    angular
        .module('app')
        .controller('meetingsIndexController', controller);

    function controller(meetingsService, $route) {

        var vm = this;
        vm.remove = remove;

        activate();

        function activate() {
            meetingsService.all().then(function (result) {
                vm.meetings = result.data;
            });
        }

        function remove(m) {
            meetingsService.remove(m.id).then(function(result) {
                $route.reload();
            });
        }
    }

})();
