(function () {
    'use strict';

    angular
        .module('app.meetings')
        .factory('meetingsService', factory);

    factory.$inject = ['rest'];

    function factory(rest) {
        return rest.api('/api/meetings1');
    }
})();