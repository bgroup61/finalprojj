using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Attemp2.Models;

namespace Attemp2.Controllers
{
    public class EpisodeController : ApiController
    {
        public List<Episode> Get()
        {
            Episode e = new Episode();
            List<Episode> list = e.Getlist();
            return list;
        }

        public List<Episode> Get(int user_id,int episode_id)
        {
            Episode e = new Episode();
            List<Episode> listseries = e.GetfavoriteEp(user_id);
            return listseries;
        }
        // GET api/<controller>?Chaptername=
        public IEnumerable<Episode> Get(string Chaptername)
        {
            Episode s = new Episode();
            List<Episode> episodeslist = s.Get(Chaptername);
            return episodeslist;
        }

       
        public Episode Get(int id)
        {
            Episode e = new Episode();
            return e.EpisodeByID(id);
        }

        // POST api/<controller>
        public void Post([FromBody] Episode ep)
        {
            ep.Insert();
        }


        // PUT api/<controller>/5
        public int Put(int id,int lastlikes ,[FromBody] string value)
        {
            Episode ep = new Episode();
            return ep.UpdatebyId(id,lastlikes);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}