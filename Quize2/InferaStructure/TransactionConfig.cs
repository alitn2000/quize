using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quize2.Entites;

namespace Quize2.InferaStructure;

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasOne(t => t.SourceCard).WithMany(t => t.SourceCards).HasForeignKey(c => c.SourceCardNumber).OnDelete(DeleteBehavior.Restrict); ;
        builder.HasOne(t => t.DestinationCard).WithMany(t => t.DestinationCards).HasForeignKey(c => c.DestinationCardNumber).OnDelete(DeleteBehavior.Restrict); ;



    }
}
