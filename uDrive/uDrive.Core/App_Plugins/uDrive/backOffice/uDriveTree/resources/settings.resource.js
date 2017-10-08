angular.module("umbraco.resources").factory("settingsResource",
    function($http) {
        return {
            getAll: function() {
                return $http.get("uDrive/SettingsApi/GetAllSettings");
            },
            save: function(settings) {
                return $http.post("uDrive/SettingsApi/SaveAllSettings", angular.toJSON(settings));
            }
        };
    }
);