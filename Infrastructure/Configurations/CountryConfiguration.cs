using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new Country { Id = 1, Name = "Türkiye" },
            new Country { Id = 2, Name = "Almanya" },
            new Country { Id = 3, Name = "Fransa" },
            new Country { Id = 4, Name = "Hollanda" },
            new Country { Id = 5, Name = "Portekiz" },
            new Country { Id = 6, Name = "İtalya" },
            new Country { Id = 7, Name = "İspanya" },
            new Country { Id = 8, Name = "Belçika" }
        );
    }
}
