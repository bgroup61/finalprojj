using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Attemp2.Models;

namespace Attemp2.Controllers
{
    public class TvShowController : ApiController
    {
        // GET api/<controller>
        public List<TVShow> Get()
        {
            TVShow t = new TVShow();
            List<TVShow> list = t.Get();
            return list;
        }

        // GET api/<controller>/5
        public TVShow Get(int id)
        {
            TVShow tv = new TVShow();
            return tv.TVShowByID(id);
        }

        // POST api/<controller>
        public void Post([FromBody] TVShow tv)
        {
            tv.Insert();
        }

        // PUT api/<controller>/5
        public int Put(int id,int lastlikes, [FromBody] string value)
        {
            TVShow tv = new TVShow();
            return tv.Updatelikes(id,lastlikes);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}