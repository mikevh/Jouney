(function() {
    'use strict';

    var core = angular.module('app.core');

    var config = {
        appErrorPrefix: '[journey web error] ',
        appTitle: 'Journey Groups',
        version: '1.0.0'
    };

    core.value('config', config);
    core.config(configure);

    function configure($logProvider, $routeProvider, routehelperConfigProvider, exceptionHandlerProvider) {
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }

        routehelperConfigProvider.config.$routeProvider = $routeProvider;
        routehelperConfigProvider.config.docTitle = 'Journey: ';
        var resolveAlways = {
            ready: function(dataservice) {
                return dataservice.ready();
            }
        }
        routehelperConfigProvider.config.resolveAlways = resolveAlways;
        exceptionHandlerProvider.configure(config.appErrorPrefix);
    }
})()