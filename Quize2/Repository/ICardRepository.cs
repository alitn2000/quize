using Quize2.Entites;

namespace Quize2.Repository;

public interface ICardRepository
{

    bool MinusMoney(string cartNo, float money);

    bool PlusMoney(string cartNo, float money);
    bool Check(string cartNo);
    Card? GetCardByCardNo(string cardNo);
    bool LogIn(string cardNo, string pass);
    void UpdateCardStatus(string cardNo);
    void UpdateCardLimits(Card updatedCard);
}
