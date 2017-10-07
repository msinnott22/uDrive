angular.module("umbraco").controller("uDrive.ViewController",
    function($scope, $routeParams) {
        var viewName = $routeParams.id;
        viewName = viewName.replace('%20', '-').replace(' ', '-');

        $scope.templatePartialURL = '../App_Plugins/uDrive/backOffice/uDriveTree/partials/' + viewName + '.html';
        $scope.sectionName = $routeParams.id;
    }
);