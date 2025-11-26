using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NotesService.ValueObjects.Validators;
using NotesService.Domain;
using NotesService.ValueObjects;
using System.Globalization;

namespace NotesService.Infrastructure.EntityFramework.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Title)
            .IsRequired(false)
            .HasConversion(title => title!.Value, str => new Title(str))
            .HasMaxLength(TitleValidator.MAX_LENGTH);
        builder.Property(x => x.Thesis)
            .IsRequired()
            .HasConversion(thesis => thesis.Value, str => new Thesis(str));
        builder.Property(x => x.CreationData).IsRequired().HasConversion
        (
            src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
            dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc)
        );
        builder.Property(x => x.ModificationData).IsRequired(false).HasConversion
        (
            src => !src.HasValue ? src : src.Value.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src.Value, DateTimeKind.Utc),
            dst => !dst.HasValue ? dst : dst.Value.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst.Value, DateTimeKind.Utc)
        );
        builder.HasOne(x => x.User).WithMany("_notes");
    }
}
