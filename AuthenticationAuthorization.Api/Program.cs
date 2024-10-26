using AuthenticationAuthorization.Api.DB;
using AuthenticationAuthorization.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    //Added auth header in top of the swagger docs
    option.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
}   );

//Add Authentication step-1
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

//Add Authorization step-2
builder.Services.AddAuthorizationBuilder();


//Configure DbContext step-3
builder.Services.AddDbContext<UserDbContext>(option =>
    option.UseSqlite("Data Source=UsersAuth.db"));

//Generating Database endpoints and store configurations step-4
builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Mapping Identity with User Class step-6
app.MapIdentityApi<User>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();