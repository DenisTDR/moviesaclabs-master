

var app = angular.module('awards', []);

app.service("AwardsService", function ($http) {
    this.getAwards = function () {
        return $http({
            method: "GET",
            url: "/api/Awards"
        });
    };
    this.addAward = function (theAward) {
        return $http.post("/api/Awards", theAward);
    };

    this.deleteAward = function (awardId) {
        return $http.delete("/api/Awards/" + awardId);
    };
});



app.controller('AwardsController', function ($scope, AwardsService) {
    $scope.awards = null;
    $scope.editingAward = null;

    AwardsService.getAwards().then(function (data) {
        $scope.awards = data.data;
    });

    $scope.editAward = function (awardId) {
        $scope.editingAward = findAwardById(awardId);
    };


    var findAwardById = function (awardId) {
        for (var i = 0; i < $scope.awards.length; i++)
            if ($scope.awards[i].Id == awardId) {
                return $scope.awards[i];
            }
        return null;
    };

});
