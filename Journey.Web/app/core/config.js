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

    function configure($logProvider, $routeProvider, $httpProvider, routehelperConfigProvider, exceptionHandlerProvider) {
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }

        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }

        $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';

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