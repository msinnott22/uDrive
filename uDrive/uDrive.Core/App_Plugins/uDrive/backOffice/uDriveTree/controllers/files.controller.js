angular.module("umbraco").controller("uDrive.FilesController",
    function ($scope, filesResource) {
        $scope.title = "Files";

        filesResource.getFiles().then(function(response) {
            console.log(response.data);
            $scope.driveInfo = response.data;
        });

        filesResource.getUserInfo().then(function (response) {
            $scope.userInfo = response.data;
        });
    }
);