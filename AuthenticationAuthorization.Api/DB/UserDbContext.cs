using AuthenticationAuthorization.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAuthorization.Api.DB;

public class UserDbContext:IdentityDbContext<User>
{
    public UserDbContext(DbContextOptions<UserDbContext>options):base(options)
    {
        
    }    
}