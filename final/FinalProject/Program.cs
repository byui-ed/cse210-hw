using System.IO;

class Program
{
    static void Main(string[] args)
    {
    
        Console.WriteLine("Welcome to the Membership Registration Program");
        Console.WriteLine("Please enter your name:");
        string name = Console.ReadLine();


        Console.WriteLine($"Hello, {name}!");

        Console.WriteLine("Would you like to become a member of the Church of Jesus Christ of Latter-day Saints? (yes/no)");
        string response = Console.ReadLine();

        if (response.ToLower() == "yes")
        {
            Console.WriteLine("Thank you for your interest in becoming a member!");
        
        }
        else
        {
            Console.WriteLine("Thank you for considering it. If you change your mind, feel free to reach out!");
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

    }
}