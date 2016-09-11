(function () {
    'use strict';

    angular
        .module('app.leaders')
        .controller('leadersIndexController', leaders);

    leaders.$inject = ['$location', '$route', 'leadersService', '$q'];

    function leaders($location, $route, leadersService, $q) {
        var vm = this;
        vm.remove = remove;

        activate();

        function activate() {
            leadersService.all().then(function(result) {
                vm.leaders = result.data;
            });
        }

        //function remove(l) {
        //    leadersService.remove(l.id).then(function(result) {
        //        $route.reload();
        //    });
        //}
    }
})();
