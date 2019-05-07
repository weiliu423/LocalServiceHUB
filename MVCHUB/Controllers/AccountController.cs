using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using MVCHUB.Services;
using MVCHUB.Models;
using System.Net.Http.Headers;
using System.Web.Http.Cors;

namespace MVCHUB.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : BaseApiController
    {
        IList<string> messages;
        AccountServices _accountSevices; 

        //Initializes a new instance of the <see cref="StresstestController"/> class.
        public AccountController()
        {
            _accountSevices = new AccountServices();
        }
        [HttpGet]
        [Route("")]
        public HttpResponseMessage Get()
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(@"<html><head><style>
            .by {margin : auto;} 
            .cn {
                  position: absolute;
                    top: 50%; left: 50%;
                transform: translate(-50%,-50%);
                  display: table-cell;
                  width: 500px;
                  height: 200px;
                  vertical-align: middle;
                  text-align: center;
                  color: red;
                  font-size: large;
                }    
            </style></head>
            <body class= ""by"">
            <div class=""cn""><div><u>Secure API Encoding</u></div><div>You are not authorised to see this message!</div></div></body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IHttpActionResult> getAllUsers()
        {
            try
            {
                //Calls method from service
                IEnumerable<string> clientNames = await _accountSevices.getAllUsers();

                if (clientNames != null)
                {
                    var response = Request.CreateResponse(
                                HttpStatusCode.OK,
                                new BaseDto<IEnumerable<string>>()
                                {
                                   // Success = true,
                                   // Message = "List of users",
                                    Data = clientNames
                                });
                    response.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(3600)
                    };
                    return ResponseMessage(response);
                    
                }
                //Error not found
                return NotFoundResponse();
            }
            catch (Exception ex)
            {
                //Returns a 500 response with the specified exception message
                return InternalServerErrorResponse(ex);
            }
        }

        [HttpGet]
        [Route("getAllUsersSql")]
        public async Task<IHttpActionResult> getAllClients()
        {
            try
            {
                //Calls method from service
                IEnumerable<string> clientNames = await _accountSevices.getAllUsersSql();

                if (clientNames != null)
                {
                    var response = Request.CreateResponse(
                                HttpStatusCode.OK,
                                new BaseDto<IEnumerable<string>>()
                                {                                 
                                    Data = clientNames
                                });
                    response.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(3600)
                    };
                    return ResponseMessage(response);

                }
                //Error not found
                return NotFoundResponse();
            }
            catch (Exception ex)
            {
                //Returns a 500 response with the specified exception message
                return InternalServerErrorResponse(ex);
            }
        }

        [HttpGet]
        [Route("getUserByName/{name}")]
        public async Task<IHttpActionResult> getUserByName(String name)
        {
            try
            {
                //Calls method from service
                IEnumerable<string> clientDetail = await _accountSevices.getUserByName(name);

                if (clientDetail != null)
                {
                    var response = Request.CreateResponse(
                                HttpStatusCode.OK,
                                new BaseDto<IEnumerable<string>>()
                                {
                                    Data = clientDetail
                                });
                    response.Headers.CacheControl = new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(3600)
                    };
                    return ResponseMessage(response);

                }
                //Error not found
                return NotFoundResponse();
            }
            catch (Exception ex)
            {
                //Returns a 500 response with the specified exception message
                return InternalServerErrorResponse(ex);
            }
        }
        //[HttpGet]
        //[Route("getAllTypes")]
        //public async Task<IHttpActionResult> getAllTypes()
        //{
        //    try
        //    {
        //        //Calls method from service
        //        IEnumerable<string> types = await _accountSevices.getAllTypes();

        //        if (types != null)
        //        {


        //            return ResponseMessage(
        //                 Request.CreateResponse(
        //                    HttpStatusCode.OK,
        //                    new BaseDto<IEnumerable<string>>()
        //                    {
        //                        Success = true,
        //                        Message = "List of types",
        //                        Data = types
        //                    }));
        //        }
        //        //Error not found
        //        return NotFoundResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Returns a 500 response with the specified exception message
        //        return InternalServerErrorResponse(ex);
        //    }
        //}

        //[HttpGet]
        //[Route("{clientName}/getAllClientStressTests")]
        //public async Task<IHttpActionResult> getAllClientStressTests(string clientName)
        //{
        //    try
        //    {
        //        if (clientName == null)
        //        {
        //            //Cannot not have a name.
        //            return NotFoundResponse();
        //        }


        //        //Calls method from service.
        //        IEnumerable<StressTestModel> allClientTest = await _accountSevices.getAllClientStressTests(clientName);

        //        if (allClientTest != null)
        //        {


        //            return ResponseMessage(
        //                 Request.CreateResponse(
        //                    HttpStatusCode.OK,
        //                    new BaseDto<IEnumerable<StressTestModel>>()
        //                    {
        //                        Success = true,
        //                        Message = "Clienet Stress Tests",
        //                        Data = allClientTest
        //                    }));
        //        }
        //        //Error if specified name does not exists
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Returns a 500 response with the specified exception message
        //        return InternalServerErrorResponse(ex);
        //    }
        //}

        //[HttpGet]
        //[Route("{clientName}/getClientStressTestById/{stressTestId}")]
        //public async Task<IHttpActionResult> getClientStressTestById(string clientName, int stressTestId)
        //{
        //    try
        //    {
        //        if (stressTestId == 0 || clientName == null)
        //        {
        //            //Return no data found
        //            return NotFoundResponse();
        //        }

        //        //Calls method from service
        //        StressTestModel stressTest = await _accountSevices.getClientStressTestById(clientName, stressTestId);

        //        if (stressTest != null)
        //        {


        //            return ResponseMessage(
        //                 Request.CreateResponse(
        //                    HttpStatusCode.OK,
        //                    new BaseDto<StressTestModel>()
        //                    {
        //                        Success = true,
        //                        Message = stressTest.ToString(),
        //                        Data = stressTest
        //                    }));
        //        }
        //        //Error not found
        //        return NoDataResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Returns a 500 response with the specified exception message
        //        return InternalServerErrorResponse(ex);
        //    }
        //}


        [HttpPost]
        [Route("createNewAccount")]
        public async Task<IHttpActionResult> CreateNewAccount(HttpRequestMessage createNew)
        {
            try
            {
                if (createNew != null)
                {
                    var json = await createNew.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(json))
                    {
                        //Return no data found
                        return NoDataResponse();
                    }

                        AccountModel createNewModel = JsonConvert.DeserializeObject<AccountModel>(json);

                    //Calls method from service
                        AccountModel createNewAccount = await _accountSevices.createNewAccount(createNewModel);

                        return ResponseMessage(
                        Request.CreateResponse(
                           HttpStatusCode.Created,
                           new BaseDto<String>()
                           {       
                               Success = true,
                              // Message = createNewAccount.UserName + " Created",
                               Data = createNewAccount.FullName
                           }));
                    }
                    return ResponseMessage(
                        Request.CreateResponse(
                           HttpStatusCode.Created,
                           new BaseDto<AccountModel>()
                           {
                               Success = false,
                               Message = "Error input " + HttpStatusCode.BadRequest,
                             
                           }));


            }
            catch (Exception ex)
            {
                //Returns a 500 response with the specified exception message
                return InternalServerErrorResponse(ex);
            }
        }

        [HttpPost]
        [Route("accountValidation")]
        public async Task<IHttpActionResult> AccountValidation(HttpRequestMessage account)
        {
            try
            {
                if (account != null)
                {
                    var json = await account.Content.ReadAsStringAsync();

                    if (string.IsNullOrEmpty(json))
                    {
                        //Return no data found
                        return NoDataResponse();
                    }

                    AccountModel accountmodel = JsonConvert.DeserializeObject<AccountModel>(json);

                    //Calls method from service
                    String check = await _accountSevices.accountValidation(accountmodel);
                    if(check == null)
                    {
                            return ResponseMessage(
                        Request.CreateResponse(
                           HttpStatusCode.Created,
                           new BaseDto<String>()
                           {
                               Success = false,
                               // Message = createNewAccount.UserName + " Created",
                               Data = check
                           }));
                    }
                    return ResponseMessage(
                    Request.CreateResponse(
                       HttpStatusCode.Created,
                       new BaseDto<String>()
                       {
                               Success = true,
                               // Message = createNewAccount.UserName + " Created",
                               Data = check
                       }));
                }
                return ResponseMessage(
                    Request.CreateResponse(
                       HttpStatusCode.Created,
                       new BaseDto<AccountModel>()
                       {
                           Success = false,
                           Message = "Error input " + HttpStatusCode.BadRequest,

                       }));


            }
            catch (Exception ex)
            {
                //Returns a 500 response with the specified exception message
                return InternalServerErrorResponse(ex);
            }
        }
 

        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<IHttpActionResult> DeleteAccount(int id)
        {
       
                    if (id == 0)
                    {
                        //Return no data found
                        return NoDataResponse();
                    }          

                    //Calls method from service
                    bool DeleteAccount = await _accountSevices.DeleteAccount(id);

                    return ResponseMessage(
                    Request.CreateResponse(
                       HttpStatusCode.Created,
                       new BaseDto<AccountModel>()
                       {
                               Success = DeleteAccount,
                               Message = "Account deleted",
                               Data = null
                       }));
                
         
         
        }
    }
}
