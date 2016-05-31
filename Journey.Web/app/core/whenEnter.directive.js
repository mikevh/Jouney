(function() {
    'use strict';

    angular
        .module('app.core')
        .directive('whenEnter', directive);

    function directive() {
        return function(scope, element, attrs) {
            element.bind('keydown keypress', keydown);

            function keydown(event) {
                if (event.which === 13) {
                    scope.$apply(apply);
                }
            }

            function apply() {
                scope.$eval(attrs.whenEnter);
            }
        }
    }
})();