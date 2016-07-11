(function() {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('dashboardController', dashboard);

    dashboard.$inject = ['$q', 'logger', '$location'];

    function dashboard($q, logger, $location) {
        var vm = this;

        activate();

        function activate() {
            $location.path('/CommunityGroups');
        }
    }
})()