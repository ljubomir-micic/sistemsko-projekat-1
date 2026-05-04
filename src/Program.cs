namespace Projekat
{
    public class Program
    {
        public static Kes kes = new Kes();
        public static void Main(string[] args)
        {
            // Thread separate = new Thread(() => {
            //     string? key = Console.ReadLine();
            //     System.Console.WriteLine(key);
                
            // });
            
            HttpServ.StartServ();
        }
    }
}
