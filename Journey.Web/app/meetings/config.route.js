(function() {
    'use strict';

    angular
        .module('app.meetings')
        .run(run);

    run.$inject = ['routehelper'];

    function run(routehelper) {
        routehelper.configureRoutes(getRoutes());
    }

    function getRoutes() {
        return [
            {
                url: '/Meetings',
                config: {
                    templateUrl: 'app/meetings/index.html',
                    controller: 'meetingsIndexController',
                    controllerAs: 'vm'
                }
            },
            {
                url: '/Meetings/:id',
                config: {
                    templateUrl: 'app/meetings/edit.html',
                    controller: 'meetingsEditController',
                    controllerAs: 'vm'
                }
            },
            {
                url: '/Meetings/ForGroup/:communityGroupId',
                config: {
                    templateUrl: 'app/meetings/edit.html',
                    controller: 'meetingsEditController',
                    controllerAs: 'vm'
                }
            }
        ];
    }
})();
