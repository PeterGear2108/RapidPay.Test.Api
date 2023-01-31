using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RapidPay.Test.DataAccess.Configurations;
using RapidPay.Test.Models.Domain;


namespace RapidPay.Test.DataAccess
{
    public partial class RapidPayDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public RapidPayDbContext(IConfiguration config)
        {
            _config = config;
        }

        public RapidPayDbContext(IConfiguration config, DbContextOptions<RapidPayDbContext> options) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("RapidPay"),
                options => options.EnableRetryOnFailure());
        }
        public override int SaveChanges()
        {
            return base.SaveChanges(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new FeeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<User> Users { get; set; }
    }
}