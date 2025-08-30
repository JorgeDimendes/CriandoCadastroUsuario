using CriandoCadastro.Api.Data;
using CriandoCadastro.Api.Services.Usuario;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

//Configuracoes de Servicos
#region ConfiguraçãoDeServiços
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
#endregion

//SqLite
#region SqLite
builder.Services.AddDbContext<AppDbContext>(context => 
    context.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
#endregion

//Service
builder.Services.AddScoped<IUsuarioInterface, UsuarioService>();

//App Configuration Padrao
#region AppConfigurationPadrao
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion