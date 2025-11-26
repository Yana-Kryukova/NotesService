using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NotesService.ValueObjects.Validators;
using NotesService.Domain;
using NotesService.ValueObjects;

namespace NotesService.Infrastructure.EntityFramework.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Username)
            .IsRequired()
            .HasConversion(username => username.Value, str => new Username(str))
            .HasMaxLength(UsernameValidator.MAX_LENGTH);
        builder.HasMany<Note>("_notes")
            .WithOne(x => x.User)
            .HasForeignKey("UserId")
            .HasPrincipalKey(x => x.Id);
    }
}

