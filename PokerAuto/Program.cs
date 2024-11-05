using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;

namespace PokerAuto
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            ChromeOptions options = new ChromeOptions(); 
            string pathProfile = Environment.CurrentDirectory + @"\UserSettings\";
            options.AddArgument($"user-data-dir={pathProfile}");
            options.AddArgument("no-sandbox");
            var driverPath = Environment.CurrentDirectory + @"\chromedriver.exe";
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverPath);
            service.HideCommandPromptWindow = true;
            var browser = new ChromeDriver(service, options);
            browser.Manage().Window.Maximize();
            browser.Navigate().GoToUrl("https://ru.tradingview.com/chart/smgnCUaE/?symbol=BINANCE%3AROSEUSDT.P");
            Application.Run(new Form1(browser));

            var cookies = browser.Manage().Cookies.AllCookies.ToList();
            var jsstr = JsonConvert.SerializeObject(cookies);
            string path = Environment.CurrentDirectory + @"\UserSettings\Cookies.json";
            File.WriteAllText(path, jsstr);
            browser.Quit();
        }
    }
}