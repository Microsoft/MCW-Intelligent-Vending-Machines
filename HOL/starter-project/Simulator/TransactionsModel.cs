namespace Simulator
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TransactionsModel : DbContext
    {
        public TransactionsModel()
            : base("name=TransactionsModel")
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(e => e.VendingMachineId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.PurchasePrice)
                .HasPrecision(10, 4);
        }
    }
}
