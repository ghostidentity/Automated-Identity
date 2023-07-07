namespace Identity
{
	public class Program
    {
        static void Main(string[] args)
        {
            MacReader mreader = new MacReader();

            mreader.ReadConfigFile();
            mreader.StartQRCodeDetection();
        }

    }
}
