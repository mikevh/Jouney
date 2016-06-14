(function () {
    'use strict';

    angular
        .module('app.users')
        .run(run);

    run.$inject = ['routehelper'];

    function run(routehelper) {
        routehelper.configureRoutes(getRoutes());
    }

    function getRoutes() {
        return [
            {
                url: '/Users',
                config: {
                    templateUrl: 'app/users/index.html',
                    controller: 'usersIndexController',
                    controllerAs: 'vm',
                    title: 'users'
                }
            },
            //{
            //    url: '/Users/:id',
            //    config: {
            //        templateUrl: 'app/users/edit.html',
            //        controller: 'usersEditController',
            //        controllerAs: 'vm',
            //        title: 'users edit'
            //    }
            //}
        ];
    }
})();