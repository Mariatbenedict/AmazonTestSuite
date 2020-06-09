using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using Newtonsoft.Json;
using Amazon_Test_Project.Utils;
using System.Web;
using Amazon_Test_Project.UILayer;
using Amazon_Test_Project.Assertion;
using Amazon_Test_Project.PageHelper;

namespace Amazon_Test_Project
{
    [TestClass]
    public class UnitTest
    {
        private RemoteWebDriver driver;
        private WebDriverWait wait;
        private TestDataModel data;
        private Common common;

        [TestInitialize]
        public void LoadTestData()
        {
            DataUtils dataUtils = new DataUtils();
            data = dataUtils.LoadData();
            common = new Common();
        }

 
        public void NavigateAmazon(string type)
        {
            DriverUtils driverUtils = new DriverUtils();
            driver = driverUtils.GetDriver(type);
            driver.Navigate().GoToUrl(Common.AmazonHomePageURL);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
        
      
        public void NavigateAmazon()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options);
            driver.Navigate().GoToUrl(Common.AmazonHomePageURL);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

       
        [TestMethod]
        public void LoginAmazonFirefox()
        {
            LoginObjectHelper loginObjectHelper = new LoginObjectHelper();
            NavigateAmazon("firefox");
            loginObjectHelper.Login(wait,data);
        }

        [TestMethod]
        public void LoginAmazonChrome()
        {
            LoginObjectHelper loginObjectHelper = new LoginObjectHelper();
            NavigateAmazon("chrome");
            loginObjectHelper.Login(wait, data);
        }

        [TestMethod]
        public void Search()
        {
            SearchAmazon();
            SearchObjectHelper searchObjectHelper = new SearchObjectHelper();
            //Assert.AreEqual(common.GetQueryParam(driver.Url,"k"),data.searchString);
            IWebElement searchTextElement = wait.Until(drv => drv.FindElement(By.CssSelector(searchObjectHelper.searchTextElement)));
            Assert.AreEqual("\""+data.searchString+"\"",searchTextElement.Text);
        }

        public void SearchAmazon()
        {
            NavigateAmazon("chrome");
            SearchObjectHelper searchObjectHelper = new SearchObjectHelper();
            IWebElement searchBox = wait.Until(drv => drv.FindElement(By.Id(searchObjectHelper.searchTextBoxID)));
            searchBox.SendKeys(data.searchString);
            searchBox.SendKeys(Keys.Enter);
        }

        [TestMethod]
        public void SortLowToHigh()
        {
            SearchAmazon();
            SortLowToHighObjectHelper sortLowToHighObjectHelper = new SortLowToHighObjectHelper();
            SortLowToHighAssertValues selectedFilterText = new SortLowToHighAssertValues();
            IWebElement sort = wait.Until(drv => drv.FindElement(By.Id(CommonObjectHelper.sortFilterDropDownID)));
            sort.Click();
            IWebElement sortLowToHigh = wait.Until(drv => drv.FindElement(By.Id(sortLowToHighObjectHelper.sortLowToHighID)));
            sortLowToHigh.Click();
            //string URL = driver.Url;
            IWebElement selectedFilter = wait.Until(drv => drv.FindElement(By.CssSelector(sortLowToHighObjectHelper.selectedFilter)));
            Assert.AreEqual(selectedFilter.Text, selectedFilterText.selectedFilterText);   
        }

        [TestMethod]
        public void SortHighToLow()
        {

            SearchAmazon();
            SortHighToLowObjectHelper sortHighToLowObjectLayer = new SortHighToLowObjectHelper();
            SortHightToLowAssertValues selectedFilterText = new SortHightToLowAssertValues();
            IWebElement sort = wait.Until(drv => drv.FindElement(By.Id(CommonObjectHelper.sortFilterDropDownID)));
            sort.Click();
            IWebElement sortHighToLow = wait.Until(drv => drv.FindElement(By.Id(sortHighToLowObjectLayer.sortHighToLowID)));
            sortHighToLow.Click();
            IWebElement selectedFilter = wait.Until(drv => drv.FindElement(By.CssSelector(sortHighToLowObjectLayer.selectedFilter)));
            Assert.AreEqual(selectedFilter.Text, selectedFilterText.selectedFilterText);
        }

        [TestMethod]
        public void SortAvgReview()
        {
            SearchAmazon();
            SortAvgReviewObjectHelper sortAvgReviewObjectHelper = new SortAvgReviewObjectHelper();
            SortAvgReviewAssertValues sortAvgReviewAssertValues = new SortAvgReviewAssertValues();
            IWebElement sort = wait.Until(drv => drv.FindElement(By.Id(CommonObjectHelper.sortFilterDropDownID)));
            sort.Click();
            IWebElement sortAvgReview = wait.Until(drv => drv.FindElement(By.Id(sortAvgReviewObjectHelper.averageReviewID)));
            sortAvgReview.Click();
            IWebElement selectedFilter = wait.Until(drv => drv.FindElement(By.CssSelector(sortAvgReviewObjectHelper.selectedFilter)));
            Assert.AreEqual(selectedFilter.Text, sortAvgReviewAssertValues.selectedFilterText);
        }

