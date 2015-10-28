using Preddy.Services;
using Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Preddy.Controllers {

    public class TweetController : ApiController {

        public IEnumerable<TweetViewModel> GetTweet(DateTime date) {
            using (var tweetService = new TweetService()) {
                return tweetService.Search(date.Date);
            }
        }

    }

}
