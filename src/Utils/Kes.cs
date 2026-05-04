using System.Collections.Concurrent;

namespace Projekat
{
    public class Kes {
        // filename originala i sama obradjena slika
        public readonly int ObjLimit = 100;
        private int LastEntered = 0;
        private List<string> ListOfLinks = [];
        public readonly ConcurrentDictionary<string, Slika> kes = new ConcurrentDictionary<string, Slika>();
        public object _lock = new object();

        public Kes()
        {
            
        }

        public void DodajStavku(string link, Slika slika)
        {
            lock (_lock)
            {   
                if(!ListOfLinks.Contains(link))
                    ListOfLinks.Add(link);

                if(kes.Count < ObjLimit)
                {
                    kes[link] = slika;
                }
                else
                {
                    if(kes.TryRemove(ListOfLinks.First(), out _))
                    {
                        if(kes.TryAdd(link, slika))
                            Console.WriteLine("The image has been successfully added" + '\n');
                    }
                    else {
                        System.Console.WriteLine("There is problem with freeing the space in cash...");
                    }
                }
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
