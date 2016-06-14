(function() {
    'use strict';

    angular
        .module('app', [
            'ngAnimate',
            'ui.bootstrap',

            'app.core',
            'app.dashboard',
            'app.layout',
            'app.leaders',
            'app.communitygroups',
            'app.attendees',
            'app.meetings',
            'app.users'
        ]);
})();
