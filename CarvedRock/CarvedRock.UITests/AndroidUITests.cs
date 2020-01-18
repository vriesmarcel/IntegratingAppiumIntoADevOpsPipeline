using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;

namespace CarvedRock.UITests
{
    [TestClass]
    public class AndroidUITests
    {
        static private AndroidDriver<AppiumWebElement> driver;
        static private AppiumLocalService _appiumLocalService;

        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            System.Environment.SetEnvironmentVariable("ANDROID_HOME", @"C:\Program Files (x86)\Android\android-sdk");
            System.Environment.SetEnvironmentVariable("JAVA_HOME", @"C:\Program Files\Android\jdk\microsoft_dist_openjdk_1.8.0.25\bin");


            var capabilities = new AppiumOptions();
            //capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "5.0.1");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.fluentbytes.carvedrock");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "crc641782d5af3c9cf50a.MainActivity");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.Avd, "demo_device");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AvdArgs, "-no-boot-anim -no-snapshot-load");
            //capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AndroidCoverage, "false");
            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "demo_device");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");

            var currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current path: {currentPath}");
            var packagePath = Path.Combine(currentPath, @"..\..\..\AppsToTest\com.fluentbytes.carvedrock-x86.apk");
            packagePath = Path.GetFullPath(packagePath);
            Console.WriteLine($"Package path: {packagePath}");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, packagePath);

            Uri serverUri = new Uri("http://127.0.0.1:4723/wd/hub");
            //var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            //_appiumLocalService.Start(); ;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }
        [ClassCleanup]
        static public void CleanUp()
        {
            _appiumLocalService?.Dispose();
            _appiumLocalService = null;
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

            driver.PressKeyCode(AndroidKeyCode.Back);

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

            var scrollableElement = driver.FindElementByClassName("android.widget.ListView");
            //new TouchAction(driver).Press(200,200).Wait(2).MoveTo(200,0).Release();



            Func<AppiumWebElement> FindElementAction = () =>
            {
                // find all text views
                // check if the text matches
                var elements = driver.FindElementsByClassName("android.widget.TextView");
               foreach(var textView in elements)
                {
                    if (textView.Text == "This is a new Item")
                        return textView;
                }

                return null;
            };

            var elementFound = ScrollUntillItemFound(driver, scrollableElement, FindElementAction, 4);


            Assert.IsTrue(elementFound != null);


            driver.CloseApp();

        }


        private void WaitForProgressbarToDisapear(AndroidDriver<AppiumWebElement> driver)
        {
            var wait = new DefaultWait<AndroidDriver<AppiumWebElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            wait.Until(d => d.FindElementByAccessibilityId("Second item"));
        }

        private AppiumWebElement ScrollUntillItemFound(AndroidDriver<AppiumWebElement> driver, AppiumWebElement relativeTo, Func<AppiumWebElement> FindElementAction, int reties)
        {
            var wait = new DefaultWait<AndroidDriver<AppiumWebElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(1000)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            AppiumWebElement elementfound = null;

            elementfound = wait.Until(d =>
                         {
                             Flick(driver, relativeTo, UpOrDown.Up);

                             return FindElementAction();
                         });

            return elementfound;
        }

        private void Flick(AndroidDriver<AppiumWebElement> driver, AppiumWebElement element, UpOrDown direction)
        {
            int moveYDirection;
            if (direction == UpOrDown.Down)
                moveYDirection = 600;
            else
                moveYDirection = -600;

            var input = new PointerInputDevice(PointerKind.Touch);
            ActionSequence FlickUp = new ActionSequence(input);
            FlickUp.AddAction(input.CreatePointerMove(element, 0, 0, TimeSpan.Zero));
            FlickUp.AddAction(input.CreatePointerDown(MouseButton.Left));

            FlickUp.AddAction(input.CreatePointerMove(element, 0, moveYDirection, TimeSpan.FromMilliseconds(200)));
            FlickUp.AddAction(input.CreatePointerUp(MouseButton.Left));
            driver.PerformActions(new List<ActionSequence>() { FlickUp });
        }

        enum UpOrDown
        {
            Up = 0,
            Down
        }
    }
}
