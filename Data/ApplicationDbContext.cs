using Microsoft.EntityFrameworkCore;
using blogmanager_nguyenngocvi_22t1020794.Models;

namespace blogmanager_nguyenngocvi_22t1020794.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Post> Posts => Set<Post>();
}
