using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data;

public class MyDBContext : IdentityDbContext<ApplicationUser>
{
    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 配置資料表名稱、主鍵、索引等
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            entity.HasIndex(e => e.NormalizedEmail).HasDatabaseName("EmailIndex");
        });
        
        base.OnModelCreating(modelBuilder);
    }
}