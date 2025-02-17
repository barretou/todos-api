using Microsoft.EntityFrameworkCore;
using TodoApi.Models.Entities;

namespace TodoApi.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Renaming the table and columns
            modelBuilder.Entity<TaskModel>().ToTable("tasks");
            modelBuilder.Entity<TaskModel>().Property(t => t.Id).HasColumnName("id").ValueGeneratedOnAdd();
            modelBuilder.Entity<TaskModel>().Property(t => t.Name).HasColumnName("name").IsRequired();
            modelBuilder.Entity<TaskModel>().Property(t => t.Description).HasColumnName("description");
            modelBuilder.Entity<TaskModel>().Property(t => t.CreatedDate).HasColumnName("create_date").IsRequired();
            modelBuilder.Entity<TaskModel>().Property(t => t.IsCompleted).HasColumnName("is_completed");
            modelBuilder.Entity<TaskModel>().Property(t => t.UserId).HasColumnName("user_id");

            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
