(function () {
    'use strict';

    angular
        .module('app.attendees')
        .factory('attendeesService', factory);

    factory.$inject = ['rest'];

    function factory(rest) {
        return rest.api('/api/attendees1');
    }
})();