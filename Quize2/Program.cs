

using Quize2.CardServices;
using Quize2.TransactionServices;
using Quize2.Entites;
CardService _cardService = new CardService();
TransactionService _transactionService = new TransactionService();
Card card = null;
int count = 0;
bool login = false;

while (count < 3)
{
    Console.WriteLine("Please Log In First:");
    Console.Write("Enter Card Number: ");
    var cardNo = Console.ReadLine();

    if (cardNo.Length != 16)
    {
        Console.WriteLine("Card number must be 16 digits!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        continue;
    }

    var exists = _cardService.CardExist(cardNo);
    if (!exists)
    {
        Console.WriteLine("Card not found!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        continue;
    }

    Console.Write("Enter Password: ");
    var password = Console.ReadLine();

    bool existsCard = _cardService.UserExist(cardNo, password);
    if (!existsCard)
    {
        count++;
        Console.WriteLine($"Incorrect password! Attempts left: {3 - count}");
        if (count >= 3)
        {
            if (card != null)
            {
                card.IsActive = false;
                _cardService.Update(card.CardNumber);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            card = _cardService.GetCard(cardNo);
            login = true;

            if (count >= 3)
            {
                if (card != null)
                {
                    card.IsActive = false;
                    _cardService.Update(card.CardNumber);
                }
                else
                {
                    Console.WriteLine("Card not found; cannot deactivate.");
                }
            }

            break;
        }
    }

    if (!login)
    {
        Console.WriteLine("Too many failed attempts. Your card is being deactivated.");
        if (card != null)
        {
            _cardService.Update(card.CardNumber);
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }

    if (!login)
    {
        Console.WriteLine("Too many failed attempts. Your card is being deactivated.");
        if (card != null)
        {
            _cardService.Update(card.CardNumber);
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }

    while (login)
    {
        Console.WriteLine("\nBank System Menu:");
        Console.WriteLine("1. Transfer");
        Console.WriteLine("2. Show all Transactions");
        Console.WriteLine("3. Exit");
        Console.Write("Select an option: ");

        var choice = Console.ReadLine();

        while (login == true)
        {
            Console.WriteLine("\nBank System Menu:");
            Console.WriteLine("1. Transfer");
            Console.WriteLine("2. Show all Transactions");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");

            var choose = Console.ReadLine();

            switch (choose)
            {
                case "1":
                    if (!card.IsActive)
                    {
                        Console.WriteLine("Your card is deactivated. You cannot perform any transfers.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
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
                    if (!int.TryParse(Console.ReadLine(), out int amount) && amount < 0)
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
                    var cardDes = Console.ReadLine();
                    bool e = _cardService.CardExist(cardDes);
                    if (e == false)
                    {
                        Console.WriteLine("Destination Card not found!!!!");
                        Console.WriteLine("Press Any Key To Continiue...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }

                    var check = _transactionService.GetTransactions(cardDes);
                    if (check == false)
                    {
                        Console.WriteLine("dont have any transactions");
                    }
                    break;

                case "3":
                    login = false;
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}