using Microsoft.EntityFrameworkCore;

namespace WebAGK.Shared.Infrastructure.Repositories;

public abstract class WebAgkDbContext(DbContextOptions options) : DbContext(options) {
    
}