using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PremisesTypes> PremisesTypes { get; set; }
        public DbSet<SpecialTag> SpecialTags { get; set; }

    }
}
