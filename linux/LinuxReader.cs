using System.Runtime.InteropServices;
using System.Text.Json;
namespace Identity
{

	public class LinuxReader
	{
		[DllImport("libX11")]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		private ConfigData configData;
		private readonly HttpClient httpClient = new HttpClient();

		public void StartQRCodeDetection()
		{
			Console.WriteLine("QR code detection started...");
			var focusTask = Task.Run(FocusConsoleWindow);

			while (true)
			{
				var qrCodeValue = Console.ReadLine();
				HandleQRCodeValue(qrCodeValue);
			}
		}

		private void FocusConsoleWindow()
		{
			while (true)
			{
				// No equivalent action needed for Linux
				Task.Delay(500).Wait();
			}
		}

		private async void HandleQRCodeValue(string qrCodeValue)
		{
			await SendHttpRequestAsync(qrCodeValue);
		}

		private async Task SendHttpRequestAsync(string qrCodeValue)
		{
			if (configData == null)
			{
				return;
			}

			try
			{
				var requestData = new
				{
					encryptioncode = configData.encryptioncode,
					signaturecode = configData.signaturecode,
					identificationcode = qrCodeValue.Replace("\"", "")
				};

				var requestJson = JsonSerializer.Serialize(requestData);
				var content = new StringContent(requestJson, System.Text.Encoding.UTF8, "application/json");

				var response = await httpClient.PostAsync(configData.url, content);
				response.EnsureSuccessStatusCode();

				var responseJson = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Response received: {responseJson}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while sending the HTTP request: {ex.Message}");
				// Handle the exception accordingly
			}
		}

		public void ReadConfigFile()
		{
			try
			{
				string configFile = "config.txt";

				if (File.Exists(configFile))
				{
					string configJson = File.ReadAllText(configFile);
					configData = JsonSerializer.Deserialize<ConfigData>(configJson);

					if (configData.encryptioncode != null && configData.signaturecode != null)
					{
						Console.WriteLine("Configuration file is valid");
					}
				}
				else
				{
					Console.WriteLine("Config file not found!");
					// Handle the absence of the config file accordingly
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while reading the config file: {ex.Message}");
				// Handle the exception accordingly
			}
		}
	}


}

