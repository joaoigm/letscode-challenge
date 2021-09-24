using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Resistence.Entities;
namespace Resistence.Middleware
{
    public class EFContext : DbContext
    {
        public DbSet<Rebelde> Rebeldes { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }

        public EFContext()
        {
        }

        public EFContext(DbContextOptions<EFContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            doRebeldeModel(modelBuilder);
            // doLocalizacaoModel(modelBuilder);
            //base.OnModelCreating(modelBuilder);
            seed(modelBuilder);
        }

        private void doRebeldeModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rebelde>(entity => {

                entity
                    .Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(80);
                entity
                    .Property(e => e.Idade)
                    .IsRequired();
                entity
                    .Property(e => e.Genero)
                    .IsRequired();
                entity
                    .Property(e => e.JsonInventory)
                    .HasMaxLength(1000);

                entity
                    .Ignore(e => e.Inventario);

                entity
                    .HasOne<Localizacao>(e => e.Localizacao)
                    .WithOne(e => e.Rebelde)
                    .HasForeignKey<Localizacao>(e => e.RebeldeId);
                    
            });
        }

        private void doLocalizacaoModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Localizacao>(entity =>
            {
                entity.HasNoKey();
            });
        }

        private void seed(ModelBuilder builder) {
            Dictionary<ItemInventario, int> inventarioPadrao = new Dictionary<ItemInventario, int>();
            inventarioPadrao.Add(ItemInventario.AGUA, 10);
            inventarioPadrao.Add(ItemInventario.ARMA, 1);
            inventarioPadrao.Add(ItemInventario.COMIDA, 20);
            inventarioPadrao.Add(ItemInventario.MUNICAO, 50);

            builder.Entity<Rebelde>()
                .HasData(
                    new Rebelde
                    {
                        Id = 1,
                        Genero = 'M',
                        Idade = 17,
                        Nome = "Poe Dameron",
                        Inventario = inventarioPadrao
                    },
                    new Rebelde
                    {
                        Id = 2,
                        Genero = 'M',
                        Idade = 50,
                        Nome = "Leia Organa",
                        Inventario = inventarioPadrao
                    },
                    new Rebelde
                    {
                        Id = 3,
                        Genero = 'O',
                        Idade = 170,
                        Nome = "C3PO",
                        Inventario = inventarioPadrao
                    }
                );

            builder.Entity<Localizacao>()
                .HasData(
                    new Localizacao
                    {
                        Id = 1,
                        RebeldeId = 1,
                        Latitude = "20 graus sul",
                        Longitude = "44 graus oeste",
                        Nome = "Endor moon"
                    },
                    new Localizacao
                    {
                        Id = 2,
                        RebeldeId = 2,
                        Latitude = "20 graus sul",
                        Longitude = "44 graus oeste",
                        Nome = "Kamino"
                    },
                    new Localizacao
                    {
                        Id = 3,
                        RebeldeId = 3,
                        Latitude = "20 graus sul",
                        Longitude = "44 graus oeste",
                        Nome = "Naboo"
                    }
                );
        }
    }
}