using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium.Windows.Enums;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CarvedRock.UITests
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
            
            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start(); 
            //driver = new WindowsDriver<WindowsElement>(_appiumLocalService, capabilities);
            driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }

        [TestMethod]
        public void ScrollToEndOfListUsingRemoteTouchScreenFlick()
        {
            driver.LaunchApp();

            var touchScreen = new RemoteTouchScreen(driver);

            touchScreen.Flick(0, 160);
            touchScreen.Flick(0, 160);
            
            driver.CloseApp();

        }

        [TestMethod]
        public void ScrollToEndOfListUsingRemoteTouchScreenScroll()
        {
            driver.LaunchApp();

            var touchScreen = new RemoteTouchScreen(driver);
            touchScreen.Scroll(0, -300);
            touchScreen.Scroll(0, -300);

            driver.CloseApp();

        }

        [TestMethod]
        public void GetUIDocument()
        {
            driver.LaunchApp();
            var document = driver.PageSource;
            ctx.WriteLine(document);

        }

        //[TestMethod]
        //public void TapElementWeFind()
        //{
        //    driver.LaunchApp();
            
        //    var ListView = driver.FindElement(MobileBy.ClassName("ListView"));
        //    var clickablePoint = ListView.GetAttribute("ClickablePoint");
        //    driver.getPro
        //    var attributesONElement = ListView.GetProperty("attributes");

        //    ListView.Click();

        ////    var stop = driver.StopRecordingScreen();

        //    driver.CloseApp();
        //}
        [TestMethod]
        public void ScrollToEndOfListUsingPointerInputDevice()
        {
            driver.LaunchApp();
            var ListView = driver.FindElement(MobileBy.ClassName("ListView"));
           // ListView.GetAttribute
            // set start point
            FlickUp(driver, ListView);

            Thread.Sleep(3000);

            FlickUp(driver, ListView);

 
            Thread.Sleep(3000);

            driver.CloseApp();

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
            Boolean found = false;
            var maxretries = 5;
            int count = 0;
            while (!found && count++ < maxretries)
            {
            // Good value typically goes around 160 - 200 pixels with diminishing delta on the bigger values
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

        private void FlickUp(WindowsDriver<WindowsElement> driver, AppiumWebElement element)
        {
            var input = new PointerInputDevice(PointerKind.Touch);
            ActionSequence FlickUp = new ActionSequence(input);
            FlickUp.AddAction(input.CreatePointerMove(element, 0, 0, TimeSpan.Zero));
            FlickUp.AddAction(input.CreatePointerDown(MouseButton.Left));
            FlickUp.AddAction(input.CreatePointerMove(element, 0, -600, TimeSpan.FromMilliseconds(200)));
            FlickUp.AddAction(input.CreatePointerUp(MouseButton.Left));
            driver.PerformActions(new List<ActionSequence>() { FlickUp });
        }
    }
}

