using System;
using System.Collections.Generic;
using System.Text;
using icecreamshop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace icecreamshop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        internal readonly object ApplicationUser;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<icecreamshop.Models.Flavour> Flavour { get; set; }
        public DbSet<icecreamshop.Models.OrderBox> OrderBoxes { get; set; }
    }
}
