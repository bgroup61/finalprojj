using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Attemp2.Models;

namespace Attemp2.Controllers
{
    public class UserController : ApiController
    {
        User us = new User();
        // GET api/<controller>
        public List<User> Get()
        {
            User u = new User();
            List<User> listseries = u.Get();
            return listseries;
        }

        [HttpGet]
        public User Get(int id)
        {
            User u = new User();
            return u.UserByID(id);
        }

        // GET api/<controller>/?email=...&password=...
        [HttpGet]
        public User Get(string email, string password)
        {
            User u = new User();
            return u.UserValid(email, password);
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post(User us)
        {
            int num = us.Insert();
            if (num == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "Email already exists,enter another email address");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "success");
            }
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