        [TestMethod]
        public void SortNewestArrivals()
        {
            SearchAmazon();
            SortNewArrivalsAssertValues sortNewArrivalsAssertValues = new SortNewArrivalsAssertValues();
            SortNewArrivalsObjectHelper sortNewArrivalsObjectHelper = new SortNewArrivalsObjectHelper();
            IWebElement sort = wait.Until(drv => drv.FindElement(By.Id(CommonObjectHelper.sortFilterDropDownID)));
            sort.Click();
            IWebElement sortAvgReview = wait.Until(drv => drv.FindElement(By.Id(sortNewArrivalsObjectHelper.newestArrivalsID)));
            sortAvgReview.Click();
            IWebElement selectedFilter = wait.Until(drv => drv.FindElement(By.CssSelector(sortNewArrivalsObjectHelper.selectedFilter)));
            Assert.AreEqual(selectedFilter.Text, sortNewArrivalsAssertValues.selectedFilterText);
        }

        [TestMethod]
        public void AmazonLogout()
        {
            NavigateAmazon();
            LoginObjectHelper loginObjectHelper = new LoginObjectHelper();
            loginObjectHelper.Login(wait,data);
            AmazonLogoutObjectHelper amazonLogoutObjectHelper = new AmazonLogoutObjectHelper();
            AmazonLogoutAssertValues amazonLogoutAssertValues = new AmazonLogoutAssertValues();
            var element = wait.Until(drv => drv.FindElement(By.Id(amazonLogoutObjectHelper.signOutIconID)));
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
            IWebElement signOut = wait.Until(drv => drv.FindElement(By.Id(amazonLogoutObjectHelper.signOutButtonID)));
            signOut.Click();
            string URL = driver.Url;
            Assert.AreEqual(URL.Split("?")[0], amazonLogoutAssertValues.postLogoutURL);

        }

        [TestMethod]
        public void AmazonSortByFourStar()
        {
            SearchAmazon();
            FourStarSortingObjectHelper fourStarSortingObjectHelper = new FourStarSortingObjectHelper();
            FourStarSortAssertValues fourStarSortAssertValues = new FourStarSortAssertValues();
            var element = wait.Until(drv => drv.FindElement(By.Id(fourStarSortingObjectHelper.fourStarSortElementID)));
            element.Click();
            Assert.AreEqual(common.GetQueryParam(driver.Url,"ref"), fourStarSortAssertValues.URLReferenceParameterValue);
        }

        [TestMethod]
        public void AmazonSortByThreeStar()
        {
            SearchAmazon();
            ThreeStarSortObjectHelper threeStarSortObjectLayer = new ThreeStarSortObjectHelper();
            ThreeStarSortAssertValues threeStarSortAssertValues = new ThreeStarSortAssertValues();
            var element = wait.Until(drv => drv.FindElement(By.Id(threeStarSortObjectLayer.threeStarSortElementID)));
            element.Click();
            Assert.AreEqual(common.GetQueryParam(driver.Url,"ref"), threeStarSortAssertValues.URLReferenceParameterValue);
        }

        [TestMethod]
        public void AmazonSortByTwoStar()
        {
            SearchAmazon();
            TwoStarSortObjectHelper twoStarSortObjectHelper = new TwoStarSortObjectHelper();
            TwoStarSortAssertValues twoStarSortAssertValues = new TwoStarSortAssertValues();
            var element = wait.Until(drv => drv.FindElement(By.Id(twoStarSortObjectHelper.twoStarSortElementID)));
            element.Click();
            Assert.AreEqual(common.GetQueryParam(driver.Url,"ref"), twoStarSortAssertValues.URLReferenceParameterValue);

        }

        [TestMethod]
        public void AmazonSortByOneStar()
        {
            SearchAmazon();
            OneStarSortObjectHelper oneStarSortObjectHelper = new OneStarSortObjectHelper();
            OneStarSortAssertValues oneStarSortAssertValues = new OneStarSortAssertValues();
            var element = wait.Until(drv => drv.FindElement(By.Id(oneStarSortObjectHelper.oneStarSortElementID)));
            element.Click();
            Assert.AreEqual(common.GetQueryParam(driver.Url,"ref"), oneStarSortAssertValues.URLReferenceParameterValue);

        }
    }
}
