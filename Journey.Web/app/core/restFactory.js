(function (undefined) {

    angular
        .module('app.core')
        .factory('rest', factory);

    factory.$inject = ['$http'];

    function factory($http) {
        var service = {
            api: api
        };

        return service;

        function api(url) {
            var service = {
                all: all,
                get: get,
                save: save,
                remove: remove
            };

            return service;

            function all() {
                return $http.get(url);
            }

            function get(id) {
                return $http.get(url + '/' + id);
            }

            function save(obj) {
                if (obj.id) {
                    return $http.put(url + '/' + obj.id, obj);
                }
                return $http.post(url, obj);
            }

            function remove(id) {
                return $http.delete(url + '/' + id);
            }
        }
    }
})();