using Microsoft.EntityFrameworkCore;
using Resistence.Entities;
namespace Resistence.Middleware
{
    public class EFContext : DbContext
    {
        public DbSet<Rebelde> Rebeldes { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Localizacao>()
                .HasOne<Rebelde>()
                .WithOne(entity => entity.Localizacao)
                .HasForeignKey<Localizacao>(entity => entity.RebeldeId);

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        private void doRebeldeModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rebelde>()
                .HasKey(entity => entity.Id);

            modelBuilder.Entity<Rebelde>()
                .Property(entity => entity.Nome)
                .IsRequired()
                .HasMaxLength(80);

            modelBuilder.Entity<Rebelde>()
                .Property(entity => entity.Idade)
                .IsRequired();
        }
    }
}