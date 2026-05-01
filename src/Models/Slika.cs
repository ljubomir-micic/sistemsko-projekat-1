using System.Security.Cryptography.X509Certificates;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Projekat
{
    public class Slika
    {
        private byte[] data;

        public Slika(byte[] data)
        {
            this.data = data;
        }

        public byte[] GetData()
        {
            return data;
        }

        public static Slika? ObradiSliku(string path)
        {
            Slika slika = null;
            try
            {
                byte[] slikaPolja = File.ReadAllBytes(path);
                
                using (var image = Image.Load(slikaPolja))
                {
                    using (var ms = new MemoryStream())
                    {
                        image.Mutate(x => x.Grayscale());
                        image.SaveAsJpeg(ms);
                        slika = new Slika(ms.ToArray());
                    }
                }
            } catch (Exception e)
            {
                return null;
            }

            return slika;
        }
    }
}
