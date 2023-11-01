using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data.Mappings
{
    public class TodosMapping : IEntityTypeConfiguration<Todos>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Todos> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(x => x.Id)
                .HasName("PK_TasksId");


            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.ListTasks)
                .HasConstraintName("FK_Tasks_Task")
                .OnDelete(DeleteBehavior.Cascade);;
        }
    }
}