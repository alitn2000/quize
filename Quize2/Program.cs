

using Quize2.CardServices;
using Quize2.TransactionServices;
using Quize2.Entites;
CardService _cardService = new CardService();
TransactionService _transactionService = new TransactionService();
int count = 0;
bool login = true;
Card card = null;
while (true)
{
    if (count == 3)
    {
        Console.WriteLine("you cant try more!!!");
        _cardService.Update(card.CardNumber);
        count = 0;
        login = false;
        card = null;
        break;
    }
    Console.WriteLine("please Log In First:");
    Console.Write("Enter Card Number: ");
    var cardNo = Console.ReadLine();
    if (cardNo.Length < 16)
    {
        Console.WriteLine("Card is incorrect!!!");
        Console.WriteLine("Press any key to continiue...");
        Console.ReadKey();
        Console.Clear();
        break;
    }
     var exist =_cardService.CardExist(cardNo);
    if (exist)
    {
        card = _cardService.GetCard(cardNo);
    }
    Console.Write("Enter Password: ");
    var password = Console.ReadLine();
    bool flag = _cardService.UserExist(cardNo, password);
    if (flag == false)
    {
        count++;
        Console.WriteLine("User not found!!!");
        Console.WriteLine("Press any key to continiue...");
        Console.ReadKey();
        Console.Clear();
        break;
    }
    var Card = _cardService.GetCard;
   
}
while (login == true)
{
    Console.WriteLine("\nBank System Menu:");
    Console.WriteLine("1. Transfer");
    Console.WriteLine("2. Show all Transactions");
    Console.WriteLine("4. Exit");
    Console.Write("Select an option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter Destination Card");
            var cardNo2 = Console.ReadLine();
            if (cardNo2.Length < 16)
            {
                Console.WriteLine("Card is incorrect!!!");
                Console.WriteLine("Press any key to continiue...");
                Console.ReadKey();
                Console.Clear();
                break;
            }
            var cardexist = _cardService.CardExist(cardNo2);
            if (cardexist == false)
            {
                Console.WriteLine("Destination Card not found!!!");
                Console.WriteLine("Card is incorrect!!!");
                Console.WriteLine("Press any key to continiue...");
                Console.ReadKey();
                Console.Clear();
                break;
            }

            Console.Write("Enter amount: ");
            if (!int.TryParse(Console.ReadLine(), out int amount)&& amount<0)
            {
                Console.WriteLine("Invalid input for amount!!!");
                Console.WriteLine("Press Any Key To Continiue...");
                Console.ReadKey();
                Console.Clear();
                break;
            }
            bool transferstatus = _transactionService.Transfer(card.CardNumber, cardNo2, amount);
            if (transferstatus)
            {
                Console.WriteLine("Transfer done successfully.");
                Console.WriteLine("Press Any Key To Continiue...");
                Console.ReadKey();
                Console.Clear();
                break;
            }
            else
            {
                Console.WriteLine("Transfer faild successfully.");
                Console.WriteLine("Press Any Key To Continiue...");
                Console.ReadKey();
                Console.Clear();
                break;
            }

        case "2":
            Console.Write("Enter your card number: ");
            var Card = Console.ReadLine();
            break;

        case "4":
            Console.WriteLine("Exiting...");
            return;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}