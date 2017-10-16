angular.module("umbraco").controller("uDrive.SettingsController",
    function($scope, settingsResource) {

        var hasUserAuthd = false;

        settingsResource.getAll().then(function(response) {
            $scope.settings = response.data;
        });

        settingsResource.checkAuth().then(function(response) {
            hasUserAuthd = response.data === "true";

            $scope.$apply(function() {
                $scope.hasAuthd = hasUserAuthd;
            });


        });

        $scope.showAuth = true;

        $scope.auth = function() {
            window.open("/App_Plugins/uDrive/backoffice/OAuth.aspx", "oAuthUDrive", "location=0,status=0,width=600,height=600");

            //will callback to udrive oauth callback
            //webapi post to umbraco site to save settings
            //once saved, close popup window
            //set showAuth to false
            //show success notification
        };

        $scope.save = function(settings) {
            settingsResource.save(settings).then(function(response) {
                $scope.settings = response.data;
            });
        }

    }
);