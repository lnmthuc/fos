﻿using FOS.Model.Domain;
using FOS.Model.Util;
using FOS.Services.SendEmailServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FOS.API.Controllers
{
    public class SendEmailController : ApiController
    {
        ISendEmailService _sendEmailService;
        public SendEmailController(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }
        [HttpGet]
        [Route("SendEmail")]
        public async Task<ApiResponse> GetIdsAsync(string eventId)
        {
            try
            {
                string html = System.IO.File.ReadAllText(@"D:\Training project\Project\FOS2\fos\fos-api\FOS\FOS.API\App_Data\email_template.txt");
                await _sendEmailService.SendEmailAsync(eventId, html);
                return ApiUtil.CreateSuccessfulResult();
            }
            catch (Exception e)
            {
                return ApiUtil.CreateFailResult(e.ToString());
            }
        }
        // GET: api/SendEmail
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SendEmail/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SendEmail
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SendEmail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SendEmail/5
        public void Delete(int id)
        {
        }
    }
}
