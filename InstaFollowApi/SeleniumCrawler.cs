using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Support.PageObjects;
using System.Linq;
using System.Web;
using InstaFollow.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InstaFollow
{
    public class SeleniumCrawler
        : ICrawler<IWebDriver, IWebElement>
    {
        private IWebDriver Browser;

        public SeleniumCrawler()
        {
            this.Browser = new ChromeDriver();
            Browser.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            Browser.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
            Browser.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(30));
        }

        public void OpenPage(String url)
        {
            this.Browser.Navigate().GoToUrl(url);

            Browser.Manage().Window.Maximize();
        }

        public IWebElement GetFirstByXpath(String xpath)
        {
            return this.Browser.FindElement(By.XPath(xpath));
        }

        public List<IWebElement> GetByXPath(String xpath)
        {
            return this.Browser.FindElements(By.XPath(xpath)).ToList();
        }

        public void ExecuteJavaScript(String javaScript)
        {
            ((IJavaScriptExecutor)this.Browser).ExecuteScript(javaScript);
        }

        public void Click(String xpath)
        {
            var element = this.Browser.FindElement(By.XPath(xpath));
            Click(element);
        }

        public void Click(IWebElement element)
        {
            element.Click();
        }
        public void SetValue(String xpath, String value)
        {
            var element = this.Browser.FindElement(By.XPath(xpath));
            element.SendKeys(value);
        }

        public IWebElement GetInternalElement(IWebElement element, String xpath)
        {
            return element.FindElement(By.XPath(xpath));
        }

        public List<IWebElement> GetInternalElements(IWebElement element, String xpath)
        {
            return element.FindElements(By.XPath(xpath)).ToList();
        }

        public String GetAttributeValue(String xpath, String attribute)
        {
            return this.Browser.FindElement(By.XPath(xpath)).GetAttribute(attribute);
        }

        public IEnumerable<string> GetElementContents(String xpath)
        {
            return this.Browser.FindElements(By.XPath(xpath)).Select(e => e.Text);
        }

        public String GetElementTagName(String xpath)
        {
            return this.Browser.FindElement(By.XPath(xpath)).TagName;
        }

        public void Wait(long seconds, String xpath)
        {
        }

        public void Close()
        {
            this.Browser.Close();
            this.Browser.Dispose();
        }

        public IEnumerable<string> GetElementContents(params IWebElement[] elements)
        {
            foreach (var element in elements)
                yield return element.Text;
        }
    }
}