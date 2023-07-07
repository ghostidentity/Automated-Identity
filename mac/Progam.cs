using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Automated Identity");
        MacReader mreader = new MacReader();

        mreader.ReadConfigFile();
        mreader.StartQRCodeDetection();

        Console.WriteLine("Starting");
    }
}
