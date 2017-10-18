angular.module("umbraco.resources").factory("filesResource",
    function ($http) {
        return {
            getDetails: function () {
                return $http.get("backoffice/uDrive/GoogleDriveApi/GetDetails");
            },
            getFiles: function() {
                return $http.get("backoffice/uDrive/GoogleDriveApi/GetFiles");
            }
        };
    }
);