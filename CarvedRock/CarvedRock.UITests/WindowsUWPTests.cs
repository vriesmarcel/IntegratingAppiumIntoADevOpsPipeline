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
        static TestContext _ctx;
     
        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            _ctx = context;
        }


        [TestMethod]
        public void CheckMasterDetailAndBack()
        {
            var driver = StartApp();
            // tap on second item
            var el1 = driver.FindElementByName("Second item");
            
            CreateScreenshot(driver, _ctx);
            el1.Click();
            var el2 = driver.FindElementByAccessibilityId("ItemText");
            Assert.IsTrue(el2.Text == "Second item");
            
            CreateScreenshot(driver, _ctx);
            var backButton = driver.FindElementByAccessibilityId("Back");
            backButton.Click();
            
            CreateScreenshot(driver, _ctx);
            var el3 = driver.FindElementByName("Fourth item");
            Assert.IsTrue(el3 != null);

            CreateScreenshot(driver, _ctx);
            driver.Close();

        }

        [TestMethod]
        public void AddNewItem()
        {
            var driver = StartApp();
            // tap on second item
            var el1 = driver.FindElementByAccessibilityId("Add");
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
                    var el3 = driver.FindElementByName("This is a new Item");
                    found = el3 != null;
                }
                catch(Exception)
                { }
            }
            Assert.IsTrue(found);


            driver.Close();

        }

        private void WaitForProgressbarToDisapear(WindowsDriver<WindowsElement> driver)
        {
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => d.FindElementByName("Second item"));
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
        private WindowsDriver<WindowsElement> StartApp()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, "8b831c56-bc54-4a8b-af94-a448f80118e7_9etdvjwkeybm6!App");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");

            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start();
            var driver = new WindowsDriver<WindowsElement>(_appiumLocalService, capabilities);
            return driver;
        }

        public void CreateScreenshot(WindowsDriver<WindowsElement> driver, TestContext ctx)
        {
            var screenshot = driver.GetScreenshot();
            var fileName = Guid.NewGuid().ToString() + ".png";
            screenshot.SaveAsFile(fileName);
            ctx.AddResultFile(fileName);
        }
    }
}

