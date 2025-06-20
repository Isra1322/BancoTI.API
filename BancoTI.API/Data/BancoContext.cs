using Microsoft.EntityFrameworkCore;

public class BancoContext : DbContext
{
    public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Cuenta> Cuentas { get; set; }
    public DbSet<Transferencia> Transferencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transferencia>()
            .HasOne(t => t.CuentaOrigenNav)
            .WithMany()
            .HasForeignKey(t => t.CuentaOrigen)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Transferencia>()
            .HasOne(t => t.CuentaDestinoNav)
            .WithMany()
            .HasForeignKey(t => t.CuentaDestino)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Cuenta>()
            .HasOne(c => c.Cliente)
            .WithMany(cl => cl.Cuentas)
            .HasForeignKey(c => c.CedulaCliente)
            .OnDelete(DeleteBehavior.Cascade);
    }
    


}
