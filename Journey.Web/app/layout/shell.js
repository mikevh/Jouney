(function() {
    'use strict';

    angular
        .module('app.layout')
        .controller('Shell', shell);

    shell.$inject = ['$timeout', 'config', 'logger'];

    function shell($timeout, config, logger) {
        var vm = this;

        vm.title = config.appTitle;

        activate();

        function activate() {
            
        }
    }
})()