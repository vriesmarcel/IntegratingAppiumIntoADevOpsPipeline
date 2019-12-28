using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium.Windows.Enums;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.UITests
{
    [TestClass]
    public class WindowsUWPTests
    {
        static TestContext ctx;
        private static  WindowsDriver<WindowsElement> driver;
        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            ctx = context;
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.App , "8b831c56-bc54-4a8b-af94-a448f80118e7_sezxftbtgh66j!App");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }


        [TestMethod]
        public void CheckMasterDetailAndBack()
        {
            driver.LaunchApp();
            // tap on second item
            var el1 = driver.FindElementByAccessibilityId("Second item");
            TouchAction a = new TouchAction(driver);
            a.Tap(el1);

            el1.Click();
            var el2 = driver.FindElementByAccessibilityId("ItemText");
            Assert.IsTrue(el2.Text == "Second item");

            var backButton = driver.FindElementByAccessibilityId("Back");
            backButton.Click();

            var el3 = driver.FindElementByAccessibilityId("Fourth item");
            Assert.IsTrue(el3 != null);

            driver.CloseApp();

        }

        [TestMethod]
        public void AddNewItem()
        {
            driver.LaunchApp();
            // tap on second item
            var el1 = driver.FindElementByAccessibilityId("Add");
            TouchAction a = new TouchAction(driver);
            a.Tap(el1);
            el1.Click();
            var elItemText = driver.FindElementByAccessibilityId("ItemText");
            elItemText.Clear();
            elItemText.SendKeys("This is a new Item");

            var elItemDetail = driver.FindElementByAccessibilityId("ItemDescription");
            elItemDetail.Clear();
            elItemDetail.SendKeys("These are the details");

            var elSave = driver.FindElementByAccessibilityId("Save");
            elSave.Click();

            WaitForProgressbarToDisapear(driver);

            var touchScreen = new RemoteTouchScreen(driver);
            // Good value typically goes around 160 - 200 pixels with diminishing delta on the bigger values
            Boolean found = false;
            var maxretries = 5;
            int count = 0;
            while (!found && count++ < maxretries)
            {
                touchScreen.Flick(0, 180);
                try
                {
                    var el3 = driver.FindElementByAccessibilityId("This is a new Item");
                    found = el3 != null;
                }
                catch(Exception)
                { }
            }
            Assert.IsTrue(found);


            driver.CloseApp();

        }

        private void WaitForProgressbarToDisapear(WindowsDriver<WindowsElement> driver)
        {
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            wait.Until(d => d.FindElementByAccessibilityId("Second item"));
        }

        private void CreateScreenshot()
        {
            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile("startScreen.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
            ctx.AddResultFile("startScreen.png");
        }
    }
}

