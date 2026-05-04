using System;
using System.IO;
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
            Slika? slika = null;
            try
            {
                string srcFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
                // string srcFolder = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
                string inputPath = Path.IsPathRooted(path) ? path : Path.Combine(srcFolder, path);
                if (!File.Exists(inputPath))
                    return null;

                byte[] slikaPolja = File.ReadAllBytes(inputPath);
                string outputFolder = Path.Combine(srcFolder, "GrayscaleImages");
                // string outputFolder = Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..")), "GrayscaleImages");
                Directory.CreateDirectory(outputFolder);
                string outputFileName = Path.ChangeExtension(Path.GetFileName(path), ".jpg");
                string outputPath = Path.Combine(outputFolder, outputFileName);

                using (var image = Image.Load(slikaPolja))
                {
                    image.Mutate(x => x.Grayscale());

                    using (var ms = new MemoryStream())
                    {
                        image.SaveAsJpeg(ms);
                        slika = new Slika(ms.ToArray());
                    }

                    image.SaveAsJpeg(outputPath);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return slika;
        }
    }
}
