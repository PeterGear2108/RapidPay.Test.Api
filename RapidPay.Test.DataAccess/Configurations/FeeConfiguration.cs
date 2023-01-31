using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RapidPay.Test.Models.Domain;

namespace RapidPay.Test.DataAccess.Configurations
{
    public class FeeConfiguration : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FeeAmmount).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
        }
    }
}
