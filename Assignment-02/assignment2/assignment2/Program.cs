double price;
string choice;
double stakeRate=3.1;
double ETHPurchased = 0;
double commissionRate = 0;
double totalCommission;
double stalkedETH = 0;
string tradeRequest;

Random rnd = new Random();
price = rnd.Next(2600, 2999);



//programs runs fine till here
// ask user if he/she want to run another trade request
// if yes, then send user to the top
do
{
    Console.Clear();

    Console.WriteLine("---- DMITCryptoEx ETH Trader ----");

    Console.WriteLine("current ETH spot price is: " + price);


    do
    {
        try
        {
            ETHPurchased = 0;
            Console.Write("Enter amount of ETH to purchase: ");
            ETHPurchased = Convert.ToDouble(Console.ReadLine());
        }
        catch (Exception)
        {
            Console.WriteLine("ETH amount must be a number.");
        }

    } while (ETHPurchased <= 0);


    Console.WriteLine("current stake rate  is: 3.100%");
    Console.Write("Stake your ETH (y/n): ");
    choice = Console.ReadLine();


    //ETHPurchased = Convert.ToDouble(Console.ReadLine());
    if (ETHPurchased >= 0.0000001 && ETHPurchased <= 0.999999)
    {
        commissionRate = 1.9;
    }
    else if (ETHPurchased >= 1.000000 && ETHPurchased <= 4.999999)
    {
        commissionRate = 1.75;
    }
    else if (ETHPurchased >= 5.000000 && ETHPurchased <= 9.999999)
    {
        commissionRate = 1.5;
    }
    else if (ETHPurchased >= 10.0000)
    {

        commissionRate = 1.25;
    }
    totalCommission = (price * commissionRate) / 100;

    if (choice == "y")
    {
        stalkedETH = ((ETHPurchased * price * stakeRate) / (12 * 100));
        Console.WriteLine();
        Console.WriteLine($"You will earn {stalkedETH:C} per month for your staked ETH");
        Console.WriteLine();
    }



    Console.WriteLine("Please review your order ...");
    Console.WriteLine($"Total ETH purchased: {ETHPurchased:N6}");
    Console.WriteLine($"ETH spot price: {price:C}");
    Console.WriteLine("Commission rate: " + commissionRate + "%");
    Console.WriteLine($"Total commission: {totalCommission:C}"
        );
    Console.WriteLine("Staked? " + choice);
    if (choice == "y")
    {
        Console.WriteLine($"staked monthly reward: {stalkedETH:C}");
    }



    Console.WriteLine("-----------------------------");
    Console.WriteLine("Total purchase: $" + (price + totalCommission));
    Console.Write("Would you like to continue with your order (y/n): ");
    string secondChoice = Console.ReadLine();
    if (secondChoice == "y")

    {
        Console.WriteLine("Your order has been sent. Thank you.");
    }
    else
    {
        Console.WriteLine("Your order has been cancelled.");
    }

    Console.WriteLine();


    Console.Write("Do you want to run another trade request(y/n) : ");
    tradeRequest = Console.ReadLine();

}
while (tradeRequest == "y" );



Console.WriteLine("Thank you for using DMITCryptoEx");

Console.ReadKey();