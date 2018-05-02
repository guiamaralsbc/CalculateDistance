using Microsoft.EntityFrameworkCore;

namespace GAB.DistanceCalculation.Model
{
    public class DCContext : DbContext
    {
        public DbSet<Friend> Friend { get; set; }
        public DbSet<History> History { get; set; }
        private string connString = @"Server=br-sao1-db05\tfs;Database=TesteCore;User ID=localapp;Password=Asdfghj000;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer(connString);
        }
    }
}
