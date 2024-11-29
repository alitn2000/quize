using Microsoft.EntityFrameworkCore;
using Quize2.Db;
using Quize2.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quize2.Repository;
public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context = new AppDbContext();
    public bool LogIn(string cardNo, string pass)
    {
        return _context.Cards.Any(c => c.CardNumber == cardNo && c.Password ==pass);
    }
    public bool Check(string cartNo)
    {
       return _context.Cards.Any(c => c.CardNumber == cartNo);
    }

    public Card? GetCardByCardNo(string cardNo)
    {
        return _context.Cards.FirstOrDefault(c => c.CardNumber == cardNo);
    }

    public bool MinusMoney(string cartNo, float money)
    {

        var Cart = GetCardByCardNo(cartNo);

        if (Cart.Balance < money)
        {
            return false;
        }
        Cart.Balance -= money;
        Cart.DailyTransferAmount += money;
        _context.SaveChanges();
        return true;
    }

   public bool PlusMoney(string cartNo, float money)

    {
        var Cart = GetCardByCardNo(cartNo);
        Cart.Balance += money;
        _context.SaveChanges();
        return true;
    }

    public void UpdateCardStatus(string cardNo)
    {
        var Cart = GetCardByCardNo(cardNo);
        Cart.IsActive = false; 
        _context.SaveChanges();
    }
    public void UpdateCardLimits(Card updatedCard)
    {
        var card = GetCardByCardNo(updatedCard.CardNumber);
            card.TodayTransaction = updatedCard.TodayTransaction;
            card.DailyTransferAmount = updatedCard.DailyTransferAmount;
            _context.Cards.Update(card); 
            _context.SaveChanges();
    }

}
