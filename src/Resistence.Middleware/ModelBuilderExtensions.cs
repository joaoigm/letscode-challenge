using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Resistence.Entities;

namespace Resistence.Middleware
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            Console.WriteLine("Fazendo seed");
            
        }
    }
}