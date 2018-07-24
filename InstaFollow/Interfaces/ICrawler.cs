using System;
using System.Collections.Generic;

namespace InstaFollow.Interfaces
{
    public interface ICrawler<P, E>
    {
        void OpenPage(String url);

        E GetFirstByXpath(String xpath);

        List<E> GetByXPath(String xpath);

        void ExecuteJavaScript(String javaScript);

        void Click(String xpath);
        void Click(E element);

        void SetValue(String xpath, String value);

        E GetInternalElement(E element, String xpath);

        List<E> GetInternalElements(E element, String xpath);

        String GetAttributeValue(String xpath, String attribute);

        IEnumerable<string> GetElementContents(String xpath);

        IEnumerable<string> GetElementContents(params E[] element);

        String GetElementTagName(String xpath);

        void Wait(long seconds, String xpath);

        void Close();
    }
}
