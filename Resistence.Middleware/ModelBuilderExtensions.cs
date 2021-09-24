using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Resistence.Entities;

namespace Resistence.Middleware
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            Dictionary<ItemInventario, int> inventarioPadrao = new Dictionary<ItemInventario, int>();
            inventarioPadrao.Add(ItemInventario.AGUA, 10);
            inventarioPadrao.Add(ItemInventario.ARMA, 1);
            inventarioPadrao.Add(ItemInventario.COMIDA, 20);
            inventarioPadrao.Add(ItemInventario.MUNICAO, 50);

            builder.Entity<Rebelde>()
                .HasData( 
                    new Rebelde {
                        Genero = 'M',
                        Idade = 17,
                        Nome = "Poe Dameron",
                        Localizacao = new Localizacao {
                            Latitude = "20 graus sul",
                            Longitude = "44 graus oeste",
                            Nome = "Endor moon"
                        },
                        Inventario = inventarioPadrao
                    },
                    new Rebelde {
                        Genero = 'M',
                        Idade = 50,
                        Nome = "Leia Organa",
                        Localizacao = new Localizacao {
                            Latitude = "20 graus sul",
                            Longitude = "44 graus oeste",
                            Nome = "Kamino"
                        },
                        Inventario = inventarioPadrao
                    },
                    new Rebelde {
                        Genero = 'O',
                        Idade = 170,
                        Nome = "C3PO",
                        Localizacao = new Localizacao {
                            Latitude = "20 graus sul",
                            Longitude = "44 graus oeste",
                            Nome = "Naboo"
                        },
                        Inventario = inventarioPadrao
                    }
                );
        }
    }
}