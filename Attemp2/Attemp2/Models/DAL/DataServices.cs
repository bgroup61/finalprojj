using Attemp2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Net.Http;


namespace Attemp2.Models
{
    public class DataServices
    {
        static List<Episode> episodes;
        static List<User> users;
        static int countlikesTV = 0;
        static int countlikesEP = 0;

        public SqlDataAdapter da;
        public DataTable dt;

        // =========== //
        //    USERS    //
        // =========== // 
        public int InsertUser(User user)
        {
            //if (users == null)
            //    users = new List<User>();
            //users.Add(user);

            SqlConnection con;
            SqlCommand cmd;

            try {
                con = connect("DBConnectionString"); // create the connection
            } catch (Exception) {
                return 1;
            }

            String cStr = BuildInsertUser(user);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            } catch (Exception) {
                return 0;
            }
            finally {
                if (con != null) {
                    // close the db connection
                    con.Close();
                }
            }
        }
        // Build the Insert command String
        private String BuildInsertUser(User u)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')", u.Name, u.LastName, u.Email,u.Birthday, u.Password, u.Telephone, u.Gender, u.Category,u.Role);
            String prefix = "INSERT INTO User_Movieweb_2021 " + "(name, lastName, email, birthday, password, telephone, gender, category,role)";
            command = prefix + sb.ToString();

