using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;


namespace Attemp2.Models
{
    public class Episode
    {

        private string episode_name;
        private int season_num;
        private string img;
        private string description;
        private string date;
        private int episode_id;
        private int show_id;
        private int likes;


        public Episode() { }

        public Episode(string episode_name, int season_num, string img, string description, string date, int episode_id, int show_id, int likes)
        {
            Episode_name = episode_name;
            Season_num = season_num;
            Img = img;
            Description = description;
            Date = date;
            Episode_id = episode_id;
            Show_id = show_id;
            Likes = likes;
        }

        public string Episode_name { get => episode_name; set => episode_name = value; }
        public int Season_num { get => season_num; set => season_num = value; }
        public string Img { get => img; set => img = value; }
        public string Description { get => description; set => description = value; }
        public string Date { get => date; set => date = value; }
        public int Episode_id { get => episode_id; set => episode_id = value; }
        public int Show_id { get => show_id; set => show_id = value; }
        public int Likes { get => likes; set => likes = value; }

        public void Insert()
        {
            DataServices ds = new DataServices();
            ds.InsertEpisode(this);
        }

       public List<Episode> Get(string name)
       {
            DataServices ds = new DataServices();
            return ds.GetEC(name);
       }
        public List<Episode> Getlist()
        {
            DataServices ds = new DataServices();
            return ds.GetEPList();
        }
        public List<Episode> GetfavoriteEp(int id)
        {
            DataServices ds = new DataServices();
            return ds.GetEpForUser(id);
        }
       
        public int UpdatebyId(int id, int lastlikes)
        {
            DataServices ds = new DataServices();
            return ds.Updatelikesep(id,lastlikes);
        }

        public Episode EpisodeByID(int id)
        {
            DataServices ds = new DataServices();
            return ds.GetEpisodeByID(id);
        }
    }

}
   
    
   







