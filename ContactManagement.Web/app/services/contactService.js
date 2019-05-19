'use strict';
app.factory('contactService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var self = this;
    var serviceBase = ngAuthSettings.apiServiceBaseUri;

    var contactServiceFactory = {};


    self.getDataByUrl = function (url) {
        return $http.get(serviceBase + url).then(function (result) {
            return result.data;
        }).catch(function (Message) {
            console.error(Message);
            exception.catcher('XHR loading Failded')(Message);
        });
    }

    self.getDataByFullUrl = function (url) {
        return $http.get(serviceBase + url).then(function (result) {
            return result.data;
        }).catch(function (message) {
            exception.catcher('XHR loading Failded')(message);
        });
    }


    //Save Data with Url
    self.saveDataByUrl = function (data, url) {
        console.log(data);
        return $http.post(serviceBase + url, data).then(function (result) {
            return result.data;
        }).catch(function (message) {
            exception.catcher('XHR loading Failded')(message);
        });
    }

    //Save Data with Url
    self.saveDataByFullUrl = function (data, url) {
        return $http.post(url, data).then(function (result) {
            return result.data;
        }).catch(function (message) {
            exception.catcher('XHR loading Failded')(message);
        });
    }

    // Delete Data By Url
    self.delDataByUrl = function (url) {
        console.log(url);
        return $http.delete(serviceBase + url).then(function (result) {
            return result.data;
        }).catch(function (message) {
            exception.catcher('XHR loading Failded')(message);
        });
    }



    // Delete Data By Full Url
    self.delDataByFullUrl = function (url, data) {
        return $http.delete(serviceBase + url).then(function (result) {
            return result.data;
        }).catch(function (message) {
            exception.catcher('XHR loading Failded')(message);
        });
    }

    var _ExportExcel = function (data) {

        return $http.post(serviceBase + 'api/Contacts/ExportExcel', data).then(function (response) {
            return response;
        });
    };


    return self;

}]);