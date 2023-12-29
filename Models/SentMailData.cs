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
        public int AmazonInputs { get; set; }
        public int AmazonPayInputs { get; set; }
        public int InstagramInputs { get; set; }
        public int TwitterInputs { get; set; }
    }
}