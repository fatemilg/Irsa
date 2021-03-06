﻿var app = angular.module('myApp', ['angularUtils.directives.dirPagination', 'ui.bootstrap', 'ngProgress']);
app.config(['$tooltipProvider', function ($tooltipProvider) {
    $tooltipProvider.options({
        appendToBody: true, //
        placement: 'bottom' // Set Default Placement
    });
}]);

app.controller('custom_flight_controller', function ($scope, $http, ngProgress, $window) {
    $scope.flight = {};
    $scope.flight.FlightType = "RoundTrip";
    $scope.Peyment = {};
    $scope.SelectedFlightIDList = [];
    $scope.SearchID = null;
    $scope.SeatRequest = [];
    $scope.AirItineraries = [];
    $scope.Multi = {};
    $scope.MultiFlights = [];

    $scope.flight.AdultCount = 1;
    $scope.flight.ChildCount = 0;
    $scope.flight.InfantCount = 0;
    $scope.flight.DepartureDate = new Date('2020-09-24');
    $scope.flight.ReturnDate = new Date('2020-09-26');
    $scope.Multi.DepartureDate = new Date('2020-09-26');

    $scope.fillAirports = function () {
        ngProgress.start();
        $http({
            method: "Get",
            url: '/Home/GetAirports',
            headers: { "Content-Type": "application/json" }
        }).then(function mySuccess(response) {
            $scope.Airports = JSON.parse(response.data.responseText);
            ngProgress.complete();
            ngProgress.stop();
        }, function myError(response) {
            alert('by');
        });
    };

    $scope.GetSeats = function () {
        var param = {
            SearchID: $scope.SearchID,
            SelectedFlightIDList: $scope.SelectedFlightIDList
        };
        $http({
            method: 'POST',
            url: '/Home/GetSeats',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: Object.toparams(param)
        }).then(function mySuccess(response) {
            if (response.data.success === false) {
                alert(response.data.responseText);
                return;
            }
            $scope.Seats = JSON.parse(response.data.responseText);
        }, function myError(response) {
            alert('by');
        });
    };
    $scope.GetFareFamily = function () {
        var param = {
            SearchID: $scope.SearchID,
            SelectedFlightIDList: $scope.SelectedFlightIDList,
            AdultCount: $scope.flight.AdultCount,
            ChildCount: $scope.flight.ChildCount,
            InfantCount: $scope.flight.InfantCount
        };
        $http({
            method: 'POST',
            url: '/Home/GetFareFamily',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: Object.toparams(param)
        }).then(function mySuccess(response) {
            if (response.data.success === false) {
                alert(response.data.responseText);
                return;
            }
            $scope.FareFamily = JSON.parse(response.data.responseText);

        }, function myError(response) {
            alert('by');
        });
    };

    $scope.ReserveSeat = function (_childIndex, _parentIndex) {
        // parentIndex= connectionID
        var list = ($scope.Seats).filter(function (n) { return n.ItineraryReference === _parentIndex && n.SegmentReference === _childIndex });
        if (list[0] !== undefined) {
            $scope.SeatMap = list[0].SeatMap;
            $scope.SelectedItineraryReference = list[0].ItineraryReference;
            $scope.SelectedSegmentReference = list[0].SegmentReference;
            $('#ModalReserveSeat').modal('show');
        }
    };

    $scope.SelectSeat = function (_RowSeatAmount, _RowNumber, _ColumnName) {


        var param = {
            RowNumber: _RowNumber,
            ColumnName: _ColumnName,
            ItineraryReference: $scope.SelectedItineraryReference,
            SegmentReference: $scope.SelectedSegmentReference
        };
        $scope.SeatRequest.push(param);
        $scope.FinalPriceDetaill.TotalFareAmout = $scope.FinalPriceDetaill.TotalFareAmout + _RowSeatAmount;

        $('#ModalReserveSeat').modal('hide');

    };


    $scope.search = function () {
        ngProgress.start();

        var _TravelerAvail = {
            AdultCount: $scope.flight.AdultCount,
            ChildCount: $scope.flight.ChildCount,
            InfantCount: $scope.flight.InfantCount
        }




        //One-Way
        var rout1 = {
            DepartureDate: $scope.formatDate($scope.flight.DepartureDate),
            Origin: $scope.flight.Origin,
            Destination: $scope.flight.Destination

        }
        $scope.AirItineraries.push(rout1);


        //RoundTrip
        if ($scope.flight.FlightType == "RoundTrip") {

            var rout2 = {
                DepartureDate: $scope.formatDate($scope.flight.ReturnDate),
                Origin: $scope.flight.Destination,
                Destination: $scope.flight.Origin,
            }
            $scope.AirItineraries.push(rout2);
        }
        //Multi
        else if ($scope.flight.FlightType == "Multi") {
            $scope.AirItineraries = $scope.MultiFlights;
        }

        angular.forEach($scope.AirItineraries, function (item, index) {
            item.AllAirportsDestination = false;
            item.AllAirportsOrigin = false;
            item.ConnectionID = index;
        });



        var param = {
            TravelerAvail: _TravelerAvail,
            AirItineraries: $scope.AirItineraries,
            FlightType: $scope.flight.FlightType,
            SecurityGUID: $scope.SecurityToken
        }


        $http({
            method: 'POST',
            url: '/Home/ManualFlightSearch',
            headers: { 'Content-Type': 'application/json' },
            params: { obj: JSON.stringify(param) },
        }).then(function mySuccess(response) {
            if (response.data.success === false) {
                alert(response.data.responseText);
                ngProgress.complete();
                ngProgress.stop();
                return;
            }
            $scope.FlightResponse = JSON.parse(response.data.responseText);
            $scope.SearchID = $scope.FlightResponse[0].FlightSearchResponse.SearchID;
            $scope.SearchType = $scope.FlightResponse[0].FlightSearchResponse.SearchType;;

            $scope.SearchPackageLists = $scope.FlightResponse[0].PackageLists;

            $scope.SelectedFlightIDList = [];
            ngProgress.complete();
            ngProgress.stop();
        }, function myError(response) {
            ngProgress.complete();
            ngProgress.stop();
        });
        $scope.AirItineraries = [];
    };

    Object.toparams = function ObjecttoParams(obj) {
        var p = [];
        for (var key in obj) {
            p.push(key + '=' + encodeURIComponent(obj[key]));
        }
        return p.join('&');
    };

    $scope.setSortColumn = function (propertyName) {
        if ($scope.orderProperty === propertyName) {
            $scope.orderProperty = '-' + propertyName;
        } else if ($scope.orderProperty === '-' + propertyName) {
            $scope.orderProperty = propertyName;
        } else {
            $scope.orderProperty = propertyName;
        }
    };

    $scope.ShowLegsModal = function (_item) {
        $scope.Legs = _item;
        $('#ModalLegs').modal('show');
    };
    $scope.ShowFaresModal = function (_item) {
        $scope.Fares = _item;
        $('#ModalFares').modal('show');
    };

    $scope.ShowPriceDetailWithExtraServiceModal = function (_packages) {
        $scope.SelectedFlightIDList = [];
        angular.forEach(_packages, function (item, index) {
            if (item != null) {
                $scope.SelectedFlightIDList.push(item.FlightID);

            }
        });
        $scope.GetPriceDetailWithExtraService("");
        $scope.GetSeats();
        $scope.GetFareFamily();

        $scope.Peyment.EmailPaymenter = 'fatemilg@yahoo.com';
        $scope.Peyment.MobilePaymenter = '3021655';
        $scope.Peyment.GenderPassenger = 'MALE';
        $scope.Peyment.BirthDatePassenger = '1985-09-16';
        $scope.Peyment.GivenNamePassenger = 'saeed';
        $scope.Peyment.SurNamePassenger = 'fatemi';
        $scope.Peyment.DocID = 'N928264534';
        $scope.Peyment.ExpireDate = '2022-02-02';
        $scope.Peyment.BirthCountry = 'TR';


        $('#ModalFinalPriceDetail').modal('show');


    };

    $scope.ShowFareRuleModal = function () {
        $('#ModalFareRule').modal('show');
        var param = {
            SearchID: $scope.SearchID,
            SelectedFlightIDList: $scope.SelectedFlightIDList
        };
        ngProgress.start();
        $http({
            method: 'POST',
            url: '/Home/FareRules',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: Object.toparams(param)
        }).then(function mySuccess(response) {
            if (response.data.success === false) {
                alert(response.data.responseText);
                return;
            }
            $scope.FareRules = JSON.parse(response.data.responseText)[0];
            ngProgress.complete();
            ngProgress.stop();
        }, function myError(response) {
            ngProgress.complete();
            ngProgress.stop();
        });
    };

    $scope.ShowFareFamilyModal = function () {
        $('#ModalFareFamily').modal('show');
    };

    $scope.GetPriceDetailWithExtraService = function (_FareFamilyID) {

        //One-Way
        var rout1 = {
            DepartureDate: $scope.formatDate($scope.flight.DepartureDate),
            Origin: $scope.flight.Origin,
            Destination: $scope.flight.Destination

        }
        $scope.AirItineraries.push(rout1);


        //RoundTrip
        if ($scope.flight.FlightType == "RoundTrip") {

            var rout2 = {
                DepartureDate: $scope.formatDate($scope.flight.ReturnDate),
                Origin: $scope.flight.Destination,
                Destination: $scope.flight.Origin,
            }
            $scope.AirItineraries.push(rout2);
        }
        //Multi
        else if ($scope.flight.FlightType == "Multi") {
            $scope.AirItineraries = $scope.MultiFlights;
        }

        angular.forEach($scope.AirItineraries, function (item, index) {
            item.AllAirportsDestination = false;
            item.AllAirportsOrigin = false;
            item.ConnectionID = index;
        });

        var _TravelerAvail = {
            AdultCount: $scope.flight.AdultCount,
            ChildCount: $scope.flight.ChildCount,
            InfantCount: $scope.flight.InfantCount
        }

        var param = {
            SearchID: $scope.SearchID,
            FlightIDs: $scope.SelectedFlightIDList,
            TravelerAvail: _TravelerAvail,
            AirItineraries: $scope.AirItineraries,
            FareFamilyID: _FareFamilyID,
            SecurityGUID: $scope.SecurityToken
        };

        ngProgress.start();
        $http({
            method: 'POST',
            url: '/Home/GetPriceDetailWithExtraService',
            headers: { 'Content-Type': 'application/json' },
            params: { obj: JSON.stringify(param) }
        }).then(function mySuccess(response) {
            if (response.data.success === false) {
                alert(response.data.responseText);
                return;
            }
            $scope.FinalPriceDetaill = JSON.parse(response.data.responseText)[0];
            $scope.PriceDetailID = $scope.FinalPriceDetaill.PriceDetailID;

            ngProgress.complete();
            ngProgress.stop();

        }, function myError(response) {
            alert('by');
            ngProgress.complete();
            ngProgress.stop();
        });
    };



    $scope.AddToCart = function () {
        var param = {
            PriceDetailID: $scope.PriceDetailID,
            EnduserEmail: $scope.Peyment.EmailPaymenter,
            EnduserCellPhone: $scope.Peyment.MobilePaymenter,
            Gender: $scope.Peyment.GenderPassenger,
            BirthDate: $scope.Peyment.BirthDatePassenger,
            GivenName: $scope.Peyment.GivenNamePassenger,
            SurName: $scope.Peyment.SurNamePassenger,
            DocID: $scope.Peyment.DocID,
            ExpireDate: $scope.Peyment.ExpireDate,
            BirthCountry: $scope.Peyment.BirthCountry,
            SeatRequest: JSON.stringify($scope.SeatRequest)
        };

        ngProgress.start();
        $http({
            method: 'POST',
            url: '/Home/AddToCart',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: Object.toparams(param)
        }).then(function mySuccess(response) {
            if (response.data.success === false) {
                alert(response.data.responseText);
                return;
            }
            $scope.AddToCartResult = JSON.parse(response.data.responseText);
            $('#ModalFinalPriceDetail').modal('hide');
            $('#ModalAddtoCart').modal('show');
            ngProgress.complete();
            ngProgress.stop();
        }, function myError(response) {
            ngProgress.complete();
            ngProgress.stop();
        });
    };

    $scope.Payment = function () {
        $('#ModalAddtoCart').modal('hide');
        var param = {
            PaymentCode: $scope.AddToCartResult.PaymentCode
        };
        ngProgress.start();
        $http({
            method: 'POST',
            url: '/Home/Payment',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: Object.toparams(param)
        }).then(function mySuccess(response) {
            if (response.data.success === false) {
                alert(response.data.responseText);
                return;
            }
            $('#ModalETicket').modal('show');
            $scope.PaymentResult = JSON.parse(response.data.responseText)[0];
            ngProgress.complete();
            ngProgress.stop();
        }, function myError(response) {
            ngProgress.complete();
            ngProgress.stop();
        });
    };



    $scope.GetBaggages = function () {
        $http({
            method: 'Get',
            url: '/Home/GetBaggages',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },

        }).then(function mySuccess(response) {
            $scope.GetBaggages = JSON.parse(response.data.responseText);
        }, function myError(response) {
            alert('by');
        });
    };

    $scope.GetBaggageAmount = function (_index) {
        var list = ($scope.GetBaggages).filter(function (n) { return n.Index === _index });
        if (list[0] !== undefined) {
            return list[0].Amount;
        }
        else {
            return '';
        }
    };
    $scope.GetBaggageUnit = function (_index) {
        var list = ($scope.GetBaggages).filter(function (n) { return n.Index == _index });
        if (list[0] !== undefined) {
            return list[0].Unit;
        }
        else {
            return '';
        }
    };
    $scope.SelectingFlight = function (_select, _flightID) {
        if (_select) {
            $scope.SelectedFlightIDList.push(_flightID);
        }
        else if ($scope.SelectedFlightIDList !== null) {
            var index = $scope.SelectedFlightIDList.indexOf(_flightID);
            $scope.SelectedFlightIDList.splice(index, 1);
        }

    };
    $scope.SetColorRowFarFamily = function (_item) {
        if (_item === 'INCLUDE') {
            return { color: "green" };
        }
        if (_item !== 'INCLUDE') {
            return { color: "red" };
        }
    };
    $scope.SelectFareFamily = function (_fareAmount, _fareFamilyID) {
        $scope.FinalPriceDetaill.TotalFareAmout += _fareAmount;
        $scope.GetPriceDetailWithExtraService(_fareFamilyID);
        $('#ModalFareFamily').modal('hide');
    };



    //call from salam air web services
    $scope.RetrieveSecurityToken = function () {
        $http({
            method: 'Get',
            url: '/Home/RetrieveSecurityToken',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },

        }).then(function mySuccess(response) {
            $scope.SecurityToken = JSON.parse(response.data.responseText);
            $scope.GetLoginTravelAgent();
            $scope.GetRetrieveAgencyCommission();
        }, function myError(response) {
            alert('by');
        });
    };

    $scope.GetLoginTravelAgent = function () {
        var param = {
            SecurityGUID: $scope.SecurityToken
        }
        $http({
            method: 'Post',
            url: '/Home/LoginTravelAgent',
            data: Object.toparams(param),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },

        }).then(function mySuccess(response) {
            //$scope.LoginTravelAgent = JSON.parse(response.data.responseText);
        }, function myError(response) {
            alert('by');
        });
    };
    $scope.GetRetrieveAgencyCommission = function () {
        var param = {
            SecurityGUID: $scope.SecurityToken
        }
        $http({
            method: 'Post',
            url: '/Home/RetrieveAgencyCommission',
            data: Object.toparams(param),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },

        }).then(function mySuccess(response) {
            //$scope.RetrieveAgencyCommission = JSON.parse(response.data.responseText);
        }, function myError(response) {
            alert('by');
        });
    };

    $scope.fillAirports();
    $scope.GetBaggages();
    $scope.RetrieveSecurityToken();


    $scope.AddMultiFlight = function (_multi) {


        var item = {};
        item.DepartureDate = $scope.formatDate(_multi.DepartureDate);
        item.Origin = _multi.Origin;
        item.Destination = _multi.Destination;
        $scope.MultiFlights.push(item);

        _multi.DepartureDate = "";
        _multi.Origin = "";
        _multi.Destination = "";


    };

    $scope.RemoveAirItinerary = function (index) {

        var Origin = $scope.MultiFlights[index].Origin;
        var Destination = $scope.MultiFlights[index].Destination;
        if ($window.confirm("Do you want to delete: " + Origin + " to " + Destination)) {
            $scope.MultiFlights.splice(index, 1);
        }
    }


    $scope.formatDate = function (date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;

        return [year, month, day].join('-');
    }


    $scope.CalculateTotalBaseAmount = function (_item) {
        if (_item.Packages[0].PricingType.toLowerCase() === "PACKAGED".toLowerCase()) {

            return _item.Packages[0].TotalFareAmout;
        }
        else if (_item.Packages[0].PricingType.toLowerCase() === "FREE_FORM".toLowerCase()) {
            if (_item.Packages[1] == null) {
                return _item.Packages[0].TotalFareAmout;
            }
            else {
                var number1 = _item.Packages[0].TotalFareAmout
                var number2 = _item.Packages[1].TotalFareAmout
                return number1 + number2;
            }

        }
    }

    //$scope.SelectItemsInMulti = function (_ConnectionID, _PackageID, _PricingType, _ProviderCode) {

    //    var list = ($scope.SearchPackageLists).filter(function (n) {
    //        return n.Packages[2].ConnectionID === _ConnectionID + 1 &&
    //            n.Packages[2].PackageID === _PackageID &&
    //            n.Packages[2].PricingType === _PricingType &&
    //            n.Packages[2].ProviderCode === _ProviderCode
    //    });

    //    if (list[0] !== undefined) {

    //        $scope.SearchPackageLists = list;

    //    }


    //}


});