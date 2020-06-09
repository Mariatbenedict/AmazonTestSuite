using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amazon_Test_Project.UILayer
{
    class LoginObjectHelper
    {
        public readonly string signInIconID = "nav-link-accountList";
        public readonly string emailTextBoxID = "ap_email";
        public readonly string continueButtonID = "continue";
        public readonly string passwordInputID = "ap_password";
        public readonly string submitButtonID = "signInSubmit";

        public void Login(WebDriverWait wait, TestDataModel data)
        {
            LoginObjectHelper loginObjectHelper = new LoginObjectHelper();
            IWebElement signIn = wait.Until(drv => drv.FindElement(By.Id(loginObjectHelper.signInIconID)));
            signIn.Click();
            IWebElement inputNumber = wait.Until(drv => drv.FindElement(By.Id(loginObjectHelper.emailTextBoxID)));
            inputNumber.SendKeys(data.userName);
            IWebElement continueButton = wait.Until(drv => drv.FindElement(By.Id(loginObjectHelper.continueButtonID)));
            continueButton.Click();
            IWebElement password = wait.Until(drv => drv.FindElement(By.Id(loginObjectHelper.passwordInputID)));
            password.SendKeys(data.password);
            IWebElement submitButton = wait.Until(drv => drv.FindElement(By.Id(loginObjectHelper.submitButtonID)));
            submitButton.Click();
        }




    }
}
