(function () {
    'use strict';

    angular
        .module('app.users')
        .controller('usersIndexController', users);

    users.$inject = ['$location', '$route', 'usersService'];

    function users($location, $route, usersService) {
        var vm = this;
        vm.users = [];
        vm.roles = [];
        //vm.remove = remove;

        activate();

        function activate() {
            usersService.all().then(function (result) {
                vm.users = result.data;
            });
            
            usersService.roles().then(function(result) {
                vm.roles = result.data;
            });
        }

        //function remove(l) {
        //    usersService.remove(l.id).then(function (result) {
        //        $route.reload();
        //    });
        //}
    }
})();
