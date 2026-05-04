namespace Projekat
{
    public class Program
    {
        internal static Kes kes = new Kes();
        public static void Main(string[] args)
        {
            HttpServ.StartServ();
        }
    }
}
