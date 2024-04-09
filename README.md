# About this Project
I created this project to experiment with Identity services in Blazor. We will go over the 
Web API which provides some services to register a user, add roles, login, and logout. 
## Packages Needed
* Microsoft.AspNetCore.Identity.EntityFramework
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.Tools
* Swashbuckle.AspNetCore.Filters
### IdentityDbContext
In this class we will create a DbContext to persist the IdentityUser class, a class that 
is included in the Microsoft Identity framework. You can extend the identity framework
by declaring a DbSet with the class you want to model. In the example below, we will 
extend Identity by adding some information about the blogs the user is associated with.  
```
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebAPIAuthentication.Data
{
    public class IdentityDbContext : IdentityDbContext<IdentityUser>    
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> dbContextOptions) 
            : base(dbContextOptions)
        {
            
        }
        
        // To extend tables in the Identity framework.  Blog is a simple
        // class in the Entity folder.  
        public DbSet<Blog> Blogs { get; set; }
    }
}
```
### Register Services
We need to add some services to inject IdentityDbContext and Identity user, then Map the
Identity API to the IdentityUser.  We will also need to build our connection string to 
establish conncetivity to our SQL database then build migrations.
```
// Add services to the container.
builder.Services.AddDbContext<IdentityDbContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("")));
builder.Services.AddIdentityApiEndpoints<IdentityUser>().
    AddEntityFrameworkStores<IdentityDbContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
```
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BlazorPrototypeIdentityDB;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```
```
PM> Add-Migration IdentityAPI
PM> Update-Database 
```
