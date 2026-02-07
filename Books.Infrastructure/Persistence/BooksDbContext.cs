using System.Data.Entity;
using Books.Domain.Entities;

namespace Books.Infrastructure.Persistence
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext()
            : base("BooksConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Author
            modelBuilder.Entity<Author>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Author>()
                .Property(a => a.FullName)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Author>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Author>()
                .Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Author>()
                .HasIndex(a => a.Email)
                .IsUnique();

            // Book
            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .Property(b => b.Genre)
                .IsRequired()
                .HasMaxLength(80);

            modelBuilder.Entity<Book>()
                .Property(b => b.Pages)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(b => b.Year)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .HasRequired(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
