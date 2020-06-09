using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Amazon_Test_Project.PageHelper
{
    public class Common
    {

        public static readonly string AmazonHomePageURL = "http://www.amazon.in";
        //public static WebDriverWait wait(RemoteWebDriver driver)
        //{

        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        //    return wait;
        //}
        public string GetQueryParam(string url, string key)
        {
            Uri uri = new Uri(url);
            String value = HttpUtility.ParseQueryString(uri.Query).Get(key);
            return value;
        }
    }
}
