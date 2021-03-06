// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Resistence.Middleware.Migrations
{
    [DbContext(typeof(EFContext))]
    partial class EFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Resistence.Entities.Localizacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Latitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Longitude")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RebeldeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RebeldeId")
                        .IsUnique();

                    b.ToTable("Localizacoes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Latitude = "20 graus sul",
                            Longitude = "44 graus oeste",
                            Nome = "Endor moon",
                            RebeldeId = 1
                        },
                        new
                        {
                            Id = 2,
                            Latitude = "20 graus sul",
                            Longitude = "44 graus oeste",
                            Nome = "Kamino",
                            RebeldeId = 2
                        },
                        new
                        {
                            Id = 3,
                            Latitude = "20 graus sul",
                            Longitude = "44 graus oeste",
                            Nome = "Naboo",
                            RebeldeId = 3
                        },
                        new
                        {
                            Id = 4,
                            Latitude = "20 graus sul",
                            Longitude = "44 graus oeste",
                            Nome = "Naboo",
                            RebeldeId = 4
                        });
                });

            modelBuilder.Entity("Resistence.Entities.Rebelde", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<int>("IndicacaoTraidor")
                        .HasColumnType("int");

                    b.Property<string>("JsonInventory")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<bool>("Traidor")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Rebeldes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Genero = "M",
                            Idade = 17,
                            IndicacaoTraidor = 0,
                            JsonInventory = "{\"AGUA\":10,\"ARMA\":1,\"COMIDA\":20,\"MUNICAO\":50}",
                            Nome = "Poe Dameron",
                            Traidor = false
                        },
                        new
                        {
                            Id = 2,
                            Genero = "M",
                            Idade = 50,
                            IndicacaoTraidor = 0,
                            JsonInventory = "{\"AGUA\":10,\"ARMA\":1,\"COMIDA\":20,\"MUNICAO\":50}",
                            Nome = "Leia Organa",
                            Traidor = false
                        },
                        new
                        {
                            Id = 3,
                            Genero = "O",
                            Idade = 170,
                            IndicacaoTraidor = 0,
                            JsonInventory = "{\"AGUA\":10,\"ARMA\":1,\"COMIDA\":20,\"MUNICAO\":50}",
                            Nome = "C3PO",
                            Traidor = false
                        },
                        new
                        {
                            Id = 4,
                            Genero = "O",
                            Idade = 170,
                            IndicacaoTraidor = 0,
                            JsonInventory = "{\"AGUA\":10,\"ARMA\":1,\"COMIDA\":20,\"MUNICAO\":50}",
                            Nome = "Jumbaka",
                            Traidor = false
                        });
                });

            modelBuilder.Entity("Resistence.Entities.Localizacao", b =>
                {
                    b.HasOne("Resistence.Entities.Rebelde", "Rebelde")
                        .WithOne("Localizacao")
                        .HasForeignKey("Resistence.Entities.Localizacao", "RebeldeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
