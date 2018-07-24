using InstaFollow.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaFollow.Services
{
    /// <summary>
    /// Class responsable to manipulate instagram
    /// </summary>
    /// <typeparam name="P">Page interface</typeparam>
    /// <typeparam name="E">Element interface</typeparam>
    public class InstaFollowCrawler<P, E>
    {
        private readonly Interfaces.ICrawler<P, E> crawler;

        #region constants
        private const String baseUri = "https://www.instagram.com/";
        private const String xpathLogin = "//input[@name='username']";
        private const String xpathPassword = "//input[@name='password']";
        private const String xpathLoginButton = "(//button[contains(.,'Entrar')])[1]";

        private const String xpathClick = "//a[contains(@href,'{0}')]";
        private const String xpathItems = "//div[contains(.,'{0}')]//li";
        private const String xpathDivScroll = "(//div[contains(.,'{0}')]/div)[1]";
        private const String xpathQuantity = "//a[contains(@href,'{0}')]//span";
        private const String xpathButtonToFollow = "//button[contains(.,'Seguir')]";
        private const String jsScroll = "(document.getElementsByClassName('{0}')[0]).scrollTop = 50000";

        private const String ptFollowers = "Seguidores";
        private const String ptFollowing = "Seguindo";
        private const String enFollowers = "Followes";
        private const String enFollowing = "Following";
        #endregion

        protected InstaFollowCrawler() { }

        public InstaFollowCrawler(ICrawler<P, E> crawler)
        {
            this.crawler = crawler;
            this.crawler.OpenPage(baseUri);
        }

        /// <summary>
        /// Login on instagram
        /// </summary>
        /// <param name="login">email or logon</param>
        /// <param name="password">password related to login</param>
        public void Login(string login, string password)
        {
            crawler.SetValue(xpathLogin, login);
            crawler.SetValue(xpathPassword, login);

            crawler.Click(xpathLoginButton);
        }

        /// <summary>
        /// Returns logins of your friend followers
        /// </summary>
        /// <param name="friendLogin">Login of some friend</param>
        public IEnumerable<String> ReturnFollowers(string friendLogin)
        {
            var followers = ReturnFollowersList(friendLogin);
            return crawler.GetElementContents(followers.ToArray());
        }

        /// <summary>
        /// Returns logins of your friend following
        /// </summary>
        /// <param name="friendLogin">Login of some friend</param>
        /// <returns></returns>
        public IEnumerable<String> ReturnFollowing(string friendLogin)
        {
            var following = ReturnFollowersList(friendLogin);
            return crawler.GetElementContents(following.ToArray());
        }

        /// <summary>
        /// Follow who your friend is following
        /// </summary>
        /// <param name="friendLogin">Login of some friend</param>
        public void FollowFriendFollowing(string friendLogin)
        {
            crawler.OpenPage(baseUri + friendLogin);

            Scroll("Seguindo", "following");

            var listToFollow = crawler.GetByXPath(xpathButtonToFollow).ToList();
            foreach (var unfollowed in listToFollow)
            {
                crawler.Click(unfollowed);
            }
        }

        /// <summary>
        /// Will Follow your friend followers
        /// </summary>
        /// <param name="friendLogin">Login of some friend</param>
        public void FollowFriendFollowers(string friendLogin)
        {
            crawler.OpenPage(baseUri + friendLogin);

            Scroll("Seguidores", "followers");

            var listToFollow = crawler.GetByXPath(xpathButtonToFollow).ToList();
            foreach (var unfollowed in listToFollow)
            {
                crawler.Click(unfollowed);
            }
        }

        /// <summary>
        /// Returns followers of some friend 
        /// </summary>
        /// <param name="friendLogin">Login of some friend</param>
        /// <returns></returns>
        public List<E> ReturnFollowersList(string friendLogin)
        {
            crawler.OpenPage(baseUri + friendLogin);

            Scroll(ptFollowers, enFollowers);
            var listOfFollowers = crawler.GetByXPath(string
                    .Format(xpathItems, ptFollowers)).ToList();

            return listOfFollowers;
        }

        /// <summary>
        /// Scroll instagram popup
        /// </summary>
        /// <param name="port"></param>
        /// <param name="eng"></param>
        internal void Scroll(string port, string eng)
        {
            var sQuantityToScroll = crawler.GetElementContents(string.
                            Format(xpathQuantity, eng)).FirstOrDefault();
            var quantity = Int32.Parse(sQuantityToScroll);

            crawler.Click(string.Format(xpathClick, eng));
            var listOfFollowers = crawler.GetElementContents(string.
                            Format(xpathItems, port)).ToList();

            var className = crawler.GetAttributeValue(
                                string.Format(xpathDivScroll, port), "class");

            while (listOfFollowers.Count < quantity)
            {
                crawler.ExecuteJavaScript(string.Format(jsScroll, className));
                listOfFollowers = crawler.GetElementContents(string
                    .Format(xpathItems, port)).ToList();
            }
        }

    }
}
