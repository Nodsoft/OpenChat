using Microsoft.EntityFrameworkCore;
using Nodsoft.OpenChat.Server.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<OpenChatDbContext>(o =>
	o.UseNpgsql(builder.Configuration.GetConnectionString("Database"), p =>
		p.EnableRetryOnFailure()));

WebApplication? app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => 0);

using (OpenChatDbContext db = app.Services.CreateScope().ServiceProvider.GetRequiredService<OpenChatDbContext>())
{
	await db.Database.MigrateAsync();
}

await app.RunAsync();