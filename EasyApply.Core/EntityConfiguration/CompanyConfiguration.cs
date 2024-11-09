using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EasyApply.Core.Domian;

namespace EasyApply.Core.EntityConfiguration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
              .HasColumnName("Id")
              .HasColumnType("int")
              .UseIdentityColumn(1, 1);

            builder.Property(x => x.UserId)
              .HasColumnName("UserId")
              .HasColumnType("varchar(250)")
              .IsRequired();

            builder.Property(x => x.Name)
              .HasColumnName("Name")
              .HasColumnType("varchar(250)")
              .IsRequired();
            
        }
    }
}
