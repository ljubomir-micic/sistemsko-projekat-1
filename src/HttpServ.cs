using System;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Text;
using System.IO;

namespace Projekat
{
    class HttpServ {
        public static HttpClient client = new HttpClient();

        public static void StartServ()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add($"http://localhost:{Podesavanja.brojPorta}/");
            listener.Start();
            Console.WriteLine("Server je pokrenut");
            Console.WriteLine($"http://localhost:{Podesavanja.brojPorta}/");
        

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                ThreadPool.QueueUserWorkItem(new WaitCallback(HttpServ.ObradaZahteva!), context);
            }
        }

        public static void ObradaZahteva(object state)
        {
            HttpListenerContext context = (HttpListenerContext) state;
            string query = context.Request.RawUrl!.Substring(1);

            if (string.IsNullOrEmpty(query))
            {
                // DONE: Vrati prazan HTML sa Error 400 Bad Request
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return;
            }

            Slika? slika = Program.kes[query];
            if (slika == null)
            {
                // TODO: Pronadji sliku iz file sistema, obradi je i smesti u kes
                slika = Slika.ObradiSliku(query);

                // TODO: Ako slika ne postoji, vrati prazan HTML sa Error 404 Not Found
                if (slika == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentLength64 = 0;
                    context.Response.Close(); // ← missing in your code
                    return;
                }

                Program.kes.DodajStavku(query, slika); // Proveriti da li je kes pun i obrisati stavku ako jeste koristeci
                                            // ogranicenje velicine
            }

            string responseStirng = $"<html><body><img src='data:image/jpeg;base64,{Convert.ToBase64String(slika.GetData())}' /></body></html>";
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = (int) HttpStatusCode.OK;
            byte[] buffer = Encoding.UTF8.GetBytes(responseStirng);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
            context.Response.Close();
        }
    }
}
