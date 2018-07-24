using System;
using System.Linq;
using System.Web;
using InstaFollow.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InstaFollow
{
    public class SomeCrawler
        : ICrawler<Page, Element>
    {
        public void Click(string xpath)
        {
            throw new NotImplementedException();
        }

        public void Click(Element element)
        {

        }

        public void Close()
        {

        }

        public void ExecuteJavaScript(string javaScript)
        {

        }

        public string GetAttributeValue(string xpath, string attribute)
        {
            return "input";
        }

        public List<Element> GetByXPath(string xpath)
        {
            return new List<Element>()
            {
                new Element(),
            };
        }

        public IEnumerable<string> GetElementContents(string xpath)
        {
            return new List<string>()
            {
                "teste 1",
                "teste 2"
            };
        }

        public IEnumerable<string> GetElementContents(params Element[] element)
        {
            return new List<string>()
            {
                "teste 1",
                "teste 2"
            };
        }

        public string GetElementTagName(string xpath)
        {
            return "input";
        }

        public Element GetFirstByXpath(string xpath)
        {
            return new Element();
        }

        public Element GetInternalElement(Element element, string xpath)
        {
            return new Element();
        }

        public List<Element> GetInternalElements(Element element, string xpath)
        {
            return new List<Element>()
            {
                new Element(),
            };
        }

        public void OpenPage(string url)
        {

        }

        public void SetValue(string xpath, string value)
        {

        }

        public void Wait(long seconds, string xpath)
        {
            
        }
    }
    public class Page
    {

    }
    public class Element
    {

    }
}