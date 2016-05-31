(function() {
    'use strict';

    angular
        .module('app')
        .controller('meetingsIndexController', controller);

    controller.$inject = ['meetingsService'];

    function controller(meetingsService) {

        var vm = this;

        activate();

        function activate() {
            meetingsService.all().then(function (result) {
                vm.meetings = result.data;
            });
        }
    }

})();
