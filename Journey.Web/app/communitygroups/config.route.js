(function() {
    'use strict';

    angular
        .module('app.communitygroups')
        .run(run);

    run.$inject = ['routehelper'];

    function run(routehelper) {
        routehelper.configureRoutes(getRoutes());
    }

    function getRoutes() {
        return [
            {
                url: '/CommunityGroups',
                config: {
                    templateUrl: 'app/communitygroups/index.html',
                    controller: 'cgIndexController',
                    controllerAs: 'vm'
                }
            },
            {
                url: '/CommunityGroups/:id',
                config: {
                    templateUrl: 'app/communitygroups/edit.html',
                    controller: 'cgEditController',
                    controllerAs: 'vm'
                }
            },
            {
                url: '/CommunityGroups/Roster/:id',
                config: {
                    templateUrl: 'app/communitygroups/roster.html',
                    controller: 'cgRosterController',
                    controllerAs: 'vm'
                }
            }
        ];
    }

})();
