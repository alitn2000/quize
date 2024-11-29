using Quize2.Entites;
using Quize2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quize2.TransactionServices;

public class TransactionService
{
    private readonly ICardRepository _cardRepository = new CardRepository();
    private readonly TransactionRepository _transactionRepository = new TransactionRepository();

    public bool Transfer(string source, string destination, float money)
    {


        bool minus = _cardRepository.MinusMoney(source, money );
        if(minus == false)
        {
            Transaction transfail = new Transaction() { Amount = money, SourceCardNumber = source, DestinationCardNumber = destination, IsSuccessful = false, TransactionDate = DateTime.Now };
            _transactionRepository.AddTransaction( transfail );
            return false;
        }
        bool plus = _cardRepository.PlusMoney(destination, money );

        if (plus == false)
        {
            Transaction transfail2 = new Transaction() { Amount = money, SourceCardNumber = source, DestinationCardNumber = destination, IsSuccessful = false, TransactionDate = DateTime.Now };
            _transactionRepository.AddTransaction(transfail2);
            return false;
        }

        Transaction transaction = new Transaction() { Amount = money, SourceCardNumber = source,DestinationCardNumber = destination, IsSuccessful =true, TransactionDate = DateTime.Now };
        _transactionRepository.AddTransaction(transaction);
        return true;
    }
    public bool CheckCard(string cartNo)
    {
       bool flag =  _cardRepository.Check(cartNo);
        if (flag)
        {
            return true;
        }
        return false;
    }

    public bool GetTransactions(string cardNo)
    {
        var trans = _transactionRepository.ShowAll(cardNo);
        if (trans == null)
        {
            return false;
        }
        foreach (var t in trans)
        {
            Console.WriteLine($"Transaction ID: {t.TransactionId}, Source: {t.SourceCardNumber}, Destination: {t.DestinationCardNumber}, Amount: {t.Amount}, Date: {t.TransactionDate}, Successful: {t.IsSuccessful}");
        }
        return true;
    }
}
