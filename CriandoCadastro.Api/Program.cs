using CriandoCadastro.Api.Data;
using Microsoft.EntityFrameworkCore;

//Configuracoes de Servicos
#region ConfiguraçãoDeServiços
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
#endregion

//SqLite
#region SqLite
builder.Services.AddDbContext<AppDbContext>(context => 
    context.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
#endregion

//App Configuration Padrao
#region AppConfigurationPadrao
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion