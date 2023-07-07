namespace QRReader
{
	internal class Program
    {
        static void Main(string[] args)
        {
            WindowsReader wreader = new WindowsReader();

            wreader.ReadConfigFile();
            wreader.StartQRCodeDetection();
        }

    }
}
