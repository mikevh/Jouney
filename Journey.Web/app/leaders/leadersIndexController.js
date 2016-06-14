(function () {
    'use strict';

    angular
        .module('app.leaders')
        .controller('leadersIndexController', leaders);

    leaders.$inject = ['$location', '$route', 'leadersService','usersService', '$q'];

    function leaders($location, $route, leadersService, usersService, $q) {
        var vm = this;
        vm.remove = remove;
        vm.isLeaderinRole = isLeaderinRole;

        activate();

        function activate() {
            var l = leadersService.all();
            var u = usersService.all();
            var r = usersService.roles();

            $q.all([l, u, r]).then(function(results) {
                vm.leaders = results[0].data;
                vm.users = results[1].data;
                vm.roles = results[2].data;
            });
        }

        function isLeaderinRole(l, r) {
            if (!vm.leaders || !vm.users || !vm.roles) return false;

            // look in vm.users to see if they have that role
            var user = _.find(vm.users, function (x) { return x.email === l.email });

            var rv = !!user && _.indexOf(user.roles, r) !== -1;
            console.log(l.email + ' is in role ' + r + ' = ' + rv);
            return rv;
        }

        function remove(l) {
            leadersService.remove(l.id).then(function(result) {
                $route.reload();
            });
        }
    }
})();
