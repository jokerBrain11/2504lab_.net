using Microsoft.EntityFrameworkCore;

public class CodeFirstContext : DbContext
{
    // 定義 DbSet 屬性，對應到資料庫中的表格
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    public CodeFirstContext(DbContextOptions<CodeFirstContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 配置 Author 實體
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Authors"); // 設定表名
            entity.HasKey(a => a.Id); // 設定主鍵
            entity.Property(a => a.FirstName).IsRequired().HasMaxLength(50); // 設定 FirstName 屬性為必填，最大長度為 50 字符
            entity.Property(a => a.LastName).IsRequired().HasMaxLength(50); // 設定 LastName 屬性為必填，最大長度為 50 字符
            entity.Property(a => a.Email).HasMaxLength(100); // 設定 Email 屬性最大長度為 100 字符
            entity.HasMany(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId).HasPrincipalKey(a => a.Id).OnDelete(DeleteBehavior.Cascade); // 配置 Author 與 Book 的一對多關聯
        });


        // 配置 Book 類別對應到資料庫中的表格(另寫成於 Configuration 做引入)
        modelBuilder.ApplyConfiguration(new BookConfiguration());
    }
}