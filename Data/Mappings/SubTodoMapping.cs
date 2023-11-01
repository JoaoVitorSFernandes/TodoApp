using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Models;

namespace TodoApp.Data.Mappings
{
    public sealed class SubTodoMapping : IEntityTypeConfiguration<SubTodo>
    {
        public void Configure(EntityTypeBuilder<SubTodo> builder)
        {
            builder.ToTable("SubTask");

            builder.HasKey(x => x.Id)
                .HasName("PK_SubTaskId");
                
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

            builder.Property(x => x.Date)
                .HasColumnName("Date")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

            builder.HasOne(x => x.Todo)
                .WithMany(x => x.SubTodo)
                .HasConstraintName("FK_SubTask_Task")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}