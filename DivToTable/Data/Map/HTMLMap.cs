using DivToTable.Model;
using DivToTable.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DivToTable.Data.Map
{
    public class HTMLMap : IEntityTypeConfiguration<HTMLModel>
    {
        public void Configure(EntityTypeBuilder<HTMLModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.HTML);
           
        }
    }
}
