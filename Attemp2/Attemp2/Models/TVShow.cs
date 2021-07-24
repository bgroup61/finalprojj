using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attemp2.Models
{
    public class TVShow
    {
        private int show_id;
        private string first_air_date;
        private string show_name;
        private string origin_country;
        private string original_language;
        private string overview;
        private float popularity;
        private string poster_path;
        private int likes;
        public TVShow(int show_id, string first_air_date, string show_name, string origin_country, string original_language, string overview, float popularity, string poster_path, int likes)
        {
            Show_id = show_id;
            First_air_date = first_air_date;
            Show_name = show_name;
            Origin_country = origin_country;
            Original_language = original_language;
            Overview = overview;
            Popularity = popularity;
            Poster_path = poster_path;
            Likes = likes;
        }

        public TVShow() { }
        public int Show_id { get => show_id; set => show_id = value; }
        public string First_air_date { get => first_air_date; set => first_air_date = value; }
        public string Show_name { get => show_name; set => show_name = value; }
        public string Origin_country { get => origin_country; set => origin_country = value; }
        public string Original_language { get => original_language; set => original_language = value; }
        public string Overview { get => overview; set => overview = value; }
        public float Popularity { get => popularity; set => popularity = value; }
        public string Poster_path { get => poster_path; set => poster_path = value; }
        public int Likes { get => likes; set => likes = value; }

        public void Insert()
        {
            DataServices ds = new DataServices();
            ds.InsertTvShow(this);
        }

       
        public TVShow TVShowByID(int id)
        {
            DataServices ds = new DataServices();
            return ds.GetTVShowByID(id);
        }

        public int Updatelikes(int id, int lastlikes)
        {
            DataServices ds = new DataServices();
            return ds.Updatelikestv(id, lastlikes);
        }

        public List<TVShow> Get()
        {
            DataServices ds = new DataServices();
            return ds.GetTVList();
        }
    }
}