            return command;
        }

        public User GetUserByID(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            User u;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                return null;
            }

            String cStr = BuildGetUserByID(id);   // helper method to build the get string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
                while (reader.Read())
                {
                    u = new User(Convert.ToString(reader["name"]), Convert.ToString(reader["lastName"]), Convert.ToString(reader["email"]),
                        Convert.ToString(reader["birthday"]), Convert.ToString(reader["password"]), Convert.ToString(reader["telephone"]),
                        Convert.ToString(reader["gender"]), Convert.ToString(reader["category"]), Convert.ToString(reader["role"]),Convert.ToInt32(reader["user_id"]));

                    return u;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        private String BuildGetUserByID(int id)
        {
            String command = "SELECT * FROM User_Movieweb_2021 WHERE user_id = '" + id + "'";
            return command;
        }

        public User GetUser(string userEmail, string password)
        {
            SqlConnection con;
            SqlCommand cmd;
            User u;

            try {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex) {
                // write to log
                throw (ex);
            }

            String cStr = BuildGetUser(userEmail, password);   // helper method to build the get string

            cmd = CreateCommand(cStr, con);             // create the command

            try {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
                while (reader.Read()) {
                    u = new User(Convert.ToString(reader["name"]), Convert.ToString(reader["lastName"]), Convert.ToString(reader["email"]),
                        Convert.ToString(reader["birthday"]), Convert.ToString(reader["password"]), Convert.ToString(reader["telephone"]),
                        Convert.ToString(reader["gender"]), Convert.ToString(reader["category"]), Convert.ToString(reader["role"]),Convert.ToInt32(reader["user_id"]));

                    return u;
                }
            } catch (Exception) {
                return null;
            } finally {
                if (con != null) {
                    // close the db connection
                    con.Close();
                }
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        private String BuildGetUser(string email, string pass)
        {
            String command = "SELECT * FROM User_Movieweb_2021 WHERE email = '" + email + "' and password = '" + pass + "'";
            return command;
        }

        public List<User> GetUserList()
        {
            SqlConnection con;
            SqlCommand cmd;
            User s;
            List<User> user_list = new List<User>();

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                return user_list ;
            }

            String cStr = BuildGetu();   // helper method to build the get string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
                while (reader.Read())
                {
                    s = new User(Convert.ToString(reader["name"]), Convert.ToString(reader["lastName"]), Convert.ToString(reader["email"]),
                        Convert.ToString(reader["birthday"]), Convert.ToString(reader["password"]), Convert.ToString(reader["telephone"]),
                        Convert.ToString(reader["gender"]), Convert.ToString(reader["category"]), Convert.ToString(reader["role"]), Convert.ToInt32(reader["user_id"]));

                    user_list.Add(s);
                }
                return user_list;
            }
            catch (Exception)
            {
                return user_list;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        private String BuildGetu()
        {
            String command = "SELECT * FROM User_Movieweb_2021";
            return command;
        }

        // ============== //
        //     TVShows    //
        // ============== // 

        public List<TVShow> GetTVList()
        {
            SqlConnection con;
            SqlCommand cmd;
            TVShow t;
            List<TVShow> tv_list = new List<TVShow>();

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                return tv_list;
            }

            String cStr = BuildGett();   // helper method to build the get string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
                while (reader.Read())
                {
                    int likes;
                    int.TryParse(Convert.ToString(reader["likes"]), out likes);
                    t = new TVShow(Convert.ToInt32(reader["show_id"]),Convert.ToString(reader["first_air_date"]),Convert.ToString(reader["show_name"]),Convert.ToString(reader["origin_country"]),Convert.ToString(reader["original_language"]),Convert.ToString(reader["overview"]),(float)Convert.ToDouble(reader["popularity"]),Convert.ToString(reader["poster_path"]),likes);
                    tv_list.Add(t);
                }
                return tv_list;
            }
            catch (Exception)
            {
                return tv_list;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        private String BuildGett()
        {
            String command = "SELECT * FROM TVShows_Movieweb_2021";
            return command;
        }


        public int InsertTvShow(TVShow tvshow)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertTvShow();      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            cmd = AddInsertTVShowCmdParams(cmd, tvshow); // assign the parameters to the command


            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }
        private String BuildInsertTvShow()
        {
            String cmdTxt;
            cmdTxt = "INSERT INTO TVShows_Movieweb_2021 ";
            cmdTxt += "(show_id, first_air_date, show_name, origin_country, original_language, overview, popularity, poster_path, likes) ";
            cmdTxt += "VALUES(@id, @date, @name, @country, @language, @overview, @popularity, @poster, @likes)";
            return cmdTxt;
        }
        private SqlCommand AddInsertTVShowCmdParams(SqlCommand cmd, TVShow t)
        {
            try
            {
                cmd.Parameters.AddWithValue("@id", t.Show_id);
                cmd.Parameters.AddWithValue("@date", t.First_air_date);
                cmd.Parameters.AddWithValue("@name", t.Show_name);
                cmd.Parameters.AddWithValue("@country", t.Origin_country);
                cmd.Parameters.AddWithValue("@language", t.Original_language);
                cmd.Parameters.AddWithValue("@overview", t.Overview);
                cmd.Parameters.AddWithValue("@popularity", t.Popularity);
                cmd.Parameters.AddWithValue("@poster", t.Poster_path);
                cmd.Parameters.AddWithValue("@likes", t.Likes);
                return cmd;
            }
            catch(Exception ex)
            {
                return cmd;
            }
        }

        // ============= //
        //   EPISODES    //
        // ============= // 
        public int InsertEpisode(Episode episode)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                return 0;
            }

            String cStr = BuildInsertEpisode();      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            cmd = AddInsertEpisodeCmdParams(cmd, episode); // assign the parameters to the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        // Build the Insert command String
        private String BuildInsertEpisode()
        {
            String cmdTxt;
            cmdTxt = "INSERT INTO Episodes_Movieweb_2021 ";
            cmdTxt += "(show_id, episode_id, episode_name, img, description, season_num, date,likes) ";
            cmdTxt += "VALUES(@show_id, @episode_id, @name, @img, @desc, @season_num, @date,@likes)";
            return cmdTxt;
        }
        private SqlCommand AddInsertEpisodeCmdParams(SqlCommand cmd, Episode e)
        {
            cmd.Parameters.AddWithValue("@show_id", e.Show_id);
            cmd.Parameters.AddWithValue("@episode_id", e.Episode_id);
            cmd.Parameters.AddWithValue("@name", e.Episode_name);
            cmd.Parameters.AddWithValue("@img", e.Img);
            cmd.Parameters.AddWithValue("@desc", e.Description);
            cmd.Parameters.AddWithValue("@season_num", e.Season_num);
            cmd.Parameters.AddWithValue("@date", e.Date);
            cmd.Parameters.AddWithValue("@likes", e.Likes);
            return cmd;
        }

        public List<Episode> GetEPList()
        {
            SqlConnection con = null;
            List<Episode> episodes_list = new List<Episode>();
            Episode e;

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Episodes_Movieweb_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (reader.Read())
                {
                    int likes = 0;
                    int.TryParse(Convert.ToString(reader["likes"]), out likes);

                    e = new Episode(Convert.ToString(reader["episode_name"]), Convert.ToInt32(reader["season_num"]), Convert.ToString(reader["img"]), Convert.ToString(reader["description"]), Convert.ToString(reader["date"]), Convert.ToInt32(reader["episode_id"]), Convert.ToInt32(reader["show_id"]),likes);
                    episodes_list.Add(e); 
                }

                return episodes_list;
            }
            catch (Exception)
            {
                return episodes_list;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }
        public List<Episode> GetEpForUser(int id)
        {

            SqlConnection con = null;
            List<Episode> episodes = new List<Episode>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "select E.*   from Favorite_Movieweb_2021 as F inner join Episodes_Movieweb_2021 as E on F.episode_id = E.episode_id where F.user_id="+id;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command

                while (reader.Read())
                {   // Read till the end of the data into a row
                    Episode e;
                    //        public Episode(string episode_name, int season_num, string img, string description, string date, int episode_id, int show_id, string show_name)

                    e = new Episode(Convert.ToString(reader["episode_name"]),Convert.ToInt32(reader["season_num"]), Convert.ToString(reader["img"]), Convert.ToString(reader["description"]), Convert.ToString(reader["date"]), Convert.ToInt32(reader["episode_id"]), Convert.ToInt32(reader["show_id"]), Convert.ToInt32(reader["likes"]));


                    episodes.Add(e);
                }

                return episodes;
            }
            catch (Exception)
            {
                return episodes;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }

        public List<Episode> GetEC(string SName)
        {
            SqlConnection con = null;
            List<Episode> episodes = new List<Episode>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Episodes_Movieweb_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (reader.Read())
                {   // Read till the end of the data into a row
                    Episode e;
                    e = new Episode(Convert.ToString(reader["episode_name"]), Convert.ToInt32(reader["season_num"]), Convert.ToString(reader["img"]), Convert.ToString(reader["description"]), Convert.ToString(reader["date"]), Convert.ToInt32(reader["episode_id"]), Convert.ToInt32(reader["show_id"]), Convert.ToInt32(reader["likes"]));

                    if (e.Episode_name == SName) { episodes.Add(e); }
                }

                return episodes;
            }
            catch (Exception ex)
            {
                return episodes;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }

        }


        // ================ //
        //   FAVORITE       //
        // ================ // 
        public int InsertFavorite(Favorite f)
        {
           

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                return 0;
            }

            String cStr = BuildInsertFavorite(f);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }
        private String BuildInsertFavorite(Favorite f)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values({0}, {1}, {2})", f.User_id, f.Episode_id, f.Show_id);
            String prefix = "INSERT INTO Favorite_Movieweb_2021 " + "(user_id, episode_id, show_id) ";
            command = prefix + sb.ToString();

            return command;
        }

        public List<Favorite> Getfavorite(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            Favorite f;
            List<Favorite> favorites = new List<Favorite>();

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                return null;
            }

            String cStr = BuildGetf(id);   // helper method to build the get string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
                while (reader.Read())
                {
                    f = new Favorite(Convert.ToInt32(reader["show_id"]), Convert.ToInt32(reader["episode_id"]), Convert.ToInt32(reader["user_id"]));
                    favorites.Add(f);
                }
                return favorites;
            }
            catch (Exception)
            {
                return favorites;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        private String BuildGetf(int id)
        {
            String command = "SELECT * FROM Favorite_Movieweb_2021 WHERE user_id = " + id;
            return command;
        }
        //
        public int Updatelikestv(int id, int lastlike)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                return 0;
            }

            String cStr = BuildUpdatelikestv(id,lastlike);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 1;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private string BuildUpdatelikestv(int id, int lastlike)
        {
            lastlike++;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            //sb.AppendFormat("Values({0})", lastlike);
            String prefix = "UPDATE TVShows_Movieweb_2021" + " SET likes="+ lastlike + "  WHERE show_id ="+id; 
            string command = prefix + sb.ToString();

            return command;
        }

        public int Updatelikesep(int id, int lastlikes)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                return 0;
            }

            String cStr = BuildUpdatelikesep(id,lastlikes);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                return 1;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        private string BuildUpdatelikesep(int id,int lastlikes)
        {
            lastlikes++;
            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            //sb.AppendFormat("Values({0})", lastlikes);
            String prefix = "UPDATE Episodes_Movieweb_2021" + " SET likes="+ lastlikes + "  WHERE episode_id = " + id;
            string command = prefix + sb.ToString();
            return command;
        }

        public Episode GetEpisodeByID(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            Episode e;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                return null;
            }

            String cStr = BuildGetEpisodeByID(id);   // helper method to build the get string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
                while (reader.Read())
                {
                    //  episode_name, season_num,  img,  description, date, episode_id, show_id,  likes)

                    e = new Episode(Convert.ToString(reader["episode_name"]),Convert.ToInt32(reader["season_num"]),Convert.ToString(reader["img"]),Convert.ToString(reader["description"]),Convert.ToString(reader["date"]),Convert.ToInt32(reader["episode_id"]),Convert.ToInt32(reader["show_id"]),Convert.ToInt32(reader["likes"]));
                    return e;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        private String BuildGetEpisodeByID(int id)
        {
            String command = "SELECT * FROM Episodes_Movieweb_2021 WHERE episode_id = '" + id + "'";
            return command;
        }

        public TVShow GetTVShowByID(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            TVShow tv;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception)
            {
                return null;
            }

            String cStr = BuildGetTVShowByID(id);   // helper method to build the get string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
                while (reader.Read())
                {
                    // show_id, first_air_date, show_name,  origin_country, original_language, overview, popularity, poster_path, likes)
                    tv = new TVShow(Convert.ToInt32(reader["show_id"]),Convert.ToString(reader["first_air_date"]),Convert.ToString(reader["show_name"]),Convert.ToString(reader["origin_country"]),Convert.ToString(reader["original_language"]),Convert.ToString(reader["overview"]),(float)(Convert.ToDouble(reader["popularity"])),Convert.ToString(reader["poster_path"]),Convert.ToInt32(reader["likes"]));
                    return tv;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        private String BuildGetTVShowByID(int id)
        {
            String command = "SELECT * FROM TVShows_Movieweb_2021 WHERE show_id = " + id  ;
            return command;
        }


        //// ======================================= //
        ////     DB Comminication Configuration      //
        //// ======================================= // 
        public SqlConnection connect(String conString)
        {
            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object
            cmd.Connection = con;              // assign the connection to the command object
            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 
            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds
            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure
            return cmd;
        }
    }
}