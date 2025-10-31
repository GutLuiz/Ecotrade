using Backend.Conexao; // importa tua classe de conexão

var builder = WebApplication.CreateBuilder(args);

// 🧩 Configura a conexão com o banco de dados (appsettings.json)
ConexaoServico.Configurar(builder.Configuration);

// Add services to the container
builder.Services.AddControllers();

// 🧩 Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🧩 Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirLocalhost3000",
        policy => policy
            .WithOrigins("http://localhost:3000") // seu front
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// 🧩 Configura o Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔹 **Ativa CORS aqui**
app.UseCors("PermitirLocalhost3000");

app.UseAuthorization();

// 🧩 Mapeia os controllers
app.MapControllers();

// 🧩 Fecha a conexão quando o app for encerrado (boa prática)
app.Lifetime.ApplicationStopped.Register(() =>
{
    var conexao = ConexaoServico.ConexaoPostgres;
    if (conexao.State == System.Data.ConnectionState.Open)
        conexao.Close();
});

app.Run();
