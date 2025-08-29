using CriandoCadastro.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace CriandoCadastro.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }

    public DbSet<UsuarioModel> Usuarios { get; set; }
}