using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Attemp2.Models;
namespace Attemp2.Models
{
    public class User
    {
        string name;
        string lastName;
        string email;
        string birthday;
        string password;
        string telephone;
        string gender;
        string category;
        int user_id ;
        string role;

        public User() { }
        public User(string name, string lastName, string email, string birthday, string password, string telephone, string gender, string category,string role,int user_id)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Birthday = birthday;
            Password = password;
            Telephone = telephone;
            Gender = gender;
            Category = category;
            Role = role;
            this.user_id = user_id;
        }

        public User UserByID(int id)
        {
            DataServices ds = new DataServices();
            return ds.GetUserByID(id);
        }

        public int Insert()
        {
            DataServices ds = new DataServices();
            return ds.InsertUser(this);
        }

        public User UserValid(string email, string password)
        {
            DataServices ds = new DataServices();
            return ds.GetUser(email, password);
        }

        public List<User> Get()
        {
            DataServices ds = new DataServices();
            return ds.GetUserList();
        }
        public string Name { get => name; set => name = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Email { get => email; set => email = value; }
        public string Birthday { get => birthday; set => birthday = value; }
        public string Password { get => password; set => password = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Category { get => category; set => category = value; }
        public int User_id { get => user_id; }
        public string Role { get => role; set => role = value; }
    }
}