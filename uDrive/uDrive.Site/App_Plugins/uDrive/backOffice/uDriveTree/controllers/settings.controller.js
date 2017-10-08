angular.module("umbraco").controller("uDrive.SettingsController",
    function($scope, settingsResource, notificationService) {

        settingsResource.getAll().then(function(response) {
            $scope.settings = response.data;
        });

        $scope.save = function(settings) {
            settingsResource.save(settings).then(function(response) {
                $scope.settings = response.data;

                notificationService.success("Settings have been saved");
            });
        }

    }
);