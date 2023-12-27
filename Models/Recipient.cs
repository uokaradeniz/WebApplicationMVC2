using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationMVC2.Models
{
    public class Recipient
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime EnterDate { get; set; }
        public int TotalClicks { get; set; }
    }
}