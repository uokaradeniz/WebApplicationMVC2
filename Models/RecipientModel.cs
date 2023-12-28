using System.Data.Entity;

namespace WebApplicationMVC2.Models
{
    public partial class RecipientModel : DbContext
    {
        public RecipientModel()
            : base("name=RecipientModel")
        {
        }

        public virtual DbSet<Recipient> Recipient { get; set; }
        public virtual DbSet<RecipientPayment> RecipientPayment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
