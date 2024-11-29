using Quize2.CardServices;
using Quize2.TransactionServices;
using Quize2.Entites;

CardService _cardService = new CardService();
TransactionService _transactionService = new TransactionService();
Card card = null;
int count = 0;
bool login = false;

// Login Loop
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

    bool isValidUser = _cardService.UserExist(cardNo, password);
    if (!isValidUser)
    {
        count++;
        Console.WriteLine($"Incorrect password! Attempts left: {3 - count}");
        if (count >= 3)
        {
            card = _cardService.GetCard(cardNo);
            if (card != null)
            {
                card.IsActive = false;
                _cardService.Update(card.CardNumber);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }
        Console.Clear();
        continue;
    }

    // Login Successful
    card = _cardService.GetCard(cardNo);
    if (card != null && card.IsActive)
    {
        login = true;
    }

    // Main Menu Loop
    while (login)
    {
        Console.Clear();
        Console.WriteLine("\nBank System Menu:");
        Console.WriteLine("1. Transfer");
        Console.WriteLine("2. Show all Transactions");
        Console.WriteLine("3. Exit");
        Console.Write("Select an option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                if (!card.IsActive)
                {
                    Console.WriteLine("Your card is deactivated. You cannot perform any transfers.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Enter Destination Card Number: ");
                var cardNo2 = Console.ReadLine();
                if (cardNo2.Length != 16)
                {
                    Console.WriteLine("Card number must be 16 digits!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }

                bool cardExist = _cardService.CardExist(cardNo2);
                if (!cardExist)
                {
                    Console.WriteLine("Destination card not found!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Enter amount: ");
                if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
                {
                    Console.WriteLine("Invalid input for amount. Please enter a positive number.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }

                bool transferStatus = _transactionService.Transfer(card.CardNumber, cardNo2, amount);
                if (transferStatus)
                {
                    Console.WriteLine("Transfer completed successfully.");
                }
                else
                {
                    Console.WriteLine("Transfer failed. Please try again.");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;

            case "2":
                Console.Write("Enter your card number: ");
                var cardNoToCheck = Console.ReadLine();
                if (!_cardService.CardExist(cardNoToCheck))
                {
                    Console.WriteLine("Card not found!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
                }

                var hasTransactions = _transactionService.GetTransactions(cardNoToCheck);
                if (!hasTransactions)
                {
                    Console.WriteLine("You don't have any transactions.");
                }
                else
                {
                    Console.WriteLine("Transactions retrieved successfully.");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;

            case "3":
                login = false;
                Console.WriteLine("Logging out...");
                break;

            default:
                Console.WriteLine("Invalid option. Please try again.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
        }
    }
}
