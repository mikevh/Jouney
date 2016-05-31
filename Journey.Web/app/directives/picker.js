(function() {
    'use strict';

    angular.module('app.picker', []);

    angular
        .module('app.picker')
        .directive('picker', pickerDirective);
    
    function pickerDirective () {
        var directive = {
            templateUrl: 'app/directives/picker.html',
            controller: controller,
            controllerAs: 'vm',
            bindToController: true,
            scope: {
                available: '=',
                selected: '='
            }
        };

        return directive;

        function controller() {
            var vm = this;
            
            vm.add = add;
            vm.remove = remove;
            vm.exclude = exclude;

            function exclude(x) {
                if (!vm.selected) {
                    return true;
                }
                var found = _.find(vm.selected, function(y) {
                    return x.id === y.id;
                });
                
                return found !== undefined;
            };

            function add() {
                angular.forEach(vm.selected_available, function (x) {
                    vm.selected.push(x);
                });
            }

            function remove () {
                angular.forEach(vm.selected_selected, function(x) {
                    var idx = vm.selected.indexOf(x);
                    if (idx >= 0) {
                        vm.selected.splice(idx, 1);
                    }
                });
            };
        }
    }
})();
