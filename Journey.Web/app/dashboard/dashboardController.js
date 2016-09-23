(function() {
    'use strict';

    angular
        .module('app.dashboard')
        .controller('dashboardController', dashboard);

    dashboard.$inject = ['logger', '$location'];

    function dashboard(logger, $location) {
        var vm = this;

        activate();

        function activate() {
            $location.path('/CommunityGroups');
        }
    }
})()