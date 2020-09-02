using Components.Irsa;
using Irsa.Components.Soap;
using Irsa.Configs;
using Irsa.Models;
using Irsa.Repository.Wrapper;
using Irsa.XMLModels.Login_Travel_Agent_Request;
using Irsa.XMLModels.Retrieve_Agency_Commission_Request;
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
using System.Xml.Serialization;

namespace Irsa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IServiceIrsa _serviceIrsa;
        private readonly IXmlService _xmlService;
        private readonly IMemoryCache _memoryCache;
        private readonly IManualLog _manualLog;
        Classes obj = new Classes();

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
        public async Task<IActionResult> ManualFlightSearch(FlightRequest obj)
        {
            int Adult = int.Parse(obj.AdultCount);
            int Child = int.Parse(obj.ChildCount);
            int infant = int.Parse(obj.InfantCount);

            if (obj.FlightType == null)
                return Json(new { success = false, responseText = " نوع پرواز را انتخاب کنید" });

            if (Adult == 0)
                return Json(new { success = false, responseText = " تعداد بزرگسال باید بیشتر از 1 باشد" });

            if (Adult + Child > 9)
                return Json(new { success = false, responseText = " مجموع بزرگسال و کودک نباید بیشتر از 9 باشد" });

            if (infant > Adult)
            {
                return Json(new { success = false, responseText = " تعداد نوزاد نباید از تعداد بزرگسال بیشتر باشد" });
            }

            dynamic generateParam = new System.Dynamic.ExpandoObject();


            if (obj.FlightType == "One-way")
            {
                generateParam = new
                {
                    TravelerAvail = new
                    {
                        AdultCount = obj.AdultCount,
                        ChildCount = obj.ChildCount,
                        InfantCount = obj.InfantCount
                    },
                    AirItineraries = new[]
                    {
                    new {
                        DepartureDate=obj.GoDate,
                        Origin=obj.Origin,
                        Destination=obj.Destination,
                        ConnectionID= 0
                        }
                    },
                    LoginID = "BBE897FF-F402-4370-9906-7FF2980FA1ED",
                    SearchFilters = new
                    {
                        Language = obj.Language,
                        Cabin = obj.Cabin,
                        Currency = obj.Currency
                    },
                };
            }
            else if (obj.FlightType == "RoundTrip")
            {
                generateParam = new
                {
                    TravelerAvail = new
                    {
                        AdultCount = obj.AdultCount,
                        ChildCount = obj.ChildCount,
                        InfantCount = obj.InfantCount
                    },
                    AirItineraries = new[]
                    {
                    new {
                        DepartureDate=obj.GoDate,
                        Origin=obj.Origin,
                        Destination=obj.Destination,
                        ConnectionID= 0
                        },

                   new {
                        DepartureDate=obj.BackDate,
                        Origin=obj.Destination,
                        Destination=obj.Origin,
                        ConnectionID= 1
                        },
                   },
                    LoginID = "BBE897FF-F402-4370-9906-7FF2980FA1ED",
                    SearchFilters = new
                    {
                        Language = obj.Language,
                        Cabin = obj.Cabin,
                        Currency = obj.Currency
                    },

                };
            }
            else if (obj.FlightType == "Multi")
            {
                generateParam = new
                {
                    TravelerAvail = new
                    {
                        AdultCount = obj.AdultCount,
                        ChildCount = obj.ChildCount,
                        InfantCount = obj.InfantCount
                    },
                    AirItineraries = new[]
                    {
                    new {
                        DepartureDate=obj.GoDate,
                        Origin=obj.Origin,
                        Destination=obj.Destination,
                        ConnectionID= 0
                        },

                    new {
                        DepartureDate=obj.GoDate2,
                        Origin=obj.Origin2,
                        Destination=obj.Destination2,
                        ConnectionID= 1
                        },
                    new {
                        DepartureDate=obj.GoDate3,
                        Origin=obj.Origin3,
                        Destination=obj.Destination3,
                        ConnectionID= 2
                        },
                    },
                    LoginID = "BBE897FF-F402-4370-9906-7FF2980FA1ED",
                    SearchFilters = new
                    {
                        Language = obj.Language,
                        Cabin = obj.Cabin,
                        Currency = obj.Currency
                    },

                };
            }

            var JsonBody = JsonConvert.SerializeObject(generateParam);

            var Result = await _serviceIrsa.Post(@" https://api-v3.iati.ir/flight/Cached_Search//908A42A77E359810068FBBEE010EA522", JsonBody.ToString());
            if (Result == "")
            {
                Result = await _serviceIrsa.Post(@"https://testapi-v3.iati.ir/flight/search/908A42A77E359810068FBBEE010EA522", JsonBody.ToString());

            }
            _manualLog.WriteLog((string)JsonBody, "SearchRequest");
            _manualLog.WriteLog((string)Result, "SearchResponse");
            return Json(new { success = true, responseText = Result });

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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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



            var xmlBody = obj.GenerateXmlBody<Retrieve_Security_Token_Request>(param);


            var Result = await _xmlService.InvokeService("http://salappuat.radixxuat.com/SAL/Radixx.Connectpoint/ConnectPoint.Security.svc",
                                                          xmlBody,
                                                          "http://tempuri.org/IConnectPoint_Security/RetrieveSecurityToken");

            Retrieve_Security_Token_Response response = obj.DeserializeXmlToClass<Retrieve_Security_Token_Response>(Result);

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
                            IATANumber= "OTAIRN03",
                            UserName= "ota_irsaaseman",
                            Password= "Confirm@1234"
                        }
                    }
                }
            };



            var xmlBody = obj.GenerateXmlBody<Login_Travel_Agent_Request>(param);


            var Result = await _xmlService.InvokeService("http://salappuat.radixxuat.com/SAL/Radixx.Connectpoint/ConnectPoint.TravelAgents.svc",
                                                          xmlBody,
                                                          "http://tempuri.org/IConnectPoint_TravelAgents/LoginTravelAgent");



            return Json(new { success = true, responseText = Result });

        }



        [HttpPost]
        public async Task<IActionResult> RetrieveAgencyCommission(RetrieveAgencyCommissionRequest LTAR)
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
                            SecurityGUID = LTAR.SecurityGUID,
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



            var xmlBody = obj.GenerateXmlBody<Retrieve_Agency_Commission_Request>(param);


            var Result = await _xmlService.InvokeService("http://salappuat.radixxuat.com/SAL/Radixx.Connectpoint/ConnectPoint.TravelAgents.svc",
                                                          xmlBody,
                                                          "http://tempuri.org/IConnectPoint_TravelAgents/RetrieveAgencyCommission");



            return Json(new { success = true, responseText = Result });

        }



    }
}
