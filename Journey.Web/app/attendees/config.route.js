(function () {
    'use strict';

    angular
        .module('app.attendees')
        .run(run);

    run.$inject = ['routehelper'];

    function run(routehelper) {
        routehelper.configureRoutes(getRoutes());
    }

    function getRoutes() {
        return [
            {
                url: '/Attendees',
                config: {
                    templateUrl: 'app/attendees/index.html',
                    controller: 'attendeesIndexController',
                    controllerAs: 'vm'
                }
            },
            {
                url: '/Attendees/:id',
                config: {
                    templateUrl: 'app/attendees/edit.html',
                    controller: 'attendeesEditController',
                    controllerAs: 'vm'
                }
            }
        ];
    }
})();