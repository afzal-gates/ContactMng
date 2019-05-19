
'use strict';
app.controller('categoryController', ['$scope', '$route', '$filter', 'ngAuthSettings', 'contactService',
    function ($scope, $route, $filter, ngAuthSettings, contactService) {

        var vm = this;
        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        $scope.categoryList = [];
        loadContact();

        $scope.viewby = 10;
        $scope.totalItems = $scope.categoryList.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5; //Number of pager buttons to show

        function loadContact() {

            return contactService.getDataByUrl('api/category/getcategory').then(function (res) {
                console.log(res);
                $scope.categoryList = res;
                $scope.totalItems = res.length;
            });

        }

        $scope.form = { CategoryId: 0, Title: '', Description: '' };

        $scope.setPage = function (pageNo) {
            $scope.currentPage = pageNo;
        };

        $scope.pageChanged = function () {
            console.log('Page changed to: ' + $scope.currentPage);
        };

        $scope.setItemsPerPage = function (num) {
            $scope.itemsPerPage = num;
            $scope.currentPage = 1; //reset to first page
        }

        $scope.editData = function (obj) {

            var dataItem = {
                'CategoryId': obj.categoryId,
                'Title': obj.title,
                'Description': obj.description,
            };
            $scope.form = angular.copy(dataItem);
        }


        function formAray(obj) {

            var dataItem = {
                'CategoryId': obj.categoryId,
                'Title': obj.title,
                'Description': obj.description,
            };
            return [dataItem]
        }

        $scope.submitAll = function () {

            var data = $scope.form;
            return contactService.saveDataByUrl(data, 'api/category/Save').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    console.log(res);
                    $route.reload();
                }
            });
        };

        $scope.deleteData = function (id) {

            var data = $scope.form;
            return contactService.delDataByUrl('api/category/delete?id=' + id).then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    console.log(res);
                    $route.reload();
                }
            });
        };

    }]);