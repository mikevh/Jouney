(function() {
    'use strict';

    angular
        .module('blocks.exception')
        .factory('exception', factory);

    factory.$inject = ['logger'];

    function factory(logger) {
        var service = {
            catcher: catcher
        };

        return service;

        function catcher(message) {
            return function(reason) {
                logger.error(message, reason);
            };
        }
    }
})()