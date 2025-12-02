using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Data Source=fashionbingo.db"));

// Register your services
builder.Services.AddScoped<BingoService>();
builder.Services.AddScoped<ClosetService>();
builder.Services.AddScoped<LeaderboardService>();
builder.Services.AddScoped<MysteryBoxService>();
builder.Services.AddScoped<PlayerService>();

// Configure CORS for Angular dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular dev server
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Optionally skip HTTPS redirection for dev
    // app.UseHttpsRedirection(); 
}
else
{
    app.UseHttpsRedirection();
}

app.UseRouting();

// Enable CORS
app.UseCors("AllowAngularDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
