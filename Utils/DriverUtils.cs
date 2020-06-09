using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Amazon_Test_Project.Utils
{
    public class DriverUtils
    {

        public RemoteWebDriver GetDriver(string type)
        {
            RemoteWebDriver driver = null;
            switch (type)
            {
                case "chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--start-maximized");
                    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
                    break;
                case "firefox":
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("--start-maximized");
                    driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), firefoxOptions);
                    break;
                default:
                    ChromeOptions defaultOptions = new ChromeOptions();
                    defaultOptions.AddArgument("--start-maximized");
                    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), defaultOptions);
                    break;

            }

            return driver;

        }

    }
}


