using System;

namespace PromptPayQR.Demo

{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter PromptPay ID : ");
                var promptpayID = Console.ReadLine();
                Console.Write("Enter Amount : ");
                var amount = Console.ReadLine();

                var qrCode = PromptPayQR.QRCodePayload(promptpayID, amount);
                Console.WriteLine($"PromptPay QR String : {qrCode}");

                new DrawQR(PromptPayQR.QRCodeImage(qrCode));

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}