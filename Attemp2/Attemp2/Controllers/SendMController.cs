using Attemp2.Services;
using Attemp2.Services.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Attemp2.Models;


namespace Attemp2.Controllers
{
    public class SendMController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>
        [System.Web.Http.HttpGet]
        public string Get(int n)
        {
            return null;
        }

        // POST api/<controller>
        public void Post([FromBody] Send login)
        {
            ISMSService smsService = new SMSService();
            smsService.SendSingleSms(login.Phone, login.Massage);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}