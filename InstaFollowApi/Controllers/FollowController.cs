
using OpenQA.Selenium;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace InstaFollow.Controllers
{
    [RoutePrefix("instafollow")]
    public class FollowController : ApiController
    {
        private Services.InstaFollowCrawler<IWebDriver, IWebElement> instaFollow;

        public FollowController(Services.InstaFollowCrawler<IWebDriver, IWebElement> instaFollow)
        {
            this.instaFollow = instaFollow;
        }

        /// <summary>
        /// Returns followers of some friend
        /// </summary>
        /// <returns></returns>
        [Route("followers")]
        [SwaggerResponse(HttpStatusCode.OK, "Followers", typeof(List<string>))]
        public IHttpActionResult GetFollowers(string login, string password, string friendLogin)
        {
            instaFollow.Login(login, password);

            var followers = instaFollow.ReturnFollowers(friendLogin);
            return Ok(followers);
        }
    }
}
