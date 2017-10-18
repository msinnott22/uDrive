angular.module("umbraco").controller("uDrive.SettingsController",
    function($scope, settingsResource) {
        
        $scope.hasAuth = false;
        $scope.showAuth = true;

        settingsResource.getAll().then(function(response) {
            $scope.settings = response.data;
        });

        settingsResource.checkAuth().then(function(response) {
            $scope.hasAuth = response.data === "true";
            if ($scope.hasAuth) {
                $scope.showAuth = false;
            }
        });

        $scope.auth = function() {
            window.open("/App_Plugins/uDrive/backoffice/OAuth.aspx", "oAuthUDrive", "location=0,status=0,width=600,height=600");
        };

        $scope.save = function(settings) {
            settingsResource.save(settings).then(function(response) {
                $scope.settings = response.data;
            });
        }

    }
);