namespace Identity
{
	public class Program
    {
        static void Main(string[] args)
        {
            LinuxReader lreader = new LinuxReader();

            lreader.ReadConfigFile();
            lreader.StartQRCodeDetection();
        }

    }
}
