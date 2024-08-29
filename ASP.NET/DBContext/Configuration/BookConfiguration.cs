using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// 定義 BookConfiguration 類，實作 IEntityTypeConfiguration<Book> 介面
// 用於配置 Book 實體的屬性和映射設置
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    // Configure 方法用於設置 Book 實體的 Fluent API 配置
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // 設定 Book 實體對應到資料庫中的 "Books" 表
        builder.ToTable("Books");

        // 設定 Book 實體的主鍵為 BookId 屬性
        builder.HasKey(b => b.BookId);

        // 設定 Title 屬性，最大長度為 256 字符，且此屬性為必填欄位
        builder.Property(b => b.Title)
            .HasMaxLength(256) // 設定最大長度
            .IsRequired(); // 設定為必填欄位

        // 設定 ISBN 屬性，最大長度為 20 字符
        builder.Property(b => b.ISBN)
            .HasMaxLength(20); // 設定最大長度

        // 設定 PageCount 屬性為必填欄位
        builder.Property(b => b.PageCount)
            .IsRequired(); // 設定為必填欄位

        // 設定 PublishDate 屬性，資料庫中的欄位型別為 "date"
        builder.Property(b => b.PublishDate)
            .HasColumnType("date"); // 設定資料庫欄位型別

        // 設定 Description 屬性，最大長度為 2000 字符
        builder.Property(b => b.Description)
            .HasMaxLength(2000); // 設定最大長度

        // 設定 Notes 屬性，最大長度為 2000 字符
        builder.Property(b => b.Notes)
            .HasMaxLength(2000); // 設定最大長度
    }
}
