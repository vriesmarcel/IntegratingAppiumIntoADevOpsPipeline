using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
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
        static TestContext _ctx;
        static private AppiumLocalService _appiumLocalService;

        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            _ctx = context;
        }
        [ClassCleanup]
        static public void CleanUp()
        {
            _appiumLocalService?.Dispose();
            _appiumLocalService = null;
        }

        [TestMethod]
        public void TestListInstalledPackages()
        {
            AndroidDriver<AppiumWebElement> driver = StartApp();
            //get a list of all installed packages on the device
            string script = "mobile: shell";
            var arguments = new Dictionary<string, string>
            {
                { "command", "pm list packages" },
                { "--show-versioncode", "" }
            };

            var list = driver.ExecuteScript(script,arguments);
            Assert.IsNotNull(list);
            Console.Write(list);
        }


        [TestMethod]
        public void CheckMasterDetailAndBack()
        {
            AndroidDriver<AppiumWebElement> driver = StartApp();

            // tap on second item
            var el1 = driver.FindElement(MobileBy.AccessibilityId("Second item"));
            el1.Click();

            var el2 = driver.FindElement(MobileBy.AccessibilityId("ItemText"));
            Assert.IsTrue(el2.Text == "Second item");

            driver.PressKeyCode(AndroidKeyCode.Back);

            var el3 = driver.FindElement(MobileBy.AccessibilityId("Fourth item"));
            Assert.IsTrue(el3 != null);

            driver.CloseApp();

        }

        [TestMethod]
        public void AddNewItem()
        {
            AndroidDriver<AppiumWebElement> driver = StartApp();

            // tap on second item
            var el1 = driver.FindElement(MobileBy.AccessibilityId("Add"));
            el1.Click();

            var elItemText = driver.FindElement(MobileBy.AccessibilityId("ItemText"));
            elItemText.Clear();
            elItemText.SendKeys("This is a new Item");

            var elItemDetail = driver.FindElement(MobileBy.AccessibilityId("ItemDescription"));
            elItemDetail.Clear();
            elItemDetail.SendKeys("These are the details");

            var elSave = driver.FindElement(MobileBy.AccessibilityId("Save"));
            elSave.Click();
            CreateScreenshot(driver);

            WaitForProgressbarToDisapear(driver);

            CreateScreenshot(driver);

            var scrollableElement = driver.FindElement(MobileBy.AccessibilityId("ItemsListView"));

            Func<AppiumWebElement> FindElementAction = () =>
            {
                // find all text views
                // check if the text matches
                var elements = driver.FindElementsByClassName("android.widget.TextView");
                foreach (var textView in elements)
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

            wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Second item")));
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


        public void CreateScreenshot(AndroidDriver<AppiumWebElement>  driver)
        {
            var screenshot = driver.GetScreenshot();
            var fileName = Guid.NewGuid().ToString() + ".png";
            screenshot.SaveAsFile(fileName);
            _ctx.AddResultFile(fileName);
            Console.WriteLine($"Created screenshot. Saved file to location: {fileName}");
        }

        private AndroidDriver<AppiumWebElement> StartApp()
        {
            System.Environment.SetEnvironmentVariable("ANDROID_HOME", @"C:\Program Files (x86)\Android\android-sdk");
            System.Environment.SetEnvironmentVariable("JAVA_HOME", @"C:\Program Files\Android\jdk\microsoft_dist_openjdk_1.8.0.25");

            var capabilities = new AppiumOptions();
            // automatic start of the emulator if not running
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.Avd, "demo_device");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AvdArgs, "-no-boot-anim -no-snapshot-load");
            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
            

            // connecting to a device or emulator
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "emulator-5554");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
            // specifyig which app we want to install and launch
            var currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current path: {currentPath}");
            var packagePath = Path.Combine(currentPath, @"..\..\..\AppsToTest\com.fluentbytes.carvedrock-x86.apk");
            packagePath = Path.GetFullPath(packagePath);
            Console.WriteLine($"Package path: {packagePath}");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, packagePath);

            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.fluentbytes.carvedrock");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "crc641782d5af3c9cf50a.MainActivity");
            // additional wait time in case we have a clean emulator and need to wait for the install
            capabilities.AddAdditionalCapability("appWaitDuration",4800000);
            // specify startup flags appium server to execute adb shell commands
            var serveroptions = new OptionCollector();
            var relaxedSecurityOption = new KeyValuePair<string, string>("--relaxed-security", "");

            serveroptions.AddArguments( relaxedSecurityOption);
            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().WithArguments(serveroptions).Build();
            _appiumLocalService.Start(); ;
            var driver = new AndroidDriver<AppiumWebElement>(_appiumLocalService, capabilities);

            return driver;

        }
    }
}
