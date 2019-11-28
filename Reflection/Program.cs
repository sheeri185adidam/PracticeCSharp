namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var info = typeof(System.Int32).Assembly;
            System.Console.WriteLine(info);
            System.Console.ReadLine();
        }
    }
}
