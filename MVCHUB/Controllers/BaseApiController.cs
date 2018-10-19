using MVCHUB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace MVCHUB.Controllers
{
    public class BaseApiController : ApiController
    {
       
        /// <summary>
        /// Gets or sets the log.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <value>
        /// The log.
        /// </value>
        public BaseApiController()
        {
           
        }

        /// <summary>
        ///     Gets the ping.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Base DTO with string</returns>
        [HttpGet]
        [Route("ping")]
        public BaseDto<string> GetPing(string message = null)
        {
            string clientAddress = HttpContext.Current.Request.UserHostAddress;

           
            try
            {
                return new BaseDto<string>
                {
                    Success = true,
                    Data = message + ": Local Service HUB API ENDPOINT BY WEI"
                };
            }
            catch (Exception e)
            {
                return new BaseDto<string>()
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }

        /// <summary>
        /// Returns the "no client" response.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        public ResponseMessageResult NoClientResponse(string client)
        {
            

            return ResponseMessage(
                Request.CreateResponse(
                    HttpStatusCode.NotFound,
                    new BaseDto<int>()
                    {
                        Success = false,
                        Message = $"No such User {client} available"
                    }));
        }

        /// <summary>
        /// Returns the "no data" response.
        /// </summary>
        /// <returns></returns>
        public ResponseMessageResult NoDataResponse()
        {
            

            return ResponseMessage(
                Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new BaseDto<int>()
                    {
                        Success = false,
                        Message = "The resource submitted contains no data or could not be serialized: please check the information is correct and try again"
                    }));
        }

        /// <summary>
        /// Noes the response.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        public ResponseMessageResult NoResponse(string service)
        {
            

            return ResponseMessage(
                Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new BaseDto<string>()
                    {
                        Success = false,
                        Message = "No response received from " + service,
                        Data = null
                    }));
        }

        /// <summary>
        /// Returns a 500 response with the specified exception message.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public ResponseMessageResult InternalServerErrorResponse(Exception e)
        {
            

            return ResponseMessage(
                Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    new BaseDto<string>()
                    {
                        Success = false,
                        Message = "Error: " + e.Message
                    }));
        }

        /// <summary>
        /// Returns an "OK" response.
        /// </summary>
        /// <returns></returns>
        public ResponseMessageResult BasicOkResponse()
        {
           

            return ResponseMessage(
                Request.CreateResponse(
                    HttpStatusCode.OK,
                    new BaseDto<int>()
                    {
                        Success = true
                    }));
        }

        /// <summary>
        /// Returns an "Not Found" response.
        /// </summary>
        /// <returns></returns>
        public ResponseMessageResult NotFoundResponse()
        {
       

            return ResponseMessage(
                Request.CreateResponse(
                    HttpStatusCode.NotFound,
                    new BaseDto<string>()
                    {
                        Success = false,
                        Message = "No resource was found matching the specified criteria"
                    }));
        }
    }
}