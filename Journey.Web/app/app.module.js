(function() {
    'use strict';

    angular
        .module('app', [
            'ngAnimate',
            'ui.bootstrap',
            'angular-loading-bar',
            'app.core',
            'app.dashboard',
            'app.layout',
            'app.leaders',
            'app.communitygroups',
            'app.attendees',
            'app.meetings'
        ]);
})();
