angular.module("umbraco").controller("uDrive.SettingsController",
    function($scope, $http) {

        $http.get("/umbraco/backoffice/uDrive/SettingsApi/GetAllSettings").success(
            function (data) {
                $scope.settings = data;
            }
        );

    }
);