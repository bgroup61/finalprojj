using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Attemp2.Models
{
    public class Favorite
    {

        int show_id;
        int episode_id;
        int user_id;

        public Favorite(int show_id, int episode_id, int user_id)
        {
            Show_id = show_id;
            Episode_id = episode_id;
            User_id = user_id;
        }
        public Favorite() { }
       
        
       
        public int User_id { get => user_id; set => user_id = value; }
        public int Show_id { get => show_id; set => show_id = value; }
        public int Episode_id { get => episode_id; set => episode_id = value; }

        public void InsertFavorite()
        {
            DataServices ds = new DataServices();
            ds.InsertFavorite(this);
        }

        public List<Favorite> GetFavorite(int id)
        {
            DataServices ds = new DataServices();
            return ds.Getfavorite(id);
        }
    }

}