using Quize2.Db;
using Quize2.Entites;

namespace Quize2.Repository;

public class TransactionRepository
{
    private readonly AppDbContext _context = new AppDbContext();
    private readonly CardRepository _cardRepository = new CardRepository();
    public void AddTransaction(Transaction trans)
    {
        _context.Transactions.Add(trans);
        _context.SaveChanges();
    }

    public List<Transaction>? ShowAll(string cartId)
    {
        return _context.Transactions.Where(c => c.SourceCardNumber == cartId || c.DestinationCardNumber == cartId).ToList();
    }
}
