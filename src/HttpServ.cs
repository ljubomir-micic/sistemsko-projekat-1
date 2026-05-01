using System;
using System.Net;
using System.Security.Cryptography;
using System.Threading;

class HttpServ {
    public static WebClient client = new WebClient();

    public static void StartServ()
    {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:5000/");
        listener.Start();
        Console.WriteLine("Server je pokrenut");
        Console.WriteLine("http://localhost:5000/");
       

        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            ThreadPool.QueueUserWorkItem(new WaitCallback(HttpServ.ObradaZahteva), context);
        }
    }

    public static void ObradaZahteva(object state)
    {
        HttpListenerContext context = (HttpListenerContext) state;
        string query = context.Request.RawUrl.Substring(1);

        if (string.IsNullOrEmpty(query))
        {
            
        }

        Slika? slika = Kes.kes[query];
        if (slika == null)
        {
            
        }
        string responseStirng;
    }
}