using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Server.Models;

namespace ExpenseTracker.Data
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsRequired();
                entity.Property(e => e.Notes)
                    .HasMaxLength(1000);
                entity.HasIndex(e => e.ExpenseDate);
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Expenses)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Expenses)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(b => b.TotalAmount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
                entity.Property(b => b.RemainingAmount)
                    .HasColumnType("decimal(18,2)");
                entity.HasOne(b => b.User)
                    .WithMany(u => u.Budgets)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(u => u.Email)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(u => u.PasswordHash)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(u => u.FirstName)
                    .HasMaxLength(50);
                entity.Property(u => u.LastName)
                    .HasMaxLength(50);
                entity.HasIndex(u => u.Username)
                    .IsUnique();
                entity.HasIndex(u => u.Email)
                    .IsUnique();
            });


            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name).HasMaxLength(100).IsRequired();
                entity.HasOne(c => c.User)  
                    .WithMany(u => u.Categories)  
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });

            base.OnModelCreating(modelBuilder);

            // Seed Db

            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = 1,
            //        Username = "JohnDoe",
            //        Email = "john.doe@example.com",
            //        PasswordHash = "hashedpassword1",
            //        FirstName = "John",
            //        LastName = "Doe",
            //        CreatedAt = DateTime.UtcNow
            //    },
            //    new User
            //    {
            //        Id = 2,
            //        Username = "JaneSmith",
            //        Email = "jane.smith@example.com",
            //        PasswordHash = "hashedpassword2",
            //        FirstName = "Jane",
            //        LastName = "Smith",
            //        CreatedAt = DateTime.UtcNow
            //    }
            //);

            //modelBuilder.Entity<Category>().HasData(
            //    new Category
            //    {
            //        Id = 1,
            //        Name = "Food",
            //        Description = "Expenses for meals, groceries, etc.",
            //        UserId = 1,
            //        Icon = "🍔"
            //    },
            //    new Category
            //    {
            //        Id = 2,
            //        Name = "Transport",
            //        Description = "Transportation costs such as fuel and tickets.",
            //        UserId = 1,
            //        Icon = "🚗"
            //    },
            //    new Category
            //    {
            //        Id = 3,
            //        Name = "Entertainment",
            //        Description = "Expenses for movies, games, events, etc.",
            //        UserId = 2,
            //        Icon = "🎮"
            //    }
            //);

            //modelBuilder.Entity<Budget>().HasData(
            //    new Budget
            //    {
            //        Id = 1,
            //        TotalAmount = 1000m,
            //        RemainingAmount = 800m,
            //        UserId = 1,
            //        Month = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1)
            //    },
            //    new Budget
            //    {
            //        Id = 2,
            //        TotalAmount = 2000m,
            //        RemainingAmount = 1500m,
            //        UserId = 2,
            //        Month = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1)
            //    }
            //);

        }
    }
}

