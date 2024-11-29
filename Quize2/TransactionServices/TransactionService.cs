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
        var sourceCard = _cardRepository.GetCardByCardNo(source);

        if (sourceCard.TodayTransaction == null || sourceCard.TodayTransaction.Value.Date != DateTime.Now.Date)
        {
            sourceCard.DailyTransferAmount = 0;
            sourceCard.TodayTransaction = DateTime.Now;
        }
        {
            sourceCard.DailyTransferAmount = 0; 
            sourceCard.TodayTransaction = DateTime.Now; 
        }

        if (money > 250 || sourceCard.DailyTransferAmount + money > 250)
        {
            Console.WriteLine("Transfer amount error (more than 250)!!!");
            return false;
        }

        bool minus = _cardRepository.MinusMoney(source, money);
        if (!minus)
        {
            var failedTransaction = new Transaction
            {Amount = money,SourceCardNumber = source,DestinationCardNumber = destination,IsSuccessful = false,TransactionDate = DateTime.Now};
            _transactionRepository.AddTransaction(failedTransaction);
            return false;
        }

        bool plus = _cardRepository.PlusMoney(destination, money);
        if (!plus)
        {
            _cardRepository.PlusMoney(source, money);
            var failedTransaction = new Transaction
            {Amount = money,SourceCardNumber = source,IsSuccessful = false,DestinationCardNumber = destination,TransactionDate = DateTime.Now};
            _transactionRepository.AddTransaction(failedTransaction);
            return false;
        }

        var transaction = new Transaction
        {Amount = money,SourceCardNumber = source,DestinationCardNumber = destination,IsSuccessful = true,TransactionDate = DateTime.Now};
        _transactionRepository.AddTransaction(transaction);

        sourceCard.DailyTransferAmount += money;
        _cardRepository.UpdateCardLimits(sourceCard);
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
