using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplicationMVC2.Models
{
    public class PhishingDbContext : DbContext
    {
        public DbSet<Recipient> Recipients {  get; set; }
    }
}