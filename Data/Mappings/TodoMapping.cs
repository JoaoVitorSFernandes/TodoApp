using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Models;

namespace TodoApp.Data.Mappings
{
    public class TodoMapping : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Task");

            builder.HasKey(x => x.Id)
                .HasName("PK_TaskId");
                
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(1023);

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("TEXT")
                .HasMaxLength(2000);

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasColumnType("Boolean")
                .HasDefaultValue(false);

            builder.Property(x => x.Favorite)
                .HasColumnName("Favorite")
                .HasColumnType("Boolean")
                .HasDefaultValue(false);

            builder.Property(x => x.Date)
                .HasColumnName("Date")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

            builder.HasOne(x => x.SubTodo)
                .WithOne(x => x.Todo)
                .HasConstraintName("FK_Task_SubTask")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ListTasks)
                .WithMany(x => x.Tasks)
                .HasConstraintName("FK_Task_Tasks")
                .OnDelete(DeleteBehavior.Cascade);
        
        
        }
    }
}