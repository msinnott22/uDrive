angular.module("umbraco.resources").factory("settingsResource",
    function($http) {
        return {
            checkAuth: function() {
                return $http.get("uDrive/SettingsApi/GetAuth");
            },
            getAll: function() {
                return $http.get("uDrive/SettingsApi/GetAllSettings");
            },
            save: function(settings) {
                return $http.post("uDrive/SettingsApi/SaveAllSettings", angular.toJSON(settings));
            }
        };
    }
);