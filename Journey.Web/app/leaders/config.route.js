(function () {
    'use strict';

    angular
        .module('app.leaders')
        .run(run);

    run.$inject = ['routehelper'];

    function run(routehelper) {
        routehelper.configureRoutes(getRoutes());
    }

    function getRoutes() {
        return [
            {
                url: '/Leaders',
                config: {
                    templateUrl: 'app/leaders/leadersIndex.html',
                    controller: 'leadersIndexController',
                    controllerAs: 'vm',
                    title: 'leaders'
                }
            },
            {
                url: '/Leaders/:id',
                config: {
                    templateUrl: 'app/leaders/leadersEdit.html',
                    controller: 'leadersEditController',
                    controllerAs: 'vm',
                    title: 'leaders edit'
                }
            }
        ];
    }
})();