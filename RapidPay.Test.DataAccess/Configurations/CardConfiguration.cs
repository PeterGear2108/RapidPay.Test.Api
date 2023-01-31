using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RapidPay.Test.Models.Domain;
using System.Data.Entity.ModelConfiguration;

namespace RapidPay.Test.DataAccess.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CardNumber).IsRequired();
            builder.Property(x => x.CardHolder).IsRequired();
            builder.Property(x => x.CreditLimit).IsRequired();

            builder.HasMany(x => x.Transactions)
                .WithOne(x => x.Card)
                .HasForeignKey(x => x.CardId)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
