using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using nba_dotnet.Entities;

namespace nba_dotnet
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) {}

        //? definicion de clases qe representarán tablas (entidades)
        public DbSet<Player> Players { get; set; }
        public DbSet <Conference> Conferences{ get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}