using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attemp2.Models
{
    public class Send
    {
        private string phone;
        private string massage;

        public Send() { }
        public Send(string phone, string massage)
        {
            Phone = phone;
            Massage = massage;
        }

        public string Phone { get => phone; set => phone = value; }
        public string Massage { get => massage; set => massage = value; }
    }
}