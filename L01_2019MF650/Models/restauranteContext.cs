using Microsoft.EntityFrameworkCore;

namespace L01_2019MF650.Models
{
    public class restauranteContext : DbContext
    {
        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options)
        {

        }

        public DbSet<clientes> clientes { get; set; }
        public DbSet<pedidos> pedidos { get; set; }
        public DbSet<platos> platos { get; set; }
        public DbSet<motoristas> motoristas { get; set; }
    }
}
