﻿(function() {
    'use strict';

    var app = angular.module('app');
    
    app.config(function ($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise('/');

        $stateProvider.state('index', {
            url: '/',
            templateUrl: 'app/meetings/indexTemplate.html',
            controller: 'indexController'
        }).state('edit', {
            url: '/edit/:id',
            templateUrl: 'app/meetings/editTemplate.html',
            controller: 'editController'
        });
    });

})();
