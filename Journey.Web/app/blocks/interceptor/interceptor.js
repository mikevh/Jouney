(function () {
    'use strict';

    angular
        .module('blocks.interceptor')
        .config(httpInterceptor);

    httpInterceptor.$inject = ['$provide', '$httpProvider', '$windowProvider'];

    function httpInterceptor($provide, $httpProvider, $windowProvider) {

        $provide.factory('httpInterceptor', interceptor);
        $httpProvider.interceptors.push('httpInterceptor');
        
        function interceptor ($q) {
            return {
                response: response,
                responseError: responseError
            };

            function response(resp) {
                return resp || $q.when(resp);
            }

            function responseError (rejection) {
                if (rejection.status === 401) {
                    var wnd = $windowProvider.$get();
                    
                    wnd.location.href = '/Account/Login';
                }
                return $q.reject(rejection);
            }
        }
    }
})();