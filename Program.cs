using ExaminationSystem.Core.Model;
using ExaminationSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionString=builder.Configuration.GetConnectionString("DefaultConnection");
var myPolicy="myPolicy";
builder.Services.AddDbContext<ApplicationDbContext>(option=>option.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddCors(option =>
{
	option.AddPolicy(myPolicy,
		builder =>
		{
			builder.AllowAnyOrigin();
			builder.AllowAnyMethod();
			builder.AllowAnyMethod();
		});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(myPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
