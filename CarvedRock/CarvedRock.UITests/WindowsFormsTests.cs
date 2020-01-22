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
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace CarvedRock.UITests
{
    [TestClass]
    public class WindowsForsTests
    {
        static TestContext ctx;
        private static  WindowsDriver<WindowsElement> driver;
        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            ctx = context;
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.App ,@"C:\temp\App3\CarvedRock\CarvedRock.winforms\bin\Debug\CarvedRock.exe");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");
            
            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start(); ;
            driver = new WindowsDriver<WindowsElement>(_appiumLocalService, capabilities);
            //driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }


        [TestMethod]
        public void CheckMasterDetailAndBack()
        {
            driver.LaunchApp();
            // tap on second item
            var listview = driver.FindElementByAccessibilityId("listView1");
            HighlightElement(listview, 3000);

            var row = listview.FindElementByName("Second item");
            HighlightElement(row, 3000);

            var column1 = row.FindElementByName("Second item");

            column1.Click();
            column1.Click();

            //find dialog
            var dialog = driver.FindElementByAccessibilityId("Details");
            var el2 = dialog.FindElementByAccessibilityId("ItemText");
            Assert.IsTrue(el2.Text == "Second item");

            var backButton = dialog.FindElementByAccessibilityId("button1");
            backButton.Click();

            var el3 = listview.FindElementByAccessibilityId("Fourth item");
            Assert.IsTrue(el3 != null);

            driver.CloseApp();

        }

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);

        private void HighlightElement(AppiumWebElement element, int milliseconds)
        {
        
            //var ptr = new IntPtr(Convert.ToInt32(element.WrappedDriver.CurrentWindowHandle, 16));
            // Draw rectangle to screen.
            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            Graphics newGraphics = Graphics.FromHdc(desktopPtr);

            newGraphics.DrawRectangle(new Pen(Color.Red, 3), element.Coordinates.LocationInViewport.X, element.Coordinates.LocationInViewport.Y, element.Rect.Width, element.Rect.Height);
            Thread.Sleep(milliseconds);
            // Dispose of new graphics.
            newGraphics.Dispose();
            ReleaseDC(IntPtr.Zero, desktopPtr);
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

