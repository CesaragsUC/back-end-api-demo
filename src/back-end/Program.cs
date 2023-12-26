using back_end;
using back_end.Banco;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FuncionarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Middlewares

//caso seja detectada a solicitac�o desse end-point, o middleware ser� adicionado a pipeline e sera executado
//N�o � poss�vel invocar o m�todo next() nos middlewares definidos com Use e UseWhen. sendo assim, nao sera possivel executar o proximo middleware.
//caso nesse endpoint tenha algum retorno JSON, ele sera substituido pelo retorno do middleware. Nesse exemplo, pela msg abaixo.
app.Map("/api/Funcionarios/obter-por-id", builder =>
{
    builder.UseMiddleware<MeuMiddleware3>();
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("Esta � a subpipeline mapeada para /api/Funcionarios/obter-por-id.");
    });
});

//caso seja detectada a solicitac�o desse end-point, o middleware ser� adicionado a pipeline e sera executado
//N�o � poss�vel invocar o m�todo next() nos middlewares definidos com Use e UseWhen. sendo assim, nao sera possivel executar o proximo middleware.
//caso nesse endpoint tenha algum retorno JSON, ele sera substituido pelo retorno do middleware. Nesse exemplo, pela msg abaixo.
app.MapWhen(context => context.Request.Path.StartsWithSegments("/api/Funcionarios/atualizar"), builder =>
{
    builder.UseMiddleware<MeuMiddleware2>();
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("Esta � a subpipeline condicional.");
    });
});

//caso seja detectada a solicitac�o desse end-point, o middleware ser� adicionado a pipeline e sera executado
//MapWhen ap�s executar o middleware, ele ir� executar o pr�ximo middleware
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Funcionarios/listar-funconarios"), app =>
{
    app.UseMiddleware<MeuMiddleware>();
});

#endregion


app.UseAuthorization();

app.MapControllers();



app.Run();
