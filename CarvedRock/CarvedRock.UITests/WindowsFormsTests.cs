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
    public class WindowsFormsTests
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
            var listview = driver.FindElementByAccessibilityId("listView1");

            var row = listview.FindElementByName("Second item");

            var column1 = row.FindElementByName("Second item");

            column1.Click();
       

            //find dialog
            var dialog = driver.FindElementByAccessibilityId("Details");
            var el2 = dialog.FindElementByAccessibilityId("lblItemText");
            Assert.IsTrue(el2.Text == "Second item");

            var backButton = dialog.FindElementByAccessibilityId("button1");
            backButton.Click();

            var el3 = listview.FindElementByName("Fourth item");
            Assert.IsTrue(el3 != null);

            driver.CloseApp();

        }

        private WindowsDriver<WindowsElement> StartApp()
        {
            var capabilities = new AppiumOptions();
            // change this location based on where you cloned the git repo
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, @"C:\temp\Getting-Started-with-UI-Testing-and-Appium\CarvedRock\CarvedRock.winforms\bin\Debug\CarvedRock.exe");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");

            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start(); 
            var driver = new WindowsDriver<WindowsElement>(_appiumLocalService, capabilities);
            return driver ;
        }

       

        [TestMethod]
        public void AddNewItem()
        {
            var driver = StartApp();
            // tap on second item
            var addButton = driver.FindElementByName("AddNewItem");
            addButton.Click();
            
            var AddDialogWindow = driver.FindElementByAccessibilityId("NewItemForm");

            var inputFieldItem = AddDialogWindow.FindElementByName("ItemText");
            inputFieldItem.Clear();
            inputFieldItem.SendKeys("New Item Text");

            var InputFieldItemDetail = AddDialogWindow.FindElementByName("ItemDetail");
            InputFieldItemDetail.Clear();
            InputFieldItemDetail.SendKeys("New item details text");

            //close the dialog
            var AddButton = AddDialogWindow.FindElementByAccessibilityId("button1");
            AddButton.Click();

            var listview = driver.FindElementByAccessibilityId("listView1");

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(1000)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            var elementfound = wait.Until(d =>
            {
                FlickUp(driver, listview);
                return d.FindElementByName("New Item Text");
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
    }
}

