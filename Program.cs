using ExaminationSystem.Core.Model;
using ExaminationSystem.Data;
using ExaminationSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var Configuration =builder.Configuration;
var connectionString=Configuration["ConnectionStrings:DefaultConnection"];
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
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(options =>
						{
							options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
							options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
							options.DefaultScheme             = JwtBearerDefaults.AuthenticationScheme;
						})
						.AddJwtBearer(o =>
						{
							o.RequireHttpsMetadata = true;
							o.SaveToken = true;
							o.TokenValidationParameters = new TokenValidationParameters
							{
								ValidateIssuer = true,
								ValidIssuer = Configuration["JWT:Issuer"],
								ValidateAudience = true,
								ValidAudience = Configuration["JWT:Audience"],
								IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]!)),
								ValidateLifetime = false,
								ValidateIssuerSigningKey = true
							};
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
