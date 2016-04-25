

var app = angular.module('todo', []);


var crtIndex = 0;
app.controller('MainController', function ($scope) {
    $scope.addingItem = "";
    $scope.items = [];


    $scope.addItem = function (completed) {
        var tmp;
        $scope.items.push(tmp = {
            Id: ++crtIndex,
            Text: $scope.addingItem,
            Completed: completed ? true : false,
            Show: true
        });
        $scope.addingItem = "";
        changePage();
    };

    $scope.show = "all";

    $scope.display = function (what) {
        $scope.show = what;        
    };
    $scope.crtPage = 0;
    $scope.next = function () {
        $scope.crtPage++;
        changePage();
    };
    $scope.prev = function () {
        $scope.crtPage--;
        if ($scope.crtPage < 0) {
            $scope.crtPage = 0;
            return;
        }
        changePage();
    }
    var changePage = function () {
        for (var i = 0; i < $scope.items.length; i++) {
            if (i >= $scope.crtPage * 4 && i < ($scope.crtPage + 1) * 4) {
                $scope.items[i].Show = true;
            }
            else {
                $scope.items[i].Show = false;
            }
        }
    };
    

    $scope.itemsLeft = function () {
        var cnt = 0;
        for (var i = 0; i < $scope.items.length; i++)
            cnt += $scope.items[i].Completed ? 0 : 1;
        return cnt;
    };

    var init = function () {
        $scope.addingItem = "Item 1";
        $scope.addItem();

        $scope.addingItem = "Item 2";
        $scope.addItem(true);

        $scope.addingItem = "Item 3";
        $scope.addItem();

    };
    init();
});

