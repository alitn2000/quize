using Quize2.Entites;
using Quize2.Repository;

namespace Quize2.CardServices;

public class CardService
{
    private readonly ICardRepository _cardRepository = new CardRepository();

    public bool UserExist(string CartNo, string pass)
    {
      var flag =  _cardRepository.LogIn(CartNo, pass);
        if (flag)
        {
            return true;
        }
        return false;
    }
    public bool CardExist(string CartNo)
    {
        var flag = _cardRepository.Check(CartNo);
        if (flag)
        {
            return true;
        }
        return false;
    }
    public Card? GetCard(string cardNo)
    {
        return _cardRepository.GetCardByCardNo(cardNo);
    }
    public void Update(string cardNo)
    {
        _cardRepository.UpdateCardStatus(cardNo);
    }
}
