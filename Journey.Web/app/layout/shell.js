(function() {
    'use strict';

    angular
        .module('app.layout')
        .controller('Shell', shell);

    shell.$inject = ['$timeout', 'config', 'logger', '$rootScope', '$http', '$window'];

    function shell($timeout, config, logger, $rootScope, $http, $window) {
        var vm = this;

        vm.logoff = logoff;
        vm.title = config.appTitle;

        activate();

        function activate() {
            $rootScope.$watch('profile', function(val,old) {
                if (!!val) {
                    vm.profile = val;
                }
            });
        }

        function logoff() {
            console.log('logoff()');
            $rootScope.profile = undefined;
            $http.post('/account/logoff').then(function () {
                console.log('out');
                $window.location.href = '/index.html';
            });
        }
    }
})()