using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyStore.Services.Identity.Domain;

namespace MyStore.Services.Identity.Repository
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext(DbContextOptions options) : base(options)
        {
            //
        }
    }
}
