using System.Collections.Concurrent;

namespace Projekat
{
    public class Kes {
        // filename originala i sama obradjena slika
        public static readonly ConcurrentDictionary<string, Slika> kes = new ConcurrentDictionary<string, Slika>();
        public object _lock = new object();

        Kes()
        {
            
        }

        public void DodajStavku(string link, Slika slika)
        {
            
        }

        public void ObrisiStavku(string link)
        {
            
        }
    }
}
