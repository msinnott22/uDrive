angular.module("umbraco.resources").factory("filesResource",
    function ($http) {
        return {
            getUserInfo: function() {
                return $http.get("backoffice/uDrive/GoogleDriveApi/GetUserInfo");
            },
            getFiles: function() {
                return $http.get("backoffice/uDrive/GoogleDriveApi/GetFiles");
            }
        };
    }
);