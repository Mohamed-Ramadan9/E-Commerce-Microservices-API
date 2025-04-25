using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthenticationApi_Domain.Entites;

namespace AuthenticationApi_Infrastructure.Data
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }

        public DbSet<AppUser>Users { get; set; }
    }
}
