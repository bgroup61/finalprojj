using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Attemp2.Models;

namespace Attemp2.Controllers
{
    public class FavoriteController : ApiController
    {
        List<Favorite> favoritelist;
        

        // GET api/<controller>/5
        public List<Favorite> Get(int id)
        {
            Favorite f = new Favorite();
            return favoritelist = f.GetFavorite(id);
            
        }

        // POST api/<controller>
        public void Post([FromBody] Favorite f)
        {
            f.InsertFavorite();
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