using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }
}