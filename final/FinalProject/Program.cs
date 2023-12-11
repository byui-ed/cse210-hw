using System.IO;

class Program
{
    static void Main(string[] args)
    {
    
         Person person = new Person();

        Console.WriteLine("Please enter your name:");
        person.Name = Console.ReadLine();

        Console.WriteLine("Please enter your details:");
        person.Details = Console.ReadLine();

        Console.WriteLine("Do you want to be a member of the Church of Jesus Christ of Latter-day Saints? (yes/no)");
        string answer = Console.ReadLine();

        person.IsMember = (answer.ToLower() == "yes");

        Console.WriteLine("Name: " + person.Name);
        Console.WriteLine("Details: " + person.Details);
        Console.WriteLine("Is a member of the Church of Jesus Christ of Latter-day Saints,: " + person.IsMember);
        Console.WriteLine("A calling will be extended to you by your Bishop, congratulation!: " + person.IsMember);

    }
}