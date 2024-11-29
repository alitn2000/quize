using System.ComponentModel.DataAnnotations;

namespace Quize2.Entites;

public class Transaction
{
    [Key]
    public int TransactionId { get; set; }
    public string SourceCardNumber { get; set; }
    public Card SourceCard { get; set; }
    public string DestinationCardNumber { get; set; }
    public Card DestinationCard { get; set; }
    public float Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsSuccessful { get; set; }
}
