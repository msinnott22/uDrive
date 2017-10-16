angular.module("umbraco.resources").factory("settingsResource",
    function($http) {
        return {
            checkAuth: function() {
                return $http.get("backoffice/uDrive/SettingsApi/GetAuth");
            },
            getAll: function() {
                return $http.get("backoffice/uDrive/SettingsApi/GetAllSettings");
            },
            save: function(settings) {
                return $http.post("backoffice/uDrive/SettingsApi/SaveAllSettings", angular.toJSON(settings));
            },
            getAccounts: function() {
                return $http.get("backoffice/uDrive/DriveApi/GetAccounts");
            }
        };
    }
);