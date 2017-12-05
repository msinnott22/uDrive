angular.module("umbraco").controller("uDrive.FilesController",
    function ($scope, filesResource) {
        $scope.title = "Files";

        $scope.userInfo = {};
        $scope.driveData = {};

        filesResource.getFiles().then(function(response) {
            $scope.driveData = response.data;
        });

        filesResource.getUserInfo().then(function (response) {
            $scope.userInfo = response.data;
        });
    }
);