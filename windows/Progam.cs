namespace Identity
{
	public class Program
    {
        static void Main(string[] args)
        {
            WindowsReader mreader = new WindowsReader();

            mreader.ReadConfigFile();
            mreader.StartQRCodeDetection();
        }

    }
}
