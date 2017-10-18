angular.module("umbraco").controller("uDrive.FilesController",
    function ($scope, filesResource) {
        $scope.title = "Files";

        filesResource.getDetails().then(
            function (response) {
                $scope.details = response.data;
                console.log(response.data);
            }
        );

        filesResource.getFiles().then(function(response) {
            $scope.files = response.data;
            console.log(response.data);
        });
    }
);