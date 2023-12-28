namespace WebApplicationMVC2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("SentMailData")]
    public partial class SentMailData
    {
        public int ID { get; set; }
        public int TotalEmailsSent { get; set; }

    }
}