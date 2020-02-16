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
    public class WindowsWPFTests
    {
        static TestContext ctx;
        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            ctx = context;
        }


        [TestMethod]
        public void CheckMasterDetailAndBack()
        {
            var driver = StartApp();
            // tap on second item
            var listview = driver.FindElementByAccessibilityId("listview");
           // HighlightElement(listview, 3000);

            var row = listview.FindElementByName("Second item");
            //HighlightElement(row, 3000);
            row.Click();

            var itemText = driver.FindElementByAccessibilityId("txtItemText");
            Assert.IsTrue(itemText.Text == "Second item");

            var backButton = driver.FindElementByAccessibilityId("ok");
            backButton.Click();

            var el3 = listview.FindElementByName("Fourth item");
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
            var driver = StartApp();
            // tap on second item
            var el1 = driver.FindElementByAccessibilityId("btnAdd");
            el1.Click();
       
            var elItemText = driver.FindElementByAccessibilityId("txtItemText");
            elItemText.Clear();
            elItemText.SendKeys("This is a new Item");

            var elItemDetail = driver.FindElementByAccessibilityId("txtDetailText");
            elItemDetail.Clear();
            elItemDetail.SendKeys("These are the details");

            var elSave = driver.FindElementByAccessibilityId("btnAdd");
            elSave.Click();


            var listview = driver.FindElementByAccessibilityId("listview");

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(1000)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
 
            var elementfound = wait.Until(d =>
            {
                FlickUp(driver, listview);
                return d.FindElementByName("This is a new Item");
            });

            driver.CloseApp();

        }
        private void FlickUp(WindowsDriver<WindowsElement> driver, AppiumWebElement element)
        {
            var input = new PointerInputDevice(PointerKind.Touch);
            ActionSequence FlickUp = new ActionSequence(input);
            FlickUp.AddAction(input.CreatePointerMove(element, 0, 0, TimeSpan.Zero));
            FlickUp.AddAction(input.CreatePointerDown(MouseButton.Left));
            FlickUp.AddAction(input.CreatePointerMove(element, 0, -300, TimeSpan.FromMilliseconds(200)));
            FlickUp.AddAction(input.CreatePointerUp(MouseButton.Left));
            driver.PerformActions(new List<ActionSequence>() { FlickUp });
        }

        public WindowsDriver<WindowsElement> StartApp()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, @"C:\temp\App3\CarvedRock\CarvedRock.Wpf\bin\Debug\CarvedRock.wpf.exe");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");

            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start(); ;
            var driver = new WindowsDriver<WindowsElement>(_appiumLocalService, capabilities);
            return driver;
        }
    }
}

