namespace WebApplicationMVC2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RecipientPayment")]
    public partial class RecipientPayment
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public DateTime EnterDate { get; set; }

        public int TotalClicks { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(50)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(50)]
        public string CreditCardNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnCard { get; set; }

        [Required]
        [StringLength(50)]
        public string Month { get; set; }

        [Required]
        [StringLength(50)]
        public string Year { get; set; }

        [Required]
        [StringLength(50)]
        public string Cvv { get; set; }
    }
}
