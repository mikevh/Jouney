(function() {
    'use strict';

    var app = angular.module('app');

    app.directive('picker', function() {
        return {
            templateUrl: 'app/directives/pickerTemplate.html',
            scope: {
                available: '=',
                selected: '='
            },
            controller: function($scope) {
                $scope.add = function() {
                    angular.forEach($scope.selected_available, function (x) {
                        $scope.selected.push(x);
                    });
                };

                $scope.remove = function() {
                    angular.forEach($scope.selected_selected, function(x) {
                        var idx = $scope.selected.indexOf(x);
                        if (idx >= 0) {
                            $scope.selected.splice(idx, 1);
                        }
                    });
                };

                $scope.notAlreadySelected = function (x) {
                    if (!$scope.selected) {
                        return true;
                    }
                    var found = _.find($scope.selected, function(y) {
                        return x.id === y.id;
                    });

                    return found === undefined;
                };
            }
        };
    });
})();
