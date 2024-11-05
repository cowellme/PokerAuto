using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PokerAuto
{
    public partial class Form1 : Form
    {
        public ChromeDriver Browser { get; set; }

        public Form1(ChromeDriver browser)
        {
            Browser = browser;
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Browser.SwitchTo().Window(Browser.WindowHandles.Last());

            var element = Browser.FindElements(By.Id(@"List of Trades")).First();
            
            if(element != null ) element.Click();

            Thread.Sleep(500);

            var tbody = Browser.FindElement(By.ClassName("ka-tbody"));

            if (tbody != null)
            {
                var root = tbody.FindElements(By.ClassName("ka-tr")).ToList();
                if (root.Count > 0)
                {
                    textBox1.Text = "";
                    foreach (var webElement in root)
                    {

                        var txt = webElement.GetAttribute("outerHTML");
                        if(txt == null && txt == "" ) continue;

                        var spans = Regex.Match(txt, "<span>([0-9]*)</span>.*<span>([А-Яа-я]*)</span>");

                        if (spans.Length > 0)
                        {
                            var ids = spans.Groups[1].Value;
                            var side = spans.Groups[2].Value;
                            textBox1.Text += $"ID Сделки: {ids} Направление: {side}" + Environment.NewLine;
                        }
                    }
                }
            }
        }
    }
}
