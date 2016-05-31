(function() {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('dashboardController', dashboard);

    dashboard.$inject = ['$q', 'logger'];

    function dashboard($q, logger) {
        var vm = this;

        activate();

        function activate() {
            
        }
    }
})()