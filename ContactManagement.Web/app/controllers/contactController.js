'use strict';
app.controller('contactController', ['$scope', '$http', '$filter', '$route', 'ngAuthSettings', 'FileUploader', 'contactService', '$window', 'authService',
    function ($scope, $http, $filter, $route, ngAuthSettings, FileUploader, contactService, $window, authService) {

        var vm = this;
        var serviceBase = ngAuthSettings.apiServiceBaseUri;
        $scope.ContactList = [];
        loadContact();
        loadCategory();

        $scope.viewby = 10;
        $scope.totalItems = $scope.ContactList.length;
        $scope.currentPage = 1;
        $scope.itemsPerPage = $scope.viewby;
        $scope.maxSize = 5; //Number of pager buttons to show

        function loadCategory() {

            return contactService.getDataByUrl('api/category/getcategory').then(function (res) {
                console.log(res);
                $scope.categoryList = res;
            });

        }

        function loadContact() {

            return contactService.getDataByUrl('api/contacts/getcenters?userName=' + authService.authentication.userName).then(function (res) {
                console.log(res);
                $scope.ContactList = res;
                $scope.totalItems = res.length;
            });

        }

        $scope.form = { ContactId: 0, CategoryId: 0, Name: '', Email: '', MobileNo: '', Address: '', ProfilePicture: "c72b16bf-0b0c-4069-9072-f7830b492ea1.jpg" };

        $scope.itemList = angular.copy($scope.ContactList);

        $scope.$watch('searchText', function () {

            var begin = (($scope.currentPage - 1) * $scope.itemsPerPage),
            end = begin + $scope.itemsPerPage;

            if ($scope.searchText) {
                var term = $scope.searchText.toLowerCase();
                if (term != "") {
                    var list = angular.copy($scope.itemList.filter(function (item) {
                        var termInId = item.id.toLowerCase().indexOf(term) > -1;
                        var termInName = item.name.toLowerCase().indexOf(term) > -1;
                        return termInId || termInName;
                    }));

                    $scope.totalItems = list.length;
                    $scope.currentPage = 1;
                }
                else {
                    $scope.totalItems = $scope.itemList.length;
                    $scope.currentPage = 1;
                }
            }
            else {
                $scope.totalItems = $scope.itemList.length;
                $scope.currentPage = 1;
            }
        });

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
                'ContactId': obj.contactId,
                'CategoryId': obj.categoryId,
                'Name': obj.name,
                'Email': obj.email,
                'MobileNo': obj.mobileNo,
                'Address': obj.address,
                'ProfilePicture': obj.profilePicture
            };
            $scope.form = angular.copy(dataItem);
        }

        $scope.submitAll = function () {

            var data = $scope.form;

            return contactService.saveDataByUrl(data, 'api/contacts/save').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    console.log(res);
                    $route.reload();

                }
            });
        }

        $scope.deleteData = function (id) {

            var data = $scope.form;
            return contactService.delDataByUrl('api/contacts/delete?id=' + id).then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    console.log(res);
                    $route.reload();
                }
            });
        };


        $scope.ExportExcel = function () {

            var data = $scope.form;
            data['ContactList'] = angular.copy($scope.ContactList);

            return contactService.saveDataByUrl(data, 'api/FileReadWrite/ExportExcel').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    console.log(res);
                    $window.open(serviceBase + '/UploadedDocuments/' + res.jsonStr, '_blank');

                }
            });
        }

        $scope.ExportCsv = function () {

            var data = $scope.form;
            data['ContactList'] = angular.copy($scope.ContactList);

            return contactService.saveDataByUrl(data, 'api/FileReadWrite/ExportCsv').then(function (res) {

                if (res.success === false) {
                    vm.errors = res.errors;
                }
                else {
                    console.log(res);
                    $window.open(serviceBase + '/UploadedDocuments/' + res.jsonStr, '_blank');

                }
            });
        }


        // Uploader Plugin Code 
        var uploader = $scope.uploader = new FileUploader({
            url: window.location.protocol + '//' + window.location.host +
                 window.location.pathname + '/api/FileReadWrite/UploadFile'
        });


        // FILTERS  
        uploader.filters.push({
            name: 'extensionFilter',
            fn: function (item, options) {
                var filename = item.name;
                var extension = filename.substring(filename.lastIndexOf('.') + 1).toLowerCase();
                if (extension == "jpg" || extension == "jpeg" || extension == "png")
                    return true;
                else {
                    alert('Invalid file format. Please select a file with jpg/jpeg or png format and try again.');
                    return false;
                }
            }
        });

        uploader.filters.push({
            name: 'sizeFilter',
            fn: function (item, options) {
                var fileSize = item.size;
                fileSize = parseInt(fileSize) / (1024 * 1024);
                if (fileSize <= 5)
                    return true;
                else {
                    alert('Selected file exceeds the 5MB file size limit. Please choose a new file and try again.');
                    return false;
                }
            }
        });

        uploader.filters.push({
            name: 'itemResetFilter',
            fn: function (item, options) {
                if (this.queue.length < 5)
                    return true;
                else {
                    alert('You have exceeded the limit of uploading files.');
                    return false;
                }
            }
        });

        // CALLBACKS  
        uploader.onWhenAddingFileFailed = function (item, filter, options) {
            console.info('onWhenAddingFileFailed', item, filter, options);
        };
        uploader.onAfterAddingFile = function (fileItem) {
            //alert('Files ready for upload.');
        };

        uploader.onSuccessItem = function (fileItem, response, status, headers) {
            $scope.uploader.queue = [];
            $scope.uploader.progress = 0;
            //alert('Selected file has been uploaded successfully.');
        };
        uploader.onErrorItem = function (fileItem, response, status, headers) {
            alert('We were unable to upload your file. Please try again.');
        };
        uploader.onCancelItem = function (fileItem, response, status, headers) {
            alert('File uploading has been cancelled.');
        };

        uploader.onAfterAddingAll = function (addedFileItems) {
            console.info('onAfterAddingAll', addedFileItems);
            console.log(uploader);
        };


        function formAray(obj) {

            var dataItem = {
                'ContactId': obj.ContactId,
                'CategoryId': obj.CategoryId,
                'Name': obj.Name,
                'Email': obj.Email,
                'MobileNo': obj.MobileNo,
                'Address': obj.Address,
                'ProfilePicture': "c72b16bf-0b0c-4069-9072-f7830b492ea1.jpg"
            };
            return [dataItem]
        }

        uploader.onBeforeUploadItem = function (item) {
            item['formData'] = formAray($scope.form);
            console.info('onBeforeUploadItem', item);
        };
        uploader.onProgressItem = function (fileItem, progress) {
            console.info('onProgressItem', fileItem, progress);
        };
        uploader.onProgressAll = function (progress) {
            console.info('onProgressAll', progress);
        };

        uploader.onCompleteItem = function (fileItem, response, status, headers) {

            var msg = angular.fromJson(response);
            console.log(msg)
            if (msg.success == true) {
                var data = $scope.form;
                data['UserName'] = authService.authentication.userName;
                data['ProfilePicture'] = msg.imgUrl;

                return contactService.saveDataByUrl(data, 'api/Contacts/Save').then(function (res) {

                    if (res.success === false) {
                        vm.errors = res.errors;
                    }
                    else {
                        console.log(res);
                        $route.reload();
                    }
                });
            }
            //console.info('onCompleteItem', fileItem, response, status, headers);
        };
        uploader.onCompleteAll = function () {
            console.info('onCompleteAll');
        };



        $scope.selectedFile = null;
        $scope.msg = "";

        $scope.loadFile = function (files) {

            $scope.$apply(function () {

                $scope.selectedFile = files[0];

            })

        }

        $scope.handleFile = function () {
            
            var file = $scope.selectedFile;

            if (file) {

                var reader = new FileReader();

                reader.onload = function (e) {

                    var data = e.target.result;

                    var workbook = XLSX.read(data, { type: 'binary' });
                    var first_sheet_name = workbook.SheetNames[0];
                    var dataObjects = XLSX.utils.sheet_to_json(workbook.Sheets[first_sheet_name]);

                    //console.log(excelData);  

                    if (dataObjects.length > 0) {
                        $scope.save(dataObjects);
                    } else {
                        $scope.msg = "Error : Something Wrong !";
                    }
                }

                reader.onerror = function (ex) {

                }

                reader.readAsBinaryString(file);
            }
        }


        $scope.save = function (data) {

            $http({
                method: "POST",
                url: "api/FileReadWrite/ImportContact",
                data: JSON.stringify(data),
                headers: {
                    'Content-Type': 'application/json'
                }

            }).then(function (data) {
                if (data.status) {
                    $scope.msg = "Data has been inserted ! ";
                }
                else {
                    $scope.msg = "Error : Something Wrong";
                }
                $route.reload();
            }, function (error) {
                $scope.msg = "Error : Something Wrong";
            })

        }

    }]);