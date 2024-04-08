using BlazorWebAPIAuthentication.Data;
using BlazorWebAPIAuthentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IdentityDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityApiEndpoints<IdentityUser>().
    AddRoles<IdentityRole>().
    AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Please provide a valid OAUTH2 token in the format 'Bearer {token}'"
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
});

//builder.Services.AddCors(option => option.AddPolicy("wasm", policy =>
//{
//    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:5001");
//}));

var app = builder.Build();
app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
