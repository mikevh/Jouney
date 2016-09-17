(function() {
    'use strict';

    angular
        .module('app', [
            'app.core',
            'app.dashboard',
            'app.layout',
            'app.leaders',
            'app.communitygroups',
            'app.attendees',
            'app.meetings',
            'ui.bootstrap',
            'angular-loading-bar'
        ]);
})();
