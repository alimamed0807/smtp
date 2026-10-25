
using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleApp31
{
    internal class Program
    {
        static string fromMail = "ali.mamed0807@gmail.com";
        static string appPassword = "tqyw wqqs cgyr thlb\r\n";

        static void Main(string[] args)
        {
            Console.Write("Ad: ");
            string name = Console.ReadLine();

            Console.Write("E-mail: ");
            string toMail = Console.ReadLine();

            Console.Write("Sifre: ");
            string password = Console.ReadLine();

            Random random = new Random();
            string code = random.Next(100000, 1000000).ToString();

            var mail = new MailMessage();
            mail.From = new MailAddress(fromMail);
            mail.To.Add(new MailAddress(toMail));
            mail.Subject = "E-mail tesdiqi";
            mail.Body = "Salam, " + name +
                        "\nTesdiq kodunuz: " + code +
                        "\nKodun etibarliliq muddeti 3 deqiqedir.";

            var smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(fromMail, appPassword);

            try
            {
                smtp.Send(mail);
                Console.WriteLine("Tesdiq kodu gonderildi...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Xeta: " + ex.Message);
                mail.Dispose();
                smtp.Dispose();
                return;
            }

            mail.Dispose();
            smtp.Dispose();

            DateTime expireTime = DateTime.Now.AddMinutes(3);
            int attempts = 0;

            while (attempts < 3 && DateTime.Now < expireTime)
            {
                Console.Write("Tesdiq kodunu daxil edin: ");
                string enteredCode = Console.ReadLine();

                if (DateTime.Now >= expireTime)
                {
                    Console.WriteLine("Kodun vaxti bitdi...");
                    return;
                }

                attempts++;

                if (enteredCode == code)
                {
                    Console.WriteLine("Qeydiyyat ugurla tamamlandi");
                    Console.WriteLine("Ad: " + name);
                    Console.WriteLine("E-mail: " + toMail);
                    return;
                }

                Console.WriteLine("Yanlis kod Qalan cehd: " + (3 - attempts));
            }

            if (DateTime.Now >= expireTime)
                Console.WriteLine("Kodun vaxti bitdi.");
            else
                Console.WriteLine("Cehd sayi bitdi. Qeydiyyat ugursuz oldu.");
        }
    }
}
