
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'angularFileUpload']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/manage", {
        controller: "manageController",
        templateUrl: "/app/views/manage.html"
    });

    $routeProvider.when("/setPassword", {
        controller: "setPasswordController",
        templateUrl: "/app/views/setPassword.html"
    });

    $routeProvider.when("/profile", {
        controller: "profileController",
        templateUrl: "/app/views/profile.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/contact", {
        controller: "contactController",
        templateUrl: "/app/views/contact.html"
    });

    $routeProvider.when("/category", {
        controller: "categoryController",
        templateUrl: "/app/views/category.html"
    });
    
    $routeProvider.when("/import", {
        controller: "importContactController",
        templateUrl: "/app/views/importContact.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

var serviceBase = 'https://localhost:44391/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


