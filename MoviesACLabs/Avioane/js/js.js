
var app = angular.module('airlines', []);

app.service("AirlinesService", function ($http) {
    this.getAirlines = function () {
        return $http({
            method: "GET",
            url: "/api/Airlines"
        });
    };
    this.get2VowelsAirlines = function () {
        return $http({
            method: "GET",
            url: "/api/Airlines/Filter"
        });
    };
    this.addAirline = function (theAirline) {
        console.log(theAirline);
        return $http.post("/api/Airlines", theAirline);
    };

    this.deleteAirline = function (airlineId) {
        return $http.delete("/api/Airlines/" + airlineId);
    };

});


app.controller('AirlinesController', function ($scope, AirlinesService) {
    $scope.airlines = null;
    $scope.addingAirline = { Planes: [] };
    $scope.feFilter = false;

    $scope.addPlaneToNewAirline = function () {
        $scope.addingAirline.Planes.push({});
    };

    $scope.addTheAirline = function () {
        console.log($scope.addingAirline);
        AirlinesService.addAirline($scope.addingAirline).then(function (data) {
            alert("added!");
        }).catch(function (data) {
            alert("ceva a crăpat");
            console.log(data);
        });
    };

    $scope.beFilter = function (enabled) {
        if (enabled) {
            AirlinesService.get2VowelsAirlines().then(function (data) {
                $scope.airlines = data.data;
            });
        }
        else {
            AirlinesService.getAirlines().then(function (data) {
                $scope.airlines = data.data;
            });
        }
    };
    $scope.beFilter(false);

    $scope.deleteAirline = function (id) {
        //console.log("deleting " + id);
        AirlinesService.deleteAirline(id).then(function (data) {
            console.log(data);
            for (var i = 0; i < $scope.airlines.length; i++) {
                if ($scope.airlines[i].Id == id) {
                    $scope.airlines.splice(i, 1);
                    //console.log("gasit la index: " + i);
                    break;
                }
            }
            //console.log("deleted!");
        }).catch(function (data) {
            alert("ceva a crăpat");
            console.log(data);
        });;
    };
});

