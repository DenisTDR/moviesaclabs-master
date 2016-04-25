
var app = angular.module('actors', []);

app.service("ActorsService", function ($http) {
    this.getActors = function () {
        return $http({
            method: "GET",
            url: "/api/Actors"
        });
    };
    this.addActor = function (theActor) {
        return $http.post("/api/Actors", theActor);
    };

    this.deleteActor = function (actorId) {
        return $http.delete("/api/Actors/" + actorId);
    };
 
})

app.controller('ActorsController', function ($scope, ActorsService) {
    $scope.actors = null;
    $scope.newActor = {DateOfBirth: "2016-04-25T12:19:02.982Z"};
    ActorsService.getActors().then(function (data) {
        $scope.actors = data.data;
    });


    $scope.addTheActor = function () {
        var addingActor = {
            Name: $scope.newActor.Name,
            Revenue: $scope.newActor.Revenue,
            DateOfBirth: $scope.newActor.DateOfBirth
        };
        console.log(addingActor);
        ActorsService.addActor(addingActor).then(function (data) {
            console.log("done");
            console.log(data);
            addingActor.Id = data.data.Id;
            $scope.actors.push(addingActor);
        }).catch(function (data) {
            alert("ceva a crăpat");
            console.log(data);
        });
    };

    $scope.deleteActor = function (id) {
        //console.log("deleting " + id);
        ActorsService.deleteActor(id).then(function (data) {
            //console.log(data);
            for (var i = 0; i < $scope.actors.length; i++) {
                if ($scope.actors[i].Id == id) {
                    $scope.actors.splice(i, 1);
                    //console.log("gasit la index: " + i);
                    break;
                }
            }
            //console.log("deleted!");
        }).catch(function(data){
            alert("ceva a crăpat");
            console.log(data);
        });;
    };
});

