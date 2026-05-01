using System.Collections.Concurrent;

namespace Projekat
{
    public class Kes {
        // filename originala i sama obradjena slika
        public readonly ConcurrentDictionary<string, Slika> kes = new ConcurrentDictionary<string, Slika>();
        public object _lock = new object();

        public Kes()
        {
            
        }

        public void DodajStavku(string link, Slika slika)
        {
            lock (_lock)
            {
                kes[link] = slika;
            }
        }

        protected Slika? PronadjiStavku(string link)
        {
            lock (_lock)
            {
                if (kes.ContainsKey(link))
                {
                    return kes[link];
                }
                return null;
            }
        }

        public void ObrisiStavku(string link)
        {
            lock (_lock)
            {
                if (kes.ContainsKey(link))
                {
                    kes.TryRemove(link, out _);
                }
            }
        }
    
        public Slika? this[string link] {
            get { return PronadjiStavku(link); }
        }
    }
}
