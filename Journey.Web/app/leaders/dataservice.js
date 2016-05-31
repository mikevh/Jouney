(function () {
    'use strict';

    angular
        .module('app.leaders')
        .factory('leadersService', factory);

    factory.$inject = ['rest'];

    function factory(rest) {
        return rest.api('/api/leaders1');
    }
})();