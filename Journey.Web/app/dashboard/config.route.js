(function() {
    'use strict';

    angular
        .module('app.dashboard')
        .run(run);

    run.$inject = ['routehelper'];

    function run(routehelper) {
        routehelper.configureRoutes(getRoutes());
    }

    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: 'app/dashboard/dashboard.html',
                    controller: 'dashboardController',
                    controllerAs: 'vm',
                    title: 'dashboard',
                    settings: {
                        nav: 1,
                        content: '<b>dashboard</b>'
                    }
                }
            }
        ]
    }
})();