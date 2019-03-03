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

        //Error messages
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
                           new BaseDto<AccountModel>()
                           {       
                               Success = true,
                              // Message = createNewAccount.UserName + " Created",
                               Data = createNewAccount
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

        //[HttpPost]
        //[Route("{clientName}/runSqlTest")]
        //public async Task<IHttpActionResult> executeStressTest(string clientName, HttpRequestMessage request)
        //{
        //    try
        //    {


        //        if (request != null)
        //        {
        //            var json = await request.Content.ReadAsStringAsync();

        //            if (string.IsNullOrEmpty(json))
        //            {
        //                //Return no data found
        //                return NoDataResponse();
        //            }

        //            // var schema = _JsonSchemaService.JsonData();
        //            //  var validate = IsValid(JObject.Parse(json), schema);

        //            //  if (validate != false)
        //            //  {
        //            ExecuteModel executeData = JsonConvert.DeserializeObject<ExecuteModel>(json);

        //            //Calls method from service
        //            string execute = await _iStressTestService.execStressTest(clientName, executeData);

        //            return ResponseMessage(
        //            Request.CreateResponse(
        //               HttpStatusCode.Created,
        //               new BaseDto<string>()
        //               {
        //                   Success = true,
        //                   Message = execute,
        //                   Data = execute
        //               }));
        //            //  }

        //            //return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error Input: " + messages));
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

        //[HttpPut]
        //[Route("{clientName}/updateStressTest/{testId}")]
        //public async Task<IHttpActionResult> updateStressTest(string clientName, int testId, HttpRequestMessage update)
        //{
        //    try
        //    {

        //        if (update != null)
        //        {
        //            var json = await update.Content.ReadAsStringAsync();

        //            if (string.IsNullOrEmpty(json))
        //            {
        //                //Return no data found
        //                return NoDataResponse();
        //            }

        //            var schema = _JsonSchemaService.JsonData();
        //            var validate = IsValid(JObject.Parse(json), schema);

        //            if (validate != false)
        //            {
        //                StressTestModel updateModel = JsonConvert.DeserializeObject<StressTestModel>(json);

        //                //Calls method from service
        //                StressTestModel updateStressTest = await _iStressTestService.updateStressTest(clientName, testId, updateModel);

        //                // Create response message instead of no content
        //                var message = Request.CreateResponse(HttpStatusCode.OK, updateStressTest);

        //                return ResponseMessage(
        //                     Request.CreateResponse(
        //                        HttpStatusCode.OK,
        //                        new BaseDto<StressTestModel>()
        //                        {
        //                            Success = validate,
        //                            Message = message.ToString(),
        //                            Data = updateStressTest
        //                        }));
        //            }

        //            return Content(HttpStatusCode.BadRequest, "Error Input: " + messages);
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

        //[HttpPut]
        //[Route("{clientName}/disableStressTest/{testId}")]
        //public async Task<IHttpActionResult> disableStressTest(string clientName, int testId, HttpRequestMessage disable)
        //{
        //    try
        //    {

        //        if (disable != null)
        //        {
        //            var json = await disable.Content.ReadAsStringAsync();

        //            if (string.IsNullOrEmpty(json))
        //            {
        //                //Return no data found
        //                return NoDataResponse();
        //            }

        //            var schema = _JsonSchemaService.JsonData();
        //            var validate = IsValid(JObject.Parse(json), schema);

        //            if (validate != false)
        //            {
        //                StressTestModel disableModel = JsonConvert.DeserializeObject<StressTestModel>(json);

        //                //Calls method from service
        //                StressTestModel disabled = await _iStressTestService.disableStressTest(clientName, testId, disableModel);

        //                // Create response message instead of no content
        //                var message = Request.CreateResponse(HttpStatusCode.OK, disabled);

        //                return ResponseMessage(
        //                     Request.CreateResponse(
        //                        HttpStatusCode.OK,
        //                        new BaseDto<StressTestModel>()
        //                        {
        //                            Success = validate,
        //                            Message = message.ToString(),
        //                            Data = disabled
        //                        }));
        //            }

        //            return Content(HttpStatusCode.BadRequest, "Error Input: " + messages);
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

        //[HttpPut]
        //[Route("{clientName}/activateStressTest/{testId}")]
        //public async Task<IHttpActionResult> activateStressTest(string clientName, int testId, HttpRequestMessage activate)
        //{
        //    try
        //    {


        //        if (activate != null)
        //        {
        //            var json = await activate.Content.ReadAsStringAsync();

        //            if (string.IsNullOrEmpty(json))
        //            {
        //                //Return no data found
        //                return NoDataResponse();
        //            }

        //            var schema = _JsonSchemaService.JsonData();
        //            var validate = IsValid(JObject.Parse(json), schema);

        //            if (validate != false)
        //            {
        //                StressTestModel activateModel = JsonConvert.DeserializeObject<StressTestModel>(json);

        //                //Calls method from service
        //                StressTestModel activated = await _iStressTestService.activateStressTest(clientName, testId, activateModel);

        //                // Create response message instead of no content
        //                var message = Request.CreateResponse(HttpStatusCode.OK, activated);

        //                return ResponseMessage(
        //                     Request.CreateResponse(
        //                        HttpStatusCode.OK,
        //                        new BaseDto<StressTestModel>()
        //                        {
        //                            Success = validate,
        //                            Message = message.ToString(),
        //                            Data = activated
        //                        }));
        //            }

        //            return Content(HttpStatusCode.BadRequest, "Error Input: " + messages);
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

        //private bool IsValid(JObject obj, JSchema schema)
        //{
        //    return obj.IsValid(schema, out messages);
        //}

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
