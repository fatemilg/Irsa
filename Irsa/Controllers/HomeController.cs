using Components.Irsa;
using Irsa.Components.Soap;
using Irsa.Configs;
using Irsa.JsonModels;
using Irsa.JsonModels.flight_search_request;
using Irsa.JsonModels.flight_search_response;
using Irsa.Models;
using Irsa.Repository.Wrapper;
using Irsa.XMLModels.Login_Travel_Agent_Request;
using Irsa.XMLModels.Retrieve_Agency_Commission_Request;
using Irsa.XMLModels.Retrieve_Fare_Quote_Request;
using Irsa.XMLModels.Retrieve_Fare_Quote_Response;
using Irsa.XMLModels.Retrieve_Security_Token_Request;
using Irsa.XMLModels.Retrieve_Security_Token_Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Irsa.Controllers
{

    public class HomeController : Controller
    {
        public bool GetFromV3 = true;

        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IServiceIrsa _serviceIrsa;
        private readonly IXmlService _xmlService;
        private readonly IMemoryCache _memoryCache;
        private readonly IManualLog _manualLog;
        Classes _class = new Classes();
        private IEnumerable<Package> cross_join_list;

        public HomeController(ILogger<HomeController> logger,
                             IRepositoryWrapper RepositoryWrapper,
                             IServiceIrsa serviceIrsa,
                             IXmlService xmlService,
                             IMemoryCache MemoryCache,
                             IManualLog ManualLog
                            )

        {
            _logger = logger;
            _serviceIrsa = serviceIrsa;
            _xmlService = xmlService;
            _repositoryWrapper = RepositoryWrapper;
            _memoryCache = MemoryCache;
            _manualLog = ManualLog;

        }

        public IActionResult index()
        {

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> ManualFlightSearch(string obj)
        {

            var convert_model = JsonConvert.DeserializeObject<flight_saerch_request>(obj);

            int Adult = int.Parse(convert_model.TravelerAvail.AdultCount);
            int Child = int.Parse(convert_model.TravelerAvail.ChildCount);
            int infant = int.Parse(convert_model.TravelerAvail.InfantCount);

            if (convert_model.FlightType == null)
                return Json(new { success = false, responseText = " نوع پرواز را انتخاب کنید" });

            if (Adult == 0)
                return Json(new { success = false, responseText = " تعداد بزرگسال باید بیشتر از 1 باشد" });

            if (Adult + Child > 9)
                return Json(new { success = false, responseText = " مجموع بزرگسال و کودک نباید بیشتر از 9 باشد" });

            if (infant > Adult)
            {
                return Json(new { success = false, responseText = " تعداد نوزاد نباید از تعداد بزرگسال بیشتر باشد" });
            }
            var Result = "";


            Result = await CalculateSearch(convert_model);


            return Json(new { success = true, responseText = Result });


        }

        private async Task<string> CalculateSearch(flight_saerch_request obj)
        {

            //Get from V3
            if (GetFromV3)
            {

                return await GetSearchFromV3(obj);
            }
            else
            {

                return await GetSearchFromSalamAir(obj);
            }

        }

        private async Task<string> GetSearchFromV3(flight_saerch_request obj)
        {
            //generate request model
            var _AirItineraries = new List<AirItinerary>();

            var generateParam = new flight_saerch_request()
            {
                TravelerAvail = new TravelerAvail()
                {
                    AdultCount = obj.TravelerAvail.AdultCount,
                    ChildCount = obj.TravelerAvail.ChildCount,
                    InfantCount = obj.TravelerAvail.InfantCount
                },
                AirItineraries = obj.AirItineraries,
                LoginID = Guid.Parse("BBE897FF-F402-4370-9906-7FF2980FA1ED")
            };

            //convert model to json
            var JsonBody = JsonConvert.SerializeObject(generateParam);

            //call service and return json
            var OutPut = "";
            OutPut = await _serviceIrsa.Post(@"https://testapi-v3.iati.ir/flight/search/908A42A77E359810068FBBEE010EA522", JsonBody.ToString());

            //convert json response to model
            var convertedOutPutToModel = JsonConvert.DeserializeObject<flight_search_response>(OutPut);


            var package_response_search_list = new List<PackageResponseSearch>();
            var package_list = new List<PackageList>();

            //cross join connection 0 and 1
            var grouped_list = convertedOutPutToModel.FlightItems.Flights.GroupBy(p => new { p.PackageID, p.ProviderCode, p.PricingType });
            foreach (var item in grouped_list)
            {
                var zero_list = convertedOutPutToModel.FlightItems.Flights.Where(x => x.ConnectionID == 0 &&
                                                                  x.PackageID == item.Key.PackageID &&
                                                                  x.ProviderCode == item.Key.ProviderCode &&
                                                                  x.PricingType == item.Key.PricingType);


                var one_list = convertedOutPutToModel.FlightItems.Flights.Where(x => x.ConnectionID == 1 &&
                                                                 x.PackageID == item.Key.PackageID &&
                                                                 x.ProviderCode == item.Key.ProviderCode &&
                                                                 x.PricingType == item.Key.PricingType);


        
                if (obj.FlightType == "RoundTrip")
                {
                     cross_join_list = from zl in zero_list

                                          from ol in one_list

                                          select new Package()

                                          {

                                              ZeroConnection = zl,

                                              OneConnection = ol
                                          };
                }
                else if (obj.FlightType == "One-way")
                {
                    cross_join_list = from zl in zero_list


                                      select new Package()

                                      {

                                          ZeroConnection = zl
                                      };
                }







                foreach (var cjl in cross_join_list.ToList())
                {
                    package_list.Add(new PackageList()
                    {
                        Packages = new List<JsonModels.flight_search_response.Flights>()
                        {
                            cjl.ZeroConnection,
                            cjl.OneConnection

                        }
                    });

                }

            }

            package_response_search_list.Add(
            new PackageResponseSearch()
            {
                PackageLists = package_list,
                FlightSearchResponse = new flight_search_response()
                {
                    SearchID = convertedOutPutToModel.SearchID,
                    SearchType = convertedOutPutToModel.SearchType
                }
            }

            );


            _manualLog.WriteLog((string)JsonBody, "SearchRequest");
            _manualLog.WriteLog((string)OutPut, "SearchResponse");

            return JsonConvert.SerializeObject(package_response_search_list);
        }



        private async Task<string> GetSearchFromSalamAir(flight_saerch_request obj)
        {
            var param = new Retrieve_Fare_Quote_Request()
            {
                Header = new XMLModels.Retrieve_Fare_Quote_Request.Header()
                {
                },
                Body = new XMLModels.Retrieve_Fare_Quote_Request.Body()
                {
                    RetrieveFareQuote = new RetrieveFareQuote()
                    {
                        RetrieveFareQuoteRequest = new RetrieveFareQuoteRequest()
                        {
                            SecurityGUID = obj.SecurityGUID,
                            CarrierCodes = new XMLModels.Retrieve_Fare_Quote_Request.CarrierCodes()
                            {
                                CarrierCode = new XMLModels.Retrieve_Fare_Quote_Request.CarrierCode()
                                {
                                    AccessibleCarrierCode = "OV"
                                }
                            },
                            ClientIPAddress = "127.0.0.1",
                            HistoricUserName = "ota_irsaaseman",
                            CurrencyOfFareQuote = "IRR",
                            PromotionalCode = "",
                            IataNumberOfRequestor = "OTAIRN03",
                            CorporationID = "-214",
                            FareFilterMethod = "NoCombinabilityRoundtripLowestFarePerFareType",
                            FareGroupMethod = "WebFareTypes",
                            InventoryFilterMethod = "All",
                            FareQuoteDetails = new FareQuoteDetails()
                            {
                                FareQuoteDetail = new List<FareQuoteDetail>()

                            },
                            ProfileId = "1"
                        }
                    }
                }
            };

            foreach (var item in obj.AirItineraries)
            {
                var _FareQuoteRequestInfo = new List<FareQuoteRequestInfo>();
                param.Body.RetrieveFareQuote.RetrieveFareQuoteRequest.FareQuoteDetails.FareQuoteDetail.Add(
                 new FareQuoteDetail()
                 {
                     Origin = item.Origin,
                     Destination = item.Destination,
                     UseAirportsNotMetroGroups = false,
                     UseAirportsNotMetroGroupsAsRule = false,
                     UseAirportsNotMetroGroupsForFrom = false,
                     UseAirportsNotMetroGroupsForTo = false,
                     DateOfDeparture = item.DepartureDate,
                     FareTypeCategory = "1",
                     FareClass = "",
                     FareBasisCode = "",
                     Cabin = "",
                     LFID = "-214",
                     OperatingCarrierCode = "",
                     MarketingCarrierCode = "",
                     NumberOfDaysBefore = "0",
                     NumberOfDaysAfter = "0",
                     LanguageCode = "en",
                     TicketPackageID = "1",
                     FareQuoteRequestInfos = new FareQuoteRequestInfos()
                     {
                         FareQuoteRequestInfo = _FareQuoteRequestInfo

                     }
                 });
                if (obj.TravelerAvail.AdultCount != "0")
                    _FareQuoteRequestInfo.Add(new FareQuoteRequestInfo
                    {
                        PassengerTypeID = "1",
                        TotalSeatsRequired = obj.TravelerAvail.AdultCount
                    });
                if (obj.TravelerAvail.ChildCount != "0")
                    _FareQuoteRequestInfo.Add(new FareQuoteRequestInfo
                    {
                        PassengerTypeID = "6",
                        TotalSeatsRequired = obj.TravelerAvail.ChildCount
                    });
                if (obj.TravelerAvail.InfantCount != "0")
                    _FareQuoteRequestInfo.Add(new FareQuoteRequestInfo
                    {
                        PassengerTypeID = "5",
                        TotalSeatsRequired = obj.TravelerAvail.InfantCount
                    });
            }

            var xmlBody = _class.GenerateXmlBody<Retrieve_Fare_Quote_Request>(param);


            var ResultWebService = await _xmlService.InvokeService("http://salappuat.radixxuat.com/SAL/Radixx.Connectpoint/ConnectPoint.Pricing.svc",
                                                          xmlBody,
                                                          "http://tempuri.org/IConnectPoint_Pricing/RetrieveFareQuote");



            var response = _class.DeserializeXmlToClass<Retrieve_Fare_Quote_Response>(ResultWebService);




            var _flight = new List<JsonModels.flight_search_response.Flights>();



            var resultModel = new flight_search_response()
            {
                SearchID = Guid.NewGuid(),
                SearchTimeSeconds = "00",
                CurrencyCode = "IRR",
                SearchType = obj.FlightType,
                DomesticFlight = false,
                HasMoreResult = false,
                AllFlightsCount = "",
                FlightItems = new JsonModels.flight_search_response.FlightItems()
                {
                    Flights = new List<JsonModels.flight_search_response.Flights>()

                }
            };

            foreach (var _flightSegment in response.Body.RetrieveFareQuoteResponse.RetrieveFareQuoteResult.FlightSegments.FlightSegment)
            {
                var _segmentDetail = response.Body.RetrieveFareQuoteResponse.RetrieveFareQuoteResult.SegmentDetails.SegmentDetail.Where(z => z.LFID == _flightSegment.LFID).SingleOrDefault();

                var LegDetails = response.Body.RetrieveFareQuoteResponse.RetrieveFareQuoteResult.LegDetails.LegDetail.Where(z => z.PFID == _flightSegment.FlightLegDetails.FlightLegDetail.SingleOrDefault().PFID);

                var _legsList = new List<Legs>();
                foreach (var leg in LegDetails)
                {

                    _legsList.Add(new Legs()
                    {
                        FlightNumber = leg.FlightNum,
                        DepartureAirport = leg.Origin,
                        ArrivalAirport = leg.Destination,
                        DepartureTime = leg.DepartureDate,
                        ArrivalTime = leg.ArrivalDate,
                        FlightDurationMinutes = leg.FlightTime,
                        LayoverDurationMinutes = "0",
                        // SeatCount = tooie fare por mishavad

                    });
                }


                foreach (var _fareType in _flightSegment.FareTypes.FareType)
                {
                    var _faresList = new List<Fares>();
                    foreach (var _fareInfo in _fareType.FareInfos.FareInfo)
                    {
                        string _PassengerType = "";
                        string _Quantity = "";
                        if (_fareInfo.PTCID == "1")
                        {
                            _PassengerType = "ADULT";
                            _Quantity = obj.TravelerAvail.AdultCount;
                        }
                        else if (_fareInfo.PTCID == "6")
                        {
                            _PassengerType = "CHILD";
                            _Quantity = obj.TravelerAvail.ChildCount;
                        }

                        else if (_fareInfo.PTCID == "5")
                        {
                            _PassengerType = "INFANT";
                            _Quantity = obj.TravelerAvail.InfantCount;

                        }


                        _faresList.Add(new Fares()
                        {
                            PassengerType = _PassengerType,
                            Quantity = int.Parse(_Quantity),
                            BaseFare = (int.Parse(_fareInfo.BaseFareAmt)) * int.Parse(_Quantity),
                            Tax = (int.Parse(_fareInfo.BaseFareAmtInclTax) - int.Parse(_fareInfo.BaseFareAmt)) * int.Parse(_Quantity),
                            ServiceCommission = "0",
                            ServiceCommissionOnFare = "0",
                            ServiceProviderCost = "0",
                            ServiceFee = "0",
                            APICost = "0",
                            Commission = "0",
                            Supplement = "0",
                            Fuel = "0",
                            Discount = "0",
                            Total = (int.Parse(_fareInfo.BaseFareAmtInclTax) * int.Parse(_Quantity))

                        });


                    }


                    int _BaggageDetailID = 0;

                    switch (_fareType.FareTypeName.Trim().ToUpper())
                    {
                        case "LIGHT":
                            _BaggageDetailID = 0;
                            break;
                        case "FRIENDLY":
                            _BaggageDetailID = 4;
                            break;
                        case "FRIENDLYVALUE":
                            _BaggageDetailID = 6;
                            break;
                        case "FRIENDLYPLUS":
                            _BaggageDetailID = 8;
                            break;
                        case "FLEXI":
                            _BaggageDetailID = 4;
                            break;
                    }

                    var _minSeatCount = _fareType.FareInfos.FareInfo.Min(a => a.SeatsAvailable);
                    var _baggageList = new List<BaggageItems>();
                    _baggageList.Add(new BaggageItems()
                    {
                        BaggageDetailID = _BaggageDetailID,
                        PassengerType = "ADULT"
                    });
                    //if (obj.TravelerAvail.ChildCount != "0")
                    //{
                    //    _baggageList.Add(new BaggageItems()
                    //    {
                    //        BaggageDetailID = _BaggageDetailID,
                    //        PassengerType = "Child"
                    //    });
                    //}
                    _legsList.ForEach(c =>
                    {
                        c.SeatCount = _minSeatCount > 10 ? "9" : _minSeatCount.ToString();
                        c.BaggageItems = _baggageList;

                    });


                    resultModel.FlightItems.Flights.Add(new JsonModels.flight_search_response.Flights()
                    {

                        FlightID = Guid.NewGuid(),
                        PricingType = "Free_Form",
                        PackageID = "1",
                        ConnectionID = _flightSegment.LegCount - 1,
                        TotalFareAmout = _faresList.Sum(c => c.Total),
                        TotalFlightDuration = "0",
                        Stops = _segmentDetail.Stops,
                        ProviderCode = _segmentDetail.CarrierCode,
                        CabinClass = _fareType.FareInfos.FareInfo.Select(x => x.Cabin).Take(1).FirstOrDefault(),
                        Fares = _faresList,
                        Legs = _legsList
                    });


                }


            }




            return JsonConvert.SerializeObject(resultModel);
        }






        [HttpPost]
        public async Task<IActionResult> GetPriceDetailWithExtraService(PriceDetailRequest obj)
        {
            string[] flighList = obj.SelectedFlightIDList.Split(",");

            dynamic generateParam = new System.Dynamic.ExpandoObject();
            generateParam = new
            {

                SearchID = obj.SearchID,
                FlightIDs = flighList,
                TravelerAvail = new
                {
                    AdultCount = obj.AdultCount,
                    ChildCount = obj.ChildCount,
                    InfantCount = obj.InfantCount
                },
                ExtraServices = new
                {
                    GetAncillaryList = true,
                    GetSeatMap = true,
                    GetFareFamily = true

                },
                FareFamilyID = obj.FareFamilyID,
                Currency = "USD",
                LanguageCode = "EN"
            };
            var JsonBody = JsonConvert.SerializeObject(generateParam);
            var Result = await _serviceIrsa.Post(@" https://testapi-v3.iati.ir/Flight/Price_Detail/908A42A77E359810068FBBEE010EA522", JsonBody.ToString());

            _manualLog.WriteLog((string)JsonBody, "PriceDetailWithExtraServiceRequest");
            _manualLog.WriteLog((string)Result, "PriceDetailWithExtraServiceResponse");

            return Json(new { success = true, responseText = Result });


        }

        [HttpPost]
        public async Task<IActionResult> GetFareFamily(FareFamilyRequest obj)
        {
            string[] flighList = obj.SelectedFlightIDList.Split(",");

            dynamic generateParam = new System.Dynamic.ExpandoObject();
            generateParam = new
            {

                SearchID = obj.SearchID,
                FlightIDs = flighList,
                TravelerAvail = new
                {
                    AdultCount = obj.AdultCount,
                    ChildCount = obj.ChildCount,
                    InfantCount = obj.InfantCount
                },
                Currency = "USD",
                LanguageCode = "EN"
            };
            var JsonBody = JsonConvert.SerializeObject(generateParam);
            var Result = await _serviceIrsa.Post(@" https://testapi-v3.iati.ir/Flight/Fare_Families/908A42A77E359810068FBBEE010EA522", JsonBody.ToString());


            _manualLog.WriteLog((string)JsonBody, "FareFamilyRequest");
            _manualLog.WriteLog((string)Result, "FareFamilyResponse");

            return Json(new { success = true, responseText = Result });


        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartRequest obj)
        {
            string[] EnduserEmail = obj.EnduserEmail.Split(",");

            var SeatRequestInAddToCart = JsonConvert.DeserializeObject<List<SeatRequestInAddToCart>>(obj.SeatRequest);

            dynamic generateParam = new System.Dynamic.ExpandoObject();
            generateParam = new
            {

                PaymentCode = "",
                LoginID = "BBE897FF-F402-4370-9906-7FF2980FA1ED",
                PriceDetailID = obj.PriceDetailID,
                ServiceName = "FLIGHT",
                PaymentType = "VMONEY",
                IPGID = "",
                IssueType = "BOOK",
                ExteraAgency = 0,
                CallBackURL = "",
                CouponCode = "",
                SendBookInfo = "false",
                ContactDetail = new
                {
                    Emails = EnduserEmail,
                    Cellphones = new[] {
                        new {
                            AreaCode = "921",
                            CountryCode = "+7",
                            PhoneNumber = obj.EnduserCellPhone
                        }
                    },
                    PhoneNumber = "",
                    Address = "",
                    AgencyPhone = "00905384114007",
                    AgencyEmail = "agancyemail@Sairosoft.com"

                },
                Travelers = new[]
                {
                    new
                    {
                        PassengerIndex= 0,
                        PassengerType= "ADULT",
                        Gender= obj.Gender,
                        BirthDate= obj.BirthDate,
                        TravelerName = new[]
                        {
                             new
                             {
                                GivenName= obj.GivenName,
                                SurName= obj.SurName,
                                LanguageCode= "EN"
                             }
                        },
                        DocumentsDetail = new[]
                        {
                             new
                             {
                                DocType= "DOCS",
                                DocID= obj.DocID,
                                DocIssueCountry= obj.BirthCountry,
                                ExpireDate= obj.ExpireDate,
                                IssueLocation= obj.BirthCountry,
                                BirthCountry= obj.BirthCountry
                             }
                        },
                        SeatsRequest=SeatRequestInAddToCart

                    }
                }
            };
            var JsonBody = JsonConvert.SerializeObject(generateParam);
            var Result = await _serviceIrsa.Post(@" https://testapi-v3.iati.ir/Payment/Add_To_Cart/908A42A77E359810068FBBEE010EA522", JsonBody.ToString());

            _manualLog.WriteLog((string)JsonBody, "AddToCartRequest");
            _manualLog.WriteLog((string)Result, "AddToCartResponse");

            return Json(new { success = true, responseText = Result });


        }

        [HttpPost]
        public async Task<IActionResult> Payment(PaymentRequest obj)
        {
            try
            {
                dynamic generateParam = new System.Dynamic.ExpandoObject();
                generateParam = new
                {

                    PaymentCode = obj.PaymentCode,

                };
                var JsonBody = JsonConvert.SerializeObject(generateParam);
                var Result = await _serviceIrsa.Post(@"https://testapi-v3.iati.ir/Payment/VMoney_Payment/908A42A77E359810068FBBEE010EA522", JsonBody.ToString());

                _manualLog.WriteLog((string)JsonBody, "PaymentRequest");
                _manualLog.WriteLog((string)Result, "PaymentResponse");

                return Json(new { success = true, responseText = Result });
            }
            catch (Exception e)
            {

                throw;
            }



        }

        [HttpPost]
        public async Task<IActionResult> FareRules(FareRuleRequest obj)
        {
            try
            {
                string[] flighList = obj.SelectedFlightIDList.Split(",");

                dynamic generateParam = new System.Dynamic.ExpandoObject();
                generateParam = new
                {

                    SearchID = obj.SearchID,
                    FlightIDs = flighList,
                    LoginID = "BBE897FF-F402-4370-9906-7FF2980FA1ED"
                };
                var JsonBody = JsonConvert.SerializeObject(generateParam);
                var Result = await _serviceIrsa.Post(@"https://testapi-v3.iati.ir/Flight/Fare_Rule/908A42A77E359810068FBBEE010EA522", JsonBody.ToString());


                return Json(new { success = true, responseText = Result });
            }
            catch (Exception e)
            {

                throw;
            }



        }

        [HttpPost]
        public async Task<IActionResult> GetSeats(SeatRequest obj)
        {
            try
            {
                string[] flighList = obj.SelectedFlightIDList.Split(",");

                dynamic generateParam = new System.Dynamic.ExpandoObject();
                generateParam = new
                {

                    SearchID = obj.SearchID,
                    FlightIDs = flighList,
                    Currency = "USD"
                };
                var JsonBody = JsonConvert.SerializeObject(generateParam);
                var Result = await _serviceIrsa.Post(@" https://testapi-v3.iati.ir/Flight/Seat_Map/908A42A77E359810068FBBEE010EA522", JsonBody.ToString());
                return Json(new { success = true, responseText = Result });
            }
            catch (Exception e)
            {

                throw;
            }



        }


        public IActionResult Custom_flight()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetAirports()
        {
            if (!_memoryCache.TryGetValue("AirPorts", out var result))
            {
                // Key not in cache, so get data.
                result = await _serviceIrsa.Post(@"https://testapi-v3.iati.ir/Flight/Airports_List/908A42A77E359810068FBBEE010EA522", "");

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(60),  // tahte har sharayeti expire mishe
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(10)  // expire mishe be sharti ke dar in modat estefade nakonim
                };
                // Save data in cache.
                _memoryCache.Set("AirPorts", result, cacheEntryOptions);
            }

            var models = JsonConvert.DeserializeObject<List<Airport>>(result.ToString());
            var matchingModel = models.Where(x => x.AirportCode == "IST" ||
                                             x.AirportCode.Contains("DX") ||
                                             x.AirportCode == "MCT" ||
                                             x.AirportCode == "CGP" ||
                                             x.AirportCode == "CDG" ||
                                             x.AirportCode == "LAX");
            return Json(new { success = true, responseText = JsonConvert.SerializeObject(matchingModel) });

        }

        [HttpGet]
        public async Task<IActionResult> GetBaggages()
        {

            if (!_memoryCache.TryGetValue("Baggages", out var result))
            {
                // Key not in cache, so get data.
                result = await _repositoryWrapper.Baggage.GetAllBaggage();

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(60),  // tahte har sharayeti expire mishe
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(10)  // expire mishe be sharti ke dar in modat estefade nakonim
                };
                // Save data in cache.
                _memoryCache.Set("Baggages", result, cacheEntryOptions);
            }

            //var models = JsonConvert.DeserializeObject<List<Baggage>>(result.ToString());
            return Json(new { success = true, responseText = JsonConvert.SerializeObject(result) });
        }






        //call from web services
        [HttpGet]
        public async Task<IActionResult> RetrieveSecurityToken()
        {

            var param = new Retrieve_Security_Token_Request()
            {
                Header = new XMLModels.Retrieve_Security_Token_Request.Header()
                {
                },
                Body = new XMLModels.Retrieve_Security_Token_Request.Body()
                {
                    RetrieveSecurityToken = new RetrieveSecurityToken()
                    {
                        RetrieveSecurityTokenRequest = new RetrieveSecurityTokenRequest()
                        {
                            CarrierCodes = new XMLModels.Retrieve_Security_Token_Request.CarrierCodes()
                            {
                                CarrierCode = new XMLModels.Retrieve_Security_Token_Request.CarrierCode()
                                {
                                    AccessibleCarrierCode = "OV"
                                }
                            },
                            LogonID = "IRAN_OV_U",
                            Password = "IAT$UAT!"
                        }
                    }
                }
            };



            var xmlBody = _class.GenerateXmlBody<Retrieve_Security_Token_Request>(param);


            var Result = await _xmlService.InvokeService("http://salappuat.radixxuat.com/SAL/Radixx.Connectpoint/ConnectPoint.Security.svc",
                                                          xmlBody,
                                                          "http://tempuri.org/IConnectPoint_Security/RetrieveSecurityToken");

            Retrieve_Security_Token_Response response = _class.DeserializeXmlToClass<Retrieve_Security_Token_Response>(Result);

            return Json(new { success = true, responseText = JsonConvert.SerializeObject(response.Body.RetrieveSecurityTokenResponse.RetrieveSecurityTokenResult.SecurityToken) });

        }


        [HttpPost]
        public async Task<IActionResult> LoginTravelAgent(LoginTravelAgentRequest LTAR)
        {

            var param = new Login_Travel_Agent_Request()
            {
                Header = new XMLModels.Login_Travel_Agent_Request.Header()
                {
                },
                Body = new XMLModels.Login_Travel_Agent_Request.Body()
                {
                    LoginTravelAgent = new LoginTravelAgent()
                    {
                        LoginTravelAgentRequest = new LoginTravelAgentRequest()
                        {
                            SecurityGUID = LTAR.SecurityGUID,
                            CarrierCodes = new XMLModels.Login_Travel_Agent_Request.CarrierCodes()
                            {
                                CarrierCode = new XMLModels.Login_Travel_Agent_Request.CarrierCode()
                                {
                                    AccessibleCarrierCode = "OV"
                                }
                            },
                            ClientIPAddress = "127.0.0.1",
                            HistoricUserName = "ota_irsaaseman",
                            IATANumber = "OTAIRN03",
                            UserName = "ota_irsaaseman",
                            Password = "Confirm@1234"
                        }
                    }
                }
            };



            var xmlBody = _class.GenerateXmlBody<Login_Travel_Agent_Request>(param);


            var Result = await _xmlService.InvokeService("http://salappuat.radixxuat.com/SAL/Radixx.Connectpoint/ConnectPoint.TravelAgents.svc",
                                                          xmlBody,
                                                          "http://tempuri.org/IConnectPoint_TravelAgents/LoginTravelAgent");



            return Json(new { success = true, responseText = Result });

        }



        [HttpPost]
        public async Task<IActionResult> RetrieveAgencyCommission(RetrieveAgencyCommissionRequest RACR)
        {

            var param = new Retrieve_Agency_Commission_Request()
            {
                Header = new XMLModels.Retrieve_Agency_Commission_Request.Header()
                {
                },
                Body = new XMLModels.Retrieve_Agency_Commission_Request.Body()
                {
                    RetrieveAgencyCommission = new RetrieveAgencyCommission()
                    {
                        RetrieveAgencyCommissionRequest = new RetrieveAgencyCommissionRequest()
                        {
                            SecurityGUID = RACR.SecurityGUID,
                            CarrierCodes = new XMLModels.Retrieve_Agency_Commission_Request.CarrierCodes()
                            {
                                CarrierCode = new XMLModels.Retrieve_Agency_Commission_Request.CarrierCode()
                                {
                                    AccessibleCarrierCode = "OV"
                                }
                            },
                            ClientIPAddress = "127.0.0.1",
                            HistoricUserName = "ota_irsaaseman",
                            CurrencyCode = "IRR"
                        }
                    }
                }
            };



            var xmlBody = _class.GenerateXmlBody<Retrieve_Agency_Commission_Request>(param);


            var Result = await _xmlService.InvokeService("http://salappuat.radixxuat.com/SAL/Radixx.Connectpoint/ConnectPoint.TravelAgents.svc",
                                                          xmlBody,
                                                          "http://tempuri.org/IConnectPoint_TravelAgents/RetrieveAgencyCommission");



            return Json(new { success = true, responseText = Result });

        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}
