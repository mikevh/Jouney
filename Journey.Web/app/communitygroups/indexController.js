(function() {
    'use strict';

    angular
        .module('app.communitygroups')
        .controller('cgIndexController', controller);

    controller.$inject = ['$route', '$location', 'communityGroupService'];
    
    function controller($route, $location, communityGroupService) {
        var vm = this;
        vm.addMeeeting = addMeeeting;
        vm.remove = remove;

        activate();

        function addMeeeting(group) {
            $location.path('/Meetings/ForGroup/' + group.id);
        }

        function activate() {
            communityGroupService.all().then(function (result) {
                vm.communityGroups = result.data;
                angular.forEach(vm.communityGroups, function(x) {
                    communityGroupService.latest(x.id).then(function (y) {
                        
                        x.latest = y.data === null ? null : new Date(y.data);
                    });
                });
            });
        }

        function remove(g) {
            communityGroupService.remove(g.id).then(function() {
                $route.reload();
            });
        }
    }
})();
